using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Thorium.Buffs
{
    public class SoulStrength : ModBuff
    {
        public static readonly float StrengthBonus = 1.5f; // Gives a 50% damage increase

        public override LocalizedText Description => base.Description.WithFormatArgs(StrengthBonus);

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += StrengthBonus - 1f;
        }
    }
}