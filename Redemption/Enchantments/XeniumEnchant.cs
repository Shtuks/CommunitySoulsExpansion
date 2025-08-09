using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Armor.PostML.Xenium;
using Redemption.Items.Weapons.PostML.Summon;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XeniumEnchant : BaseEnchant
    {
        public class XeniumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XeniumEnchant>();
            public override bool MinionEffect => true;
            public override void PostUpdateMiscEffects(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<XeniumTurret>()] < 1)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            Vector2.Zero,
                            ModContent.ProjectileType<XeniumTurret>(),
                            0,
                            0f,
                            player.whoAmI
                        );
                    }
                }
            }
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Redemption;
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

        public override Color nameColor => new Color(143, 227, 84);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<XeniumEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<XeniumVisor>();
            recipe.AddIngredient<XeniumBreastplate>();
            recipe.AddIngredient<XeniumLeggings>();
            recipe.AddIngredient<XeniumDrone>();
            recipe.AddIngredient<InfectionShield>();
            recipe.AddIngredient<BeelzebubConcoction>();
            recipe.AddTile(412);

            recipe.Register();
        }
    }
}
