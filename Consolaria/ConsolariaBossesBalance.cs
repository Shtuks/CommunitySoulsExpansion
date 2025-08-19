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
            //if (npc.type == ModContent.NPCType<Ocram>())
            //{
            //    npc.lifeMax = npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
            //    npc.damage = (int)(npc.damage * 1.5f);
            //}

            //if (npc.type == ModContent.NPCType<TurkortheUngrateful>())
            //{
            //    npc.lifeMax = npc.lifeMax = (int)(npc.lifeMax * (ModCompatibility.Calamity.Loaded ? 2 : 1.5f));
            //    npc.damage = (int)(npc.damage * 1.5f);
            //}
        }
    }
}