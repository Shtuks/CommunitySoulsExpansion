using Fargowiltas.Items.Ammos;
using ssm.Core;
using Terraria.ModLoader;
using Consolaria.Content.Items.Weapons.Ammo;

namespace ssm.Consolaria.InfiniteAmmos.Arrows
{
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    public class HeartQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<HeartArrow>();
    }

    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    public class SpectralQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SpectralArrow>();
    }
}