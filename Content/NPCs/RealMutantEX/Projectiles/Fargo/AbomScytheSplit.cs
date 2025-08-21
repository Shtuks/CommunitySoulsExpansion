using System;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class AbomScytheSplit : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_274";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.hostile = true;
		Projectile.penetrate = -1;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 900;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		CooldownSlot = 1;
		Projectile.scale = 2f;
	}

	public override void AI()
	{
		Projectile.rotation += 1f;
		if ((Projectile.ai[0] -= 1f) <= -300f)
		{
			Projectile.Kill();
		}
	}

	public override void Kill(int timeLeft)
	{
		int dustMax = ((Projectile.ai[1] >= 0f) ? 50 : 25);
		float speed = ((Projectile.ai[1] >= 0f) ? 15 : 6);
		for (int i = 0; i < dustMax; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 70, 0f, 0f, 0, default(Color), 3.5f);
			Main.dust[d].velocity *= speed;
			Main.dust[d].noGravity = true;
		}
		if (!(Projectile.ai[1] >= 0f) || Main.netMode == 1)
		{
			return;
		}
		int p = Player.FindClosest(Projectile.Center, 0, 0);
		if (p != -1)
		{
			Vector2 vel = ((Projectile.ai[1] == 0f) ? Vector2.Normalize(Projectile.velocity) : Projectile.DirectionTo(Main.player[p].Center));
			vel *= 30f;
			int max = ((Projectile.ai[1] == 0f) ? 6 : (WorldSavingSystem.MasochistModeReal ? 10 : 8));
			for (int i = 0; i < max; i++)
			{
				Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, vel.RotatedBy((float)Math.PI * 2f / (float)max * (float)i), ModContent.ProjectileType<AbomSickle3>(), Projectile.damage, Projectile.knockBack, Projectile.owner, (float)p, 0f);
			}
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300);
			target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120);
		}
		target.AddBuff(30, 600);
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
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			Color color27 = color26;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 0) * Projectile.Opacity;
	}
}
