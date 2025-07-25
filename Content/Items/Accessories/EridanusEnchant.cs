using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Patreon.Sasha;
using FargowiltasSouls.Content.Patreon.DevAesthetic;

namespace ssm.Content.Items.Accessories
{
    public class EridanusEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EternityForce;
        }
        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[Type] = true;

        public override Color nameColor => new(100, 40, 130);

        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Purple;
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "UniverseCore").UpdateAccessory(player, true);

            if (player.AddEffect<EridanusEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "EridanusBattleplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "EridanusHat").UpdateArmorSet(player);
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "EridanusLegwear").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<EridanusHat>();
            recipe.AddIngredient<EridanusBattleplate>();
            recipe.AddIngredient<EridanusLegwear>();
            recipe.AddIngredient<UniverseCore>();
            recipe.AddIngredient<MissDrakovisFishingPole>();
            recipe.AddIngredient<DeviousAestheticus>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public class EridanusEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EridanusHat>();
        }
    }
}
