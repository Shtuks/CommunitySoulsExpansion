using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.ADD;
using Redemption.NPCs.Bosses.PatientZero;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.Bosses.Erhan;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.Obliterator;
using Redemption.NPCs.Bosses.Neb.Clone;
using CalamityMod.Events;
using Redemption.NPCs.Bosses.Keeper;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionHPBalance : GlobalNPC
    {
        public bool mayo;
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }
        public override bool InstancePerEntity => true;
        public override void AI(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (mayo)
            {
                if (!ssm.SwarmActive && !num)
                {
                    if (npc.type == ModContent.NPCType<Nebuleus>() || npc.type == ModContent.NPCType<Nebuleus_Clone>())
                    {
                        float multiplierD = 0;
                        float multiplierL = 0;

                        if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 2f; }
                        if (ModCompatibility.SacredTools.Loaded) { multiplierL += 0.3f; multiplierD += 3f; }
                        if (ModCompatibility.Calamity.Loaded) { multiplierL += 1.5f; multiplierD += 5f; }
                        if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.3f; multiplierD += 1f; }

                        npc.lifeMax = (int)(1900000 + (1000000 * multiplierL));
                        npc.damage = (int)(300 + (10 * multiplierD));
                    }

                    if (npc.type == ModContent.NPCType<Nebuleus2>() || npc.type == ModContent.NPCType<Nebuleus2_Clone>())
                    {
                        float multiplierD = 0;
                        float multiplierL = 0;

                        if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 2f; }
                        if (ModCompatibility.SacredTools.Loaded) { multiplierL += 0.3f; multiplierD += 3f; }
                        if (ModCompatibility.Calamity.Loaded) { multiplierL += 1.5f; multiplierD += 5f; }
                        if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.3f; multiplierD += 1f; }

                        npc.lifeMax = (int)(2600000 + (1000000 * multiplierL));
                        npc.damage = (int)(350 + (10 * multiplierD));
                    }

                    if (npc.type == ModContent.NPCType<Thorn>())
                    {
                        npc.lifeMax = (int)(3800 * (ModCompatibility.Calamity.Loaded ? 1.2f : 1.1f));
                        npc.damage = (int)(37 * 1.2f);
                    }

                    if (npc.type == ModContent.NPCType<Keeper>())
                    {
                        npc.lifeMax = (int)(4200 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.2f));
                        npc.damage = (int)(40 * 1.2f);
                    }

                    if (npc.type == ModContent.NPCType<Erhan>())
                    {
                        npc.lifeMax = (int)(3900 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.2f));
                        npc.damage = (int)(40 * 1.2f);
                    }

                    if (npc.type == ModContent.NPCType<SoI>())
                    {
                        npc.lifeMax = (int)(6000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                        npc.damage = (int)(54 * 1.3f);
                    }

                    if (npc.type == ModContent.NPCType<KS3>())
                    {
                        npc.lifeMax = (int)(43000 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1.6f));
                        npc.damage = (int)(120 * 1.2f);
                    }

                    if (npc.type == ModContent.NPCType<OmegaCleaver>())
                    {
                        npc.lifeMax = (int)(50400 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1.6f));
                        npc.damage = (int)(160 * 1.2f);
                    }

                    if (npc.type == ModContent.NPCType<Gigapora_ShieldCore>())
                    {
                        npc.lifeMax = (int)(9600 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1.6f));
                    }

                    if (npc.type == ModContent.NPCType<OO>())
                    {
                        npc.lifeMax = (int)(640000 * (ModCompatibility.Calamity.Loaded ? 1.3f : 1));
                        npc.damage = (int)(200 * 1.1f);
                    }

                    if (npc.type == ModContent.NPCType<PZ>())
                    {
                        npc.lifeMax = (int)(660000 * (ModCompatibility.Calamity.Loaded ? 2f : 1));
                        npc.damage = (int)(180 * 1.1f);
                    }
                    if (npc.type == ModContent.NPCType<PZ_Kari>())
                    {
                        npc.lifeMax = (int)(440000 * (ModCompatibility.Calamity.Loaded ? 2f : 1));
                        npc.damage = (int)(200 * 1.1f);
                    }

                    if (npc.type == ModContent.NPCType<EaglecrestGolem2>())
                    {
                        npc.lifeMax = (int)(180000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f));
                        npc.damage = (int)(180 * 1.1f);
                    }

                    if (npc.type == ModContent.NPCType<Ukko>())
                    {
                        npc.lifeMax = (int)(540000 * (ModCompatibility.Calamity.Loaded ? 2f : 1));
                        npc.damage = (int)(180 * 1.1f);
                    }
                    if (npc.type == ModContent.NPCType<Akka>())
                    {
                        npc.lifeMax = (int)(500000 * (ModCompatibility.Calamity.Loaded ? 2f : 1));
                        npc.damage = (int)(140 * 1.1f);
                    }

                    if (Main.masterMode)
                    {
                        npc.lifeMax = (int)(npc.lifeMax * 1.4f);
                        npc.damage = (int)(npc.damage * 1.3f);
                    }
                    if (Main.getGoodWorld)
                    {
                        npc.lifeMax *= 2;
                        npc.damage *= 2;
                    }

                    if (CSEUtils.GetPlayerCount() > 1)
                    {
                        npc.lifeMax *= ((CSEUtils.GetPlayerCount() - 1) / 2);
                    }

                }

                npc.life = npc.lifeMax;
                mayo = true;
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
