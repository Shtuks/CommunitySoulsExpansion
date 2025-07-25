using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using ContinentOfJourney.Items.Accessories;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Homeward.Name)]
    public class HwjSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<VoidSpurs>() && !recipe.HasIngredient<Horizon>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<RoyalRunners>());
                    recipe.AddIngredient<Horizon>();
                }
            }
        }
    }
}
