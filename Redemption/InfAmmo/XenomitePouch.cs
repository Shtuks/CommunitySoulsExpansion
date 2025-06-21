using Fargowiltas.Items.Ammos;
using Terraria.ModLoader;
using Redemption.Items.Weapons.PreHM.Ammo;
using ssm.Core;

namespace ssm.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XenomitePouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<XenomiteBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}
