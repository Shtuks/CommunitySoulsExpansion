using Fargowiltas.Items.Tiles;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;

namespace ssm.Thorium.Items
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreamEssence : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<OceanEssence>());
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);

            recipe.AddIngredient<DeathEssence>();
            recipe.AddIngredient<InfernoEssence>();
            recipe.AddIngredient<OceanEssence>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    internal class DreamEssenceReplacement : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.createItem.type == ModContent.ItemType<DreamEssence>())
                    continue;

                for (int j = 0; j < recipe.requiredItem.Count; j++)
                {
                    Item ingredient = recipe.requiredItem[j];

                    if (ingredient.type == ModContent.ItemType<OceanEssence>() && recipe.HasIngredient<DeathEssence>() && recipe.HasIngredient<InfernoEssence>())
                    {
                        int originalStack = ingredient.stack;

                        recipe.RemoveIngredient(ModContent.ItemType<InfernoEssence>());
                        recipe.RemoveIngredient(ModContent.ItemType<DeathEssence>());
                        recipe.requiredItem[j] = new Item();
                        recipe.requiredItem[j].SetDefaults(ModContent.ItemType<DreamEssence>());
                        recipe.requiredItem[j].stack = originalStack;
                    }
                }
            }
        }
    }
}