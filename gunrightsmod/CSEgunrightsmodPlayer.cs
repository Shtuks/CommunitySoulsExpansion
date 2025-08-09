using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.gunrightsmod.Enchantments;
using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;


namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class CSEgunrightsmodPlayer : ModPlayer
    {
        // ENCHANTMENTS
        public bool AstatineEnchantEquipped;
        public int AstatineExplosionCharge;
        public int PlutoniumCharge;
        public int PlutoniumMissileDamage;

        public override void ResetEffects()
        {
            // ENCHANTMENTS
            if (!AstatineEnchantEquipped)
            {
                AstatineExplosionCharge = 0;
            }
            AstatineEnchantEquipped = false;
        //    if (!Player.HasEffect<PlutoniumEffect>())
        //    {
        //        PlutoniumCharge = 0;
        //    }
        }

        public override float UseSpeedMultiplier(Item item)
        {
            var FargoSoulsPlayer = Player.GetModPlayer<FargoSoulsPlayer>();

            if (AstatineExplosionCharge >= 15 * 60 || Player.FargoSouls().ForceEffect<AstatineEnchant>())
            {
                FargoSoulsPlayer.AttackSpeed += 0.3f;
            }
            return FargoSoulsPlayer.AttackSpeed;
        }

        public override void PostUpdateEquips()
        {
            if (Player.HasEffect<PlutoniumEffect>())
            {
                int WeaponBase = Player.HeldItem.damage;
                int WeaponFull = Player.GetWeaponDamage(Player.HeldItem);
                int WeaponAdd = WeaponFull - WeaponBase;
                PlutoniumMissileDamage = WeaponBase + (int)(WeaponAdd * 0.25f);
            }
            if (Player.ForceEffect<UraniumEffect>())
            {
                Player.GetDamage(DamageClass.Generic) += 0.2f;
            }
        }
    }
}