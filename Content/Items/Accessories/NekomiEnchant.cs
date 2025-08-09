using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls;
using FargowiltasSouls.Content.Patreon.Tiger;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using ssm.Content.Projectiles.Minions;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;

namespace ssm.Content.Items.Accessories
{
    public class NekomiEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EternityForce;
        }

        private readonly Mod FargoSoul = ModLoader.GetMod("FargowiltasSouls");

        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[Type] = true;

        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override Color nameColor => new(200, 20, 250);

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (!(((TooltipLine)line).Mod == "Terraria") || !(((TooltipLine)line).Name == "ItemName"))
                return true;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)1, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            GameShaders.Armor.GetShaderFromItemId(3562).Apply((Entity)Item, new DrawData?());
            Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float)line.X, (float)line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(FargoSoul.Name, "SparklingAdoration").UpdateAccessory(player, false);

            player.AddEffect<NekomiEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<NekomiHood>();
            recipe.AddIngredient<NekomiHoodie>();
            recipe.AddIngredient<NekomiLeggings>();
            recipe.AddIngredient<SparklingAdoration>();
            recipe.AddIngredient<TheLightningRod>();
            recipe.AddIngredient<TouhouStaff>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class NekomiEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NekomiHood>();
            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<DevianttSoul>()] < 1)
                        FargoSoulsUtil.NewSummonProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<DevianttSoul>(), 40, 19f, player.whoAmI);
                }
            }
        }
    }
}
