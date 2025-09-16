using FargowiltasSouls.Content.Items;
using ssm.Core;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Buffs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework;

namespace ssm.Thorium.Emode.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class VampiresBlessing : SoulsItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EmodeThorium;
        }
        public override bool Eternity => true;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<VampiresCurseBuff>()] = true;
            player.AddEffect<VampiresBlessingEffect>(Item);
        }

        public class VampiresBlessingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EnergizedMobileDeviceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VampiresBlessing>();
            public override void ModifyHurt(Player player, ref Player.HurtModifiers modifiers)
            {
                if (modifiers.FinalDamage.Flat > 50)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 position = player.Center + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        Vector2 velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));

                        int projectile = Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            position,
                            velocity,
                            ModContent.ProjectileType<BatProj>(),
                            20,
                            2f,
                            player.whoAmI
                        );
                    }
                }
            }
        }
    }
    public class BatProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180; 
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Projectile.velocity *= 0.98f; 
            Projectile.velocity += new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.3f, 0.3f));

            float maxDetectRadius = 200f;
            float homingSpeed = 0.2f;

            NPC target = CSEUtils.FindClosestNPCForProj(maxDetectRadius, Projectile);
            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= homingSpeed;
                Projectile.velocity += direction;
            }

            if (Projectile.velocity.Length() > 6f)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 6f;
            }

            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0f, 0f, 100, default, 1f);
                dust.velocity *= 0.3f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Bleeding, 600); 
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.8f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
            }
            return false;
        }
    }
}