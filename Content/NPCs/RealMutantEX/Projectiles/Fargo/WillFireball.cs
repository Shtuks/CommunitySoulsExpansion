using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class WillFireball : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_711";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.timeLeft = 600;
		Projectile.alpha = 60;
		Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void Kill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, (Vector2?)Projectile.Center);
		for (int i = 0; i < 30; i++)
		{
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 3f);
			Main.dust[dust].velocity *= 1.4f;
		}
		for (int i = 0; i < 20; i++)
		{
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 7f;
			dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dust].velocity *= 3f;
		}
		float scaleFactor9 = 0.5f;
		for (int j = 0; j < 4; j++)
		{
			int gore = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, default(Vector2), Main.rand.Next(61, 64));
			Main.gore[gore].velocity *= scaleFactor9;
			Main.gore[gore].velocity.X += 1f;
			Main.gore[gore].velocity.Y += 1f;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (FargoSoulsUtil.BossIsAlive(ref CSENpcs.RealMutantEX, ModContent.NPCType<RealMutantEX>()))
		{
			if (WorldSavingSystem.EternityMode)
			{
				target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300);
				target.AddBuff(ModContent.BuffType<MidasBuff>(), 300);
			}
			target.AddBuff(30, 300);
		}
		if (FargoSoulsUtil.BossIsAlive(ref CSENpcs.RealMutantEX, 551) && WorldSavingSystem.EternityMode)
		{
			target.AddBuff(195, Main.rand.Next(60, 300));
			target.AddBuff(196, Main.rand.Next(60, 300));
			target.AddBuff(67, 300);
		}
		Projectile.timeLeft = 0;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = Color.White;
		color26 = Projectile.GetAlpha(color26);
		color26.A = (byte)Projectile.alpha;
		SpriteEffects effects = ((Projectile.spriteDirection >= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			float lerpamount = 0f;
			if (i > 3 && i < 5)
			{
				lerpamount = 0.6f;
			}
			if (i >= 5)
			{
				lerpamount = 0.8f;
			}
			Color color27 = Color.Lerp(Color.White, Color.Purple, lerpamount) * 0.75f * 0.5f;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			float scale = Projectile.scale * (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, scale, effects, 0);
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color26, Projectile.rotation, origin2, Projectile.scale, effects, 0);
		return false;
	}
}
