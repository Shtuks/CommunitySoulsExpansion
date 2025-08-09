using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamityMod.Items.Materials;
using ssm.Core;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items;
using FargowiltasSouls.Content.Items.Summons;
using Terraria.ID;
using ssm.Content.Items.DevItems;
using FargowiltasSouls.Content.Items.Materials;
using System;
using FargowiltasSouls.Core.Systems;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CalRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            //prevent double recipes
            Recipe recipe = ModContent.GetInstance<GalacticaSingularity>().CreateRecipe(1);
            recipe.AddIngredient(ItemID.FragmentNebula);
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.AddIngredient(ItemID.FragmentStardust);
            recipe.AddIngredient(ItemID.FragmentVortex);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.DisableDecraft();
            recipe.Register();
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Auric Helmet", ModContent.ItemType<AuricTeslaHoodedFacemask>(), ModContent.ItemType<AuricTeslaSpaceHelmet>(), ModContent.ItemType<AuricTeslaPlumedHelm>(), ModContent.ItemType<AuricTeslaRoyalHelm>(), ModContent.ItemType<AuricTeslaWireHemmedVisage>());
            RecipeGroup.RegisterGroup("ssm:Auric", rec);
            RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Godslayer Helmet", ModContent.ItemType<GodSlayerHeadMelee>(), ModContent.ItemType<GodSlayerHeadRanged>(), ModContent.ItemType<GodSlayerHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:Godslayer", rec2);
            RecipeGroup rec3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silva Helmet", ModContent.ItemType<SilvaHeadMagic>(), ModContent.ItemType<SilvaHeadSummon>());
            RecipeGroup.RegisterGroup("ssm:Silva", rec3);
            RecipeGroup rec4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tarragon Helmet", ModContent.ItemType<TarragonHeadMagic>(), ModContent.ItemType<TarragonHeadMelee>(), ModContent.ItemType<TarragonHeadRanged>(), ModContent.ItemType<TarragonHeadSummon>(), ModContent.ItemType<TarragonHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:Tarragon", rec4);
            RecipeGroup rec5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Bloodflare Helmet", ModContent.ItemType<BloodflareHeadMagic>(), ModContent.ItemType<BloodflareHeadMelee>(), ModContent.ItemType<BloodflareHeadRanged>(), ModContent.ItemType<BloodflareHeadSummon>(), ModContent.ItemType<BloodflareHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:Bloodflare", rec5);
        }

        public override void PostSetupContent()
        {
            //not related to recipes but who cares
            //Why i added it? Just because i can
            ModCompatibility.Calamity.Mod.Call("CreateCodebreakerDialogOption", Language.GetTextValue("Mods.ssm.CodebreakerOptions.Mutant"), Language.GetTextValue("Mods.ssm.CodebreakerOptions.Mutant"), (Func<bool>)(() => WorldSavingSystem.DownedMutant));
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (!recipe.HasIngredient<ShadowspecBar>() && recipe.HasIngredient<AshesofAnnihilation>())
                {
                    if (recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>())
                    {
                        if (recipe.RemoveIngredient(ModContent.ItemType<AshesofAnnihilation>()) && recipe.RemoveIngredient(ModContent.ItemType<ExoPrism>())) 
                        {
                            recipe.AddIngredient<ShadowspecBar>(5);
                            recipe.AddIngredient<MiracleMatter>();
                        }
                    }
                }
                if (CSEConfig.Instance.DevItems)
                {
                    if (recipe.HasResult<Catlight>() && !recipe.HasIngredient<ShadowspecBar>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<AbomEnergy>());
                        recipe.AddIngredient<Rock>(1);
                        recipe.AddIngredient<ShadowspecBar>(5);
                    }
                }

                //if (recipe.HasResult<ShadowspecBar>() && !recipe.HasResult<MiracleMatter>())
                //{
                //    recipe.RemoveIngredient(ModContent.ItemType<ExoPrism>());
                //    recipe.RemoveIngredient(ModContent.ItemType<AuricBar>());
                //    recipe.AddIngredient<MiracleMatter>();
                //}

                //if (CSEConfig.Instance.ExperimentalContent && !recipe.HasIngredient<Rock>() && recipe.HasResult<MacroverseSoul>())
                //{
                //    recipe.AddIngredient<Rock>(1);
                //}

                if (!recipe.HasIngredient<Rock>() && recipe.HasResult<AbominationnVoodooDoll>())
                {
                    recipe.AddIngredient<Rock>(1);
                }

                if (recipe.HasResult<GalacticaSingularity>() && !recipe.DecraftDisabled)
                {
                    recipe.DisableRecipe();
                }

                if (recipe.HasResult(ItemID.DrillContainmentUnit) && !recipe.HasIngredient<AerialiteBar>())
                {
                    recipe.AddIngredient<LifeAlloy>(20);
                    recipe.AddIngredient<AerialiteBar>(20);
                }
            }
        }
    }
}