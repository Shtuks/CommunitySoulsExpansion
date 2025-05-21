using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Systems;
using ssm.Content.NPCs.MutantEX;

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

                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplier += 0.5f; }
                if (ModCompatibility.Thorium.Loaded){multiplier+=0.5f;}
                if (ModCompatibility.Calamity.Loaded) {multiplier+=1f;} 
                if (ModCompatibility.SacredTools.Loaded) {multiplier+=0.5f;}
                if (ModCompatibility.DBZ.Loaded) { multiplier += 1; }
                if (ModCompatibility.Redemption.Loaded && ModCompatibility.Homeward.Loaded && ModCompatibility.Polarities.Loaded && ModCompatibility.Spooky.Loaded && !ModCompatibility.Calamity.Loaded) { multiplier++;}

                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = (int)(500 + (150 * multiplier));
                    npc.lifeMax = (int)(mutantHealth + (mutantHealth * multiplier));
                }

                if (npc.type == ModContent.NPCType<MutantEX>())
                {
                    npc.lifeMax = (int)(monstrHealth * 3 + monstrHealth * multiplier);
                }
            }
        }
    }
}
