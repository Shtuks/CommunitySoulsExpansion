using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using ssm.Core;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumGeyser : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 180;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = Main.rand.Next(120, 301);
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20; 
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.position.Y -= Projectile.height / 2;
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        }

        public override void AI()
        {
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] >= 0f)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Type: ModContent.ProjectileType<FlariumGeyser2>(), X: Projectile.Center.X, Y: Projectile.Center.Y, SpeedX: Main.rand.NextFloat(-0.5f, 0.5f), SpeedY: -5f, Damage: Projectile.damage, KnockBack: Projectile.knockBack, Owner: Projectile.owner);
                }

                Projectile.ai[1] = -3f;
            }

            Lighting.AddLight(Projectile.Center, 1f, 0.6f, 0.2f);

            Projectile.scale = 0.9f + (float)System.Math.Sin(Main.GameUpdateCount * 0.2f) * 0.1f;
        }
    }
}
