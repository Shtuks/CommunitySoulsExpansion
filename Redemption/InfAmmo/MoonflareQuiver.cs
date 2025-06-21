using Fargowiltas.Items.Ammos;
using Redemption.Items.Weapons.PreHM.Ammo;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class MoonflareQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<MoonflareArrow>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}
