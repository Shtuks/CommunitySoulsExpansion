using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.CryoliteSet.CryoliteArmor;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Armor.Masks;
using SpiritMod.Items.Accessory.AceCardsSet;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class CryoliteEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(134, 245, 251);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.SpiritMod.Name, "WinterHat").UpdateAccessory(player, false);
            if (AccessoryEffectLoader.AddEffect<CryoliteCard>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FourOfAKind").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<CryoliteAura>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CryoliteHead").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CryoliteBody").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CryoliteLegs").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CryoliteHead>(1);
            recipe.AddIngredient<CryoliteBody>(1);
            recipe.AddIngredient<CryoliteLegs>(1);
            recipe.AddIngredient<SlowCodex>(1);
            recipe.AddIngredient<WinterHat>(1);
            recipe.AddIngredient<FourOfAKind>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class CryoliteAura : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryoliteEnchant>();
        }
        public class CryoliteCard : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryoliteEnchant>();
        }
    }
}
