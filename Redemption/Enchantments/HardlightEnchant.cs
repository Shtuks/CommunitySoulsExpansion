using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using Redemption.Items.Armor.PreHM.DragonLead;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Summon;
using Redemption.Items.Accessories.HM;
using ssm.Content.Projectiles.Enchantments;
using FargowiltasSouls;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class HardlightEnchant : BaseEnchant
    {
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

        public override Color nameColor => new Color(0, 242, 170);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HardlightEffect>(Item);
            player.AddEffect<ShieldGeneratorEffect>(Item);
        }

        public class HardlightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override bool MinionEffect => true;
            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();

            private int attackTimer;
            public override void PostUpdateEquips(Player player)
            { 
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TwigProj>()] < 1)
                {
                    Vector2 position = player.Center;
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        position,
                        Vector2.Zero,
                        ModContent.ProjectileType<HardlightDrone>(),
                        20,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }

        public class ShieldGeneratorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();

            public override void PostUpdateMiscEffects(Player player)
            {
                player.RedemptionPlayerBuff().shieldGenerator = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:HardlightHelms");
            recipe.AddIngredient<HardlightPlate>();
            recipe.AddIngredient<HardlightBoots>();
            recipe.AddIngredient<SlayerGun>();
            recipe.AddIngredient<PocketShieldGenerator>();
            recipe.AddIngredient<SlayerController>();

            recipe.AddTile(125);

            recipe.Register();
        }
    }
}
