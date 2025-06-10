using Terraria;
using Terraria.ID;
using FargowiltasSouls.Core.Systems;
using ssm.Systems;
using Terraria.ModLoader;

namespace ssm.Content.Items.DevItems
{
    public class Catlight : DevItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExperimentalContent;
        }
        public override string devName => "StarlightCat";
        public override void SetDefaults()
        {
            Item.damage = WorldSavingSystem.DownedMutant ? WorldSaveSystem.downedMutantEX ? 74000 : 7400 : 740;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.autoReuse = true;
            //Item.shoot = ModContent.ProjectileType<CatlightProj>();
            Item.shootSpeed = 0f;
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe(1);

        //    if (!ModCompatibility.Calamity.Loaded)
        //    {
        //        recipe.AddIngredient<AbomEnergy>(10);
        //    }

        //    recipe.AddTile<MutantsForgeTile>();
        //    recipe.Register();
        //}
    }
}
