using CalamityMod.Items.Materials;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using NoxusBoss.Content.Items;
using ssm.Calamity.Souls;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity
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
                if (recipe.HasResult(ModContent.ItemType<MasochistSoul>()) && recipe.HasIngredient<MetallicChunk>())
                {
                    recipe.AddIngredient<MetallicChunk>();
                }
            }
        }
    }
}
