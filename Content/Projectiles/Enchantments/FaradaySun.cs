using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.gunrightsmod.Enchantments;
using ssm.gunrightsmod;
using System.Collections.Generic;
using static ssm.gunrightsmod.Enchantments.FaradayEnchant;


namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class FaradaySun : ModProjectile
    {
        private float FaradaySunAngle;
        private float RotationOffset;

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

            if (!player.HasEffect<FaradayEffect>() || player.dead)
            {
                Projectile.Kill();
                return;
            }
            if (Projectile.localAI[0] == 0f)
            {
                RotationOffset = Projectile.ai[0];
                Projectile.localAI[0] = 1f;
            }
            FaradaySunAngle += 0.02f;
            float totalAngle = FaradaySunAngle + RotationOffset;
            Vector2 offset = new Vector2(
                MathF.Cos(totalAngle),
                MathF.Sin(totalAngle)
            ) * 180f;
            Projectile.Center = player.Center + offset;
        }
    }
}