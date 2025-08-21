using System;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;
public class AbomDeathrayMark : BaseDeathray
{
	public bool DontS;

    public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomDeathray";

    public AbomDeathrayMark()
		: base(30f)
	{
	}
	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		Vector2? vector78 = null;
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		float num801 = 0.3f;
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] >= maxTime)
		{
			Projectile.Kill();
			return;
		}
		Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * (float)Math.PI / maxTime) * 0.6f * num801;
		if (Projectile.scale > num801)
		{
			Projectile.scale = num801;
		}
		float num805 = 3f;
		_ = Projectile.width;
		_ = Projectile.Center;
		if (vector78.HasValue)
		{
			_ = vector78.Value;
		}
		float[] array3 = new float[(int)num805];
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i] = 3000f;
		}
		float num807 = 0f;
		for (int num808 = 0; num808 < array3.Length; num808++)
		{
			num807 += array3[num808];
		}
		num807 /= num805;
		float amount = 0.5f;
		Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);
		Vector2 vector79 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
		for (int num809 = 0; num809 < 2; num809++)
		{
			float num810 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
			Vector2 vector80 = new Vector2((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
			int num812 = Dust.NewDust(vector79, 0, 0, 244, vector80.X, vector80.Y);
			Main.dust[num812].noGravity = true;
			Main.dust[num812].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
			int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, 244, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num813].velocity *= 0.5f;
			Main.dust[num813].velocity.Y = 0f - Math.Abs(Main.dust[num813].velocity.Y);
		}
		Projectile.position -= Projectile.velocity;
		Projectile.rotation = Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

    public override void Kill(int timeLeft)
    {
        ((AbomDeathray)Projectile.NewProjectileDirect(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AbomDeathray>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0], Projectile.ai[1]).ModProjectile).DontSpawn = this.DontS;
    }
}
