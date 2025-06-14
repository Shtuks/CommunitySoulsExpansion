using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Consolaria.Content.Items.Accessories;
using Consolaria.Content.Items.Weapons.Ranged;
using Consolaria.Content.Items.Consumables;
using Consolaria.Content.Items.Armor.Misc;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using ssm.Content.SoulToggles;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class OstaraEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(148, 214, 107);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<OstaraJump>(player, base.Item))
            {
                player.GetModPlayer<OstarasPlayer>().bunnyHop = true;
            }
            if (AccessoryEffectLoader.AddEffect<OstaraGift>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.Consolaria.Name, "OstarasGift").UpdateAccessory(player, false);
            }
            player.noFallDmg = true;
            player.moveSpeed += 0.03f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OstaraHat>(1);
            recipe.AddIngredient<OstaraJacket>(1);
            recipe.AddIngredient<OstaraBoots>(1);
            recipe.AddIngredient<OstarasGift>(1);
            recipe.AddIngredient<EggCannon>(1);
            recipe.AddIngredient<CandiedFruit>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
        public class OstaraJump : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<OstaraEnchant>();
        }
        public class OstaraGift : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<OstaraEnchant>();
        }
    }
}
