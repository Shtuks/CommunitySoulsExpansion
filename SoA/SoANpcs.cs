using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Systems;
using ssm.Content.Buffs;
using Terraria.ID;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public partial class SoANpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void OnKill(NPC npc)
        {
            if(npc.type == ModContent.NPCType<Nihilus>() && !WorldSaveSystem.downedNihilus)
            {
                WorldSaveSystem.downedNihilus = true;
            }
        }
        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Nihilus>() || npc.type == ModContent.NPCType<Nihilus2>())
            {
                if (Main.expertMode && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<NihilityPresenceBuff>(), 2);
            }
            return base.PreAI(npc);
        }
    }
}
