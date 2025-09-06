using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using static ssm.Thorium.Enchantments.AstroEnchant;

namespace ssm.Content.Projectiles.Enchantments
{
    public class SpaceshipMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        private int shootTimer = 0;
        private const int ShootInterval = 90;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.dead || !owner.active || !owner.HasEffect<AstroEffect>())
            {
                Projectile.Kill();
                return;
            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Vector2 targetPosition = owner.Center + new Vector2(0, -120);
            Vector2 direction = targetPosition - Projectile.Center;
            float distance = direction.Length();

            if (distance > 1000)
            {
                Projectile.position = targetPosition;
            }
            else if (distance > 20)
            {
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * 20f + direction * 10f) / 21f;
            }
            else
            {
                Projectile.velocity *= 0.95f;
            }

            NPC target = null;
            float maxDistance = 800f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(Projectile))
                {
                    float distanceToTarget = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distanceToTarget < maxDistance && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        maxDistance = distanceToTarget;
                        target = npc;
                    }
                }
            }

            shootTimer++;
            if (shootTimer >= ShootInterval && target != null)
            {
                ShootAtTarget(target, owner);
                shootTimer = 0;
            }

            Lighting.AddLight(Projectile.Center, 0.3f, 0.5f, 1f);
            Projectile.rotation = Projectile.velocity.X * 0.05f;
        }

        private void ShootAtTarget(NPC target, Player owner)
        {
            Item fakeWeapon = new Item();
            fakeWeapon.SetDefaults(ItemID.Minishark);
            fakeWeapon.damage = 10; 

            bool canShoot = owner.PickAmmo(
                fakeWeapon,
                out int projType,
                out float shootSpeed,
                out int damage,
                out float knockback,
                out _
            );

            if (!canShoot) return;

            Vector2 direction = target.Center - Projectile.Center;
            direction.Normalize();
            direction *= shootSpeed;

            int bullet = Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                direction,
                projType,
                damage,
                knockback,
                owner.whoAmI
            );

            Main.projectile[bullet].DamageType = DamageClass.Ranged;
            Main.projectile[bullet].penetrate = 1;
            Main.projectile[bullet].timeLeft = 300;

            for (int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Electric,
                    direction.X * 0.5f,
                    direction.Y * 0.5f,
                    150,
                    default,
                    1.2f
                );
                d.noGravity = true;
            }
        }

        public override bool MinionContactDamage() => false;
    }
}
