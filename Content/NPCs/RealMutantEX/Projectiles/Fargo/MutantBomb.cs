using System;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;
public class MutantBomb : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_645";
    public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = Main.projFrames[645];
	}

	public override void SetDefaults()
	{
		Projectile.width = 400;
		Projectile.height = 400;
		Projectile.aiStyle = -1;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 60;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		CooldownSlot = 1;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCheck = (Projectile projectile) => false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		int clampedX = projHitbox.Center.X - targetHitbox.Center.X;
		int clampedY = projHitbox.Center.Y - targetHitbox.Center.Y;
		if (Math.Abs(clampedX) > targetHitbox.Width / 2)
		{
			clampedX = targetHitbox.Width / 2 * Math.Sign(clampedX);
		}
		if (Math.Abs(clampedY) > targetHitbox.Height / 2)
		{
			clampedY = targetHitbox.Height / 2 * Math.Sign(clampedY);
		}
		int num = projHitbox.Center.X - targetHitbox.Center.X - clampedX;
		int dY = projHitbox.Center.Y - targetHitbox.Center.Y - clampedY;
		return Math.Sqrt(num * num + dY * dY) <= (double)(Projectile.width / 2);
	}

	public override bool CanHitPlayer(Player target)
	{
		return target.hurtCooldowns[1] == 0;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
			Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			SoundEngine.PlaySound(SoundID.Item14, (Vector2?)Projectile.Center);
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 161, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dust].velocity *= 1.4f;
			}
			for (int i = 0; i < 5; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 161, 0f, 0f, 0, default(Color), 3.5f);
				Main.dust[d].noGravity = true;
				Main.dust[d].noLight = true;
				Main.dust[d].velocity *= 4f;
			}
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 161, 0f, 0f, 100, default(Color), 3.5f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 7f;
				dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 161, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dust].velocity *= 3f;
			}
			float scaleFactor9 = 0.5f;
			int gore = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, default(Vector2), Main.rand.Next(61, 64));
			Main.gore[gore].velocity *= scaleFactor9;
			Main.gore[gore].velocity.X += 1f;
			Main.gore[gore].velocity.Y += 1f;
		}
		if (++Projectile.frameCounter >= 3)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame--;
				Projectile.Kill();
			}
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 127) * Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color = Projectile.GetAlpha(lightColor);
		color.A = 210;
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color, Projectile.rotation, origin2, Projectile.scale * 4f, SpriteEffects.None, 0);
		return false;
	}
}
