using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using SacredTools.Content.Items.Materials;
using ssm.Calamity.Souls;
using ssm.SoA.Souls;
using CalamityMod.Items.Materials;
using SacredTools.Content.Items.Weapons.Relic;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Placeable.Obelisks;
using CalamityMod.Items;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<CalamitySoul>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
                if (recipe.HasIngredient<Rock>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(3);
                }
                if (recipe.HasResult<SoASoul>() && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                    recipe.AddIngredient<MiracleMatter>();
                }
                if ((recipe.HasResult<NihilusObelisk>() || 
                    recipe.HasResult<PaleRuin>() ||
                    recipe.HasResult<AshenWake>() ||
                    recipe.HasResult<CeruleanCyclone>() ||
                    recipe.HasResult<Malevolence>() ||
                    recipe.HasResult<NightTerror>() ||
                    recipe.HasResult<RogueWave>() ||
                    recipe.HasResult<Sharpshooter>() ||
                    recipe.HasResult<SwordOfGreed>()) && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(1);
                }
                if ((recipe.HasResult<AsthraltiteHelmetRevenant>() ||
                    recipe.HasResult<AsthralRanged>() ||
                    recipe.HasResult<AsthralMelee>() ||
                    recipe.HasResult<AsthralChest>() ||
                    recipe.HasResult<AsthralMage>() ||
                    recipe.HasResult<AsthralLegs>() ||
                    recipe.HasResult<AsthralSummon>()) && !recipe.HasIngredient<AuricBar>())
                {
                    recipe.AddIngredient<AuricBar>(1);
                }

                if (recipe.HasResult<GalacticaSingularity>() && !recipe.HasIngredient<FragmentQuasar>())
                {
                    recipe.AddIngredient<FragmentQuasar>(1);
                }

                if (recipe.HasResult<AscendantSpiritEssence>() && !recipe.HasIngredient<FragmentBlight>())
                {
                    recipe.AddIngredient<FragmentBlight>(3);
                    recipe.AddIngredient<FragmentHatred>(3);
                }

                //Waiting for erazor rework
                //if (recipe.HasResult<ShadowspecBar>() && !recipe.HasIngredient<IDontExist>())
                //{
                //    recipe.AddIngredient<IDontExist>(5);
                //}
            }
        }
    }
}
