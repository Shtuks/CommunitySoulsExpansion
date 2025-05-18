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
                int mutantBaseHealth = 1000000;
                int mutantAddHealth = 1000000;
                int monstrBaseHealth = 10000000;
                int monstrAddHealth = 10000000;
                int multiplier = 0;

                if (ModCompatibility.Thorium.Loaded && !ModCompatibility.Calamity.Loaded) {multiplier += 1;}
                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplier += 1; }
                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplier += 2; }
                if (ModCompatibility.Thorium.Loaded){multiplier+=2;}
                if (ModCompatibility.Calamity.Loaded) {multiplier+=4;} 
                if (ModCompatibility.SacredTools.Loaded) {multiplier+=2;}
                if (ModCompatibility.Homeward.Loaded) {multiplier+=2;}
                if (ModCompatibility.DBZ.Loaded) { multiplier += 5; }
                if (ModCompatibility.Redemption.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded) {multiplier++;}
                if (ModCompatibility.Entropy.Loaded) { multiplier++; }
                if (ModCompatibility.Polarities.Loaded && ModCompatibility.Spooky.Loaded && !ModCompatibility.Calamity.Loaded) { multiplier++;}

                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = WorldSavingSystem.MasochistModeReal ? npc.target.ToPlayer().statLifeMax2/2 :npc.target.ToPlayer().statLifeMax2/3; //(int)(500 + (100 * multiplier * 0.7f));
                    npc.defense = 300 + (ModCompatibility.Calamity.Loaded ? 500 : 400 * multiplier);
                    npc.lifeMax = (int)(mutantBaseHealth + (mutantAddHealth * (!WorldSavingSystem.MasochistModeReal ? multiplier : multiplier *  1.5f)));
                }

                if (npc.type == ModContent.NPCType<MutantEX>())
                {
                    npc.lifeMax = monstrBaseHealth * 3 + (monstrAddHealth * multiplier / 2);
                }
            }
        }
    }
}
