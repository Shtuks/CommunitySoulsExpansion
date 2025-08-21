using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Buffs.Boss;
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

public class AbomSickle : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/AbomBoss/AbomSickle";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.alpha = 100;
		Projectile.hostile = true;
		Projectile.timeLeft = 300;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		CooldownSlot = 1;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item8, (Vector2?)Projectile.Center);
		}
		Projectile.rotation += 0.8f;
		if ((Projectile.localAI[1] += 1f) < 90f)
		{
			Projectile.velocity *= 1.045f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
		{
			Color color27 = color26;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
			Vector2 value4 = Projectile.oldPos[i];
			float num165 = Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
		return false;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300);
			target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120);
		}
		target.AddBuff(30, 600);
	}
}
