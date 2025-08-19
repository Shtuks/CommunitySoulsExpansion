using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using SacredTools.Content.Items.Accessories;
using Terraria.Localization;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Dragon;
using ssm.SoA.Souls;
using ssm.Core;
using SacredTools.Content.Items.Accessories.Wings;
using SacredTools.Content.Items.DEV;
using SacredTools.Content.Items.Placeable.CraftingStations;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using SacredTools.Content.Items.Materials;
using Terraria.ID;
using SacredTools.Content.Items.Armor.Oblivion;
using SacredTools.Content.Items.Weapons.Relic;
using FargowiltasSouls.Content.Items.Materials;
using SacredTools.Content.Items.Accessories.Sigils;
using ssm.Content.Items.DevItems;
using ssm.Content.Items.Materials;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoARecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Asthral Helmet", ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthraltiteHelmetRevenant>());
            RecipeGroup.RegisterGroup("ssm:AsthralHelms", rec);
            RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Flarium Helmet", ModContent.ItemType<FlariumCrown>(), ModContent.ItemType<FlariumMask>(), ModContent.ItemType<FlariumCowl>());
            RecipeGroup.RegisterGroup("ssm:FlariumHelms", rec2);
            RecipeGroup rec3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Void Warden Chestplate", ModContent.ItemType<VoidChest>(), ModContent.ItemType<VoidChestOffense>());
            RecipeGroup.RegisterGroup("ssm:VoidWardenChest", rec3);
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<OblivionForge>(), 1).AddIngredient<BetaCoupon>(2).Register();
            Recipe.Create(ModContent.ItemType<RageSuppressor>(), 1).AddIngredient<BetaCoupon>(2).Register();
            Recipe.Create(ModContent.ItemType<MilinticaDash>(), 1).AddIngredient<BetaCoupon>(2).Register();
            Recipe.Create(ModContent.ItemType<HeartOfThePlough>(), 1).AddIngredient<BetaCoupon>(2).Register();
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<StalkerSoul>())
                    {
                        recipe.AddIngredient<StalkerSoul>();
                    }
                }

                if (recipe.HasResult<CosmoForce>() && !recipe.HasIngredient<LuminousEnergy>())
                {
                    recipe.AddIngredient<LuminousEnergy>(5);
                }

                if ((recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>()) && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
                
                //if (CSEConfig.Instance.DevItems)
                //{
                //    if (recipe.HasResult<Catlight>() && !recipe.HasIngredient<EmberOfOmen>())
                //    {
                //        recipe.AddIngredient<EmberOfOmen>(5);
                //    }
                //}

                //if (CSEConfig.Instance.AlternativeSiblings)
                //{
                //    if (recipe.HasResult<tModLoadiumBar>() && !recipe.HasIngredient<EmberOfOmen>())
                //    {
                //        recipe.AddIngredient<EmberOfOmen>();
                //    }
                //}

                //if (/*!CSEConfig.Instance.ExperimentalContent && */recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<SoASoul>())
                //{
                //    recipe.AddIngredient<SoASoul>();
                //}

                //if (recipe.HasResult<BerserkerSoul>() && recipe.HasIngredient(ItemID.CelestialShell))
                //{
                //    recipe.RemoveIngredient(ItemID.CelestialShell);
                //}

                //if (recipe.HasResult<ConjuristsSoul>() && !recipe.HasIngredient<StarstreamVeil>())
                //{
                //    recipe.AddIngredient<StarstreamVeil>();
                //    recipe.RemoveIngredient(3812);
                //    recipe.RemoveIngredient(3810);
                //    recipe.RemoveIngredient(3811);
                //    recipe.RemoveIngredient(3809);
                //}

                //if (recipe.HasResult<ColossusSoul>() && !recipe.HasIngredient<RoyalGuard>())
                //{
                //    recipe.AddIngredient<RoyalGuard>();
                //    recipe.AddIngredient<NightmareBlindfold>();
                //}

                //if (recipe.HasResult<SupersonicSoul>() && !recipe.HasIngredient<MilinticaDash>())
                //{
                //    recipe.AddIngredient<MilinticaDash>();
                //    recipe.AddIngredient<HeartOfThePlough>();
                //}

                //if (recipe.HasResult<WorldShaperSoul>() && !recipe.HasIngredient<LunarRing>())
                //{
                //    recipe.AddIngredient<LunarRing>();
                //    recipe.AddIngredient<RageSuppressor>();
                //}

                //if (recipe.HasResult<BerserkerSoul>() && !recipe.HasIngredient<SolarSigil>())
                //{
                //    recipe.AddIngredient<SolarSigil>();
                //}
                //if (recipe.HasResult<ArchWizardsSoul>() && !recipe.HasIngredient<NebulaSigil>())
                //{
                //    recipe.AddIngredient<NebulaSigil>();
                //}
                //if (recipe.HasResult<SnipersSoul>() && !recipe.HasIngredient<VortexSigil>())
                //{
                //    recipe.AddIngredient<VortexSigil>();
                //}
                //if (recipe.HasResult<ConjuristsSoul>() && !recipe.HasIngredient<StardustSigil>())
                //{
                //    recipe.AddIngredient<StardustSigil>();
                //}

                //if (recipe.HasResult<MasochistSoul>() && !recipe.HasIngredient<YataMirror>())
                //{
                //    recipe.AddIngredient<YataMirror>();
                //    recipe.AddIngredient<PrimordialCore>();
                //}

                //if (recipe.HasResult<BerserkerSoul>() && !recipe.HasIngredient<FloraFist>())
                //{
                //    if (recipe.HasIngredient(1343))
                //    {
                //        recipe.RemoveIngredient(1343);
                //    }
                //    recipe.AddIngredient<FloraFist>();
                //}

                if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasIngredient<GrandWings>())
                {
                    recipe.AddIngredient<GrandWings>();
                    //recipe.AddIngredient<AsthraltiteWings>();
                    recipe.AddIngredient<DespairBoosters>();
                    recipe.AddIngredient<AuroraWings>();
                    recipe.AddIngredient<FlariumWings>();
                }

                if (recipe.createItem.ModItem is BaseForce)
                {
                    if (!recipe.HasIngredient<TraceOfChaos>())
                        recipe.AddIngredient<TraceOfChaos>(4);
                }

                if ((recipe.HasResult<PaleRuin>() ||
                        recipe.HasResult<AshenWake>() ||
                        recipe.HasResult<CeruleanCyclone>() ||
                        recipe.HasResult<Malevolence>() ||
                        recipe.HasResult<NightTerror>() ||
                        recipe.HasResult<RogueWave>() ||
                        recipe.HasResult<Sharpshooter>() ||
                        recipe.HasResult<SwordOfGreed>()) && !recipe.HasIngredient<AbomEnergy>())
                {
                    recipe.AddIngredient<AbomEnergy>(5);
                }

                if ((recipe.HasResult<AsthraltiteHelmetRevenant>() ||
                    recipe.HasResult<AsthralRanged>() ||
                    recipe.HasResult<AsthralMelee>() ||
                    recipe.HasResult<AsthralChest>() ||
                    recipe.HasResult<AsthralMage>() ||
                    recipe.HasResult<AsthralLegs>() ||
                    recipe.HasResult<AsthralSummon>()) && !recipe.HasIngredient<AbomEnergy>())
                {
                    recipe.AddIngredient<AbomEnergy>(5);
                }
            }
        }
    }
}
