using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.BossLoot.ScarabeusDrops.ChitinArmor;
using SpiritMod.Items.BossLoot.ScarabeusDrops.RadiantCane;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.ScarabeusDrops.LocustCrook;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class ChitinEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(141, 163, 239);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<ChitinTornadoDash>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ChitinHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ChitinChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ChitinLeggings").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ChitinHelmet>(1);
            recipe.AddIngredient<ChitinChestplate>(1);
            recipe.AddIngredient<ChitinLeggings>(1);
            recipe.AddIngredient<LocustCrook>(1);
            recipe.AddIngredient<RadiantCane>(1);
            recipe.AddIngredient<DesertSlab>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class ChitinTornadoDash : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChitinEnchant>();
        }
    }
}