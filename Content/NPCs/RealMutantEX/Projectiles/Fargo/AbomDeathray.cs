using System;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class AbomDeathray : BaseDeathray
{
	private Vector2 spawnPos;

	public bool DontSpawn;
    public bool fadeStart = false;
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomDeathray";
    public AbomDeathray()
		: base(120f)
	{
	}

	public override void AI()
	{
		if (!Main.dedServ && Main.LocalPlayer.active)
		{
			Main.LocalPlayer.GetModPlayer<CSEPlayer>().Screenshake = 2;
		}
		Vector2? vector78 = null;
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		if (Projectile.localAI[0] == 0f)
		{
			if (!Main.dedServ)
			{
				SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104");
				soundStyle.Volume = 0.5f;
				SoundEngine.PlaySound(ref soundStyle, (Vector2?)Projectile.Center);
			}
			spawnPos = Projectile.Center;
		}
		else
		{
			Projectile.Center = spawnPos + Main.rand.NextVector2Circular(5f, 5f);
		}
		float num801 = 5f;
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] >= maxTime)
		{
			Projectile.Kill();
			return;
		}
		Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * (float)Math.PI / maxTime) * num801 * 6f;
		if (Projectile.scale > num801)
		{
			Projectile.scale = num801;
		}
		if (Projectile.localAI[0] > maxTime / 2f && Projectile.scale < num801 && Projectile.ai[0] > 0f && !this.DontSpawn)
		{
			if (Main.netMode != 1)
			{
				for (int i = Main.rand.Next(120); i < 3000; i += 500)
				{
					Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center + Projectile.velocity * i, Vector2.Zero, ModContent.ProjectileType<AbomScytheSplit>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0], -1f);
				}
			}
			Projectile.ai[0] = 0f;
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

    public float WidthFunction(float _) => Projectile.width * Projectile.scale * 2;
    public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300);
			target.AddBuff(67, 180);
		}
		target.AddBuff(195, 600);
		target.AddBuff(196, 600);
	}

    public override bool PreDraw(ref Color lightColor)
    {
        AbomSword.DrawStyxGazerDeathray(Projectile, drawDistance, WidthFunction, false, fadeStart);
        return false;
    }
}
