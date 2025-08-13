using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Buffs.Masomode;
using static ssm.Thorium.Enchantments.JesterEnchant;
using FargowiltasSouls;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.Content.Projectiles
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MinionBellProj : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        private int ringTimer = 0;
        private const int RingInterval = 600;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.dead || !owner.active || !owner.HasEffect<JesterEffect>())
            {
                Projectile.Kill();
                return;
            }

            Vector2 position = owner.Center;
            position.Y -= 70;
            Projectile.Center = position;

            Projectile.rotation = MathHelper.SmoothStep(
                -0.1f,
                0.1f,
                (float)(Main.GameUpdateCount % 120) / 120
            );

            ringTimer++;
            if (ringTimer >= RingInterval)
            {
                ringTimer = 0;
                RingBell();
            }

            Projectile.timeLeft = 2;
        }

        private void RingBell()
        {
            Player owner = Main.player[Projectile.owner];

            SoundEngine.PlaySound(SoundID.Item35, Projectile.Center);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BellMinionPulse>(), 0, 0);

            float radius = 800f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.CanBeChasedBy() &&
                    Vector2.DistanceSquared(npc.Center, Projectile.Center) < radius * radius)
                {
                    if (npc.boss)
                    {
                        npc.AddBuff(BuffID.Confused, owner.ForceEffect<JesterEffect>() ? 600 : 300);
                    }
                    else
                    {
                        npc.AddBuff(ModContent.BuffType<StunnedBuff>(), owner.ForceEffect<JesterEffect>() ? 600 : 300);
                    }
                }
            }
        }
    }
}