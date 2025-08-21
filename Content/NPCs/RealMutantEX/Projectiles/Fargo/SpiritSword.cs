using System;
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

public class SpiritSword : ModProjectile
{
	public override string Texture => "Terraria/Images/Item_368";

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.timeLeft = 600;
		Projectile.scale = 1.5f;
		Projectile.hide = true;
		CooldownSlot = 1;
		Projectile.tileCollide = false;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = (Main.rand.NextBool() ? 1 : (-1));
			Projectile.rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			Projectile.hide = false;
		}
		if (Projectile.ai[0] == 0f)
		{
			Projectile.tileCollide = false;
			Projectile.velocity -= new Vector2(Projectile.ai[1], 0f).RotatedBy(Projectile.velocity.ToRotation());
			Projectile.rotation += Projectile.velocity.Length() * 0.1f * Projectile.localAI[0];
			if (Projectile.velocity.Length() < 1f)
			{
				int p = Player.FindClosest(Projectile.Center, 0, 0);
				if (p != -1)
				{
					Projectile.velocity = Projectile.DirectionTo(Main.player[p].Center) * 30f;
					Projectile.ai[0] = 1f;
					Projectile.netUpdate = true;
					SoundEngine.PlaySound(SoundID.Item1, (Vector2?)Projectile.Center);
				}
				Projectile.ai[1] = Main.rand.Next(2);
			}
		}
		else
		{
			if (!Projectile.tileCollide && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.tileCollide = true;
			}
			if (Projectile.velocity != Vector2.Zero)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI * 3f / 4f;
			}
		}
	}

	public override void Kill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Dig, (Vector2?)Projectile.Center);
		for (int i = 0; i < 16; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 87);
			Main.dust[d].noGravity = true;
			Main.dust[d].velocity *= 3f;
			Main.dust[d].scale *= 1.3f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity != Vector2.Zero)
		{
			Projectile.velocity = Vector2.Zero;
			SoundEngine.PlaySound(SoundID.Dig, (Vector2?)Projectile.Center);
			for (int i = 0; i < 10; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 87);
				Main.dust[d].noGravity = true;
				Main.dust[d].velocity *= 1.5f;
				Main.dust[d].scale *= 0.9f;
			}
		}
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 2;
		height = 2;
		fallThrough = Projectile.ai[1] == 0f;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360);
			target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 180);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = lightColor;
		color26 = Projectile.GetAlpha(color26);
		SpriteEffects effects = ((Projectile.spriteDirection >= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			Color color27 = Color.White * Projectile.Opacity * 0.75f * 0.5f;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, effects, 0);
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
		return false;
	}
}
