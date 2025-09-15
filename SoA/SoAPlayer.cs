using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using SacredTools;
using ssm.Content.Buffs;
using ssm.Content.Projectiles.Enchantments;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.FlariumEnchant;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAPlayer : ModPlayer
    {
        public int rivalStreak;
        public int EerieEnchantCooldown;
        public int SpaceJunkCooldown;
        public override void PreUpdateMovement()
        {
            if (Player.HasBuff(ModContent.BuffType<SniperBuff>()))
            {
                Player.controlLeft = false;
                Player.controlRight = false;
                Player.controlJump = false;
                Player.controlDown = false;
                Player.controlUp = false;
                Player.velocity = Vector2.Zero;
                Player.gravity = 0f;
                Player.fallStart = (int)(Player.position.Y / 16f);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.owner.ToPlayer().ownedProjectileCounts[ModContent.ProjectileType<FlariumTornado>()] < 6)
            {
                if (proj.owner.ToPlayer().HasEffect<FlariumEffect>() && Main.rand.NextFloat() < 0.05f && proj.damage > 0 && !proj.minion)
                {
                    Vector2 spawnPosition = proj.Center;
                    Projectile.NewProjectile(
                    proj.GetSource_FromThis(),
                    spawnPosition,
                    Vector2.Zero,
                    ModContent.ProjectileType<FlariumTornado>(),
                    100,
                    0,
                        proj.owner
                    );
                }
            }
        }
        public override void UpdateEquips()
        {
            if (Player.GetModPlayer<ModdedPlayer>().DragonSetEffect)
            {
                Player.GetDamage(DamageClass.Generic) += 0.05f;
            }
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (Player.HasBuff(ModContent.BuffType<SniperBuff>()))
            {
                damage += 1.25f;
            }
            else if (Player.HasBuff(ModContent.BuffType<SniperCooldownBuff>()))
            {
                damage *= 0.75f;
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAPlayerTest : ModPlayer
    {
        public bool flariumWings;

        public override void ResetEffects()
        {
            flariumWings = false;
        }
    }
}
