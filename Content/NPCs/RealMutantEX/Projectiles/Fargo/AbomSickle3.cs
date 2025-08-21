using FargowiltasSouls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class AbomSickle3 : AbomSickle
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/AbomBoss/AbomSickle";

    public override void SetDefaults()
	{
		base.SetDefaults();
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = base.Projectile.Center.X;
			base.Projectile.localAI[1] = base.Projectile.Center.Y;
			SoundEngine.PlaySound(SoundID.Item8, (Vector2?)base.Projectile.Center);
		}
		base.Projectile.rotation += 0.8f;
		if (base.Projectile.ai[1] == 0f)
		{
			Player target = FargoSoulsUtil.PlayerExists(base.Projectile.ai[0]);
			if (target != null)
			{
				Vector2 spawnPoint = new Vector2(base.Projectile.localAI[0], base.Projectile.localAI[1]);
				if (base.Projectile.Distance(spawnPoint) > target.Distance(spawnPoint) - 160f)
				{
					base.Projectile.ai[1] = 1f;
					base.Projectile.velocity.Normalize();
					base.Projectile.timeLeft = 300;
					base.Projectile.netUpdate = true;
				}
			}
		}
		else if ((base.Projectile.ai[1] += 1f) < 60f)
		{
			base.Projectile.velocity *= 1.065f;
		}
	}
}
