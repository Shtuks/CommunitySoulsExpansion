using ContinentOfJourney.Items.Material;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ContinentOfJourney.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using Terraria.ID;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name)]
    public class HwjRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>()) && !recipe.HasIngredient<EssenceofBright>())
                    {
                        recipe.AddIngredient<EssenceofBright>(5);
                    }
                }
                if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasIngredient<Altitude>())
                {
                    recipe.RemoveIngredient(1131);
                    recipe.RemoveIngredient(1871);
                    recipe.RemoveIngredient(822);
                    recipe.RemoveIngredient(821);
                    recipe.AddIngredient<Altitude>();
                    recipe.AddIngredient<FinalBar>(5);
                }
                if ((recipe.HasResult<ColossusSoul>() ||
                    recipe.HasResult<ArchWizardsSoul>() ||
                    recipe.HasResult<BerserkerSoul>() ||
                    recipe.HasResult<SnipersSoul>() ||
                    recipe.HasResult<ConjuristsSoul>()
                    ) && !recipe.HasIngredient<FinalBar>())
                {
                    recipe.AddIngredient<FinalBar>(5);
                }
                if (recipe.HasResult(ItemID.Zenith) && recipe.HasIngredient<EssenceofBright>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<EssenceofBright>());
                }
            }
        }
    }
}
