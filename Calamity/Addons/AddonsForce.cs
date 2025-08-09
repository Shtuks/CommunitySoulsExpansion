using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Fargowiltas.Items.Tiles;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AddonsForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModCompatibility.Goozma.Loaded || ModCompatibility.Clamity.Loaded || ModCompatibility.Catalyst.Loaded;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModCompatibility.Clamity.Loaded)
            {
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "ClamitasEnchant").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FrozenEnchant").UpdateAccessory(player, hideVisual);
            }

            if (ModCompatibility.Catalyst.Loaded)
            {
                //ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AuricPlusEnchant").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "IntergelacticEnchant").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "ShogunEnchant").UpdateAccessory(player, false);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            if (ModCompatibility.Clamity.Loaded)
            {
                recipe.AddIngredient(null, "FrozenEnchant");
                recipe.AddIngredient(null, "ClamitasEnchant");
            }

            if (ModCompatibility.Catalyst.Loaded)
            {
                recipe.AddIngredient(null, "IntergelacticEnchant");
                //recipe.AddIngredient(null, "AuricPlusEnchant");
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                recipe.AddIngredient(null, "ShogunEnchant");
            }

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
