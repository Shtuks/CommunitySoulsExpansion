using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ssm.Consolaria.Enchantments;


namespace ssm.Consolaria.Forces
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class HeroForce : BaseForce
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(base.Mod.Name, "OstaraEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "DragonEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "TitanEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "PhantasmalEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(base.Mod.Name, "WarlockEnchant").UpdateAccessory(player, false);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OstaraEnchant>(1);
            recipe.AddIngredient<DragonEnchant>(1);
            recipe.AddIngredient<TitanEnchant>(1);
            recipe.AddIngredient<PhantasmalEnchant>(1);
            recipe.AddIngredient<WarlockEnchant>(1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
