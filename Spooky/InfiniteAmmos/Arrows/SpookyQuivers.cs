using Fargowiltas.Items.Ammos;
using ssm.Core;
using Terraria.ModLoader;
using Spooky.Content.Items.SpookyBiome;
using Spooky.Content.Items.SpookyHell;

namespace ssm.Spooky.InfiniteAmmos.Arrows
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class OldWoodQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OldWoodArrow>();
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class BoogerQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SnotArrow>();
    }
}