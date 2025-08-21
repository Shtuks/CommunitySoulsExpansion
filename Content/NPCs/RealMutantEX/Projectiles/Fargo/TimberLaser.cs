using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class TimberLaser : ModProjectile
{
    public override string Texture => "ssm/Content/NPCs/RealMutantEX/Projectiles/Fargo/TimberLaser";
    public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.timeLeft = 600;
		Projectile.extraUpdates = 4;
		Projectile.ignoreWater = true;
		Projectile.alpha = 255;
		CooldownSlot = 1;
		Projectile.scale = 2f;
	}

	public override void AI()
	{
		NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[0], ModContent.NPCType<RealMutantEX>());
		if (npc != null && Projectile.Colliding(Projectile.Hitbox, npc.Hitbox))
		{
			SoundEngine.PlaySound(SoundID.NPCHit4, (Vector2?)Projectile.Center);
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226, Projectile.velocity.X * 0.4f, (0f - Projectile.velocity.Y) * 0.4f);
			}
			Projectile.Kill();
			return;
		}
		if (Projectile.alpha > 0)
		{
			Projectile.alpha -= 10;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation();
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
		if (Projectile.alpha < 200)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
		}
		return Color.Transparent;
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
		SpriteEffects effects = ((Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
		return false;
	}
}
