using Harmony;
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
        // Token: 0x06003718 RID: 14104 RVA: 0x001DB5B1 File Offset: 0x001D99B1
        static CoffeeAndTeaDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(CoffeeAndTeaDefOf));
        }
        public static HediffDef SyrCoffeeHigh;
        public static HediffDef SyrTeaHigh;
        public static ThingDef SyrCoffee;
        public static ThingDef SyrTea;
    }
}
