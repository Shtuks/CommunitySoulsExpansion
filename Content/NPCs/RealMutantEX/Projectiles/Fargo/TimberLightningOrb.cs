using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class TimberLightningOrb : ModProjectile
{
	private int originalSize = 70;

	private NPC npc;

	private bool spawned;

	public override string Texture => "Terraria/Images/Projectile_465";

	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		Projectile.width = (Projectile.height = originalSize);
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 360;
		Projectile.penetrate = -1;
		Projectile.scale = 0.1f;
		CooldownSlot = 1;
		Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override bool? CanDamage()
	{
		return Projectile.alpha == 0;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		return Projectile.Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, Projectile.Center)) <= (float)(Projectile.width / 2);
	}

	public override void OnSpawn(IEntitySource source)
	{
		if (source is EntitySource_Parent { Entity: NPC sourceNPC })
		{
			npc = sourceNPC;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write((npc != null) ? npc.whoAmI : (-1));
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		npc = FargoSoulsUtil.NPCExists(reader.ReadInt32());
	}

	public override void AI()
	{
		if (!spawned)
		{
			spawned = true;
			if (!Main.dedServ)
			{
				SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Thunder");
				soundStyle.Volume = 0.5f;
				SoundEngine.PlaySound(ref soundStyle, (Vector2?)Projectile.Center);
			}
		}
		Projectile.rotation += Main.rand.NextFloat(-0.2f, 0.2f);
		Projectile.position = Projectile.Center;
		Projectile.scale += 0.3f;
		if (Projectile.scale > 3f)
		{
			Projectile.scale = 3f;
		}
		Projectile.width = (int)((float)originalSize * Projectile.scale);
		Projectile.height = (int)((float)originalSize * Projectile.scale);
		Projectile.Center = Projectile.position;
		if (Projectile.timeLeft < 30)
		{
			Projectile.alpha += 10;
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
				Projectile.Kill();
			}
		}
		if (++Projectile.frameCounter > 2)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
		}
		if (Main.rand.NextBool(3))
		{
			float num11 = (float)(Main.rand.NextDouble() * 1.0 - 0.5);
			if ((double)num11 < -0.5)
			{
				num11 = -0.5f;
			}
			if ((double)num11 > 0.5)
			{
				num11 = 0.5f;
			}
			Vector2 vector21 = new Vector2((float)(-Projectile.width) * 0.2f * Projectile.scale, 0f).RotatedBy((double)num11 * 6.28318548202515).RotatedBy(Projectile.velocity.ToRotation());
			int index21 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, 226, (float)((0.0 - (double)Projectile.velocity.X) / 3.0), (float)((0.0 - (double)Projectile.velocity.Y) / 3.0), 150, Color.Transparent, 0.7f);
			Main.dust[index21].position = Projectile.Center + vector21 * Projectile.scale;
			Main.dust[index21].velocity = Vector2.Normalize(Main.dust[index21].position - Projectile.Center) * 2f;
			Main.dust[index21].noGravity = true;
			float num1 = (float)(Main.rand.NextDouble() * 1.0 - 0.5);
			if ((double)num1 < -0.5)
			{
				num1 = -0.5f;
			}
			if ((double)num1 > 0.5)
			{
				num1 = 0.5f;
			}
			Vector2 vector2 = new Vector2((float)(-Projectile.width) * 0.6f * Projectile.scale, 0f).RotatedBy((double)num1 * 6.28318548202515).RotatedBy(Projectile.velocity.ToRotation());
			int index2 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, 226, (float)((0.0 - (double)Projectile.velocity.X) / 3.0), (float)((0.0 - (double)Projectile.velocity.Y) / 3.0), 150, Color.Transparent, 0.7f);
			Main.dust[index2].velocity = Vector2.Zero;
			Main.dust[index2].position = Projectile.Center + vector2 * Projectile.scale;
			Main.dust[index2].noGravity = true;
		}
		float distance = npc.width / 2 + Projectile.width;
		if (Projectile.Distance(npc.Center) < distance)
		{
			Vector2 target = Projectile.Center + Projectile.DirectionTo(npc.Center) * distance;
			npc.Center = Vector2.Lerp(npc.Center, target, 0.1f);
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 300);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
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
