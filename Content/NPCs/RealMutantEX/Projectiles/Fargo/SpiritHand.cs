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

public class SpiritHand : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/Champions/Spirit/SpiritChampionHand";

    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 100;
		Projectile.height = 100;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.timeLeft = 300;
		CooldownSlot = 1;
		Projectile.tileCollide = false;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(SoundID.Item8, (Vector2?)Projectile.Center);
			for (int i = 0; i < 5; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 1.5f);
				Main.dust[d].velocity *= 6f;
			}
		}
		if ((Projectile.localAI[0] += 1f) > 30f && Projectile.localAI[0] < 120f)
		{
			Projectile.velocity *= Projectile.ai[0];
		}
		if (Projectile.localAI[0] > 60f && Projectile.localAI[0] < 180f && FargoSoulsUtil.BossIsAlive(ref CSENpcs.RealMutantEX, ModContent.NPCType<RealMutantEX>()))
		{
			float rotation = Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[Main.npc[CSENpcs.RealMutantEX].target].Center - Projectile.Center).ToRotation();
			Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(rotation.AngleLerp(targetAngle, Projectile.ai[1]));
		}
		Projectile.direction = (Projectile.spriteDirection = ((!(Projectile.velocity.X > 0f)) ? 1 : (-1)));
		Projectile.rotation = Projectile.velocity.ToRotation();
		if (Projectile.spriteDirection < 0)
		{
			Projectile.rotation += (float)Math.PI;
		}
		Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 54, 0f, 0f, 0, default(Color), 2f)].noGravity = true;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360);
			target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 180);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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
