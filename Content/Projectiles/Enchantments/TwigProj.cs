using Microsoft.Xna.Framework;
using ssm.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class TwigProj : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation = 0;

            if (Projectile.velocity.Y < 10f)
            {
                Projectile.velocity.Y += 0.2f;
            }

            if (Projectile.velocity.Y == 0)
            {
                Projectile.velocity.X *= 0.9f;
                if (Math.Abs(Projectile.velocity.X) < 0.1f)
                {
                    Projectile.velocity.X = 0f;
                }
            }
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = 0;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 0)
            {
                Projectile.velocity.Y = 0;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
    }
}