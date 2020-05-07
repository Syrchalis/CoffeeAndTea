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
    public class ThoughtWorker_CoffeeVsTea : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            if (!p.RaceProps.Humanlike)
            {
                return ThoughtState.Inactive;
            }
            if (!other.RaceProps.Humanlike)
            {
                return ThoughtState.Inactive;
            }
            if (!RelationsUtility.PawnsKnowEachOther(p, other))
            {
                return ThoughtState.Inactive;
            }
            float valuePawn = Rand.ValueSeeded(p.thingIDNumber ^ 125);
            float valueOtherPawn = Rand.ValueSeeded(other.thingIDNumber ^ 125);
            if (valuePawn < 0.2f || valueOtherPawn < 0.2f)
            {
                return ThoughtState.Inactive;
            }
            if ((valuePawn >= 0.6f && valueOtherPawn < 0.6f) || (valuePawn < 0.6f && valueOtherPawn >= 0.6f))
            {
                return ThoughtState.ActiveAtStage(0);
            }
            if (valuePawn < 0.6f && valueOtherPawn < 0.6f)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            if (valuePawn >= 0.6f && valueOtherPawn >= 0.6f)
            {
                return ThoughtState.ActiveAtStage(2);
            }
            return ThoughtState.Inactive;
        }
    }
}
