using CalamityMod.Items;
using FargowiltasSouls.Content.Items.Summons;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class WotGRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //if ((recipe.HasResult(ModContent.ItemType<DimensionSoul>())
                //    || recipe.HasResult(ModContent.ItemType<MasochistSoul>())
                //    || recipe.HasResult(ModContent.ItemType<UniverseSoul>())) && !recipe.HasIngredient<MetallicChunk>())
                //{
                //    recipe.AddIngredient<MetallicChunk>();
                //}

                if (!recipe.HasIngredient<NDMaterialPlaceholder>() && recipe.HasResult<AbominationnVoodooDoll>())
                {
                    recipe.AddIngredient<NDMaterialPlaceholder>(1);
                }

                if (recipe.TryGetResult<ModItem>(out Item result) && result != null)
                {
                    bool hasRock = recipe.TryGetIngredient(ModContent.ItemType<Rock>(), out Item rockIngredient);

                    if (hasRock &&
                        !CSEUtils.IsModItem(result, "ssm") &&
                        !CSEUtils.IsModItem(result, "FargowiltasSouls"))
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<Rock>());
                        recipe.AddIngredient(ModContent.ItemType<NDMaterialPlaceholder>(), 1);
                    }
                }
            }
        }
    }
}
