using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Siblings
{
    internal class MonstrosityAxe : ModProjectile
    {
        public override string Texture => FargoSoulsUtil.EmptyTexture;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.FargoSouls().DeletionImmuneRank = 2;
            CooldownSlot = 1;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }

            Rectangle rectangle = projHitbox;
            rectangle.X = (int)Projectile.oldPosition.X;
            rectangle.Y = (int)Projectile.oldPosition.Y;
            if (rectangle.Intersects(targetHitbox))
            {
                return true;
            }

            rectangle = projHitbox;
            rectangle.X = (int)MathHelper.Lerp(Projectile.position.X, Projectile.oldPosition.X, 0.5f);
            rectangle.Y = (int)MathHelper.Lerp(Projectile.position.Y, Projectile.oldPosition.Y, 0.5f);
            if (rectangle.Intersects(targetHitbox))
            {
                return true;
            }

            return false;
        }

        public override void AI()
        {
            NPC nPC = FargoSoulsUtil.NPCExists(Projectile.ai[0], ModContent.NPCType<MutantEX>());
            if (nPC != null)
            {
                if (Projectile.localAI[0] == 0f)
                {
                    Projectile.localAI[0] = 1f;
                    Projectile.localAI[1] = Projectile.DirectionFrom(nPC.Center).ToRotation();
                }

                Vector2 vector = new Vector2(Projectile.ai[1], 0f).RotatedBy(nPC.ai[3] + Projectile.localAI[1]);
                Projectile.Center = nPC.Center + vector;
            }
            else
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(in SoundID.NPCDeath6, Projectile.Center);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.velocity.X = ((target.Center.X < Main.npc[(int)Projectile.ai[0]].Center.X) ? (-15f) : 15f);
            target.velocity.Y = -10f;
            target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240);
        }
    }
}
