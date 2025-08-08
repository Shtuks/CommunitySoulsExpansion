using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using ssm.Core;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class FaradayMoon : ModProjectile
    {
        private float angle; // Current angle in radians
        private float rotationSpeed = 0.02f; // How fast to orbit
        private float orbitDistance = 180f; // Distance from player center
        private float rotationOffset; // Offset to separate the two projectiles

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // If the player dies or stops holding the item, kill the projectile
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            // Initialise rotationOffset on first tick
            if (Projectile.localAI[0] == 0f)
            {
                rotationOffset = Projectile.ai[0]; // ai[0] passed when spawning
                Projectile.localAI[0] = 1f;
            }

            // Increase angle over time
            angle += rotationSpeed;

            // Calculate position
            float totalAngle = angle + rotationOffset;
            Vector2 offset = new Vector2(
                MathF.Cos(totalAngle),
                MathF.Sin(totalAngle)
            ) * orbitDistance;

            Projectile.Center = player.Center + offset;

            // Face movement direction (optional)
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}