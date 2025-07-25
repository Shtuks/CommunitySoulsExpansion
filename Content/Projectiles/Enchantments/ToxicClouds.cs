using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using ssm.Core;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class ToxicCloudsProj : ModProjectile
    {
        private float expandTimer = 0;
        private float initialScale = 0.5f;

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            expandTimer++;
            if (expandTimer < 120) 
            {
                float progress = expandTimer / 120f;
                Projectile.scale = initialScale + progress * 1.0f; // От 0.5 до 1.5
                Projectile.width = (int)(40 * Projectile.scale);
                Projectile.height = (int)(40 * Projectile.scale);
            }

            if (Main.rand.NextBool(30))
            {
                Projectile.velocity += new Vector2(Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.1f, 0.1f));
            }

            if (Projectile.velocity.Length() > 0.8f)
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * 0.8f;
            }

            if (Main.rand.NextBool(20))
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage &&
                        Vector2.Distance(npc.Center, Projectile.Center) < Projectile.width / 2)
                    {
                        npc.AddBuff(ModContent.BuffType<BInfectionDebuff>(), 1800);
                        npc.AddBuff(ModContent.BuffType<BileDebuff>(), 1800); 
                    }
                }
            }
        }
    }
}
