using HarmonyLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PNEScoreboard
{
    [HarmonyPatch]
    public partial class Plugin
    {
        static BuildingBar buildingBar;
        [HarmonyPatch]
        public static class MoveBazzarrSideBar
        {
            [HarmonyPrepare]
            public static void Prepare(MethodBase original)
            {
                if (original != null)
                {
                    LOG.LogWarning($"Preparing patch in {original.Name}!");
                }
            }
        }
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return typeof(BuildingBar).GetMethod("RefreshAvailableCategories");
        }

        [HarmonyPrefix]
        public static void Prefix(BuildingBar __instance)
        {
            LOG.LogWarning($"The BuildingBar instance = {__instance.name}");
            buildingBar = __instance;
        }

        [HarmonyPostfix]
        public static void Postfix()
        {
            if (buildingBar == null)
            {
                return;
            }
            var catagoryHolder = Traverse.Create(buildingBar).Field("_categoryHolder").GetValue<Transform>();

            LOG.LogWarning($"The catagoryHolder {catagoryHolder.name} has {catagoryHolder.childCount} children.");
            for(int i = 0; i < catagoryHolder.childCount; i++) 
            {
                LOG.LogWarning($"\t{catagoryHolder.GetChild(i).name}");
            }
        }
    }
}
