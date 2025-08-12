using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.CoralEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DepthDiverEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DepthDiverEffect>(Item);
            player.AddEffect<CoralEffect>(Item);
        }

        public class DepthDiverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DepthDiverEnchant>();

            public override void PostUpdateMiscEffects(Player player)
            {
                if (player.wet)
                {
                    float depthFactor = CalculateDepthFactor(player);

                    player.lifeRegen += (int)(1 + 1.5f * depthFactor);
                    player.GetDamage(DamageClass.Generic) += 0.02f + 0.08f * depthFactor; 
                    player.statDefense += (int)(2 + 8 * depthFactor); 
                }
            }

            private float CalculateDepthFactor(Player player)
            {
                float spaceHeight = (float)(Main.worldSurface * 0.35f * 16);

                float underworldHeight = (Main.maxTilesY - 200) * 16;

                float playerY = player.Center.Y;

                float depthFactor = (playerY - spaceHeight) / (underworldHeight - spaceHeight);

                return MathHelper.Clamp(depthFactor, 0f, 1f);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DepthDiverHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CoralEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DrownedDoubloon>());
            recipe.AddIngredient(ModContent.ItemType<MagicConch>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
