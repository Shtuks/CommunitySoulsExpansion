using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using Microsoft.Xna.Framework.Graphics;
using gunrightsmod;
using gunrightsmod.Content.Items;
using ssm.Content.SoulToggles;

namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class AstatineEnchant : BaseEnchant
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
            Item.rare = 11;
            Item.value = 3135864;
        }

        public override Color nameColor => new(94, 48, 117);

        public class AstatineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstatineEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<AstatineHelmet>();
            recipe.AddIngredient<AstatineBreastplate>();
            recipe.AddIngredient<AstatineGreaves>();
            recipe.AddIngredient<ATFsNightmare>();
            recipe.AddIngredient<PlasmaRifle3>();
            recipe.AddIngredient<FissileDart>(3396);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}


