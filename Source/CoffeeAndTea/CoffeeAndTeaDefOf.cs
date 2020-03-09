using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace CoffeeAndTea
{
    [DefOf]
    public static class CoffeeAndTeaDefOf
    {
        static CoffeeAndTeaDefOf()
        {

        }
        public static HediffDef SyrCoffeeHigh;
        public static HediffDef SyrTeaHigh;
        public static ThingDef SyrCoffee;
        public static ThingDef SyrTea;
    }
}
