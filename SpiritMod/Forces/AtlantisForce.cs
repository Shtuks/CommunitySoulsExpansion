using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using ssm.SpiritMod.Enchantments;


namespace ssm.SpiritMod.Forces
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class AtlantisForce : BaseForce
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(base.Mod.Name, "BismiteEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "CascadeEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "GraniteChunkEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "StreamSurferEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "SpiritEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "PrimalstoneEnchant").UpdateAccessory(player, false);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismiteEnchant>(1);
            recipe.AddIngredient<CascadeEnchant>(1);
            recipe.AddIngredient<GraniteChunkEnchant>(1);
            recipe.AddIngredient<StreamSurferEnchant>(1);
            recipe.AddIngredient<SpiritEnchant>(1);
            recipe.AddIngredient<PrimalstoneEnchant>(1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
