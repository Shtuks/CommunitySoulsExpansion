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
    public class HurricaneForce : BaseForce
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(base.Mod.Name, "RogueEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "ChitinEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "ApostleEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "MarbleChunkEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "AstraliteEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "SeraphEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "RunicEnchant").UpdateAccessory(player, false);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RogueEnchant>(1);
            recipe.AddIngredient<ChitinEnchant>(1);
            recipe.AddIngredient<ApostleEnchant>(1);
            recipe.AddIngredient<MarbleChunkEnchant>(1);
            recipe.AddIngredient<AstraliteEnchant>(1);
            recipe.AddIngredient<SeraphEnchant>(1);
            recipe.AddIngredient<RunicEnchant>(1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
