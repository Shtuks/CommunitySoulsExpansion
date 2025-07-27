using FargowiltasCrossmod.Content.Calamity.Bosses.SkeletronPrime;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Redemption.Enchantments.XeniumEnchant;
using ssm.Core;
using Terraria.Audio;
using FargowiltasSouls.Assets.Sounds;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XeniumTurret : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        private const int ShootCooldown = 90;
        private int shootTimer = 0;
        private Vector2 offset = new Vector2(50, -30);

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.timeLeft = 2;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center + new Vector2(
                offset.X * player.direction,
                offset.Y
            );

            Projectile.spriteDirection = player.direction;


            NPC target = FindTarget(600f);

            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                Projectile.rotation = direction.ToRotation();

                if (Projectile.spriteDirection == -1)
                    Projectile.rotation += MathHelper.Pi;
            }
            else
            {
                Projectile.rotation = 0;
            }

            if (++shootTimer >= ShootCooldown && target != null && player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(FargosSoundRegistry.NukeFishronFire, player.Center);
                ShootRocket(target);
                shootTimer = 0;
            }

            if (player.HasEffect<XeniumEffect>())
            {
                Projectile.timeLeft = 2;
            }
        }

        private NPC FindTarget(float radius)
        {
            return Main.npc
                .Where(n => n.active &&
                           !n.friendly &&
                           n.CanBeChasedBy() &&
                           Vector2.Distance(Projectile.Center, n.Center) <= radius)
                .OrderBy(n => Vector2.Distance(Projectile.Center, n.Center))
                .FirstOrDefault();
        }
        private void ShootRocket(NPC target)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 velocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitX) * 8f;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                velocity,
                ModContent.ProjectileType<HomingRocket>(),
                90,
                5f,
                player.whoAmI
            );
        }
    }
}