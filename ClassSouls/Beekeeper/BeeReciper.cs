using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using Terraria;
using ssm.ClassSouls.Beekeeper.Souls;
using ssm.Core;

namespace ssm.ClassSouls.Beekeeper
{
    [ExtendsFromMod(ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
    public class BeeReciper : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<BeekeeperSoul>())
                {
                    recipe.AddIngredient<BeekeeperSoul>(1);
                }
            }
        }
    }
}
