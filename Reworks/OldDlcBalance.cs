using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Content.NPCs.MutantEX;
using FargowiltasSouls.Core.Systems;

namespace ssm.Reworks
{
    public class OldCalDlcNpcBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                int mutantHealth = 10000000;
                int monstrHealth = 100000000;
                float multiplier = 0;

                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplier += 1f; }
                if (ModCompatibility.Thorium.Loaded){multiplier+=0.6f;}
                if (ModCompatibility.Calamity.Loaded) {multiplier+=1.4f;} 
                if (ModCompatibility.SacredTools.Loaded) {multiplier+=1f;}

                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = (int)(500 + (100 * multiplier));
                    npc.lifeMax = (int)(mutantHealth + (mutantHealth * (WorldSavingSystem.MasochistModeReal ? multiplier * 1.5f : multiplier)));
                }

                //if (npc.type == ModContent.NPCType<MutantEX>())
                //{
                //    npc.defense = (int)(1000 + 1000 * multiplier);
                //    npc.lifeMax = (int)(monstrHealth * 5 + monstrHealth * multiplier);
                //}
            }
        }
    }
}
