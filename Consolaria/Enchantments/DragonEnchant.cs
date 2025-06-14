using Consolaria.Content.Items.Armor.Melee;
using Consolaria.Content.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class DragonEnchant2 : BaseEnchant
    {
        public override Color nameColor => new Color(151, 191, 241);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<DragonBurst>(player, base.Item))
            {
                player.GetModPlayer<DragonPlayer>().dragonBurst = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DragonMask>(1);
            recipe.AddIngredient<DragonBreastplate>(1);
            recipe.AddIngredient<DragonGreaves>(1);
            recipe.AddIngredient<AncientDragonEnchant>(1);
            recipe.AddIngredient<Tonbogiri>(1);
            recipe.AddIngredient<Tizona>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
        public class DragonBurst : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<DragonEnchant2>();
        }
    }
}
