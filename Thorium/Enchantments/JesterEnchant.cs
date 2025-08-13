using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossQueenJellyfish;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.GraniteEnchant;
using FargowiltasSouls.Core.Toggler.Content;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles;


namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class JesterEnchant : BaseEnchant
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<JesterEffect>(Item);
        }
        public class JesterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<JesterEnchant>();
            public override bool MinionEffect => true;
            public override void PostUpdateMiscEffects(Player player)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MinionBellProj>()] < 1)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<MinionBellProj>(),
                        0,
                        0,
                        player.whoAmI
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:AnyJesterMask");
            recipe.AddRecipeGroup("ssm:AnyJesterShirt");
            recipe.AddRecipeGroup("ssm:AnyJesterLeggings");
            recipe.AddRecipeGroup("ssm:AnyLetter");
            recipe.AddRecipeGroup("ssm:AnyTambourine");
            recipe.AddIngredient(ModContent.ItemType<SkywareLute>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
