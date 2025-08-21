using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class MutantFishronRitual : ModProjectile
{
	private const int safeRange = 150;

    public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FishronRitual";

    public override void SetDefaults()
	{
		Projectile.width = 320;
		Projectile.height = 320;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 600;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
		CooldownSlot = -1;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCheck = (Projectile projectile) => this.CanDamage() == true && Math.Abs((Main.LocalPlayer.Center - Projectile.Center).Length() - 150f) < 42f + Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().GrazeRadius;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override bool? CanDamage()
	{
		return (float)Projectile.alpha == 0f && Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantBomb>()] > 0;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if ((projHitbox.Center.ToVector2() - targetHitbox.Center.ToVector2()).Length() < 150f)
		{
			return false;
		}
		int clampedX = projHitbox.Center.X - targetHitbox.Center.X;
		int clampedY = projHitbox.Center.Y - targetHitbox.Center.Y;
		if (Math.Abs(clampedX) > targetHitbox.Width / 2)
		{
			clampedX = targetHitbox.Width / 2 * Math.Sign(clampedX);
		}
		if (Math.Abs(clampedY) > targetHitbox.Height / 2)
		{
			clampedY = targetHitbox.Height / 2 * Math.Sign(clampedY);
		}
		int num = projHitbox.Center.X - targetHitbox.Center.X - clampedX;
		int dY = projHitbox.Center.Y - targetHitbox.Center.Y - clampedY;
		return Math.Sqrt(num * num + dY * dY) <= 1200.0;
	}

	public override void AI()
	{
		NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[0], ModContent.NPCType<RealMutantEX>());
		if (npc != null && npc.ai[0] == 34f)
		{
			Projectile.alpha -= 7;
			Projectile.timeLeft = 300;
			Projectile.Center = npc.Center;
			Projectile.position.Y -= 100f;
		}
		else
		{
			Projectile.alpha += 17;
		}
		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
		}
		if (Projectile.alpha > 255)
		{
			Projectile.alpha = 255;
			Projectile.Kill();
			return;
		}
		Projectile.scale = 1f - (float)Projectile.alpha / 255f;
		Projectile.rotation += (float)Math.PI / 70f;
		Lighting.AddLight(Projectile.Center, 0.4f, 0.9f, 1.1f);
		if (Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD > 10)
		{
			Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD = 10;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
