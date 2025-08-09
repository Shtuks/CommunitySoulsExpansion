using Fargowiltas.Items.Ammos;
using ssm.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.Misc;

namespace ssm.Thorium.InfiniteAmmos.Misc
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BaseballJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<Baseball>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LilTorpedoBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<LilTorpedo>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PillCase : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<Pill>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SeethingChargeJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SeethingCharge>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SnotBallJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SnotBall>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SpudJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<ThoriumMod.Items.NPCItems.Spud>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SteamBatteryCase : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SteamBattery>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SyringeCase : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<Syringe>();
    }
}