using Fargowiltas.Items.Ammos;
using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Items.Weapons.Ammo;
using SacredTools.Content.Items.Weapons.Flarium;

namespace ssm.SoA.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<FlariumArrow>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumPouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<FlariumBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class LunarianPouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<PhaseBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class LunarianQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<PhaseArrow>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    //[ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    //[JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    //public class AsthralQuiver : BaseAmmo
    //{
    //    public override int AmmunitionItem => ModContent.ItemType<AsthralArrow>();

    //    public override void SetStaticDefaults()
    //    {
    //        base.SetStaticDefaults();
    //    }
    //}
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class AdamantiteJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbAdamantite>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class ChlorophyteJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbChlorophyte>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class CobaltJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbCobalt>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class MarstechJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbMarstech>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class MythrilJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbMythril>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class OrichalcumJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbOrichalcum>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class PalladiumJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbPalladium>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SpectralJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbSpectral>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class TitaniumJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbTitanium>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VenomiteJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OrbVenomite>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class AsthraltitePouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<AsthralBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class OblivionQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<OblivionArrow>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class OblivionPouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<ShadowBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}
