using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using Spooky.Content.NPCs.Boss.RotGourd;
using Spooky.Content.NPCs.Boss.SpookySpirit;

namespace ssm.Spooky
{
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    public class SpookyBossesBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<RotGourd>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 6200 : 5200;
            }

            if (npc.type == ModContent.NPCType<SpookySpirit>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 9000 : 7000;
            }
        }
    }
}