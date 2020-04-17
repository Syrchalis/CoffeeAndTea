using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;


namespace CoffeeAndTea
{
    public class IngestionOutcomeDoer_CoffeeAndTea : IngestionOutcomeDoer
    {
        public ChemicalDef toleranceChemical;
        public float severity = -1f;
        public HediffDef hediffDefAdd;
        public HediffDef hediffDefRemove;
        public bool divideByBodySize;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            Hediff hediffAdd = HediffMaker.MakeHediff(hediffDefAdd, pawn, null);
            float num;
            if (severity > 0f)
            {
                num = severity;
            }
            else
            {
                num = hediffDefAdd.initialSeverity;
            }
            if (divideByBodySize)
            {
                num /= pawn.BodySize;
            }
            AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref num);
            hediffAdd.Severity = num;
            pawn.health.AddHediff(hediffAdd, null, null, null);

            Hediff hediffRemove = pawn.health.hediffSet.hediffs.Find((Hediff h) => h.def == hediffDefRemove);
            if (hediffRemove != null)
            {
                pawn.health.RemoveHediff(hediffRemove);
            }
        }

        public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
        {
            foreach (StatDrawEntry s in hediffDefAdd.SpecialDisplayStats(StatRequest.ForEmpty()))
            {
                yield return s;
            }
            yield break;
        }
    }
}