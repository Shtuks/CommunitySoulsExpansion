using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.ADD;
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
using CalamityMod.Events;
using FargowiltasSouls.Core.Systems;

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

            if (!ssm.SwarmActive && WorldSavingSystem.EternityMode)
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

                if (!ModLoader.HasMod("RevengeagcePlus"))
                {
                    if (npc.type == ModContent.NPCType<Akka>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<Ukko>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<PZ>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<PZ_Kari>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<Calavia>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<Thorn>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.3f);
                    }

                    if (npc.type == ModContent.NPCType<Erhan>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.3f);
                    }

                    if (npc.type == ModContent.NPCType<EaglecrestGolem>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<EaglecrestGolem2>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 2f);
                    }

                    if (npc.type == ModContent.NPCType<SoI>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 1.5f);
                    }

                    if (npc.type == ModContent.NPCType<KS3>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 2f);
                    }

                    if (npc.type == ModContent.NPCType<OmegaCleaver>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 2f);
                    }

                    if (npc.type == ModContent.NPCType<Gigapora_ShieldCore>())
                    {
                        npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                        npc.damage = (int)(npc.damage * 2f);
                    }
                }
                if (npc.type == ModContent.NPCType<OO>())
                {
                    npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                    npc.damage = (int)(npc.damage * 1.5f);
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
