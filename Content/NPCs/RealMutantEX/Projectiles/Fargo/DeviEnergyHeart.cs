using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class DeviEnergyHeart : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/DeviBoss/DeviEnergyHeart";
    public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.penetrate = -1;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.aiStyle = -1;
		CooldownSlot = 1;
		Projectile.alpha = 150;
		Projectile.timeLeft = 90;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item44, (Vector2?)Projectile.Center);
		}
		if (Projectile.alpha >= 60)
		{
			Projectile.alpha -= 10;
		}
		Projectile.rotation = Projectile.ai[0];
		Projectile.scale += 0.01f;
		float speed = Projectile.velocity.Length();
		speed += Projectile.ai[1];
		Projectile.velocity = Vector2.Normalize(Projectile.velocity) * speed;
	}

	public override void Kill(int timeLeft)
	{
		FargoSoulsUtil.HeartDust(Projectile.Center, Projectile.rotation + (float)Math.PI / 2f);
        for (int i = 0; i < 4; i++)
        {
            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.UnitX.RotatedBy(Projectile.rotation + (float)Math.PI / 2f * (float)i), ModContent.ProjectileType<DeviDeathray>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * Projectile.Opacity;
	}
}
