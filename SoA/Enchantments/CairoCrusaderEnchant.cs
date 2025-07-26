using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SacredTools;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.CairoCrusader;
using SacredTools.Items.Weapons.Sand;
using SacredTools.Items.Tools;
using SacredTools.Items.Weapons;
using SacredTools.Projectiles.Minions.EternalOasis;
using static ssm.SoA.Forces.FoundationsForce;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class CairoCrusaderEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(242, 208, 114);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CairoEffect>(Item);
        }

        public class CairoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CairoCrusaderEnchant>();
            public override bool MinionEffect => true;

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<ModdedPlayer>().cairoCrusader = true;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<EternalOasis>()] <= 0 && player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis("SetBonus_CairoCrusader"), player.Center, Vector2.Zero, ModContent.ProjectileType<EternalOasis>(), (int)player.GetTotalDamage<ThrowingDamageClass>().ApplyTo(player.HasEffect<FoundationsEffect>() ? 1000f : 100f), 0f, player.whoAmI);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<CairoCrusaderTurban>();
            recipe.AddIngredient<CairoCrusaderRobe>();
            recipe.AddIngredient<CairoCrusaderFaulds>();
            recipe.AddIngredient<DesertStaff>();
            recipe.AddIngredient<SandstormMedallion>();
            recipe.AddIngredient<ElementalFlinger>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
