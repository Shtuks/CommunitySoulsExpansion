using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using CalamityMod.Events;

namespace ssm.Reworks
{
    public class OldCalDlcNpcBalance : GlobalNPC
    {
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                int mutantHealth = 10000000;
                float multiplier = 0;

                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplier += 1f; }
                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplier += 1f; }
                if (ModCompatibility.Thorium.Loaded){multiplier+=0.7f;}
                if (ModCompatibility.Calamity.Loaded) {multiplier+=1.7f;} 
                if (ModCompatibility.SacredTools.Loaded) {multiplier+=1.6f;}

                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = Main.getGoodWorld ? 2000 :(int)(500 + (100 * multiplier));
                    npc.lifeMax = (int)(mutantHealth + (mutantHealth * multiplier));
                }
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                int abomHealth = 1000000;
                float multiplierA = 0;


                if (ModCompatibility.Thorium.Loaded)  {multiplierA += 3f;} //post primoridals
                if (ModCompatibility.Calamity.Loaded) {multiplierA += 6f;} //post yharon
                if (ModCompatibility.SacredTools.Loaded) {multiplierA += 1f;} //post lost sibings

                if (npc.type == ModContent.NPCType<AbomBoss>())
                {
                    npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (20 * multiplierA));
                    npc.lifeMax = (int)(2800000 + (abomHealth * multiplierA))/2; //like wtf
                }
            }
        }

        //public override bool PreAI(NPC npc)
        //{
        //    bool num = false;
        //    if (ModCompatibility.Calamity.Loaded)
        //    {
        //        num = CheckBossRush();
        //    }

        //    if (num && npc.type == ModContent.NPCType<MutantBoss>())
        //    {
        //        npc.active = false;
        //    }

        //    return base.PreAI(npc);
        //}
    }
}
