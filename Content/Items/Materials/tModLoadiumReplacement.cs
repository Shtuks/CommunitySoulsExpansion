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

                if (recipe.createItem.type == ModContent.ItemType<MutantsForgeItem>() || recipe.createItem.type == ModContent.ItemType<tModLoadiumBar>() || recipe.createItem.type == ModContent.ItemType<UltimateHealingPotion>())
                    continue;

                for (int j = 0; j < recipe.requiredItem.Count; j++)
                {
                    Item ingredient = recipe.requiredItem[j];

                    if (ingredient.type == ModContent.ItemType<EternalEnergy>())
                    {
                        int originalStack = ingredient.stack;

                        if (recipe.HasIngredient<DeviatingEnergy>()) {
                            recipe.RemoveIngredient(ModContent.ItemType<DeviatingEnergy>());}
                        if (recipe.HasIngredient<AbomEnergy>()){
                            recipe.RemoveIngredient(ModContent.ItemType<AbomEnergy>());}

                        recipe.requiredItem[j] = new Item();
                        recipe.requiredItem[j].SetDefaults(ModContent.ItemType<tModLoadiumBar>());
                        recipe.requiredItem[j].stack = originalStack;
                    }
                }
            }
        }
    }
}
