using Fargowiltas.Items.Misc;
using Fargowiltas.Items.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm
{
    public class ShtunSets : ModSystem
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
