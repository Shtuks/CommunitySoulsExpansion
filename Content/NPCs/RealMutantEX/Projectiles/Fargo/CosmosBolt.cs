using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class CosmosBolt : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_462";

	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 5;
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.aiStyle = 1;
		AIType = 462;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.scale = 2f;
		Projectile.hostile = true;
		Projectile.extraUpdates = 3;
		Projectile.timeLeft = 300;
		CooldownSlot = 1;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
	}

	public override void AI()
	{
		int index = Dust.NewDust(Projectile.Center, 0, 0, 229, 0f, 0f, 100);
		Main.dust[index].noLight = true;
		Main.dust[index].noGravity = true;
		Main.dust[index].velocity = Projectile.velocity;
		Main.dust[index].position -= Vector2.One * 4f;
		Main.dust[index].scale = 0.8f;
		if (WorldSavingSystem.EternityMode && FargoSoulsUtil.BossIsAlive(ref CSENpcs.RealMutantEX, ModContent.NPCType<RealMutantEX>()))
		{
			float rotation = Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[Main.npc[CSENpcs.RealMutantEX].target].Center - Projectile.Center).ToRotation();
			Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(rotation.AngleLerp(targetAngle, 0.001f));
		}
	}

	public override void Kill(int timeLeft)
	{
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<CosmosDeathray>(), Projectile.damage, 0f, Main.myPlayer, 1f, 0f);
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 128) * (1f - (float)Projectile.alpha / 255f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
