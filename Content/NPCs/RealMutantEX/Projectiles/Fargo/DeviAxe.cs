using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class DeviAxe : ModProjectile
{
	public override string Texture => "FargowiltasSouls/Content/Projectiles/Empty";
	public override void SetDefaults()
	{
		Projectile.width = 46;
		Projectile.height = 46;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 180;
		Projectile.hide = true;
		Projectile.penetrate = -1;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		CooldownSlot = 1;
	}

	public override void AI()
	{
		NPC npc = Main.npc[(int)Projectile.ai[0]];
		if (npc != null && npc.type == ModContent.NPCType<RealMutantEX>())
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				Projectile.localAI[1] = Projectile.DirectionFrom(npc.Center).ToRotation();
			}
			Vector2 offset = new Vector2(Projectile.ai[1], 0f).RotatedBy(((RealMutantEX)npc.ModNPC).NewAI[3] + Projectile.localAI[1]);
			Projectile.Center = npc.Center + offset;
		}
		else
		{
			Projectile.Kill();
		}
	}

	public override void Kill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath6, (Vector2?)Projectile.Center);
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.velocity.X = ((target.Center.X < Main.npc[(int)Projectile.ai[0]].Center.X) ? (-15f) : 15f);
		target.velocity.Y = -10f;
		target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240);
	}
}
