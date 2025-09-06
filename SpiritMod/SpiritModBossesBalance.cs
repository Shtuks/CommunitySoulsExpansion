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
                npc.lifeMax = (int)(2500 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                npc.damage = (int)(40 * 1.1f);
            }

            if (npc.type == ModContent.NPCType<MoonWizard>())
            {
                npc.lifeMax = (int)(3200 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                npc.damage = (int)(50 * 1.1f);
            }

            if (npc.type == ModContent.NPCType<ReachBoss>())
            {
                npc.lifeMax = (int)(7900 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                npc.damage = (int)(55 * 1.1f);
            }

            if (npc.type == ModContent.NPCType<AncientFlyer>())
            {
                npc.lifeMax = (int)(4960 * (ModCompatibility.Calamity.Loaded ? 1.6f : 1.43f));
                npc.damage = (int)(60 * 1.2f);
            }

            if (npc.type == ModContent.NPCType<SteamRaiderHead>())
            {
                npc.lifeMax = (int)(9600 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.3f));
                npc.damage = (int)(60 * 1.2f);
            }

            if (npc.type == ModContent.NPCType<SteamRaiderBody>())
            {
                npc.lifeMax = (int)(9600 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.3f));
                npc.damage = (int)(50 * 1.2f);
            }

            if (npc.type == ModContent.NPCType<SteamRaiderBody2>())
            {
                npc.lifeMax = (int)(9600 * (ModCompatibility.Calamity.Loaded ? 1.4f : 1.3f));
                npc.damage = (int)(50 * 1.2f);
            }

            if (npc.type == ModContent.NPCType<Infernon>())
            {
                npc.lifeMax = (int)(26000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                npc.damage = (int)(70 * 1.1f);
            }

            if (npc.type == ModContent.NPCType<Dusking>())
            {
                npc.lifeMax = (int)(42000 * (ModCompatibility.Calamity.Loaded ? 1.6f : 1.3f));
                npc.damage = (int)(npc.damage * 1.5f);
            }

            if (npc.type == ModContent.NPCType<Atlas>())
            {
                npc.lifeMax = (int)(69000 * (ModCompatibility.Calamity.Loaded ? 1.6f : 1.4f));
                npc.damage = (int)(npc.damage * 1.5f);
            }
        }
    }
}