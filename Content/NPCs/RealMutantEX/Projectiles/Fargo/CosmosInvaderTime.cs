using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;
public class CosmosInvaderTime : CosmosInvader
{
	public bool SpeedUP;

	public override string Texture => "Terraria/Images/Projectile_539";

	public override bool PreAI()
	{
		if (this.SpeedUP)
		{
			Projectile.velocity = ((Projectile.velocity.Length() < 35f) ? (Projectile.velocity * 1.01f) : Projectile.velocity);
		}
		if (!spawned)
		{
			Projectile.localAI[1] = Projectile.velocity.Length();
			Projectile.velocity = Vector2.Zero;
			SoundStyle soundStyle = SoundID.Item25 with
			{
				Volume = 0.5f,
				Pitch = 0f
			};
			SoundEngine.PlaySound(ref soundStyle, (Vector2?)Projectile.Center);
			for (int index1 = 0; index1 < 4; index1++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[index2].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.14159274101257) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
			}
			for (int index1 = 0; index1 < 20; index1++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 176, 0f, 0f, 200, default(Color), 3.7f);
				Main.dust[index2].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.14159274101257) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3f;
			}
			for (int index1 = 0; index1 < 20; index1++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, 0f, 0f, 0, default(Color), 2.7f);
				Main.dust[index2].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.14159274101257).RotatedBy(Projectile.velocity.ToRotation()) * Projectile.width / 2f;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3f;
			}
			for (int index1 = 0; index1 < 10; index1++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[index2].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.14159274101257).RotatedBy(Projectile.velocity.ToRotation()) * Projectile.width / 2f;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3f;
			}
		}
		return PreAI();
	}

	public override void AI()
	{
		if (Projectile.localAI[0] < 60f)
		{
			Projectile.velocity += Projectile.ai[1].ToRotationVector2() * Projectile.localAI[1] / 60f;
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + 1.570796f;
		if (++Projectile.frameCounter >= 4)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
		}
		Projectile.localAI[0] += 1f;
	}
}
