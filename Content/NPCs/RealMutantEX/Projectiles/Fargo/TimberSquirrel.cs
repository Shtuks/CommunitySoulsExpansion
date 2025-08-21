using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

internal class TimberSquirrel : ModProjectile
{
	public int Counter;

	private const int baseTimeleft = 120;

	private NPC npc;

	private bool spawned;

	public override string Texture => "FargowiltasSouls/Content/Items/Weapons/Misc/TophatSquirrelWeapon";

	private bool EvilSqurrl => Projectile.ai[0] != 0f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 19;
		Projectile.height = 19;
		Projectile.hostile = true;
		Projectile.timeLeft = 120;
		Projectile.penetrate = -1;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = false;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		npc = FargoSoulsUtil.NPCExists(Projectile.ai[1]);
	}

	public override void AI()
	{
		if (!spawned)
		{
			spawned = true;
			for (int i = 0; i < 50; i++)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0f, 0f, 0, default(Color), 3f);
				Main.dust[d].noGravity = true;
				Main.dust[d].velocity *= 3f;
				Main.dust[d].velocity += Projectile.velocity * Main.rand.NextFloat(9f);
			}
		}
		Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
		Projectile.rotation += 0.2f * (float)Projectile.spriteDirection;
		if (++Counter >= 45)
		{
			Projectile.scale += 0.1f;
		}
		if (!EvilSqurrl)
		{
			return;
		}
		Projectile.timeLeft++;
		FargoSoulsUtil.AuraDust(Projectile, 1000f, 156, Color.White, reverse: true, 1f - Projectile.localAI[1] / 20f);
		if (Counter == 120 && Main.netMode != 1)
		{
			Projectile.NewProjectile(Terraria.Entity.InheritSource((npc != null) ? ((Entity)npc) : ((Entity)Projectile)), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<TimberLightningOrb>(), Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
		}
		if (Counter <= 120)
		{
			return;
		}
		Projectile.hide = true;
		Projectile.position -= Projectile.velocity;
		Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitX);
		Projectile.rotation += 0.1349578f * (float)Math.Sign(Projectile.velocity.X);
		if (!((Projectile.localAI[0] -= 1f) < 0f))
		{
			return;
		}
		Projectile.localAI[0] = 5f;
		SoundEngine.PlaySound(SoundID.Item157, (Vector2?)Projectile.Center);
		int max = 8;
		for (int i = 0; i < max; i++)
		{
			float ai0 = ((npc != null) ? npc.whoAmI : (-1));
			float rotationBaseOffset = (float)Math.PI * 2f / (float)max;
			Vector2 vel = 4f * (Projectile.rotation + rotationBaseOffset * (float)i).ToRotationVector2();
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(Terraria.Entity.InheritSource((npc != null) ? ((Entity)npc) : ((Entity)Projectile)), Projectile.Center, vel, ModContent.ProjectileType<TimberLaser>(), Projectile.damage, 0f, Main.myPlayer, ai0, 0f);
			}
			if (npc != null)
			{
				float edgeRotation = Projectile.rotation + rotationBaseOffset * ((float)i + 0.5f);
				Vector2 spawnPos = Projectile.Center + 1000f * edgeRotation.ToRotationVector2();
				Vector2 edgeVel = (Main.player[npc.target].Center + Main.player[npc.target].velocity * Main.rand.NextFloat(30f) + Main.rand.NextVector2Circular(128f, 128f) - spawnPos).SafeNormalize(Vector2.UnitY);
				float angleDifference = MathHelper.WrapAngle(edgeVel.ToRotation() - edgeRotation);
				if (Math.Abs(angleDifference) > (float)Math.PI / 2f)
				{
					edgeVel = (edgeRotation + (float)Math.PI / 2f * (float)Math.Sign(angleDifference)).ToRotationVector2();
				}
				edgeVel *= 12f;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(Terraria.Entity.InheritSource(npc), spawnPos, edgeVel, ModContent.ProjectileType<TimberLaser>(), Projectile.damage, 0f, Main.myPlayer, ai0, 0f);
				}
			}
		}
		if ((Projectile.localAI[1] += 1f) >= 60f)
		{
			Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
		int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		int y3 = num156 * Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		SpriteEffects effects = ((Projectile.spriteDirection > 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		if (EvilSqurrl)
		{
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 3)
			{
				Color color27 = new Color(93, 255, 241, 0);
				color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
				Vector2 value4 = Projectile.oldPos[i];
				float num165 = Projectile.oldRot[i];
				Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale * 1.1f, effects, 0);
			}
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
		return false;
	}
}
