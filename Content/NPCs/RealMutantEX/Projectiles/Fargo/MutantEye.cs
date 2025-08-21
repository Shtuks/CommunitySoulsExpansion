using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Render.Primitives;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class MutantEye : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_452";

    protected bool DieOutsideArena;

	private int ritualID = -1;

	public PrimDrawer TrailDrawer { get; private set; }

	public virtual int TrailAdditive => 0;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 300;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.alpha = 0;
		CooldownSlot = 1;
		this.DieOutsideArena = Projectile.type == ModContent.ProjectileType<MutantEye>();
	}

	public override void AI()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
		Projectile.rotation = Projectile.velocity.ToRotation() + 1.570796f;
		if (Projectile.localAI[0] < (float)ProjectileID.Sets.TrailCacheLength[Projectile.type])
		{
			Projectile.localAI[0] += 0.1f;
		}
		else
		{
			Projectile.localAI[0] = ProjectileID.Sets.TrailCacheLength[Projectile.type];
		}
		Projectile.localAI[1] += 0.25f;
		if (!this.DieOutsideArena)
		{
			return;
		}
		if (this.ritualID == -1)
		{
			this.ritualID = -2;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>())
				{
					this.ritualID = i;
					break;
				}
			}
		}
		Projectile ritual = FargoSoulsUtil.ProjectileExists(this.ritualID, ModContent.ProjectileType<MutantRitual>());
		if (ritual != null && Projectile.Distance(ritual.Center) > 1200f)
		{
			Projectile.timeLeft = 0;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (!target.GetModPlayer<FargoSoulsPlayer>().BetsyDashing)
		{
			target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
			target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
			target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
			target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
			Projectile.timeLeft = 0;
		}
	}

	public override void Kill(int timeleft)
	{
		SoundEngine.PlaySound(SoundID.Zombie103, (Vector2?)Projectile.Center);
		Projectile.position = Projectile.Center;
		Projectile.width = (Projectile.height = 144);
		Projectile.position.X -= Projectile.width / 2;
		Projectile.position.Y -= Projectile.height / 2;
		for (int index = 0; index < 2; index++)
		{
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int index1 = 0; index1 < 5; index1++)
		{
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 20, 255f, 0f, 0, default(Color), 2.5f);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].velocity *= 3f;
			int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 20, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index3].velocity *= 2f;
			Main.dust[index3].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * Projectile.Opacity;
	}

	public float WidthFunction(float completionRatio)
	{
		return MathHelper.SmoothStep(Projectile.scale * (float)Projectile.width * 1.7f, 3.5f, completionRatio);
	}

	public Color ColorFunction(float completionRatio)
	{
		return Color.Lerp(Color.Cyan, Color.Transparent, completionRatio) * 0.7f;
	}

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow").Value;
        int rect1 = glow.Height / Main.projFrames[Projectile.type];
        int rect2 = rect1 * Projectile.frame;
        Rectangle glowrectangle = new(0, rect2, glow.Width, rect1);
        Vector2 gloworigin2 = glowrectangle.Size() / 2f;
        Color glowcolor = Color.Lerp(
            FargoSoulsUtil.AprilFools ? new Color(255, 0, 0, TrailAdditive) : new Color(31, 187, 192, TrailAdditive),
            Color.Transparent,
            0.74f);
        Vector2 drawCenter = Projectile.Center - Projectile.velocity.SafeNormalize(Vector2.UnitX) * 14;
        for (int i = 0; i < 3; i++) 
        {
            Vector2 drawCenter2 = drawCenter + (Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8).RotatedBy(MathHelper.Pi / 5 - i * MathHelper.Pi / 5); //use a normalized version of the projectile's velocity to offset it at different angles
            drawCenter2 -= Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8;
            float scale = Projectile.scale;
            scale += (float)Math.Sin(Projectile.localAI[1]) / 10;
            Main.EntitySpriteDraw(glow, drawCenter2 - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle),
                glowcolor, Projectile.velocity.ToRotation() + MathHelper.PiOver2, gloworigin2, scale, SpriteEffects.None, 0);
        }


        for (float i = Projectile.localAI[0] - 1; i > 0; i -= Projectile.localAI[0] / ProjectileID.Sets.TrailCacheLength[Projectile.type]) //trail grows in length as projectile travels
        {

            float lerpamount = 0.2f;
            if (i > 5 && i < 10)
                lerpamount = 0.4f;
            if (i >= 10)
                lerpamount = 0.6f;

            Color color27 = Color.Lerp(glowcolor, Color.Transparent, 0.1f + lerpamount);

            color27 *= (int)((Projectile.localAI[0] - i) / Projectile.localAI[0]) ^ 2;
            float scale = Projectile.scale * (float)(Projectile.localAI[0] - i) / Projectile.localAI[0];
            scale += (float)Math.Sin(Projectile.localAI[1]) / 10;
            Vector2 value4 = Projectile.oldPos[(int)i] - Projectile.velocity.SafeNormalize(Vector2.UnitX) * 14;
            Main.EntitySpriteDraw(glow, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle), color27,
                Projectile.velocity.ToRotation() + MathHelper.PiOver2, gloworigin2, scale * 0.8f, SpriteEffects.None, 0);
        }

        return false;
    }

    public override void PostDraw(Color lightColor)
    {
        Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
        int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; 
        int y3 = num156 * Projectile.frame; 
        Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
        Vector2 origin2 = rectangle.Size() / 2f;
        Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.White, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);

    }
}
