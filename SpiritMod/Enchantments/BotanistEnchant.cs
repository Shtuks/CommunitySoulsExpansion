using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using SpiritMod.Items.Armor.BotanistSet;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.BriarChestLoot;
using SpiritMod.Items.Placeable.Furniture;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class BotanistEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(206, 182, 95);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.SpiritMod.Name, "ReachBrooch").UpdateAccessory(player, false);
            if (AccessoryEffectLoader.AddEffect<BotanistHerbEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BotanistHat").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BotanistBody").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BotanistLegs").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BotanistHat>(1);
            recipe.AddIngredient<BotanistBody>(1);
            recipe.AddIngredient<BotanistLegs>(1);
            recipe.AddIngredient<ReachBrooch>(1);
            recipe.AddIngredient<ForagerTableItem>(1);
            recipe.AddIngredient<SunPot>(5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class BotanistHerbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BotanistEnchant>();
        }
    }
}
