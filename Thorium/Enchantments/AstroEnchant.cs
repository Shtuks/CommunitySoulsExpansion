using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls;
using ssm.Content.Buffs;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles;
using ssm.Content.Projectiles.Enchantments;
using static ssm.Thorium.Enchantments.JesterEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AstroEnchant : BaseEnchant
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
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AstroEffect>(Item);
        }

        public class AstroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstroEnchant>();
            public override bool MinionEffect => true;
            public override void PostUpdateMiscEffects(Player player)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SpaceshipMinion>()] < 1)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<SpaceshipMinion>(),
                        10,
                        0,
                        player.whoAmI
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AstroHelmet>());
            recipe.AddIngredient(ModContent.ItemType<AstroSuit>());
            recipe.AddIngredient(ModContent.ItemType<AstroBoots>());
            recipe.AddIngredient(ModContent.ItemType<MeteorHeadStaff>());
            recipe.AddIngredient(ModContent.ItemType<TechniqueMeteorStomp>());
            recipe.AddIngredient(ModContent.ItemType<MeteoriteClusterBomb>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
