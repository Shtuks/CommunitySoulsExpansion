using System;
using System.Linq;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class WillDeathrayBig : BaseDeathray
{
	public override string Texture => "FargowiltasSouls/Content/Bosses/Champions/Will/WillDeathray";

	public PrimDrawer LaserDrawer { get; private set; }

	public WillDeathrayBig()
		: base(20f, 0f, 1f, 3600)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[Projectile.type] = 5;
	}

	public override bool? CanDamage()
	{
		return Projectile.scale == 10f;
	}

	public override void AI()
	{
		Vector2? vector78 = null;
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		if (Projectile.localAI[0] == 0f && !Main.dedServ)
		{
			SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104");
			SoundEngine.PlaySound(soundStyle, (Vector2?)new Vector2(Projectile.Center.X, Main.LocalPlayer.Center.Y));
		}
		float num801 = 10f;
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] >= maxTime)
		{
			Projectile.Kill();
			return;
		}
		Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * (float)Math.PI / maxTime) * 1.5f * num801;
		if (Projectile.scale > num801)
		{
			Projectile.scale = num801;
		}
		float num804 = Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Projectile.rotation = num804;
		num804 += (float)Math.PI / 2f;
		Projectile.velocity = num804.ToRotationVector2();
		float num805 = 3f;
		_ = Projectile.width;
		_ = Projectile.Center;
		if (vector78.HasValue)
		{
			_ = vector78.Value;
		}
		float[] array3 = new float[(int)num805];
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i] = 4000f;
		}
		float num807 = 0f;
		for (int num808 = 0; num808 < array3.Length; num808++)
		{
			num807 += array3[num808];
		}
		num807 /= num805;
		float amount = 0.5f;
		Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);
		Vector2 vector79 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
		for (int num809 = 0; num809 < 2; num809++)
		{
			float num810 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
			Vector2 vector80 = new Vector2((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
			int num812 = Dust.NewDust(vector79, 0, 0, 228, vector80.X, vector80.Y);
			Main.dust[num812].noGravity = true;
			Main.dust[num812].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
			int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, 228, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num813].velocity *= 0.5f;
			Main.dust[num813].velocity.Y = 0f - Math.Abs(Main.dust[num813].velocity.Y);
		}
		Projectile.position -= Projectile.velocity;
		if (Main.LocalPlayer.active && !Main.dedServ)
		{
			Main.LocalPlayer.GetModPlayer<CSEPlayer>().Screenshake = 10;
			if (Projectile.localAI[0] < maxTime / 2f)
			{
				for (int i = 0; (float)i < array3[0]; i += 100)
				{
					float offset = (float)i + Main.rand.NextFloat(-100f, 100f);
					Vector2 spawnPos = Projectile.position + Projectile.velocity * offset;
					if (!(Math.Abs(spawnPos.Y - Main.LocalPlayer.Center.Y) > (float)Main.screenHeight * 0.75f))
					{
						int d = Dust.NewDust(spawnPos, Projectile.width, Projectile.height, 228, 0f, 0f, 0, default(Color), 6f);
						Main.dust[d].noGravity = Main.rand.NextBool();
						Main.dust[d].velocity += Projectile.velocity.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-6f, 6f);
						Main.dust[d].velocity *= Main.rand.NextFloat(1f, 3f);
					}
				}
			}
		}
		if (++Projectile.frame >= Main.projFrames[Projectile.type])
		{
			Projectile.frame = 0;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300);
			target.AddBuff(ModContent.BuffType<MidasBuff>(), 300);
		}
		target.AddBuff(30, 300);
	}

    public float WidthFunction(float _) => Projectile.width * Projectile.scale * 2;

    public static Color ColorFunction(float _) => new(253, 254, 32, 100);

    public override bool PreDraw(ref Color lightColor)
    {
        // This should never happen, but just in case.
        if (Projectile.velocity == Vector2.Zero)
            return false;

        ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.WillBigDeathray");

        // Get the laser end position.
        Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * drawDistance;

        // Create 8 points that span across the draw distance from the projectile center.

        // This allows the drawing to be pushed back, which is needed due to the shader fading in at the start to avoid
        // sharp lines.
        Vector2 initialDrawPoint = Projectile.Center - Projectile.velocity * 150f;
        Vector2[] baseDrawPoints = new Vector2[8];
        for (int i = 0; i < baseDrawPoints.Length; i++)
            baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

        // Set shader parameters. This one takes a fademap and a color.

        // The laser should fade to this in the middle.
        Color brightColor = new(252, 252, 192, 100);
        shader.TrySetParameter("mainColor", brightColor);
        // GameShaders.Misc["FargoswiltasSouls:MutantDeathray"].UseImage1(); cannot be used due to only accepting vanilla paths.
        Texture2D fademap = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak").Value;
        FargoSoulsUtil.SetTexture1(fademap);

        PrimitiveRenderer.RenderTrail(baseDrawPoints, new(WidthFunction, ColorFunction, Shader: shader), 30);
        return false;
    }
}
