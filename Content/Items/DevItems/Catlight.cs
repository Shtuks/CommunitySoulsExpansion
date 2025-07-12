using Terraria;
using Terraria.ID;
using FargowiltasSouls.Core.Systems;
using ssm.Systems;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using Fargowiltas.Items.Tiles;

namespace ssm.Content.Items.DevItems
{
    public class Catlight : DevItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.DevItems;
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
            Item.DamageType = DamageClass.Generic;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            if (!ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient<AbomEnergy>(10);
            }
            else
            {
                if (ModContent.TryFind("ShadowspecBar", out ModItem cal1))
                {
                    recipe.AddIngredient(cal1, 10);
                }
                if (ModContent.TryFind("Rock", out ModItem cal2))
                {
                    recipe.AddIngredient(cal2, 1);
                }
            }

            //recipe.AddIngredient<AmalgamEnergy>(10);

            if (ModCompatibility.SacredTools.Loaded)
            {
                if (ModContent.TryFind("EmberOfOmen", out ModItem soa1))
                {
                    recipe.AddIngredient(soa1, 10);
                }
            }

            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.Catfish);
            recipe.AddIngredient(ItemID.FallenStar, 3996);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
