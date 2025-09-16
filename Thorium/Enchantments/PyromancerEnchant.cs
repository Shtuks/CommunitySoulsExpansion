using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ssm.Core;
using ThoriumMod.Items.Cultist;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.Buffs;
using FargowiltasSouls;
using ssm.Content.Projectiles.Enchantments;
using System;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PyromancerEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override List<AccessoryEffect> ActiveSkillTooltips =>
            [AccessoryEffectLoader.GetEffect<PyroEffect>()];
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PyroEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<PyromancerCowl>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerTabard>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<PlasmaGenerator>());
            recipe.AddIngredient(ModContent.ItemType<AncientFlame>());
            recipe.AddIngredient(ModContent.ItemType<AlmanacofAgony>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class PyroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<PyromancerEnchant>();
            public override bool ActiveSkill => true;

            public int cd;
            public override void PostUpdateEquips(Player player)
            {
                if(cd > 0)
                {
                    cd--;
                }
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (cd < 1)
                {
                    player.AddBuff(ModContent.BuffType<PureFlameBuff>(), 1200);
                    cd += 3600;
                }
            }
            public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
            {
                if (player.HasBuff<PureFlameBuff>()) {
                    Vector2 center = player.Center;
                    Vector2 vector = Vector2.Normalize(Main.MouseWorld - center);

                    if (Main.rand.Next(30) != 0)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            vector.RotatedByRandom(Math.PI) * Main.rand.NextFloat(6f, 10f) * 2,
                            ModContent.ProjectileType<PureFireballProj>(),
                            player.ForceEffect<PyroEffect>() ? 500 : 250,
                            0f,
                            player.whoAmI
                        );
                    }
                }
            }
        }
    }
}
