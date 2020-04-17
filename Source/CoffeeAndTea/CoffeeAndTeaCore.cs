using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;

namespace CoffeeAndTea
{
    public class CoffeeAndTeaCore : Mod
    {
        public CoffeeAndTeaCore(ModContentPack content) : base(content)
        {

        }
        public override string SettingsCategory() => "SyrCoffeeAndTeaCategory".Translate();
        public static bool policiesAdded = false;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            checked
            {
                Listing_Standard listing_Standard = new Listing_Standard();
                listing_Standard.Begin(inRect);
                
                if (policiesAdded)
                {
                    GUI.color = Color.green;
                    listing_Standard.Label("SyrCoffeeAndTeaPoliciesAdded".Translate());
                    GUI.color = Color.white;
                    listing_Standard.Gap(24f);
                }
                if (listing_Standard.ButtonText("SyrCoffeeAndTeaAddPolicies".Translate(), "SyrCoffeeAndTeaAddPoliciesTooltip".Translate()))
                {
                    SoundDefOf.Designate_PlanRemove.PlayOneShotOnCamera(null);
                    AddNewPolicies();
                }
                listing_Standard.End();
            }
        }
        public override void WriteSettings()
        {
            base.WriteSettings();
        }

        public static void AddNewPolicies()
        {
            if (Current.ProgramState == ProgramState.Playing)
            {
                foreach (DrugPolicy drugPolicy in Current.Game.drugPolicyDatabase.AllPolicies)
                {
                    List<DrugPolicyEntry> entriesInt = Traverse.Create(drugPolicy).Field("entriesInt").GetValue<List<DrugPolicyEntry>>();
                    if (entriesInt.Find(i => i.drug == CoffeeAndTeaDefOf.SyrCoffee) == null)
                    {
                        DrugPolicyEntry drugPolicyEntry = new DrugPolicyEntry();
                        drugPolicyEntry.drug = CoffeeAndTeaDefOf.SyrCoffee;
                        drugPolicyEntry.allowedForJoy = true;
                        drugPolicyEntry.allowedForAddiction = true;
                        entriesInt.Add(drugPolicyEntry);
                    }
                    if (entriesInt.Find(i => i.drug == CoffeeAndTeaDefOf.SyrTea) == null)
                    {
                        DrugPolicyEntry drugPolicyEntry = new DrugPolicyEntry();
                        drugPolicyEntry.drug = CoffeeAndTeaDefOf.SyrTea;
                        drugPolicyEntry.allowedForJoy = true;
                        drugPolicyEntry.allowedForAddiction = true;
                        entriesInt.Add(drugPolicyEntry);
                    }
                    if (entriesInt.Find(i => i.drug == CoffeeAndTeaDefOf.SyrHotChocolate) == null)
                    {
                        DrugPolicyEntry drugPolicyEntry = new DrugPolicyEntry();
                        drugPolicyEntry.drug = CoffeeAndTeaDefOf.SyrHotChocolate;
                        drugPolicyEntry.allowedForJoy = true;
                        entriesInt.Add(drugPolicyEntry);
                    }
                    entriesInt.SortBy((DrugPolicyEntry e) => e.drug.GetCompProperties<CompProperties_Drug>().listOrder);
                    Traverse.Create(drugPolicy).Field("entriesInt").SetValue(entriesInt);
                    policiesAdded = true;
                }
            }
        }
    }
}
