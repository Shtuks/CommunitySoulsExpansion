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
using Redemption.NPCs.Minibosses.Calavia;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.Bosses.Erhan;
using Redemption.NPCs.Minibosses.EaglecrestGolem;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.Obliterator;

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

            if (!ssm.SwarmActive)
            {
                if (npc.type == ModContent.NPCType<Nebuleus>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.3f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplier += 0.5f; }

                    npc.lifeMax = (int)(3400000 + (1000000 * multiplier));
                    npc.damage = 500;
                }

                if (npc.type == ModContent.NPCType<Nebuleus2>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.3f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplier += 0.5f; }

                    npc.lifeMax = (int)(3700000 + (1000000 * multiplier));
                    npc.damage = 700;
                }

                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = num ? 5000000 : ModCompatibility.Calamity.Loaded ? 1600000 : 540000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = num ? 6000000 : ModCompatibility.Calamity.Loaded ? 1800000 : 640000;
                    npc.damage = 470;
                }

                if (npc.type == ModContent.NPCType<PZ>())
                {
                    npc.lifeMax = num ? 5000000 : ModCompatibility.Calamity.Loaded ? 1600000 : 440000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<PZ_Kari>())
                {
                    npc.lifeMax = num ? 4000000 : ModCompatibility.Calamity.Loaded ? 100000 : 500000;
                    npc.damage = 470;
                }

                if (npc.type == ModContent.NPCType<Calavia>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 14000 : 9000;
                    npc.damage = 100;
                }

                if (npc.type == ModContent.NPCType<Thorn>())
                {
                    npc.lifeMax = 4000;
                    npc.damage = 60;
                }

                if (npc.type == ModContent.NPCType<Erhan>())
                {
                    npc.lifeMax = 5600;
                    npc.damage = 65;
                }

                if (npc.type == ModContent.NPCType<EaglecrestGolem>())
                {
                    npc.lifeMax = 6000;
                    npc.damage = 60;
                }

                if (npc.type == ModContent.NPCType<EaglecrestGolem2>())
                {
                    npc.lifeMax = 600000;
                    npc.damage = 320;
                }

                if (npc.type == ModContent.NPCType<SoI>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 10000 : 8000;
                    npc.damage = 90;
                }

                if (npc.type == ModContent.NPCType<SoI>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 10000 : 8000;
                    npc.damage = 90;
                }

                if (npc.type == ModContent.NPCType<KS3>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 99000 : 60000;
                    npc.damage = 190;
                }

                if (npc.type == ModContent.NPCType<OmegaCleaver>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 120000 : 90000;
                    npc.damage = 220;
                }

                if (npc.type == ModContent.NPCType<Gigapora_ShieldCore>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 23000 : 15000;
                    npc.damage = 220;
                }

                if (npc.type == ModContent.NPCType<OO>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 670000 : 340000;
                    npc.damage = 320;
                }
            }
        }
        public override bool CheckDead(NPC npc)
        {
            //sometimes it can freeze with 1 hp stopping boss rush
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
