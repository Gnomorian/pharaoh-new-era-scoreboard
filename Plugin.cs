using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace PNEScoreboard
{
    [BepInPlugin("kenzie.pharaoh.PNEScoreboard", "PNEScoreboard", "0.0.1")]
    [HarmonyPatch]
    public partial class Plugin : BaseUnityPlugin
    {
        private static Harmony _hi;
        internal static ManualLogSource LOG;
        internal static ConfigEntry<bool> Debug;

        public void Awake()
        {
            LOG = new ManualLogSource("Log");
            BepInEx.Logging.Logger.Sources.Add(LOG);
            LOG.LogWarning($"Plugin PNEScoreboard! is loaded!");
            Debug = Config.Bind("General", "Debug", false, "Enable debug logging.");
            _hi = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnDestroy()
        {
            _hi?.UnpatchSelf();
        }
    }
}