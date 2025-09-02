using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Fargowiltas.Items.Tiles;
using ssm.Content.Items.Materials;

namespace ssm.Content.Items.DevItems
{
    internal class Catlight : DevItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.DevItems && CSEConfig.Instance.AlternativeSiblings;
        }
        public override string devName => "StarlightCat";
        public override bool isUpgradeable => true;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shootSpeed = 0f;
            Item.DamageType = DamageClass.Generic;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            recipe.AddIngredient<tModLoadiumBar>(10);
   
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.Catfish);
            recipe.AddIngredient(ItemID.FallenStar, 3996);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
