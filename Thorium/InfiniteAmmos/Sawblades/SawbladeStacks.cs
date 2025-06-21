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
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DazzlingSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeLight>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FrozenSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeFrozen>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MoltenSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeMolten>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<Sawblade>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class VileSawbladeStack : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SawbladeIchor>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }
}