using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Eerie;
using SacredTools.Items.Weapons;
using FargowiltasSouls;
using ssm.Content.Projectiles.Minions;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class EerieEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 70000;
        }

        public override Color nameColor => new(165, 37, 72);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<EerieEffect>(Item))
            {
                player.GetModPlayer<SoAPlayer>().eerieEnchant = player.ForceEffect<EerieEffect>() ? 2 : 1;
            }
        }

        public class EerieEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EerieEnchant>();

            public override void PreUpdate(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    int minionType = ModContent.ProjectileType<EerieMinion>();
                    if (player.ownedProjectileCounts[minionType] < 1)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            Vector2.Zero,
                            minionType,
                            30,
                            2f, 
                            player.whoAmI);
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<EerieChest>();
            recipe.AddIngredient<EerieLegs>();
            recipe.AddIngredient<EerieHelmet>();
            recipe.AddIngredient<EerieCane>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
