using CalamityMod.Events;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }
        public override bool InstancePerEntity => true;

        public int chtuxlagorInferno;
        public static int ECH = -1;
        public static int DukeEX = -1;
        public static int boss = -1;
        public static int mutantEX = -1;

        public static float multiplierM = 0;
        public static float multiplierA = 0;

        public override void Load()
        {
            if (ModCompatibility.IEoR.Loaded) { multiplierM += 5f; }
            if (ModCompatibility.Redemption.Loaded) { multiplierA += 1f; }
            if (ModCompatibility.Thorium.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierA += 1f; }
            if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierM += 0.7f; }
            if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplierM += 1f; }
            if (ModCompatibility.Thorium.Loaded) { multiplierM += 0.9f; multiplierA += 3f; }
            if (ModCompatibility.Calamity.Loaded) { multiplierM += 1.8f; multiplierA += 7f; }
            if (ModCompatibility.SacredTools.Loaded) { multiplierM += 1.3f; multiplierA += 2f; }
        }
        public override void SetDefaults(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                multiplierM *= num ? 0.5f : 1f;

                npc.damage = Main.getGoodWorld ? 2000 : (int)(500 + (125 * (ModCompatibility.IEoR.Loaded ? Math.Round(multiplierM, 1) * 0.5f : Math.Round(multiplierM, 1))));
                npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1)));
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (15 * multiplierA));
                npc.lifeMax = (int)(2800000 + (1000000 * multiplierA)) / (Main.expertMode ? 2 : 4);
            }
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantBoss>()] = true;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (chtuxlagorInferno > 0)
                ApplyDPSDebuff(npc.lifeMax / 10, npc.lifeMax / 100, ref npc.lifeRegen, ref damage);
        }

        public override void PostAI(NPC npc)
        {
            if (chtuxlagorInferno > 0)
            {
                chtuxlagorInferno--;
            }
        }
        
        public void ApplyDPSDebuff(int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
        {
            if (lifeRegen > 0)
            {
                lifeRegen = 0;
            }

            lifeRegen -= lifeRegenValue;
            if (damage < damageValue)
            {
                damage = damageValue;
            }
        }
    }
}
