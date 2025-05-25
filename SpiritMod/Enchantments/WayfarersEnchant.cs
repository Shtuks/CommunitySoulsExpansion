using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Armor.WayfarerSet;
using SpiritMod.Items.Sets.ToolsMisc.BrilliantHarvester;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class WayfarersEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(150, 105, 97);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<WayfarerTreads>(player, base.Item))
            {
                player.GetModPlayer<MyPlayer>().explorerTreads = true;
            }
            if (AccessoryEffectLoader.AddEffect<WayfarerBand>(player, base.Item))
            {
                player.GetSpiritPlayer().metalBand = true;
            }
            if (AccessoryEffectLoader.AddEffect<WayfarerArmorBuff>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "WayfarerHead").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "WayfarerBody").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "WayfarerLegs").UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<WayfarerHead>(1);
            recipe.AddIngredient<WayfarerBody>(1);
            recipe.AddIngredient<WayfarerLegs>(1);
            recipe.AddIngredient<GemPickaxe>(1);
            recipe.AddIngredient<MetalBand>(1);
            recipe.AddIngredient<ExplorerTreads>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public class WayfarerTreads : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WayfarersEnchant>();
        }
        public class WayfarerBand : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WayfarersEnchant>();
        }
        public class WayfarerArmorBuff : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WayfarersEnchant>();
        }
    }
}
