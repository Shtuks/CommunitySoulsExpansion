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
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<ReachBoss>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.3f);
            }

            if (npc.type == ModContent.NPCType<MoonWizard>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.3f);
            }

            if (npc.type == ModContent.NPCType<AncientFlyer>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<Infernon>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<SteamRaiderHead>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<SteamRaiderBody>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<SteamRaiderBody2>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<Dusking>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<Atlas>())
            {
                npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
                npc.damage = (int)(npc.damage * 1.5f);
            }
        }
    }
}