using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;
using Fargowiltas.Items.Tiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ssm.Content.Items.DevItems
{
    internal class Catlight : DevItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.DevItems;
        }
        public override string devName => "StarlightCat";
        public override void SetDefaults()
        {
            Item.damage = 740;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.autoReuse = true;
            //Item.shoot = ModContent.ProjectileType<CatlightCat>();
            Item.shootSpeed = 0f;
            Item.DamageType = DamageClass.Generic;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            recipe.AddIngredient<AbomEnergy>(10);
   
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.Catfish);
            recipe.AddIngredient(ItemID.FallenStar, 3996);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
