using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.gunrightsmod.Enchantments;
using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.gunrightsmod.Enchantments.PlutoniumEnchant;
using static ssm.gunrightsmod.Enchantments.UraniumEnchant;


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
        public int MicroplasticStack = 0;
        public int MicroplasticTimer = 0;
        public bool MicroplasticForce = false;

        public override void ResetEffects()
        {
            // ENCHANTMENTS
            if (!AstatineEnchantEquipped)
            {
                AstatineExplosionCharge = 0;
            }
            AstatineEnchantEquipped = false;

            if (MicroplasticTimer > 0)
            {
                MicroplasticTimer--;
            }

            if (MicroplasticTimer <= 0)
            {
                MicroplasticStack = 0;
                MicroplasticForce = false;
            }
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
        public override void UpdateBadLifeRegen()
        {
            if (Player.lifeRegen > 0)
                Player.lifeRegen = 0;
            if (MicroplasticStack > 0 && MicroplasticTimer > 0)
                Player.lifeRegen -= 10 * MicroplasticStack;
            base.UpdateBadLifeRegen();
        }
        public override void ModifyHurt(ref Player.HurtModifiers Hurt)
        {
            if (MicroplasticForce == true && MicroplasticStack >= 4)
            {
                Hurt.FinalDamage *= 1.2f;
            }
            else if (MicroplasticStack >= 3)
            {
                Hurt.FinalDamage *= 1.1f;
            }
        }
    }
}