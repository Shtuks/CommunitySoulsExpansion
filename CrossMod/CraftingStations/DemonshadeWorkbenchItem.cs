﻿using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using static Terraria.ModLoader.ModContent;
using Terraria;
using ssm.Core;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeWorkbenchItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(2, 0, 0, 0);
            Item.CloneDefaults(ItemType<ShadowspecBar>());
            Item.createTile = TileType<DemonshadeWorkbenchTile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DraedonsForge>());
            recipe.AddIngredient(ItemType<StaticRefiner>());
            recipe.AddIngredient(ItemType<ProfanedCrucible>());
            recipe.AddIngredient(ItemType<PlagueInfuser>());
            recipe.AddIngredient(ItemType<MonolithAmalgam>());
            recipe.AddIngredient(ItemType<EutrophicShelf>());
            recipe.AddIngredient(ItemType<EffulgentManipulator>());
            recipe.AddIngredient(ItemType<AncientAltar>());
            recipe.AddIngredient(ItemType<AshenAltar>());
            recipe.AddIngredient(ItemType<BotanicPlanter>());
            recipe.AddIngredient(ItemType<VoidCondenser>());
            recipe.AddIngredient(ItemType<WulfrumLabstationItem>());
            recipe.AddIngredient(ItemType<ShadowspecBar>(), 15);
            recipe.Register();
        }
    }
}
