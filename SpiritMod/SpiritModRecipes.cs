using FargowiltasSouls.Content.Items.Accessories.Souls;
using SpiritMod.Items.Accessory;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SpiritMod
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class ConsolariaRecipes : ModSystem
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
            }
        }
    }
}
