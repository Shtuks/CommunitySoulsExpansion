using Fargowiltas.Items.Ammos;
using ssm.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.Icy;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Geode;

namespace ssm.Thorium.InfiniteAmmos.Arrows
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SpiritQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SpiritArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DurasteelQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<DurasteelArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GhostPulseQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<GhostPulseArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class IcyQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<IcyArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SteelQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SteelArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TalonQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<TalonArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CrystalQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<CrystalArrow>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }
}