using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using CalamityMod.Items.Materials;
using ssm.Core;
using ssm.Calamity.Souls;
using Fargowiltas.Items.Tiles;
using ssm.CrossMod.CraftingStations;
using CalamityMod.Tiles.Furniture.CraftingStations;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalDlcRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult(ModContent.ItemType<EternitySoul>()) && recipe.HasTile<DraedonsForge>())
                {
                    recipe.RemoveTile(ModContent.TileType<DraedonsForge>());
                    recipe.AddTile<MutantsForgeTile>();
                }
                //FUCK PERSON WHO PUT ETERNAL ENERGY IN THAT DUMB BAR
                if (recipe.HasResult(ModContent.ItemType<ShadowspecBar>()) && recipe.HasIngredient<EternalEnergy>())
                    {
                        recipe.AddIngredient<AbomEnergy>();
                        recipe.RemoveIngredient(ModContent.ItemType<EternalEnergy>());
                    }
                if (/*!ShtunConfig.Instance.ExperimentalContent && */recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<CalamitySoul>() && recipe.HasIngredient<BrandoftheBrimstoneWitch>())
                {
                    if (recipe.RemoveIngredient(ModContent.ItemType<BrandoftheBrimstoneWitch>()))
                        recipe.AddIngredient<CalamitySoul>();
                }
            }
        }
    }
}


