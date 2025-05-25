using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Armor;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Weapon.Thrown;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class RogueEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(195, 154, 92);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<RogueArmorEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RogueHood").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RoguePlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RoguePants").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<RogueCrestEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RogueCrest").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<RogueRuneEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "SwiftRune").UpdateAccessory(player, false);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RogueHood>(1);
            recipe.AddIngredient<RoguePlate>(1);
            recipe.AddIngredient<RoguePants>(1);
            recipe.AddIngredient<Kunai_Throwing>(50);
            recipe.AddIngredient<RogueCrest>(1);
            recipe.AddIngredient<SwiftRune>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class RogueCrestEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RogueEnchant>();
        }
        public class RogueRuneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RogueEnchant>();
        }
        public class RogueArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RogueEnchant>();
        }
    }
}
