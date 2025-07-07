using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ContinentOfJourney.Items.Material;
using CalamityMod.Items.Materials;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Homeward.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Homeward.Name, ModCompatibility.Crossmod.Name)]
    public class HwjCalRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ShadowspecBar>() && !recipe.HasIngredient<EssenceofBright>())
                {
                    recipe.AddIngredient<EssenceofBright>();
                }

                if (recipe.HasResult<VagabondsSoul>() && !recipe.HasIngredient<FinalBar>())
                {
                    recipe.AddIngredient<FinalBar>(5);
                }
            }
        }
    }
}
