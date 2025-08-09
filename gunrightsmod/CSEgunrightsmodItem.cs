using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;

namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class CSEgunrightsmodItem : ModPlayer
    {
        public class WeaponUseGlobalItem : GlobalItem
        {
            public override bool? UseItem(Item item, Player player)
            {
                if (item.damage > 0 && item.pick == 0 && item.axe == 0 && item.hammer == 0)
                {
                    var modPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();
                    modPlayer.PlutoniumCharge++;
                }
                return base.UseItem(item, player);
            }
        }
    }
}