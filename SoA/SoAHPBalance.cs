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
using FargowiltasSouls.Core.Systems;
using CalamityMod.Events;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAHPBalance : GlobalNPC
    {
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }

        public bool fullHP = false;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded) 
            {
                num = CheckBossRush();
            }

            if (WorldSavingSystem.EternityMode && !num)
            {
                if (npc.type == ModContent.NPCType<ErazorBoss>())
                {
                    npc.defense = 500;
                    npc.lifeMax = 2800000;
                    npc.damage = 400;
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
                    npc.lifeMax = 980000;
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
                    npc.damage = 90;
                }
                if (npc.type == ModContent.NPCType<Jensen>() || npc.type == ModContent.NPCType<JensenLegacy>())
                {
                    npc.lifeMax = 18000;
                    npc.damage = 60;
                }
                if (npc.type == ModContent.NPCType<Decree>() || npc.type == ModContent.NPCType<DecreeLegacy>())
                {
                    npc.lifeMax = 7000;
                    npc.damage = 50;
                }
                if (npc.type == ModContent.NPCType<Ralnek>())
                {
                    npc.lifeMax = 9700;
                    npc.damage = 70;
                }
                if (npc.type == ModContent.NPCType<Ralnek2>())
                {
                    npc.lifeMax = 8000;
                    npc.damage = 70;
                }
                if (npc.type == ModContent.NPCType<Nihilus>())
                {
                    npc.defense = ModCompatibility.Calamity.Loaded ? 700 : 500;
                    npc.lifeMax = Main.masterMode ? ModCompatibility.Calamity.Loaded ? 12000000 : 3300000 : ModCompatibility.Calamity.Loaded ? 10000000 : 2800000;
                    npc.damage = ModCompatibility.Calamity.Loaded ? 600 : 500;
                }
                if (npc.type == ModContent.NPCType<RelicShieldNihilus>())
                {
                    npc.lifeMax = 1500000;
                }
                if (npc.type == ModContent.NPCType<NihilusLanternRisen>() || npc.type == ModContent.NPCType<NihilusLantern>() || npc.type == ModContent.NPCType<NihilusLanternBig>())
                {
                    npc.lifeMax = 1000000;
                }
                if (npc.type == ModContent.NPCType<Nihilus2>())
                {
                    npc.defense = ModCompatibility.Calamity.Loaded ? 700 : 500;
                    npc.lifeMax = Main.masterMode ? ModCompatibility.Calamity.Loaded ? 16000000 : 5700000 : ModCompatibility.Calamity.Loaded ? 14000000 : 4700000;
                    npc.damage = ModCompatibility.Calamity.Loaded ? 600 : 500;
                }
                if (!fullHP) { npc.life = npc.lifeMax; fullHP = true; }
            }
            return base.PreAI(npc);
        }
    }
}
