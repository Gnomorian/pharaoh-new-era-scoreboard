using HarmonyLib;
using I2.Loc.SimpleJSON;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;


namespace PNEScoreboard
{
    [HarmonyPatch]
    public partial class Plugin
    {
        [HarmonyPatch]
        public static class MapGameplayPatch
        {
            private static string CreateVictorySaveGame(MapGameplay __instance)
            {
                // the pause game, ui state stuff copied from void QuickSave() in MapGameplay class.
                var timeManager = Traverse.Create(__instance).Field("_timeManager").GetValue<TimeManager>();
                var uiManager = Traverse.Create(__instance).Field("_uiManager").GetValue<UIManager>();

                bool isPaused = timeManager.IsPaused;
                timeManager.PauseGame(isPaused: true);
                uiManager.StackObscurationState();

                // now actually save the game
                string savePath = FileManager.GetSavePath($"Victory_{__instance.CurrentFamily.Name}_{__instance.Level.MapName}_{DateTime.Now.ToString("yyyyMMdd")}");
                LOG.LogWarning($"Saving the game as  \"{savePath}\"");
                GlobalAccessor.Level.SaveMap(savePath);

                uiManager.ApplyLastObscurationState();
                timeManager.PauseGame(isPaused);

                return savePath;
            }

            private static void CreateVictoryStatsFile(MapGameplay __instance, string path)
            {
                var stats = new VictoryStatistics(__instance);
                var serializer = new XmlSerializer(typeof(VictoryStatistics));

                var filePath = Path.ChangeExtension(path, ".xml");
                LOG.LogWarning($"Saving victory statistics to {filePath}");
                TextWriter writer = new StreamWriter(filePath);
                serializer.Serialize(writer, stats);
                writer.Close();
                //TODO: confic option to not open the save folder.
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(filePath));
            }

            [HarmonyPrepare]
            public static void Prepare(MethodBase original)
            {
                if (original != null)
                {
                    LOG.LogWarning($"Preparing patch in {original.Name}!");
                }
            }
            [HarmonyTargetMethods]
            public static IEnumerable<MethodBase> TargetMethods()
            {
                yield return typeof(MapGameplay).GetMethod("ComputeVictoryData");
            }

            [HarmonyPrefix]
            public static void Prefix(MapGameplay __instance)
            {
                var savePath = CreateVictorySaveGame(__instance);
                CreateVictoryStatsFile(__instance, savePath);
            }
        }

    }
}
