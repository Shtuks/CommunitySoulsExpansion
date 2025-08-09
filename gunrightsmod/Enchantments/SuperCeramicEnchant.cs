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
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class SuperCeramicEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 5;
            Item.value = 269977;
        }

        public override Color nameColor => new(94, 48, 117);

        public class SuperCeramicEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<IdeocracyForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SuperCeramicEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<SuperCeramicFedora>();
            recipe.AddIngredient<SuperCeramicChestplate>();
            recipe.AddIngredient<SuperCeramicLeggings>();
            recipe.AddIngredient<CeramicMachete>();
            recipe.AddIngredient<CopperShortmachinegun>();
            recipe.AddIngredient<CeramicHorseshoeBalloon>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}