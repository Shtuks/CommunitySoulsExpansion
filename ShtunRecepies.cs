using Terraria;
using Terraria.ModLoader;
using MagicStorage.Items;
using ssm.Core;
using ssm.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls;
using Fargowiltas.Items.Tiles;
using ssm.CrossMod.CraftingStations;
using Terraria.ID;
using Redemption.Items.Materials.PostML;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class Recipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return FargoSoulsUtil.AprilFools && ShtunConfig.Instance.AlternativeSiblings;
        }
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<CreativeStorageUnit>())
                .AddIngredient<TrueLumberjackBody>()
                .AddIngredient<TrueLumberjackMask>()
                .AddIngredient<TrueLumberjackPants>()
                .Register();
        }
    }

    public class ShtunRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (!ModCompatibility.Crossmod.Loaded && ModCompatibility.SacredTools.Loaded && (recipe.HasResult<ArchWizardsSoul>() || recipe.HasResult<BerserkerSoul>() || recipe.HasResult<ConjuristsSoul>() || recipe.HasResult<ColossusSoul>()) && !recipe.HasResult<AbomEnergy>())
                {
                    recipe.AddIngredient<AbomEnergy>(10);
                }
                if ((recipe.HasResult(ModContent.ItemType<Penetrator>()) || recipe.HasResult(ModContent.ItemType<StyxGazer>()) || recipe.HasResult(ModContent.ItemType<SparklingLove>())) && !recipe.HasIngredient(ModContent.ItemType<Sadism>()) && ShtunConfig.Instance.AlternativeSiblings)
                {
                    recipe.AddIngredient<Sadism>(30);
                }
                if (recipe.HasResult(ModContent.ItemType<EternitySoul>()) && recipe.HasTile<CrucibleCosmosSheet>())
                {
                    recipe.RemoveTile(ModContent.TileType<CrucibleCosmosSheet>());
                    recipe.AddTile<MutantsForgeTile>();
                }
                if (ModCompatibility.Calamity.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>()) && recipe.HasIngredient<AbomEnergy>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<AbomEnergy>());
                    }
                }
            }
        }
    }
}