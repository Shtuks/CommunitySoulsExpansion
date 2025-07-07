using Fargowiltas.Items.Ammos;
using Redemption.Items.Weapons.HM.Ammo;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class BilePouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<BileBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}
