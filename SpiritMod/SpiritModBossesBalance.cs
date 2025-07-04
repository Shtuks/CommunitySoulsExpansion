using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SpiritMod.NPCs.Boss.Scarabeus;
using SpiritMod.NPCs.Boss.ReachBoss;
using SpiritMod.NPCs.Boss.MoonWizard;
using SpiritMod.NPCs.Boss;
using SpiritMod.NPCs.Boss.Infernon;
using SpiritMod.NPCs.Boss.SteamRaider;
using SpiritMod.NPCs.Boss.Dusking;
using SpiritMod.NPCs.Boss.Atlas;

namespace ssm.SpiritMod
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SpiritModBossesBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Scarabeus>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 4800 : 3900;
            }

            if (npc.type == ModContent.NPCType<ReachBoss>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 13000 : 10000;
                npc.damage = 60;
            }

            if (npc.type == ModContent.NPCType<ReachBoss>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 8000 : 6000;
                npc.damage = 65;
            }

            if (npc.type == ModContent.NPCType<ReachBoss>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 8000 : 6000;
                npc.damage = 65;
            }

            if (npc.type == ModContent.NPCType<MoonWizard>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 8200 : 6200;
                npc.damage = 60;
            }

            if (npc.type == ModContent.NPCType<AncientFlyer>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 15000 : 12000;
                npc.damage = 60;
            }

            if (npc.type == ModContent.NPCType<Infernon>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 53000 : 32000;
                npc.damage = 110;
            }

            if (npc.type == ModContent.NPCType<SteamRaiderHead>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 22000 : 19000;
                npc.damage = 100;
            }

            if (npc.type == ModContent.NPCType<SteamRaiderHead>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 22000 : 19000;
                npc.damage = 100;
            }

            if (npc.type == ModContent.NPCType<Dusking>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 75000 : 64000;
                npc.damage = 140;
            }

            if (npc.type == ModContent.NPCType<Dusking>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 75000 : 64000;
                npc.damage = 140;
            }

            if (npc.type == ModContent.NPCType<Atlas>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 175000 : 98000;
                npc.damage = 170;
            }
        }
    }
}