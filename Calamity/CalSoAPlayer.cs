using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using SacredTools;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAPlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Nihilus>()) || NPC.AnyNPCs(ModContent.NPCType<Nihilus2>()))
            {
                Player.ClearBuff(ModContent.BuffType<Enraged>());
            }
        }
        public override void PostUpdateEquips()
        {
            if (Player.GetModPlayer<ModdedPlayer>().NovanielArmor)
            {
                Player.GetModPlayer<CalamityPlayer>().wearingRogueArmor = true;
                Player.GetModPlayer<CalamityPlayer>().rogueStealthMax += 1.3f;
            }
        }
    }
}