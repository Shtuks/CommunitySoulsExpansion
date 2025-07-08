using FargowiltasSouls.Assets.Sounds;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using FargowiltasSouls.Content.Buffs.Boss;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Siblings
{
    public class MonstrosityDEH : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Content/Bosses/DeviBoss/DeviEnergyHeart";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;

            Projectile.alpha = 150;
            Projectile.timeLeft = 80;

            Projectile.FargoSouls().DeletionImmuneRank = 1;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = 1;
                SoundEngine.PlaySound(new SoundStyle("FargowiltasSouls/Assets/Sounds/Siblings/Deviantt/DeviHeartThrow" + Main.rand.Next(1, 3)), Projectile.Center);
            }

            if (Projectile.alpha >= 60)
                Projectile.alpha -= 10;

            Projectile.rotation = Projectile.ai[0];
            Projectile.scale += 0.01f;

            float speed = Projectile.velocity.Length();
            speed += Projectile.ai[1];
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * speed;
        }

        public override void OnKill(int timeLeft)
        {
            FargoSoulsUtil.HeartDust(Projectile.Center, Projectile.rotation + MathHelper.PiOver2);
            SoundEngine.PlaySound(FargosSoundRegistry.DeviHeartExplosion with { MaxInstances = 0, Volume = 0.33f }, Projectile.Center);
            if (FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()))
            {
                if (FargoSoulsUtil.HostCheck)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.UnitX.RotatedBy(Projectile.rotation + (float)Math.PI / 2 * i),
                            ModContent.ProjectileType<DeviDeathray>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300);
            target.AddBuff(BuffID.Bleeding, 600);
        }

        public override Color? GetAlpha(Color lightColor) => Color.White * Projectile.Opacity;

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; 
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);

            SpriteEffects spriteEffects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                Color color27 = color26;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 value4 = Projectile.oldPos[i];
                float num165 = Projectile.oldRot[i];
                Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, spriteEffects, 0);
            }

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
