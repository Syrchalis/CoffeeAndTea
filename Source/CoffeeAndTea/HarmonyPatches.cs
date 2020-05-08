using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using System.Reflection;
using Verse.AI;

namespace CoffeeAndTea
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("Syrchalis.Rimworld.CoffeeAndTea");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(DrugPolicyDatabase), "GenerateStartingDrugPolicies")]
    public class GenerateStartingDrugPoliciesPatch
    {
        [HarmonyPostfix]
        public static void GenerateStartingDrugPolicies_Postfix(DrugPolicyDatabase __instance)
        {
            DrugPolicy drugPolicy = __instance.AllPolicies.First(dp => dp.label == "SocialDrugs".Translate());
            if (drugPolicy != null)
            {
                drugPolicy[CoffeeAndTeaDefOf.SyrCoffee].allowedForJoy = true;
                drugPolicy[CoffeeAndTeaDefOf.SyrTea].allowedForJoy = true;
                drugPolicy[CoffeeAndTeaDefOf.SyrHotChocolate].allowedForJoy = true;
            }
        }
    }

    [HarmonyPatch(typeof(JoyGiver_Ingest), "CanIngestForJoy")]
    public class CanIngestForJoyPatch
    {
        [HarmonyPostfix]
        public static void CanIngestForJoy_Postfix(Pawn pawn, Thing t, ref bool __result)
        {
            if (pawn.health.hediffSet.HasHediff(CoffeeAndTeaDefOf.SyrCoffeeHigh) && t.def == CoffeeAndTeaDefOf.SyrTea)
            {
                __result = false;
            }
            if (pawn.health.hediffSet.HasHediff(CoffeeAndTeaDefOf.SyrTeaHigh) && t.def == CoffeeAndTeaDefOf.SyrCoffee)
            {
                __result = false;
            }
        }
    }

    [HarmonyPatch(typeof(JoyGiver_TakeDrug), "BestIngestItem")]
    public class BestIngestItemPatch
    {
        [HarmonyPostfix]
        public static void BestIngestItem_Postfix(Pawn pawn, ref Thing __result)
        {
            float value = Rand.ValueSeeded(pawn.thingIDNumber ^ 125);
            if (value < 0.2f)
            {
                return;
            }
            else if (value < 0.6f && __result != null && __result.def == CoffeeAndTeaDefOf.SyrTea)
            {
                bool predicate(Thing t) => CanIngestForJoy(pawn, t);
                Thing thing = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(CoffeeAndTeaDefOf.SyrCoffee), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, predicate, null);
                if (thing != null)
                {
                    __result = thing;
                }
            }
            else if (value >= 0.6f && __result != null && __result.def == CoffeeAndTeaDefOf.SyrCoffee)
            {
                bool predicate(Thing t) => CanIngestForJoy(pawn, t);
                Thing thing = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(CoffeeAndTeaDefOf.SyrTea), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, predicate, null);
                if (thing != null)
                {
                    __result = thing;
                }
            }
        }
        public static bool CanIngestForJoy(Pawn pawn, Thing t)
        {
            if (!t.def.IsIngestible || t.def.ingestible.joyKind == null || t.def.ingestible.joy <= 0f || !pawn.WillEat(t, null, true))
            {
                return false;
            }
            if (t.Spawned)
            {
                if (!pawn.CanReserve(t, 1, -1, null, false))
                {
                    return false;
                }
                if (t.IsForbidden(pawn))
                {
                    return false;
                }
                if (!t.IsSociallyProper(pawn))
                {
                    return false;
                }
                if (!t.IsPoliticallyProper(pawn))
                {
                    return false;
                }
            }
            return !t.def.IsDrug || pawn.drugs == null || pawn.drugs.CurrentPolicy[t.def].allowedForJoy || pawn.story == null || pawn.story.traits.DegreeOfTrait(TraitDefOf.DrugDesire) > 0 || pawn.InMentalState;
        }
    }
}
