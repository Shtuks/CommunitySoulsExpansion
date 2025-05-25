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
using ssm.gunrightsmod.Enchantments;
using ssm.Content.SoulToggles;

namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class PurifiedSaltEnchant : BaseEnchant
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
            Item.value = 614398;
        }

        public override Color nameColor => new(94, 48, 117);

        public class PurifiedSaltEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<IdeocracyForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PurifiedSaltEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PurifiedSaltFedora>();
            recipe.AddIngredient<PurifiedSaltChestplate>();
            recipe.AddIngredient<PurifiedSaltLeggings>();
            recipe.AddIngredient<RockSaltEnchant>();
            recipe.AddIngredient<ThePurifier>();
            recipe.AddIngredient<SpiritProtectionCharm>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}