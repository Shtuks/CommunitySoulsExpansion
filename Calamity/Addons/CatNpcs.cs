using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Buffs.Boss;
using Terraria;
using CatalystMod.NPCs.Boss.Astrageldon;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name)]
    public partial class CatNpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Astrageldon>())
            {
                if (Main.expertMode && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<AbomPresenceBuff>(), 2);
            }
            return base.PreAI(npc);
        }
    }
}
