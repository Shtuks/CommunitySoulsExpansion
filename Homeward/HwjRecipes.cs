using ContinentOfJourney.Items.Material;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ContinentOfJourney.Items.Accessories;
using Terraria.ID;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using Fargowiltas.Items.Tiles;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name)]
    public class HwjRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<Horizon>())
                .AddIngredient<AeolusBoots>()
                .AddIngredient<FinalBar>()
                .AddIngredient<TankOfThePastJungle>(4)
                .AddTile<CrucibleCosmosSheet>()
                .Register();
        }
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

                if (recipe.createItem.ModItem is BaseForce)
                {
                    if (!recipe.HasIngredient<SolarFlareScoria>())
                        recipe.AddIngredient<SolarFlareScoria>(4);
                }

                //nuke 3897430 recipes of that item
                if (recipe.HasResult<Horizon>() && !recipe.HasTile<CrucibleCosmosSheet>())
                {
                    recipe.DisableRecipe();
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
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    //horizon to supersonic
                    if (recipe.HasResult(ModContent.ItemType<SupersonicSoul>()) && recipe.HasIngredient(ModContent.ItemType<Horizon>()))
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                        recipe.AddIngredient<Horizon>(1);
                    }
                }
            }
        }
    }
}
