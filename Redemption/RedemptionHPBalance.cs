using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Core.Systems;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.ADD;
using CalamityMod.Events;
using Redemption.NPCs.Bosses.PatientZero;
using SpiritMod.Items.Ammo.Rocket.Warhead;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionHPBalance : GlobalNPC
    {
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            //if (WorldSavingSystem.EternityMode)
            //{
            if (npc.type == ModContent.NPCType<Nebuleus>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.3f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplier += 0.5f; }

                    npc.lifeMax = (int)(3400000 + (1000000 * multiplier));
                    npc.damage = 730;
                }

                if (npc.type == ModContent.NPCType<Nebuleus2>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.3f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplier += 0.5f; }

                    npc.lifeMax = (int)(3700000 + (1000000 * multiplier));
                    npc.damage = 800;
                }

                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = num ? 5000000 : 1600000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = num ? 6000000 : 1800000;
                    npc.damage = 470;
                }

                if (npc.type == ModContent.NPCType<PZ>())
                {
                    npc.lifeMax = num ? 5000000 : 1600000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<PZ_Kari>())
                {
                    npc.lifeMax = num ? 4000000 : 1800000;
                    npc.damage = 470;
                }
        }
        public override bool CheckDead(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (npc.type == ModContent.NPCType<PZ>() && num)
            {
                if (npc.life < 10)
                {
                    return true;
                }
                return false;
            }
            return base.CheckDead(npc);
        }
    }
}
