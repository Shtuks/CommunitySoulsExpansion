using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.CascadeSet.Armor;
using SpiritMod.Items.Sets.ReefhunterSet;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class CascadeEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(136, 113, 92);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<CascadeBubble>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CascadeHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CascadeChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CascadeLeggings").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<CascadePendant>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "PendantOfTheOcean").UpdateAccessory(player, false);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CascadeHelmet>(1);
            recipe.AddIngredient<CascadeChestplate>(1);
            recipe.AddIngredient<CascadeLeggings>(1);
            recipe.AddIngredient<ReefSpear>(1);
            recipe.AddIngredient<UrchinStaff>(1);
            recipe.AddIngredient<PendantOfTheOcean>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class CascadeBubble : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CascadeEnchant>();
        }
        public class CascadePendant : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CascadeEnchant>();
        }
    }
}
