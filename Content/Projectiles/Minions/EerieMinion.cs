using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SacredTools.Buffs;
using ssm.SoA;
using ssm.Core;

namespace ssm.Content.Projectiles.Minions
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class EerieMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || !(player.GetModPlayer<SoAPlayer>().eerieEnchant > 0))
            {
                Projectile.Kill();
                return;
            }

            MinionAI(player);

            if (Main.rand.NextBool(10))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0f, 0f, 100, new Color(255, 0, 0), 1.5f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }

        private void MinionAI(Player player)
        {
            float distanceFromTarget = 700f;
            Vector2 targetCenter = Projectile.position;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (npc.CanBeChasedBy())
                {
                    float distance = Projectile.Distance(npc.Center);
                    if (distance < distanceFromTarget)
                    {
                        distanceFromTarget = distance;
                        targetCenter = npc.Center;
                        foundTarget = true;
                    }
                }
            }

            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float distance = Projectile.Distance(npc.Center);
                        if (distance < distanceFromTarget)
                        {
                            distanceFromTarget = distance;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            Vector2 destination = foundTarget ? targetCenter : player.Center;
            float speed = 8f;
            float inertia = 20f;

            Vector2 direction = destination - Projectile.Center;
            direction.Normalize();
            direction *= speed;

            Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = 1;
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
            }

            Projectile.rotation = Projectile.velocity.X * 0.05f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            player.AddBuff(ModContent.BuffType<EerieRegen>(), player.GetModPlayer<SoAPlayer>().eerieEnchant > 1 ? 600 : 450); 

            int healAmount = (int)(damageDone * player.GetModPlayer<SoAPlayer>().eerieEnchant > 1 ? 0.75f : 0.25f);
            if (healAmount > 0)
            {
                player.statLife += healAmount;
                player.HealEffect(healAmount);
            }
        }
    }
}
