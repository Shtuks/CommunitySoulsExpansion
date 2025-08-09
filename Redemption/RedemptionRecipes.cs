using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.HM;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using Terraria.ID;
using Terraria.Localization;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Armor.PreHM.CommonGuard;
using SacredTools.Content.Items.Materials;
using ssm.Content.Items.Armor;
using Redemption.Items.Materials.PreHM;
using ssm.CrossMod.CraftingStations;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Common Guard Helmet", ModContent.ItemType<CommonGuardHelm2>(), ModContent.ItemType<CommonGuardHelm1>());
            RecipeGroup.RegisterGroup("ssm:CommonGuardHelms", rec);
            RecipeGroup rec1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardlight Helmet", ModContent.ItemType<HardlightCasque>(), ModContent.ItemType<HardlightCowl>(), ModContent.ItemType<HardlightHelm>(), ModContent.ItemType<HardlightHood>(), ModContent.ItemType<HardlightVisor>());
            RecipeGroup.RegisterGroup("ssm:HardlightHelms", rec1);
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<PureIronAlloy>())
                .AddIngredient<DragonLeadAlloy>()
                .AddTile<RedemptionCraftingStationTile>()
                .Register();
            Recipe.Create(ModContent.ItemType<DragonLeadAlloy>())
                .AddIngredient<PureIronAlloy>()
                .AddTile<RedemptionCraftingStationTile>()
                .Register();
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.ModItem is BaseForce)
                {
                    if (!recipe.HasIngredient<RoboBrain>())
                        recipe.AddIngredient<RoboBrain>();
                }

                //if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasIngredient<NebWings>())
                //{
                //    //recipe.AddIngredient<NebWings>();
                //    recipe.AddIngredient<XenomiteJetpack>();
                //}

                //if (recipe.HasResult<ColossusSoul>() && !recipe.HasIngredient<HEVSuit>())
                //{
                //    recipe.AddIngredient<HEVSuit>();
                //}

                //if (recipe.HasResult<SupersonicSoul>() && !recipe.HasIngredient<InfectionShield>())
                //{
                //    recipe.RemoveIngredient(ItemID.EoCShield);
                //    recipe.AddIngredient<InfectionShield>();
                //}

                if (recipe.HasResult(ItemID.Zenith) && recipe.HasIngredient<LifeFragment>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<LifeFragment>());
                }

                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>()) && !recipe.HasIngredient<LifeFragment>())
                    {
                        recipe.AddIngredient<LifeFragment>(5);
                    }
                }

                //emblem -> essence -> mutagen/post-dog acc ->
                //                                                  soul     
                //                     hwj emblem -> sigil ->
                if (recipe.HasResult<MutagenMagic>() && !recipe.HasResult<ApprenticesEssence>())
                {
                    recipe.AddIngredient<ApprenticesEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }
                if (recipe.HasResult<MutagenMelee>() && !recipe.HasResult<BarbariansEssence>())
                {
                    recipe.AddIngredient<BarbariansEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }
                if (recipe.HasResult<MutagenSummon>() && !recipe.HasResult<OccultistsEssence>())
                {
                    recipe.AddIngredient<OccultistsEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }
                if (recipe.HasResult<MutagenRanged>() && !recipe.HasResult<SharpshootersEssence>())
                {
                    recipe.AddIngredient<SharpshootersEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }

                //if (recipe.HasResult<ArchWizardsSoul>() && !recipe.HasResult<MutagenMagic>())
                //{
                //    recipe.RemoveIngredient(ModContent.ItemType<ApprenticesEssence>());
                //    recipe.AddIngredient<MutagenMagic>();
                //}
                //if (recipe.HasResult<BerserkerSoul>() && !recipe.HasResult<MutagenMelee>())
                //{
                //    recipe.AddIngredient<MutagenMelee>();
                //    recipe.RemoveIngredient(ModContent.ItemType<BarbariansEssence>());
                //}
                //if (recipe.HasResult<ConjuristsSoul>() && !recipe.HasResult<MutagenSummon>())
                //{
                //    recipe.AddIngredient<MutagenSummon>();
                //    recipe.RemoveIngredient(ModContent.ItemType<OccultistsEssence>());
                //}
                //if (recipe.HasResult<SnipersSoul>() && !recipe.HasResult<MutagenRanged>())
                //{
                //    recipe.AddIngredient<MutagenRanged>();
                //    recipe.RemoveIngredient(ModContent.ItemType<SharpshootersEssence>());
                //}
            }
        }
    }
}