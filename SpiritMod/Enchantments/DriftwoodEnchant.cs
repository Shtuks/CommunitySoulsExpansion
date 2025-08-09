using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using SpiritMod.Items.Sets.FloatingItems.Driftwood.DriftwoodArmor;
using SpiritMod.Items.Consumable;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Placeable;
using SpiritMod.Items.Consumable.Fish;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class DriftwoodEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(186, 154, 114);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneBeach)
            {
                player.fishingSkill += 5;
                player.cratePotion = true;
                player.statDefense += 3;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DriftwoodHelmet>(1);
            recipe.AddIngredient<DriftwoodChestplate>(1);
            recipe.AddIngredient<DriftwoodLeggings>(1);
            recipe.AddIngredient<FishChips>(1);
            recipe.AddIngredient<CrinoidItem>(1);
            recipe.AddIngredient<FishCrate>(2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
