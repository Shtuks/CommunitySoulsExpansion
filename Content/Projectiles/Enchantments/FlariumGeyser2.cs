using Terraria.ModLoader;
using Terraria;
using SacredTools.Content.Buffs;
using Microsoft.Xna.Framework;
using ssm.Core;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    internal class FlariumGeyser2 : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<FlariumInfernoDebuff>(), 300);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.3f, 0.2f, 0.1f);
            Projectile.alpha += 5;
            Projectile.rotation -= 0.1f;
            //Projectile.frameCounter++;
            //if (Projectile.frameCounter > 5)
            //{
            //    Projectile.frame++;
            //    Projectile.frameCounter = 0;
            //    if (Projectile.frame >= 4)
            //    {
            //        Projectile.tileCollide = true;
            //    }
            //}

            //if (Projectile.frame >= Main.projFrames[Projectile.type])
            //{
            //    Projectile.frame = Main.projFrames[Projectile.type];
            //    Projectile.Kill();
            //}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.velocity = oldVelocity / 3f;
            if (Projectile.timeLeft > 20)
            {
                Projectile.timeLeft = 20;
            }

            return false;
        }
    }
}
