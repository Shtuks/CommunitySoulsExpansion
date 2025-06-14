using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Blightbone;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Weapons.Dreadfire;
using FargowiltasSouls;
using System;
using Terraria.Audio;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class BlightboneEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(124, 10, 10);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "FeatherHairpin").UpdateAccessory(player, false);

            player.AddEffect<BlightboneEffect>(Item);
        }

        public class BlightboneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlightboneEnchant>();
            public override bool ExtraAttackEffect => true;
            public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
            {
                FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
                if (player.whoAmI != Main.myPlayer || fargoSoulsPlayer.AdditionalAttacksTimer > 0)
                {
                    return;
                }

                fargoSoulsPlayer.AdditionalAttacksTimer = 30;
                float num = 50f;
                Vector2 center = player.Center;
                Vector2 vector = Vector2.Normalize(Main.MouseWorld - center);
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(GetSource_EffectItem(player), center, vector.RotatedByRandom(Math.PI / 2.0) * Main.rand.NextFloat(6f, 10f), ModContent.ProjectileType<Blightbone>(), (int)(num * player.ActualClassDamage(DamageClass.Throwing)), 9f, player.whoAmI);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<BlightMask>();
            recipe.AddIngredient<BlightChest>();
            recipe.AddIngredient<BlightLegs>();
            recipe.AddIngredient<PumpkinAmulet>();
            recipe.AddIngredient<FeatherHairpin>();
            recipe.AddIngredient<PumpGlove>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
