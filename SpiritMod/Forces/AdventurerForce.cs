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
    public class AdventurerForce : BaseForce
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic).Flat += 10f;
            ModContent.Find<ModItem>(base.Mod.Name, "DriftwoodEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "BotanistEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "FloranEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "WayfarersEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "SunflowerEnchant").UpdateAccessory(player, false);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ElderbarkEnchant>(1);
            recipe.AddIngredient<DriftwoodEnchant>(1);
            recipe.AddIngredient<BotanistEnchant>(1);
            recipe.AddIngredient<FloranEnchant>(1);
            recipe.AddIngredient<WayfarersEnchant>(1);
            recipe.AddIngredient<SunflowerEnchant>(1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
