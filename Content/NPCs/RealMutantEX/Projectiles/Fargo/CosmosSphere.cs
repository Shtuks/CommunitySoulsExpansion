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
public class CosmosSphere : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_454";

	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 2;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 46;
		Projectile.height = 46;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 480;
		CooldownSlot = 1;
	}

	public override bool? CanDamage()
	{
		return Projectile.ai[0] <= 0f;
	}

	public override void AI()
	{
		for (int i = 0; i < 2; i++)
		{
			float num = Main.rand.NextFloat(-0.5f, 0.5f);
			Vector2 vector2 = new Vector2((float)(-Projectile.width) * 0.65f * Projectile.scale, 0f).RotatedBy((double)num * 6.28318548202515).RotatedBy(Projectile.velocity.ToRotation());
			int index2 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, 229, (0f - Projectile.velocity.X) / 3f, (0f - Projectile.velocity.Y) / 3f, 150, Color.Transparent, 0.7f);
			Main.dust[index2].velocity = Vector2.Zero;
			Main.dust[index2].position = Projectile.Center + vector2;
			Main.dust[index2].noGravity = true;
		}
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = -Math.Sign(Projectile.velocity.Y);
		}
		if (Projectile.timeLeft % Projectile.MaxUpdates != 0)
		{
			return;
		}
		if (Projectile.alpha > 0)
		{
			Projectile.alpha -= 11;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
		}
		Projectile.scale = 1f - (float)Projectile.alpha / 255f;
		if (++Projectile.frameCounter >= 6)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame > 1)
			{
				Projectile.frame = 0;
			}
		}
		if ((Projectile.ai[0] -= 1f) == 0f)
		{
			Projectile.velocity = Vector2.Zero;
			Projectile.netUpdate = true;
		}
		if ((Projectile.ai[1] -= 1f) == 0f)
		{
			Projectile.velocity.Y = 60f / (float)Projectile.MaxUpdates * Projectile.localAI[0];
			Projectile.netUpdate = true;
		}
		NPC eridanus = FargoSoulsUtil.NPCExists(CSENpcs.RealMutantEX, ModContent.NPCType<RealMutantEX>());
		if (Projectile.ai[1] > 0f && eridanus != null && eridanus.HasValidTarget)
		{
			float modifier = Projectile.ai[1] / 60f;
			if (modifier < 0f)
			{
				modifier = 0f;
			}
			if (modifier > 1f)
			{
				modifier = 1f;
			}
			Projectile.position.Y += (Main.player[eridanus.target].position.Y - Main.player[eridanus.target].oldPosition.Y) * 0.6f * modifier;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
		}
	}

	public override void Kill(int timeleft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath6, (Vector2?)Projectile.Center);
		Projectile.position = Projectile.Center;
		Projectile.width = (Projectile.height = 208);
		Projectile.Center = Projectile.position;
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index2].position = new Vector2(Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + Projectile.Center;
		}
		for (int index1 = 0; index1 < 10; index1++)
		{
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[index2].position = new Vector2(Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + Projectile.Center;
			Main.dust[index2].noGravity = true;
			Main.dust[index2].velocity *= 1f;
			int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index3].position = new Vector2(Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + Projectile.Center;
			Main.dust[index3].velocity *= 1f;
			Main.dust[index3].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * Projectile.Opacity;
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
		SpriteEffects effects = ((Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			Color color27 = color26 * 0.5f;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, effects, 0);
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
		return false;
	}
}
