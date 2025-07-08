using Terraria.ModLoader;
using ssm.Core;
using Consolaria.Content.NPCs.Bosses.Ocram;
using Terraria;
using Consolaria.Content.NPCs.Bosses.Turkor;

namespace ssm.Consolaria
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class ConsolariaBossesBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Ocram>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 150000 : 100000;
                npc.damage = 230;
            }

            if (npc.type == ModContent.NPCType<TurkortheUngrateful>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 21000 : 16000;
                npc.damage = 420;
            }

            if (npc.type == ModContent.NPCType<TurkortheUngratefulHead>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 5000 : 2600;
                npc.damage = 420;
            }
        }
    }
}