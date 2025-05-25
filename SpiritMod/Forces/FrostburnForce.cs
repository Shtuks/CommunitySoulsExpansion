using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ssm.SpiritMod.Enchantments;
using Fargowiltas.Items.Tiles;

namespace ssm.SpiritMod.Forces
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class FrostburnForce : BaseForce
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(base.Mod.Name, "BloodcourtEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "CryoliteEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "DuskEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "FrigidEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "MarksmanEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "PainMongerEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "SlagTyrantEnchant").UpdateAccessory(player, false);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BloodcourtEnchant>(1);
            recipe.AddIngredient<CryoliteEnchant>(1);
            recipe.AddIngredient<DuskEnchant>(1);
            recipe.AddIngredient<FrigidEnchant>(1);
            recipe.AddIngredient<MarksmanEnchant>(1);
            recipe.AddIngredient<PainMongerEnchant>(1);
            recipe.AddIngredient<SlagTyrantEnchant>(1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
