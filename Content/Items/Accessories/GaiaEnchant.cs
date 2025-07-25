using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Weapons.Challengers;

namespace ssm.Content.Items.Accessories
{
    public class GaiaEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EternityForce;
        }

        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[Type] = true;

        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        /*public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
          if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
            return true;
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
          GameShaders.Armor.GetShaderFromItemId(2869).Apply((Entity) this.Item, new DrawData?());
          Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
          return false;
        }*/

        public override Color nameColor => new(0, 255, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<GaiaEffect>(Item))
            {
                ModContent.Find<ModItem>(FargoSoul.Name, "GaiaHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(FargoSoul.Name, "GaiaPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(FargoSoul.Name, "GaiaGreaves").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<GaiaHelmet>();
            recipe.AddIngredient<GaiaPlate>();
            recipe.AddIngredient<GaiaGreaves>();
            recipe.AddIngredient<DecrepitAirstrikeRemote>();
            recipe.AddIngredient<Lightslinger>();
            recipe.AddIngredient<EgyptianFlail>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class GaiaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GaiaHelmet>();
        }
    }
}
