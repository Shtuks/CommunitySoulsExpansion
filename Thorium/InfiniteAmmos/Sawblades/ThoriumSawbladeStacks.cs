using Fargowiltas.Items.Ammos;
using ssm.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;

namespace ssm.Thorium.InfiniteAmmos.Sawblades
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CursedSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeCursed>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DazzlingSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeLight>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FrozenSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeFrozen>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MoltenSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeMolten>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<Sawblade>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class VileSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeIchor>();
    }
}