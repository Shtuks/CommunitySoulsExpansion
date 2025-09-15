using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.SpaceJunk;
using SacredTools.Content.Items.Weapons.Event;
using ssm.Core;
using ssm.Content.Projectiles.Enchantments;
using System;
using static ssm.SoA.Forces.GenerationsForce;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.UI.Elements;
using CalamityMod.Items.Potions.Alcohol;
using FargowiltasSouls.Assets.Sounds;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls;
using Terraria.Audio;
using FargowiltasSouls.Core.ModPlayers;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SpaceJunkEnchant : BaseEnchant
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
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(120, 135, 154);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SpaceJunkEffect>(Item);
            player.AddEffect<SpaceJunkAbilityEffect>(Item);
        }

        public class SpaceJunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpaceJunkEnchant>();
            public override void OnHitByEither(Player player, NPC npc, Projectile proj)
            {
                if (Main.rand.NextFloat() < 0.33f)
                {
                    float spread = 40f * 0.0174f;
                    double startAngle = Math.Atan2(player.velocity.X, player.velocity.Y) - spread / 2;
                    double deltaAngle = spread / 4f;
                    double offsetAngle;

                    if (player.whoAmI == Main.myPlayer)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            int dmg = (int)player.GetDamage<GenericDamageClass>().ApplyTo(player.HasEffect<GenerationsEffect>() ? 400 : 40);
                            offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                            int shard = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<SatelliteShard>(), dmg, 1, player.whoAmI, 0f, 0f);
                            Main.projectile[shard].DamageType = DamageClass.Generic;
                        }
                    }
                }
            }
        }

        public class SpaceJunkAbilityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override bool ActiveSkill => true;
            public int SpaceJunkAbilityCooldown;
            public override int ToggleItemType => ModContent.ItemType<SpaceJunkEnchant>();
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (SpaceJunkAbilityCooldown <= 0)
                {
                    NPC nearestEnemy = null;
                    float closestDistance = float.MaxValue;

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.CanBeChasedBy() && npc.immune[player.whoAmI] == 0)
                        {
                            float distance = Vector2.Distance(player.Center, npc.Center);
                            if (distance < closestDistance)
                            {
                                closestDistance = distance;
                                nearestEnemy = npc;
                            }
                        }
                    }

                    if (nearestEnemy != null)
                    {
                        FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
                        bool num = fargoSoulsPlayer.ForceEffect<MeteorEnchant>();
                        int num2 = (num ? 400 : 70);

                        Vector2 vector = new Vector2(player.Center.X + Main.rand.NextFloat(-1000f, 1000f), player.Center.Y - 1000f);
                        Vector2 velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(8f, 12f));
                        vector.X = nearestEnemy.Center.X + Main.rand.NextFloat(-320f, 320f);
                        Vector2 vector2 = Main.rand.NextFloat(10f, 30f) * nearestEnemy.velocity;
                        vector.X += vector2.X;
                        Vector2 center = nearestEnemy.Center;
                        if (vector.Y < center.Y)
                        {
                            velocity = FargoSoulsUtil.PredictiveAim(vector, center, nearestEnemy.velocity / 3f, 12f);
                        }

                        SoundEngine.PlaySound(in FargosSoundRegistry.ThrowShort, vector);
                        int num3 = (num ? 1 : 0);
                        Projectile.NewProjectile(GetSource_EffectItem(player), vector, velocity, ModContent.ProjectileType<SpaceJunkProj>(), (int)((float)num2 * player.ActualClassDamage(DamageClass.Throwing)), 0.5f, player.whoAmI, num3);

                        SpaceJunkAbilityCooldown += 60 * 15;
                    }
                }
            }
            public override void PostUpdateEquips(Player player)
            {
                CooldownBarManager.Activate("SpaceJunkEnchantCooldown", ModContent.Request<Texture2D>("ssm/SoA/Enchantments/SpaceJunkEnchant").Value, new(237, 73, 78),
                    () => SpaceJunkAbilityCooldown / (60f * 15), true, activeFunction: player.HasEffect<SpaceJunkEffect>);

            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<SpaceJunkHelm>();
            recipe.AddIngredient<SpaceJunkBody>();
            recipe.AddIngredient<SpaceJunkLegs>();
            recipe.AddIngredient<OrbFlayer>();
            recipe.AddIngredient<HornetNeedle>();
            recipe.AddIngredient<GoldDoorHandle>();
            recipe.AddTile(125);
            recipe.Register();
        }
    }
}