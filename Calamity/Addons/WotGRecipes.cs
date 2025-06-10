using FargowiltasSouls.Content.Items.Accessories.Souls;
using NoxusBoss.Content.Items;
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
                if (recipe.HasResult(ModContent.ItemType<MasochistSoul>()) && !recipe.HasIngredient<MetallicChunk>())
                {
                    recipe.AddIngredient<MetallicChunk>();
                }
            }
        }
    }
}
