using Clamity.Content.Bosses.Clamitas.Crafted;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Clamity.Name)]
    public class ClamRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult(ModContent.ItemType<TrawlerSoul>()) && !recipe.HasIngredient<PearlOfFishCalamity>())
                {
                    recipe.AddIngredient<PearlOfFishCalamity>();
                }
            }
        }
    }
}
