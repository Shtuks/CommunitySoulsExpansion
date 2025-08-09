using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    //bruh
    [ExtendsFromMod(ModCompatibility.IEoR.Name)]
    [JITWhenModsEnabled(ModCompatibility.IEoR.Name)]
    public class IEoRRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (ModCompatibility.SacredTools.Loaded)
                {
                    if ((recipe.HasResult(ModCompatibility.IEoR.Mod.Find<ModItem>("Swordofthe14thGlitch")) ||
                        recipe.HasResult(ModCompatibility.IEoR.Mod.Find<ModItem>("NovaBomb")) ||
                        recipe.HasResult(ModLoader.GetMod("InfernumMode").Find<ModItem>("Kevin"))
                        ) && !recipe.HasIngredient(ModContent.Find<ModItem>("EmberofOmen")))
                    {
                        recipe.AddIngredient(ModContent.Find<ModItem>("EmberofOmen"), 3);
                    }
                }
            }
        }
    }
}
