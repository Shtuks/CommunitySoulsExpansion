using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework.Graphics;
using gunrightsmod.Content.Items;
using ssm.Core;
using ssm.Content.SoulToggles;
using System;
using FargowiltasSouls.Content.Projectiles.Souls;
using ssm.Content.Projectiles.Enchantments;
/*
namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]

    public class FaradayEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override Color nameColor => new(44, 6, 149);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
            Item.value = 609331;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FaradayEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FaradayFedora>();
            recipe.AddIngredient<FaradayBodyArmor>();
            recipe.AddIngredient<FaradayPants>();
            recipe.AddIngredient<PortableTower>();
            recipe.AddIngredient<TheAshesOfCalamity>();
            recipe.AddIngredient<TheMoon>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class FaradayEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FaradayEnchant>();
            public override void PostUpdate(Player player)
            {
                if (Main.myPlayer != player.whoAmI)
                    return;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<FaradaySun>()] < 1)
                {
                    var source = player.GetSource_Misc("FaradayEffect");
                    Projectile.NewProjectile(source, player.Center, Vector2.Zero,
                        ModContent.ProjectileType<FaradaySun>(), 20, 1f, player.whoAmI, 0f);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<FaradayMoon>()] < 1)
                {
                    var source = player.GetSource_Misc("FaradayEffect");
                    Projectile.NewProjectile(source, player.Center, Vector2.Zero,
                        ModContent.ProjectileType<FaradayMoon>(), 20, 1f, player.whoAmI, MathHelper.Pi);
                }
            }
        }
    }
}
*/