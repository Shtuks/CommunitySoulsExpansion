using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Consumables;
using ssm.CrossMod.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Materials
{
    internal class tModLoadiumReplacement : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.createItem.type == ModContent.ItemType<MutantsForgeItem>() ||
                    recipe.createItem.type == ModContent.ItemType<tModLoadiumBar>() ||
                    recipe.createItem.type == ModContent.ItemType<UltimateHealingPotion>())
                    continue;

                bool hasEternal = false;

                for (int j = 0; j < recipe.requiredItem.Count; j++)
                {
                    if (recipe.requiredItem[j].type == ModContent.ItemType<EternalEnergy>())
                    {
                        hasEternal = true;
                        break;
                    }
                }

                if (!hasEternal)
                    continue;

                recipe.requiredItem.RemoveAll(ingredient =>
                    ingredient.type == ModContent.ItemType<DeviatingEnergy>() ||
                    ingredient.type == ModContent.ItemType<AbomEnergy>()
                );

                for (int j = 0; j < recipe.requiredItem.Count; j++)
                {
                    if (recipe.requiredItem[j].type == ModContent.ItemType<EternalEnergy>())
                    {
                        int stack = recipe.requiredItem[j].stack;
                        recipe.requiredItem[j] = new Item();
                        recipe.requiredItem[j].SetDefaults(ModContent.ItemType<tModLoadiumBar>());
                        recipe.requiredItem[j].stack = stack;
                    }
                }
            }
        }
    }
}