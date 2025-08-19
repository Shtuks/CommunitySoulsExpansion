using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Redemption.Enchantments.HardlightEnchant;
using ssm.Core;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class HardlightDrone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_183";
        private const int AttackCooldown = 600;
        private int attackTimer = 0;

        public override void SetStaticDefaults()
        {
           // Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override bool MinionContactDamage() => false;

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            Vector2 targetPosition = owner.Center + new Vector2(0, -60);
            Vector2 direction = targetPosition - Projectile.Center;
            if (direction.Length() > 10f)
            {
                direction.Normalize();
                Projectile.velocity = direction * 5f;
            }
            else
            {
                Projectile.velocity *= 0.95f;
            }

            //Projectile.frameCounter++;
            //if (Projectile.frameCounter > 6)
            //{
            //    Projectile.frameCounter = 0;
            //    Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            //}

            if (++attackTimer >= AttackCooldown)
            {
                attackTimer = 0;
                ExecuteRandomAttack(owner);
            }

            if (owner.HasEffect<HardlightEffect>())
            {
                Projectile.timeLeft = 2;
            }
        }

        private void ExecuteRandomAttack(Player owner)
        {
            float chance = Main.rand.NextFloat();

            if (chance < 0.125f) 
            {
                ShootMissile(owner);
            }
            else if (chance < 0.375f) 
            {
                ShootLaser(owner);
            }
            else if (chance < 0.875f)
            {
                ShootCarePackage(owner);
            }
            else 
            {
                DashAttack(owner);
            }
        }

        private void ShootMissile(Player owner)
        {
            Vector2 target = FindTarget(800f);
            if (target == Vector2.Zero) return;

            Vector2 velocity = (target - Projectile.Center).SafeNormalize(Vector2.UnitX) * 8f;
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                velocity,
                ProjectileID.MiniNukeRocketII,
                150, 
                5f,
                owner.whoAmI
            );
        }

        private void ShootLaser(Player owner)
        {
            Vector2 target = FindTarget(1000f);
            if (target == Vector2.Zero) return;

            int weaponDamage = owner.HeldItem.damage;
            int calculatedDamage = (int)(weaponDamage * 0.15f);
            if (calculatedDamage < 1) calculatedDamage = 1;

            Vector2 velocity = (target - Projectile.Center).SafeNormalize(Vector2.UnitX) * 12f;
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                velocity,
                ProjectileID.PinkLaser,
                calculatedDamage,
                2f,
                owner.whoAmI
            );
        }

        private void ShootCarePackage(Player owner)
        {
            Vector2 spawnPosition = owner.Center + new Vector2(Main.rand.Next(-100, 100), -300);
            Vector2 velocity = new Vector2(0, 5f);

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                spawnPosition,
                velocity,
                ModContent.ProjectileType<CarePackage>(),
                0,
                0f,
                owner.whoAmI
            );
        }

        private void DashAttack(Player owner)
        {
            Vector2 target = FindTarget(500f);
            if (target == Vector2.Zero) return;

            Vector2 dashVector = (target - Projectile.Center).SafeNormalize(Vector2.UnitX) * 20f;
            Projectile.velocity = dashVector;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 2;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.damage = 150;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        private Vector2 FindTarget(float maxDistance)
        {
            NPC closest = null;
            float distance = maxDistance;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && npc.active)
                {
                    float npcDistance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (npcDistance < distance)
                    {
                        distance = npcDistance;
                        closest = npc;
                    }
                }
            }

            return closest?.Center ?? Vector2.Zero;
        }
    }
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class CarePackage : ModProjectile
    {
        public override string Texture => "Redemption/Items/Usable/MedicKit";
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f;
            Projectile.velocity.Y += 0.3f;
        }

        public override void OnKill(int timeLeft)
        {
            Player owner = Main.player[Projectile.owner];

            int healAmount = (int)(owner.statLifeMax2 * 0.25f);
            owner.statLife = System.Math.Min(owner.statLife + healAmount, owner.statLifeMax2);
            owner.HealEffect(healAmount);

            int manaAmount = (int)(owner.statManaMax2 * 0.25f);
            owner.statMana = System.Math.Min(owner.statMana + manaAmount, owner.statManaMax2);
            owner.ManaEffect(manaAmount);

            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.HeartCrystal);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ManaRegeneration);
            }
        }
    }
}
