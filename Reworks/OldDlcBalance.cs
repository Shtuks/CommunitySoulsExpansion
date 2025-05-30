﻿using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using rail;

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
                float multiplier = 0;

                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplier += 1f; }
                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplier += 1f; }
                if (ModCompatibility.Thorium.Loaded){multiplier+=0.6f;}
                if (ModCompatibility.Calamity.Loaded) {multiplier+=1.5f;} 
                if (ModCompatibility.SacredTools.Loaded) {multiplier+=1.5f;}

                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = Main.getGoodWorld ? 2000 :(int)(500 + (100 * multiplier));
                    npc.lifeMax = (int)(mutantHealth + (mutantHealth * (WorldSavingSystem.MasochistModeReal ? multiplier * 1.5f : multiplier)));
                }
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                int abomHealth = 1000000;
                float multiplierA = 0;


                if (ModCompatibility.Thorium.Loaded)  {multiplierA += 3f;} //post primoridals
                if (ModCompatibility.Calamity.Loaded) {multiplierA += 7f;} //post yharon
                if (ModCompatibility.SacredTools.Loaded) {multiplierA += 2f;} //post lost sibings

                if (npc.type == ModContent.NPCType<AbomBoss>())
                {
                    npc.damage = Main.getGoodWorld ? 500 : (int)(250 + (50 * multiplierA));
                    npc.lifeMax = (int)(2800000 + (abomHealth * (WorldSavingSystem.MasochistModeReal ? multiplierA * 1.2f : multiplierA)))/2; //like wtf
                }
            }
        }
    }
}
