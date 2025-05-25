using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework.Graphics;
using gunrightsmod.Content.Items;
using ssm.Core;
using ssm.Content.SoulToggles;

namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class FaradayEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TerMerica;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 5;
            Item.value = 609331;
        }

        public override Color nameColor => new(94, 48, 117);

        public class FaradayEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FaradayEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FaradayFedora>();
            recipe.AddIngredient<FaradayBodyArmor>();
            recipe.AddIngredient<FaradayPants>();
            recipe.AddIngredient<PortableTower>();
            recipe.AddIngredient<TheAshesOfCalamity>();
            recipe.AddIngredient<TheMoon>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}