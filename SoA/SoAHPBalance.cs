using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Erazor;
using SacredTools.NPCs.Boss.Lunarians;
using SacredTools.NPCs.Boss.Araghur;
using SacredTools.NPCs.Boss.Abaddon;
using SacredTools.NPCs.Boss.Primordia;
using SacredTools.NPCs.Boss.Araneas;
using SacredTools.Content.NPCs.Boss.Jensen;
using SacredTools.NPCs.Boss.Jensen;
using SacredTools.Content.NPCs.Boss.Decree;
using SacredTools.NPCs.Boss.Pumpkin;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using CalamityMod.Events;
using SacredTools.NPCs.Boss.Raynare;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAHPBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool mayo;
        public bool mayo2;
        public bool mayo3;

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }

        public override void SetDefaults(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (!mayo2)
            {
                if (!num && !ssm.SwarmActive)
                {
                    if (npc.type == ModContent.NPCType<ErazorBoss>())
                    {
                        float multiplier = 0;

                        if (ModCompatibility.Thorium.Loaded) { multiplier += 0.5f; }
                        if (ModCompatibility.Homeward.Loaded) { multiplier += 0.5f; }
                        if (ModCompatibility.Calamity.Loaded) { multiplier += 1f; }

                        npc.lifeMax = (int)((3100000 + (1000000 * multiplier)) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(300 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }

                    if (npc.type == ModContent.NPCType<Novaniel>())
                    {
                        npc.lifeMax = (int)(600000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.4f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(210 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Nuba>())
                    {
                        npc.lifeMax = (int)(170000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(200 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Solarius>())
                    {
                        npc.lifeMax = (int)(220000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(230 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Voxa>())
                    {
                        npc.lifeMax = (int)(150000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(210 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Dustite>())
                    {
                        npc.lifeMax = (int)(160000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(100 * 1.5f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<AraghurHead>() || npc.type == ModContent.NPCType<AraghurBody>() || npc.type == ModContent.NPCType<AraghurTail>())
                    {
                        npc.lifeMax = (int)(600000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.75f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(300 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Abaddon>())
                    {
                        npc.lifeMax = (int)(400000 * (ModCompatibility.Calamity.Loaded ? 1.75f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(100 * 1.5f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Primordia>())
                    {
                        npc.lifeMax = (int)(85000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(200 * 1.1f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Primordia2>())
                    {
                        npc.lifeMax = (int)(55000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(200 * 1.1f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Araneas>())
                    {
                        npc.lifeMax = (int)(50000 * (ModCompatibility.Calamity.Loaded ? 1.3f : 1.1f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(60 * 1.4f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Raynare>())
                    {
                        npc.lifeMax = (int)(55000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(70 * 1.4f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Jensen>() || npc.type == ModContent.NPCType<JensenLegacy>())
                    {
                        npc.lifeMax = (int)(9000 * (ModCompatibility.Calamity.Loaded ? 1.7f : 1.4f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(35 * 2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Decree>() || npc.type == ModContent.NPCType<DecreeLegacy>())
                    {
                        npc.lifeMax = (int)(4000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(60 * 1.2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Ralnek>())
                    {
                        npc.lifeMax = (int)(8000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(20 * 2f * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Ralnek2>())
                    {
                        npc.lifeMax = (int)(2000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                        npc.damage = (int)(25 * 2f * (Main.masterMode ? 1.5f : 1));
                    }

                    if (npc.type == ModContent.NPCType<RelicShieldNihilus>())
                    {
                        npc.lifeMax = (int)((ModCompatibility.Calamity.Loaded ? 1500000 : 1000000) * (Main.masterMode ? 1.3f : 1));
                    }
                    if (npc.type == ModContent.NPCType<NihilusLanternRisen>() || npc.type == ModContent.NPCType<NihilusLantern>() || npc.type == ModContent.NPCType<NihilusLanternBig>())
                    {
                        npc.lifeMax = (int)((ModCompatibility.Calamity.Loaded ? 600000 : 400000) * (Main.masterMode ? 1.3f : 1));
                    }

                    if (npc.type == ModContent.NPCType<Nihilus>())
                    {
                        float multiplierL = 0;
                        float multiplierD = 0;

                        if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                        if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                        if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 1f; }

                        npc.lifeMax = (int)((4100000 + (1000000 * multiplierL)) * (Main.masterMode ? 1.3f : 1));
                        npc.lifeMax = (int)((550 + (100 * multiplierD)) * (Main.masterMode ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Nihilus2>())
                    {
                        float multiplierL = 0;
                        float multiplierD = 0;

                        if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                        if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                        if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 1f; }

                        npc.lifeMax = (int)((5900000 + (1000000 * multiplierL)) * (Main.masterMode ? 1.3f : 1));
                        npc.lifeMax = (int)((680 + (100 * multiplierD)) * (Main.masterMode ? 1.5f : 1));
                    }
                }
                mayo2 = true;
            }
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Nihilus>())
            {
                float multiplierL = 0;
                float multiplierD = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 1f; }

                npc.lifeMax = (int)((4100000 + (1000000 * multiplierL)) * (Main.masterMode ? 1.3f : 1));
                npc.lifeMax = (int)((550 + (100 * multiplierD)) * (Main.masterMode ? 1.5f : 1));
            }
            if (npc.type == ModContent.NPCType<Nihilus2>())
            {
                float multiplierL = 0;
                float multiplierD = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 1f; }

                npc.lifeMax = (int)((5900000 + (1000000 * multiplierL)) * (Main.masterMode ? 1.3f : 1));
                npc.lifeMax = (int)((680 + (100 * multiplierD)) * (Main.masterMode ? 1.5f : 1));
            }

            if (!mayo3)
            {
                npc.life = npc.lifeMax;
                mayo3 = true;
            }

            return base.PreAI(npc);
        }
        public override void AI(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (!num && !ssm.SwarmActive)
            {
                if (npc.type == ModContent.NPCType<ErazorBoss>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 0.5f; }
                    if (ModCompatibility.Homeward.Loaded) { multiplier += 0.5f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplier += 1f; }

                    npc.lifeMax = (int)((3100000 + (1000000 * multiplier)) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(300 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }

                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    npc.lifeMax = (int)(600000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.4f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(210 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    npc.lifeMax = (int)(170000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(200 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    npc.lifeMax = (int)(220000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(230 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    npc.lifeMax = (int)(150000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(210 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    npc.lifeMax = (int)(160000 * (ModCompatibility.Calamity.Loaded ? 1.8f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(100 * 1.5f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<AraghurHead>() || npc.type == ModContent.NPCType<AraghurBody>() || npc.type == ModContent.NPCType<AraghurTail>())
                {
                    npc.lifeMax = (int)(600000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.75f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(300 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Abaddon>())
                {
                    npc.lifeMax = (int)(400000 * (ModCompatibility.Calamity.Loaded ? 1.75f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(100 * 1.5f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Primordia>())
                {
                    npc.lifeMax = (int)(85000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(200 * 1.1f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Primordia2>())
                {
                    npc.lifeMax = (int)(55000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(200 * 1.1f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Araneas>())
                {
                    npc.lifeMax = (int)(50000 * (ModCompatibility.Calamity.Loaded ? 1.3f : 1.1f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(60 * 1.4f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Raynare>())
                {
                    npc.lifeMax = (int)(55000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(70 * 1.4f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Jensen>() || npc.type == ModContent.NPCType<JensenLegacy>())
                {
                    npc.lifeMax = (int)(9000 * (ModCompatibility.Calamity.Loaded ? 1.7f : 1.4f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(35 * 2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Decree>() || npc.type == ModContent.NPCType<DecreeLegacy>())
                {
                    npc.lifeMax = (int)(4000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(60 * 1.2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Ralnek>())
                {
                    npc.lifeMax = (int)(8000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(20 * 2f * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Ralnek2>())
                {
                    npc.lifeMax = (int)(2000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f) * (Main.masterMode ? 1.3f : 1));
                    npc.damage = (int)(25 * 2f * (Main.masterMode ? 1.5f : 1));
                }

                if (npc.type == ModContent.NPCType<RelicShieldNihilus>())
                {
                    npc.lifeMax = (int)((ModCompatibility.Calamity.Loaded ? 1500000 : 1000000) * (Main.masterMode ? 1.3f : 1));
                }
                if (npc.type == ModContent.NPCType<NihilusLanternRisen>() || npc.type == ModContent.NPCType<NihilusLantern>() || npc.type == ModContent.NPCType<NihilusLanternBig>())
                {
                    npc.lifeMax = (int)((ModCompatibility.Calamity.Loaded ? 600000 : 400000) * (Main.masterMode ? 1.3f : 1));
                }

                if (npc.type == ModContent.NPCType<Nihilus>())
                {
                    float multiplierL = 0;
                    float multiplierD = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                    if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 1f; }

                    npc.lifeMax = (int)((4100000 + (1000000 * multiplierL)) * (Main.masterMode ? 1.3f : 1));
                    npc.lifeMax = (int)((550 + (100 * multiplierD)) * (Main.masterMode ? 1.5f : 1));
                }
                if (npc.type == ModContent.NPCType<Nihilus2>())
                {
                    float multiplierL = 0;
                    float multiplierD = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                    if (ModCompatibility.Homeward.Loaded) { multiplierL += 0.5f; multiplierD += 0.5f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplierL += 2f; multiplierD += 1f; }

                    npc.lifeMax = (int)((5900000 + (1000000 * multiplierL)) * (Main.masterMode ? 1.3f : 1));
                    npc.lifeMax = (int)((680 + (100 * multiplierD)) * (Main.masterMode ? 1.5f : 1));
                }
            }

            if (npc.boss && npc.ModNPC is ModNPC)
            {
                if (npc.ModNPC.Mod.Name == ModCompatibility.SacredTools.Name)
                {
                    if (Main.getGoodWorld)
                    {
                        npc.lifeMax *= 2;
                        npc.damage *= 2;
                    }

                    int playerCount = CSEUtils.GetPlayerCount();

                    if (playerCount > 1)
                    {
                        double multiplayerFactor = 1.0;
                        double healthAdded = 0.35;

                        for (int i = 2; i <= playerCount; i++)
                        {
                            multiplayerFactor += healthAdded;
                            if (i < playerCount)
                                healthAdded += (1 - healthAdded) / 3;
                        }

                        if (playerCount >= 10)
                            multiplayerFactor = (multiplayerFactor * 2 + 8) / 3;

                        npc.lifeMax = (int)(npc.lifeMax * multiplayerFactor);
                    }
                }
            }

            if (!mayo)
            {
                npc.life = npc.lifeMax;
                mayo = true;
            }

            base.AI(npc);
        }
    }
}
