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
using ThoriumMod.Items.BardItems;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAHPBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool mayo;

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }
        public override bool PreAI(NPC npc)
        {
            if (!mayo)
            {
                if (npc.type == ModContent.NPCType<Nihilus>())
                {
                    npc.lifeMax = Main.masterMode ? ModCompatibility.Calamity.Loaded ? 11000000 : 7300000 : ModCompatibility.Calamity.Loaded ? 9600000 : 6800000;
                    npc.damage = ModCompatibility.Calamity.Loaded ? 500 : 450;
                    npc.life = npc.lifeMax;
                }
                if (npc.type == ModContent.NPCType<Nihilus2>())
                {
                    npc.lifeMax = Main.masterMode ? ModCompatibility.Calamity.Loaded ? 16000000 : 11000000 : ModCompatibility.Calamity.Loaded ? 15000000 : 9700000;
                    npc.damage = ModCompatibility.Calamity.Loaded ? 500 : 450;
                    npc.life = npc.lifeMax;
                }
                mayo = true;
            }
            return base.PreAI(npc);
        }
        public override void SetDefaults(NPC npc)
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

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 1f; }
                    if (ModCompatibility.Calamity.Loaded) { multiplier += 1f; }

                    npc.lifeMax = (int)(3500000 + (1000000 * multiplier));
                    npc.damage = 630;
                }

                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    npc.lifeMax = 1100000;
                    npc.damage = 300;
                }
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    npc.lifeMax = 350000;
                    npc.damage = 250;
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    npc.lifeMax = 400000;
                    npc.damage = 240;
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    npc.lifeMax = 370000;
                    npc.damage = 260;
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    npc.lifeMax = 280000;
                    npc.damage = 290;
                }
                if (npc.type == ModContent.NPCType<AraghurHead>() || npc.type == ModContent.NPCType<AraghurBody>() || npc.type == ModContent.NPCType<AraghurTail>())
                {
                    npc.lifeMax = 1800000;
                    npc.damage = 500;
                }
                if (npc.type == ModContent.NPCType<Abaddon>())
                {
                    npc.lifeMax = 720000;
                    npc.damage = 400;
                }
                if (npc.type == ModContent.NPCType<Primordia>())
                {
                    npc.lifeMax = 120000;
                    npc.damage = 180;
                }
                if (npc.type == ModContent.NPCType<Primordia2>())
                {
                    npc.lifeMax = 90000;
                    npc.damage = 170;
                }
                if (npc.type == ModContent.NPCType<Araneas>())
                {
                    npc.lifeMax = 70000;
                    npc.damage = 110;
                }
                if (npc.type == ModContent.NPCType<Jensen>() || npc.type == ModContent.NPCType<JensenLegacy>())
                {
                    npc.lifeMax = 10000;
                    npc.damage = 100;
                }
                if (npc.type == ModContent.NPCType<Decree>() || npc.type == ModContent.NPCType<DecreeLegacy>())
                {
                    npc.lifeMax = 9000;
                    npc.damage = 70;
                }
                if (npc.type == ModContent.NPCType<Ralnek>())
                {
                    npc.lifeMax = 12000;
                    npc.damage = 90;
                }
                if (npc.type == ModContent.NPCType<Ralnek2>())
                {
                    npc.lifeMax = 9000;
                    npc.damage = 90;
                }

                if (npc.type == ModContent.NPCType<RelicShieldNihilus>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 1500000 : 1000000;
                }
                if (npc.type == ModContent.NPCType<NihilusLanternRisen>() || npc.type == ModContent.NPCType<NihilusLantern>() || npc.type == ModContent.NPCType<NihilusLanternBig>())
                {
                    npc.lifeMax = ModCompatibility.Calamity.Loaded ? 500000 : 400000;
                }

                if (Main.getGoodWorld)
                {
                    npc.lifeMax *= 5;
                    npc.damage *= 3;
                }
            }
        }
    }
}
