using ssm.Content.Buffs;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ssm.Content.Projectiles.Enchantments
{
    public class MushroomProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_183";
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300; 
            Projectile.aiStyle = 2;
        }

        public override void AI()
        {
            Projectile.rotation += 0.05f;
            Lighting.AddLight(Projectile.Center, 0.2f, 0.2f, 0.8f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomSpray);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            int healAmount = target.statLifeMax2 / 10;
            target.HealEffect(healAmount);

            target.AddBuff(ModContent.BuffType<MushroomBuff>(), 600); 

            SoundEngine.PlaySound(SoundID.Item2, Projectile.position);
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleMoss, 0f, 0f, 150, default, 1.5f);
            }

            Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new(0, y3, texture.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color = Projectile.GetAlpha(lightColor);

            Main.spriteBatch.UseBlendState(BlendState.Additive);
            for (int j = 0; j < 12; j++)
            {
                Vector2 afterimageOffset = (MathHelper.TwoPi * j / 12f).ToRotationVector2() * 3f;
                Color glowColor = Color.Cyan;

                Main.EntitySpriteDraw(texture, drawPosition + afterimageOffset, rectangle, glowColor, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.ResetToDefault();

            Main.EntitySpriteDraw(texture, drawPosition, rectangle, color, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}
