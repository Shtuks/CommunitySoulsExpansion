using System;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class CosmosDeathray : BaseDeathray
{
	private bool hasHit;

	public override string Texture => "ssm/Content/NPCs/RealMutantEX/Projectiles/Fargo/ShadowDeathraySmall";

	public CosmosDeathray()
		: base(20f, 0f, 1f, 3600)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.friendly = true;
		base.Projectile.trap = true;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.scale >= 1f && !this.hasHit;
	}

	public override void AI()
	{
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			Vector2 spawnPos = base.Projectile.Center;
			if (base.Projectile.ai[0] == 0f)
			{
				spawnPos += base.Projectile.velocity * 3000f;
			}
			SoundEngine.PlaySound(SoundID.Item12, (Vector2?)spawnPos);
			SoundEngine.PlaySound(SoundID.Item14, (Vector2?)spawnPos);
		}
		float num801 = 1f;
		base.Projectile.localAI[0] += 1f;
		if (base.Projectile.localAI[0] >= base.maxTime)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / base.maxTime) * 1f * num801;
		if (base.Projectile.scale > num801)
		{
			base.Projectile.scale = num801;
		}
		float num804 = base.Projectile.velocity.ToRotation();
		base.Projectile.rotation = num804 - (float)Math.PI / 2f;
		base.Projectile.velocity = num804.ToRotationVector2();
		float num805 = 3f;
		_ = base.Projectile.width;
		_ = base.Projectile.Center;
		if (vector78.HasValue)
		{
			_ = vector78.Value;
		}
		float[] array3 = new float[(int)num805];
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i] = ((base.Projectile.ai[0] == 0f) ? 6000f : 3000f);
		}
		float num807 = 0f;
		for (int num808 = 0; num808 < array3.Length; num808++)
		{
			num807 += array3[num808];
		}
		num807 /= num805;
		float amount = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], num807, amount);
		Vector2 vector79 = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14f);
		for (int num809 = 0; num809 < 2; num809++)
		{
			float num810 = base.Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
			Vector2 vector80 = new Vector2((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
			int num812 = Dust.NewDust(vector79, 0, 0, 244, vector80.X, vector80.Y);
			Main.dust[num812].noGravity = true;
			Main.dust[num812].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value29 = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * base.Projectile.width;
			int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, 244, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num813].velocity *= 0.5f;
			Main.dust[num813].velocity.Y = 0f - Math.Abs(Main.dust[num813].velocity.Y);
		}
		base.Projectile.position -= base.Projectile.velocity;
		if (base.Projectile.scale == 1f)
		{
			base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD = 0;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
		this.hasHit = true;
	}
}
