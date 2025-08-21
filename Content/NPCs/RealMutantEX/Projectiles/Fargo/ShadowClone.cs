using System;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class ShadowClone : ModProjectile
{
	public override string Texture => "FargowiltasSouls/Content/Bosses/Champions/Shadow/ShadowChampion";

	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 5;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 110;
		Projectile.height = 110;
		Projectile.penetrate = -1;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.aiStyle = -1;
		CooldownSlot = 1;
		Projectile.timeLeft = 720;
	}

	public override void AI()
	{
		Player player = Main.player[(int)Projectile.ai[0]];
		Projectile.direction = (Projectile.spriteDirection = ((Projectile.Center.X < player.Center.X) ? 1 : (-1)));
		for (int i = 0; i < 3; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 27, 0f, 0f, 0, default(Color), 2f);
			Main.dust[d].noGravity = true;
			Main.dust[d].velocity *= 4f;
		}
		for (int i = 0; i < 3; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 54, 0f, 0f, 0, default(Color), 5f);
			Main.dust[d].noGravity = true;
		}
		if ((Projectile.ai[1] -= 1f) > 0f)
		{
			Vector2 targetPos = player.Center + Projectile.DirectionFrom(player.Center) * 400f;
			if (Projectile.Distance(targetPos) > 50f)
			{
				this.Movement(targetPos, 0.3f, 24f);
			}
			Projectile.position += player.velocity * 0.9f;
		}
		else if (Projectile.ai[1] == 0f)
		{
			Projectile.velocity = Vector2.Zero;
			Projectile.position += player.velocity / 4f;
			Projectile.netUpdate = true;
			Projectile.localAI[0] = Projectile.DirectionTo(player.Center).ToRotation();
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.DirectionTo(player.Center), ModContent.ProjectileType<ShadowDeathraySmall>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		else if (Projectile.ai[1] == -30f)
		{
			Projectile.velocity = 45f * Vector2.UnitX.RotatedBy(Projectile.localAI[0]);
		}
		if (++Projectile.frameCounter > 3)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 5)
			{
				Projectile.frame = 0;
			}
		}
	}

	private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
	{
		if (Projectile.Center.X < targetPos.X)
		{
			Projectile.velocity.X += speedModifier;
			if (Projectile.velocity.X < 0f)
			{
				Projectile.velocity.X += speedModifier * 2f;
			}
		}
		else
		{
			Projectile.velocity.X -= speedModifier;
			if (Projectile.velocity.X > 0f)
			{
				Projectile.velocity.X -= speedModifier * 2f;
			}
		}
		if (Projectile.Center.Y < targetPos.Y)
		{
			Projectile.velocity.Y += (fastY ? (speedModifier * 2f) : speedModifier);
			if (Projectile.velocity.Y < 0f)
			{
				Projectile.velocity.Y += speedModifier * 2f;
			}
		}
		else
		{
			Projectile.velocity.Y -= (fastY ? (speedModifier * 2f) : speedModifier);
			if (Projectile.velocity.Y > 0f)
			{
				Projectile.velocity.Y -= speedModifier * 2f;
			}
		}
		if (Math.Abs(Projectile.velocity.X) > cap)
		{
			Projectile.velocity.X = cap * (float)Math.Sign(Projectile.velocity.X);
		}
		if (Math.Abs(Projectile.velocity.Y) > cap)
		{
			Projectile.velocity.Y = cap * (float)Math.Sign(Projectile.velocity.Y);
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(22, 300);
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300);
			target.AddBuff(80, 300);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.Black;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
		GameShaders.Armor.GetShaderFromItemId(3530).Apply(Projectile);
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		Texture2D texture2D14 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Shadow/ShadowChampion_Trail").Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = lightColor;
		color26 = Projectile.GetAlpha(color26);
		SpriteEffects effects = ((Projectile.spriteDirection >= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			Color color27 = Color.White * 0.25f;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D14, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, effects, 0);
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		return false;
	}
}
