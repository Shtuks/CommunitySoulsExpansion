using Terraria.ModLoader;
using Terraria;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using FargowiltasSouls.Content.Buffs.Boss;
using ssm.Core;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionNPCS : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Nebuleus>() || npc.type == ModContent.NPCType<Nebuleus2>())
            {
                if (Main.expertMode && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<AbomPresenceBuff>(), 2);
            }
            return base.PreAI(npc);
        }
    }
}
