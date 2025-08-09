using SacredTools.NPCs.Boss.Araghur;
using SacredTools.NPCs.Boss.Erazor;
using SacredTools.NPCs.Boss.Lunarians;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using SacredTools.NPCs.Boss.Abaddon;
using CalamityMod.Events;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.ADD;
using Redemption.NPCs.Bosses.PatientZero;
using ThoriumMod.NPCs.BossThePrimordials;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    public class SoABossRushHPBalance : GlobalNPC
    {
        public bool fullHP = false;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (BossRushEvent.BossRushActive)
            {
                if (npc.type == ModContent.NPCType<ErazorBoss>())
                {
                    npc.defense = 500;
                    npc.lifeMax = 19800000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    npc.lifeMax = 15100000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    npc.lifeMax = 4350000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    npc.lifeMax = 6400000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    npc.lifeMax = 4370000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    npc.lifeMax = 3280000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<AraghurHead>() || npc.type == ModContent.NPCType<AraghurBody>() || npc.type == ModContent.NPCType<AraghurTail>())
                {
                    npc.lifeMax = 18980000;
                    npc.damage = 700;
                }
                if (npc.type == ModContent.NPCType<Abaddon>())
                {
                    npc.lifeMax = 10700000;
                    npc.damage = 700;
                }
                if (!fullHP) { npc.life = npc.lifeMax; fullHP = true; }
            }
            return base.PreAI(npc);
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    public class RedemptionBossRushHPBalance : GlobalNPC
    {
        public bool fullHP = false;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (BossRushEvent.BossRushActive)
            {
                if (npc.type == ModContent.NPCType<Nebuleus>())
                {
                    npc.lifeMax = 9000000;
                    npc.damage = 600;
                }
                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = 4900000;
                    npc.damage = 500;
                }
                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = 4100000;
                    npc.damage = 500;
                }
                if (npc.type == ModContent.NPCType<PZ>())
                {
                    npc.lifeMax = 6400000;
                    npc.damage = 500;
                }
                if (!fullHP) { npc.life = npc.lifeMax; fullHP = true; }
            }
            return base.PreAI(npc);
        }
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class ThroriumBossRushHPBalance : GlobalNPC
    {
        public bool fullHP = false;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (BossRushEvent.BossRushActive)
            {
                if (npc.type == ModContent.NPCType<DreamEater>())
                {
                    npc.lifeMax = 10000000;
                    npc.damage = 800;
                }
                if (npc.type == ModContent.NPCType<SlagFury>())
                {
                    npc.lifeMax = 5000000;
                    npc.damage = 800;
                }
                if (npc.type == ModContent.NPCType<Aquaius>())
                {
                    npc.lifeMax = 5000000;
                    npc.damage = 800;
                }
                if (npc.type == ModContent.NPCType<Omnicide>())
                {
                    npc.lifeMax = 5000000;
                    npc.damage = 800;
                }
                if (!fullHP) { npc.life = npc.lifeMax; fullHP = true; }
            }
            return base.PreAI(npc);
        }
    }
}
