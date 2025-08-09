using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Consolaria.Content.Items.Armor.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Consolaria.Content.Items.Weapons.Magic;
using Consolaria.Content.Items.Accessories;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class PhantasmalEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(207, 142, 231);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<PhantasmalAura> (player, base.Item))
            {
                ModContent.Find<ModItem>(this.Consolaria.Name, "PhantasmalHeadgear").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Consolaria.Name, "PhantasmalRobe").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Consolaria.Name, "PhantasmalSubligar").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<PhantasmalJump> (player, base.Item))
            {
                ModContent.Find<ModItem>(this.Consolaria.Name, "ShadowboundExoskeleton").UpdateAccessory(player, false);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PhantasmalHeadgear>(1);
            recipe.AddIngredient<PhantasmalRobe>(1);
            recipe.AddIngredient<PhantasmalSubligar>(1);
            recipe.AddIngredient<AncientPhantasmalEnchant>(1);
            recipe.AddIngredient<RomanCandle>(1);
            recipe.AddIngredient<ShadowboundExoskeleton>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
        public class PhantasmalAura : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhantasmalEnchant>();
        }
        public class PhantasmalJump : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhantasmalEnchant>();
        }
    }
}
