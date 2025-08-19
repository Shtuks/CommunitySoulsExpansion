using CalamityMod.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using SacredTools.Content.Items.Materials;
using SpiritMod.Items.Accessory;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SpiritMod
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SpiritModRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<TrawlerSoul>() && !recipe.HasIngredient<KoiTotem>())
                {
                    recipe.AddIngredient<KoiTotem>(1);
                }
                //if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<SpiritSoul>())
                //{
                //    recipe.AddIngredient<SpiritSoul>(1);
                //}
            }
        }
    }
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name, ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name, ModCompatibility.Calamity.Name)]
    public class SpiritCalRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<SpiritSoul>() && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                }
            }
        }
    }
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name, ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name, ModCompatibility.SacredTools.Name)]
    public class SpiritSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<SpiritSoul>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
            }
        }
    }
}
