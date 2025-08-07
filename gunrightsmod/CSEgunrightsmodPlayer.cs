using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ssm.gunrightsmod.Forces;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Core;
using ssm.gunrightsmod.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Content.Bosses.CursedCoffin;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Dyes;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Content.UI;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.IO;
using static FargowiltasSouls.Core.Systems.DashManager;


namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class CSEgunrightsmodPlayer : ModPlayer
    {
        // ENCHANTMENTS
        public bool AstatineEnchantEquipped;
        public Item AstatineEnchantItem;
        public int AstatineExplosionCharge = 0;
        public int AstatineExplosionCooldown;
        public bool FaradayEnchantEquipped;
        public Item FaradayEnchantItem;
        public bool KevlarEnchantEquipped;
        public Item KevlarEnchantItem;
        public bool PlasticEnchantEquipped;
        public Item PlasticEnchantItem;
        public bool PlutoniumEnchantEquipped;
        public Item PlutoniumEnchantItem;
        public bool PurifiedSaltEnchantEquipped;
        public Item PurifiedSaltEnchantItem;
        public bool RockSaltEnchantEquipped;
        public Item RockSaltEnchantItem;
        public bool SuperCeramicEnchantEquipped;
        public Item SuperCeramicEnchantItem;
        public bool UraniumEnchantEquipped;
        public Item UraniumEnchantItem;

        // FORCES
        public bool RadioactiveForceEquipped;
        public bool IdeocracyForceEquipped;

        public override void ResetEffects()
        {
            // ENCHANTMENTS
            AstatineEnchantEquipped = false;
            AstatineEnchantItem = null;
            FaradayEnchantEquipped = false;
            FaradayEnchantItem = null;
            KevlarEnchantEquipped = false;
            KevlarEnchantItem = null;
            PlasticEnchantEquipped = false;
            PlasticEnchantItem = null;
            PlutoniumEnchantEquipped = false;
            PlutoniumEnchantItem = null;
            PurifiedSaltEnchantEquipped = false;
            PurifiedSaltEnchantItem = null;
            RockSaltEnchantEquipped = false;
            RockSaltEnchantItem = null;
            SuperCeramicEnchantEquipped = false;
            SuperCeramicEnchantItem = null;
            UraniumEnchantEquipped = false;
            UraniumEnchantItem = null;

            // FORCES
            RadioactiveForceEquipped = false;
            IdeocracyForceEquipped = false;
        }

        public override float UseSpeedMultiplier(Item item)
        {
            var FargoSoulsPlayer = Player.GetModPlayer<FargoSoulsPlayer>();

            if (AstatineExplosionCharge >= AstatineExplosionCooldown)
            {
                FargoSoulsPlayer.AttackSpeed += 0.3f;
            }
            return FargoSoulsPlayer.AttackSpeed;
        }
    }
}