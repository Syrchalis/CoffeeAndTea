using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using System.Reflection;

namespace CoffeeAndTea
{
    [StaticConstructorOnStartup]
    static class HarmonyPatch
    {
        static HarmonyPatch()
        {
            var harmony = new Harmony("Syrchalis.Rimworld.CoffeeAndTea");
            MethodInfo method = typeof(JoyGiver_Ingest).GetMethod("CanIngestForJoy", BindingFlags.NonPublic | BindingFlags.Instance);
            HarmonyMethod prefix = null;
            HarmonyMethod postfix = new HarmonyMethod(typeof(CoffeeAndTea).GetMethod("CanIngestForJoy_Postfix"));
            harmony.Patch(method, prefix, postfix, null);
        }
    }
}
