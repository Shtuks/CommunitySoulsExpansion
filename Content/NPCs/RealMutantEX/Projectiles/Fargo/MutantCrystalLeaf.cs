using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class MutantCrystalLeaf : ModProjectile
{
	public override string Texture => "FargowiltasSouls/Content/NPCs/EternityModeNPCs/CrystalLeaf";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.hostile = true;
		Projectile.timeLeft = 900;
		Projectile.aiStyle = -1;
		Projectile.scale = 2.5f;
		CooldownSlot = 1;
	}

	public override void AI()
	{
		if ((Projectile.localAI[0] += 1f) == 0f)
		{
			for (int index1 = 0; index1 < 30; index1++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 157, 0f, 0f, 0, default(Color), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 5f;
			}
		}
		Lighting.AddLight(Projectile.Center, 0.1f, 0.4f, 0.2f);
		Projectile.scale = ((float)(int)Main.mouseTextColor / 200f - 0.35f) * 0.2f + 0.95f;
		Projectile.scale *= 2.5f;
		int byIdentity = FargoSoulsUtil.GetProjectileByIdentity(Projectile.owner, (int)Projectile.ai[0], ModContent.ProjectileType<MutantMark2>());
		if (byIdentity != -1)
		{
			Vector2 offset = new Vector2(100f, 0f).RotatedBy(Projectile.ai[1]);
			Projectile.Center = Main.projectile[byIdentity].Center + offset;
			Projectile.localAI[1] = Math.Max(0f, 150f - Main.projectile[byIdentity].ai[1]) / 150f;
			Projectile.ai[1] += 0.15f * Projectile.localAI[1];
			if (Projectile.localAI[1] > 1f)
			{
				Projectile.localAI[1] = 1f;
			}
		}
		Projectile.rotation = Projectile.ai[1] + (float)Math.PI / 2f;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(20, Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<InfestedBuff>(), Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override Color? GetAlpha(Color drawColor)
	{
		float num4 = (float)(int)Main.mouseTextColor / 200f - 0.3f;
		int num5 = (int)(255f * num4) + 50;
		if (num5 > 255)
		{
			num5 = 255;
		}
		return new Color(num5, num5, num5, 200);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = lightColor;
		color26 = Projectile.GetAlpha(color26);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			Color color27 = Color.White * Projectile.Opacity * Projectile.localAI[1];
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
