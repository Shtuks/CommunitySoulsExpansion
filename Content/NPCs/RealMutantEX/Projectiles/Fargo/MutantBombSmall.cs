using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class MutantBombSmall : MutantBomb
{
    public override string Texture => "Terraria/Images/Projectile_645";

	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 275;
		Projectile.height = 275;
		Projectile.scale = 0.75f;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = false;
	}

	public override bool? CanDamage()
	{
		if (Projectile.frame > 2 && Projectile.frame <= 4)
		{
			Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD = 1;
			return false;
		}
		return true;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
			Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			SoundEngine.PlaySound(SoundID.Item14, (Vector2?)Projectile.Center);
		}
		if (++Projectile.frameCounter >= 3)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame--;
				Projectile.Kill();
			}
		}
	}
}
