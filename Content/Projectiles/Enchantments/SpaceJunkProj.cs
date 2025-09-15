using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace ssm.Content.Projectiles.Enchantments
{
    public class SpaceJunkProj : ModProjectile, IPixelatedPrimitiveRenderer
    {
        public static readonly Color colorr = Color.Lerp(Color.OrangeRed, Color.Orange, 0.4f);

        public ref float Force => ref Projectile.ai[0];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 60;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Force != 0f)
            {
                Projectile.position = Projectile.Center;
                Projectile.width = 120;
                Projectile.height = 120;
                Projectile.Center = Projectile.position;
            }
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 150f)
            {
                Projectile.tileCollide = true;
            }

            if (Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = (Main.rand.NextBool() ? 1 : (-1));
            }

            Projectile.rotation += Projectile.localAI[1] * (MathF.PI * 2f) / 24f;
            if ((Projectile.localAI[0] += 1f) % 2f == 1f)
            {
                int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X, Projectile.velocity.Y);
                Main.dust[num].noGravity = true;
                int num2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, Projectile.velocity.X, Projectile.velocity.Y);
                Main.dust[num2].noGravity = true;
            }

            if (Main.rand.NextBool(2))
            {
                float num3 = 0.4f;
                int num4 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Main.rand.NextVector2FromRectangle(new Rectangle(0, 0, Projectile.width, Projectile.height)) * 0.75f, default(Vector2), Main.rand.Next(61, 64));
                Gore obj = Main.gore[num4];
                obj.position -= Projectile.velocity * 4f;
                obj.velocity *= num3;
                obj.velocity = -Projectile.velocity * Main.rand.NextFloat(0.5f, 0.9f);
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Projectile.Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, Projectile.Center)) < (float)(projHitbox.Width / 2);
        }

        public float WidthFunction(float completionRatio)
        {
            return MathHelper.SmoothStep(Projectile.scale * (float)Projectile.width * 1.1f, amount: MathF.Pow(completionRatio, 1.5f), value2: 25f * Projectile.scale);
        }

        public static Color ColorFunction(float completionRatio)
        {
            Color color = Color.Lerp(colorr, Color.SkyBlue, completionRatio);
            float num = 0.7f;
            return color * num;
        }

        public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
        {
            ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
            FargosTextureRegistry.FadedStreak.Value.SetTexture1();
            PrimitiveRenderer.RenderTrail(Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, (float _) => Projectile.Size * 0.5f, Smoothen: true, Pixelate: true, shader), 44);
        }
        public override void SetDefaults()
        {
            Projectile.width = 53;
            Projectile.height = 47;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Generic;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int shardCount = Main.rand.Next(2, 5); 

            for (int i = 0; i < shardCount; i++)
            {
                Vector2 position = Projectile.Center;

                int shard = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    position,
                    Vector2.Zero, 
                    ModContent.ProjectileType<SatelliteShard>(),
                    Projectile.damage / 2, 
                    0,
                    Projectile.owner);

                Main.projectile[shard].rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            }
            return true; 
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            SpriteEffects effects = SpriteEffects.None;

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }
    }
}
