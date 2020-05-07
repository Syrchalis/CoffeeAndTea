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
    public class ThoughtWorker_Coffee : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Hediff hediff = p.health.hediffSet.GetFirstHediffOfDef(CoffeeAndTeaDefOf.SyrCoffeeHigh);
            if (hediff == null || hediff.def.stages == null)
            {
                return ThoughtState.Inactive;
            }
            float value = Rand.ValueSeeded(p.thingIDNumber ^ 125);
            if (value >= 0.6f)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return ThoughtState.ActiveDefault;
        }
    }

    public class ThoughtWorker_Tea : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Hediff hediff = p.health.hediffSet.GetFirstHediffOfDef(CoffeeAndTeaDefOf.SyrTeaHigh);
            if (hediff == null || hediff.def.stages == null)
            {
                return ThoughtState.Inactive;
            }
            float value = Rand.ValueSeeded(p.thingIDNumber ^ 125);
            if (value >= 0.2 && value < 0.6f)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return ThoughtState.ActiveDefault;
        }
    }
}
