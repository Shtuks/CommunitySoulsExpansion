using CalamityMod.Items.Materials;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.CatTech
{
    //I am catteching it
    //I am catteching it so good
    //yeah

    [ExtendsFromMod(ModCompatibility.Crossmod.Name, ModCompatibility.CatTech.Name)]
    [JITWhenModsEnabled(ModCompatibility.Crossmod.Name, ModCompatibility.CatTech.Name)]
    public class CalCatRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if ((recipe.HasResult<VagabondsSoul>()
                    || recipe.HasResult<BerserkerSoul>()
                    || recipe.HasResult<ColossusSoul>()
                    || recipe.HasResult<SnipersSoul>()
                    || recipe.HasResult<ConjuristsSoul>()
                    || recipe.HasResult<ArchWizardsSoul>()) && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                }
            }
        }
    }
}