using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using SacredTools.Content.Projectiles.Weapons.Dreamscape.Nihilus;
using SacredTools.Projectiles.Dreamscape;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.FallenPrinceEnchant;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            if (proj.type == ModContent.ProjectileType<DesperatioFlame>())
            {
                proj.damage -= (int)(proj.damage * 0.4f);
            }
        }
        public override void AI(Projectile projectile)
        {
            if(projectile.type == ModContent.ProjectileType<TenebrisLink2>())
            {
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 1600, 8, 2);
                }
            }
            
            if (projectile.type == ModContent.ProjectileType<SpookGrenade>())
            {
                projectile.velocity *= 1.05f;
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 700, 8, 2);
                }
            }

            if (projectile.type == ModContent.ProjectileType<DesperatioBullet>())
            {
                if ((projectile.ai[2] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 300, 8, 2);
                }
            }

            if (projectile.DamageType == DamageClass.Throwing && projectile.owner.ToPlayer().HasEffect<GravityEffect>())
            {
                CreateGravityField(projectile, projectile.damage);
            }
        }

        public void CreateGravityField(Projectile proj, int damage)
        {
            float gravityStrength = MathHelper.Clamp(damage / 50f, 1f, 20f);
            float radius = 300f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                {
                    float distance = Vector2.Distance(npc.Center, proj.Center);
                    if (distance < radius)
                    {
                        Vector2 direction = proj.Center - npc.Center;
                        direction.Normalize();
                        float pullStrength = gravityStrength * (1f - distance / radius);
                        npc.velocity += direction * pullStrength;

                        //if (Main.rand.NextBool(5))
                        //{
                        //    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                        //    dust.velocity = direction * pullStrength * 2f;
                        //    dust.noGravity = true;
                        //}
                    }
                }
            }
        }

        //public override void OnKill(Projectile projectile, int timeLeft)
        //{
        //    if (projectile.owner == Main.myPlayer &&
        //        projectile.velocity.Y != 0 &&
        //        projectile.tileCollide &&
        //        projectile.position.Y + projectile.height >= projectile.ai[1])
        //    {
        //        Player player = Main.player[projectile.owner];

        //        if (&&
        //            Main.rand.NextFloat() < 0.15f)
        //        {
        //            int damage = 100;
        //            int duration = Main.rand.Next(120, 300); 
        //            Vector2 position = new Vector2(projectile.Center.X, projectile.position.Y + projectile.height);

        //            Projectile.NewProjectile(
        //                projectile.GetSource_FromThis(),
        //                position,
        //                Vector2.Zero,
        //                ModContent.ProjectileType<FlameGeyserProj>(),
        //                damage,
        //                0f,
        //                player.whoAmI,
        //                duration);
        //        }
        //    }
        //}
    }
}
