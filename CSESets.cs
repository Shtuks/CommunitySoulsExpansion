﻿using Terraria.ID;
using Terraria.ModLoader;

namespace ssm
{
    public class CSESets : ModSystem
    {
        public class NPCs
        {
            public static int[] SwarmHealth;
        }
        public override void PostSetupContent()
        {
            NPCs.SwarmHealth = NPCID.Sets.Factory.CreateIntSet(0);
        }
    }
}
