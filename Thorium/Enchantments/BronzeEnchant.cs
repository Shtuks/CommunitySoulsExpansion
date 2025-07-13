using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Bronze;
using ThoriumMod.Items.ThrownItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Utilities;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles.Enchantments;
using static ssm.Thorium.Enchantments.CyberPunkEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BronzeEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BronzeEffect>(Item);
        }

        public class BronzeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BronzeEnchant>();
            public override bool MutantsPresenceAffects => true;

            private int timer;
            public override void PostUpdate(Player player)
            {
                if (player.wingTime > 0 && player.velocity.Y != 0)
                {
                    timer++;
                    if (timer >= 42)
                    {
                        SpawnSword(player);
                        timer = 0;
                    }
                }
                else
                {
                    timer = 0;
                }
            }
            private void SpawnSword(Player player)
            {
                Vector2 position = new Vector2(
                    player.position.X + Main.rand.Next(-20, 20),
                    player.position.Y + player.height + 10);

                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    position,
                    new Vector2(0, 10), 
                    ModContent.ProjectileType<SwordRainProjectile>(),
                    50, 
                    5f, 
                    player.whoAmI);
            }
        }
        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BronzeHelmet>());
            recipe.AddIngredient(ModContent.ItemType<BronzeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<BronzeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<OlympicTorch>());
            recipe.AddIngredient(ModContent.ItemType<ChampionsRebuttal>());
            recipe.AddIngredient(ModContent.ItemType<SpartanSandles>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
