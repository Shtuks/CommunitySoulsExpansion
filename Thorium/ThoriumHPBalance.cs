using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using CalamityMod.Events;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossViscount;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossBuriedChampion;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.NPCs.BossBoreanStrider;
using ThoriumMod.NPCs.BossFallenBeholder;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossThePrimordials;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumHPBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool mayo;

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }

        public override void AI(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (!mayo)
            {
                if (!num && !ssm.SwarmActive)
                {
                    //pre hm
                    if (npc.type == ModContent.NPCType<TheGrandThunderBird>())
                    {
                        npc.lifeMax = (int)(2100 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.2f));
                        npc.damage = (int)(55);
                    }

                    if (npc.type == ModContent.NPCType<QueenJellyfish>())
                    {
                        npc.lifeMax = (int)(5600 * (ModCompatibility.Calamity.Loaded ? 1.7f : 1.4f));
                        npc.damage = (int)(60);
                    }

                    if (npc.type == ModContent.NPCType<Viscount>())
                    {
                        npc.lifeMax = (int)(7000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.3f));
                        npc.damage = (int)(60);
                    }

                    if (npc.type == ModContent.NPCType<BuriedChampion>()
                        || npc.type == ModContent.NPCType<GraniteEnergyStorm>())
                    {
                        npc.lifeMax = (int)(9800 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f));
                        npc.damage = (int)(70);
                    }

                    if (npc.type == ModContent.NPCType<StarScouter>())
                    {
                        npc.lifeMax = (int)(10500 * (ModCompatibility.Calamity.Loaded ? 2f : 1.6f));
                        npc.damage = (int)(80);
                    }

                    //hm
                    if (npc.type == ModContent.NPCType<BoreanStrider>())
                    {
                        npc.lifeMax = (int)(16800 * (ModCompatibility.Calamity.Loaded ? 2f : 1.6f));
                        npc.damage = (int)(100);
                    }
                    if (npc.type == ModContent.NPCType<BoreanStriderPopped>())
                    {
                        npc.lifeMax = (int)(16800 * (ModCompatibility.Calamity.Loaded ? 2f : 1.6f));
                        npc.damage = (int)(100);
                    }

                    if (npc.type == ModContent.NPCType<FallenBeholder>())
                    {
                        npc.lifeMax = (int)(21000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f));
                        npc.damage = (int)(75);
                    }
                    if (npc.type == ModContent.NPCType<FallenBeholder2>())
                    {
                        npc.lifeMax = (int)(21000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f));
                        npc.damage = (int)(75);
                    }

                    if (npc.type == ModContent.NPCType<Lich>())
                    {
                        npc.lifeMax = (int)(39200 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f));
                        npc.damage = (int)(160);
                    }
                    if (npc.type == ModContent.NPCType<LichHeadless>())
                    {
                        npc.lifeMax = (int)(39200 * (ModCompatibility.Calamity.Loaded ? 2f : 1.5f));
                        npc.damage = (int)(160);
                    }

                    if (npc.type == ModContent.NPCType<ForgottenOne>())
                    {
                        npc.lifeMax = (int)(84000 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.1f));
                        npc.damage = (int)(165);
                    }
                    if (npc.type == ModContent.NPCType<ForgottenOneCracked>())
                    {
                        npc.lifeMax = (int)(84000 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.1f));
                        npc.damage = (int)(165);
                    }
                    if (npc.type == ModContent.NPCType<ForgottenOneReleased>())
                    {
                        npc.lifeMax = (int)(84000 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.1f));
                        npc.damage = (int)(170);
                    }

                    //post ml
                    if (npc.type == ModContent.NPCType<SlagFury>())
                    {
                        npc.lifeMax = (int)(500000 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1));
                        npc.damage = (int)(250 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Omnicide>())
                    {
                        npc.lifeMax = (int)(550000 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1));
                        npc.damage = (int)(230 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<Aquaius>())
                    {
                        npc.lifeMax = (int)(575000 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1));
                        npc.damage = (int)(200 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1));
                    }
                    if (npc.type == ModContent.NPCType<DreamEater>())
                    {
                        npc.lifeMax = (int)(600000 * (ModCompatibility.Calamity.Loaded ? 2.1f : 1));
                        npc.damage = (int)(270 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1));
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

                        if (multiplayerFactor > 1000)
                            multiplayerFactor = 1000;

                        npc.lifeMax = (int)(npc.lifeMax * multiplayerFactor);
                    }
                }

                npc.life = npc.lifeMax;
                mayo = true;
            }
        }
    }
}
