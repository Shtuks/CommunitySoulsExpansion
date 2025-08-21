using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles.Fargo
{
    public class MutantBigSting22 : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_222";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Main.npcFrameCount[222];
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 66;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.scale = 0.5f;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X);
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.spriteDirection > 0)
            {
                Projectile.rotation += MathF.PI;
            }

            if (++Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }

            if (Projectile.frame >= 4)
            {
                Projectile.frame = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
            int num = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y = num * Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y, value.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color newColor = lightColor;
            newColor = Projectile.GetAlpha(newColor);
            SpriteEffects effects = ((Projectile.spriteDirection < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                Color color = newColor * 0.5f;
                color *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 vector = Projectile.oldPos[i];
                float rotation = Projectile.oldRot[i];
                Main.EntitySpriteDraw(value, vector + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, color, rotation, origin, Projectile.scale, effects);
            }

            Main.EntitySpriteDraw(value, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, effects);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(in SoundID.NPCDeath1, Projectile.Center);
            for (int i = 0; i < 20; i++)
            {
                int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].scale += 0.75f;
            }
        }
    }
}
