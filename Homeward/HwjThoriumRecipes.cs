﻿using ssm.Thorium.Souls;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ContinentOfJourney.Items.Material;
using ThoriumMod.Items.Terrarium;
using ContinentOfJourney.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Masomode;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Homeward.Name)]
    public class HwjThoriumRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<FinalBar>() && !recipe.HasIngredient<TerrariumCore>())
                {
                    recipe.DisableRecipe();
                }

                if ((recipe.HasResult<BardSoul>() ||
                    recipe.HasResult<GuardianAngelsSoul>()
                    ) && !recipe.HasIngredient<FinalBar>())
                {
                    recipe.AddIngredient<FinalBar>(5);
                }

                if (recipe.HasResult<Horizon>() && !recipe.HasIngredient<TerrariumParticleSprinters>() && !ModCompatibility.Calamity.Loaded)
                {
                    if (recipe.HasIngredient<AeolusBoots>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                    }
                    recipe.AddIngredient<TerrariumParticleSprinters>();
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<FinalBar>())
                .AddIngredient<TerrariumCore>()
                .AddIngredient<FinalOre>(7)
                .AddIngredient<EternalBar>()
                .AddIngredient<LivingBar>()
                .AddIngredient<CubistBar>()
                .Register();
        }
    }
}
