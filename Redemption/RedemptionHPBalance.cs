using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.ADD;
using CalamityMod.Events;
using Redemption.NPCs.Bosses.PatientZero;
using Redemption.NPCs.Minibosses.Calavia;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.Bosses.Erhan;
using Redemption.NPCs.Minibosses.EaglecrestGolem;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.Obliterator;
using Redemption.NPCs.Bosses.Neb.Clone;

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
                if (npc.type == ModContent.NPCType<Nebuleus>() || npc.type == ModContent.NPCType<Nebuleus_Clone>())
                {
                    float multiplierD = 0;
                    float multiplierL = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.7f; multiplierD += 2f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplierL += 0.5f; multiplierD += 3f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 5f; }
                    if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 1f; }

                    npc.lifeMax = (int)(1900000 + (1000000 * multiplierL));
                    npc.damage = (int)(300 + (10 * multiplierD));
                }

                if (npc.type == ModContent.NPCType<Nebuleus2>() || npc.type == ModContent.NPCType<Nebuleus2_Clone>())
                {
                    float multiplierD = 0;
                    float multiplierL = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.7f; multiplierD += 2f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplierL += 0.5f; multiplierD += 3f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 5f; }
                    if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 1f; }

                    npc.lifeMax = (int)(2600000 + (1000000 * multiplierL));
                    npc.damage = (int)(350 + (10 * multiplierD));
                }

                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = num ? 5000000 : ModCompatibility.Calamity.Loaded ? 1200000 : 540000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = num ? 6000000 : ModCompatibility.Calamity.Loaded ? 1400000 : 640000;
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
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 9000 : 6000;
                    npc.damage = 100;
                }

                if (npc.type == ModContent.NPCType<Thorn>())
                {
                    npc.lifeMax = 3500;
                    npc.damage = 60;
                }

                if (npc.type == ModContent.NPCType<Erhan>())
                {
                    npc.lifeMax = 4600;
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
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 9000 : 6000;
                    npc.damage = 90;
                }

                if (npc.type == ModContent.NPCType<KS3>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 77000 : 50000;
                    npc.damage = 190;
                }

                if (npc.type == ModContent.NPCType<OmegaCleaver>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 100000 : 70000;
                    npc.damage = 220;
                }

                if (npc.type == ModContent.NPCType<Gigapora_ShieldCore>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 23000 : 15000;
                    npc.damage = 220;
                }

                if (npc.type == ModContent.NPCType<OO>())
                {
                    npc.lifeMax = (ModCompatibility.Calamity.Loaded ? 670000 : 340000);
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
