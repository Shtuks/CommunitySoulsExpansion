using Terraria.ModLoader;
using Terraria;
using ssm.SoA;
using ssm.Core;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class RivalBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            var killStreak = Main.LocalPlayer.GetModPlayer<SoAPlayer>().rivalStreak;
            tip = $"Increased damage by {20 * killStreak}% ({killStreak}/5 stacks)";
        }
    }
}
