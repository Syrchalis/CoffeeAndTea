using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace CoffeeAndTea
{
    public class CoffeeAndTea
    {
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
}
