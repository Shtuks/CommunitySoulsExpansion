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
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class WillDeathraySmall : BaseDeathray
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomDeathray";

    public PrimDrawer LaserDrawer { get; private set; }

	public WillDeathraySmall()
		: base(60f, 0f, 1f, 3600)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		Vector2? vector78 = null;
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[1], ModContent.NPCType<RealMutantEX>());
		if (npc != null && ((npc.ai[0] == 2f && npc.ai[1] < 30f) || (npc.ai[0] == -1f && npc.ai[1] < 10f)))
		{
			Projectile.Kill();
			return;
		}
		if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = -Vector2.UnitY;
		}
		float num801 = 0.2f;
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] >= maxTime)
		{
			Projectile.Kill();
			return;
		}
		Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * (float)Math.PI / maxTime) * 2.5f * num801;
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
			float num810 = Projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
			Vector2 vector80 = new Vector2((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
			int num812 = Dust.NewDust(vector79, 0, 0, 244, vector80.X, vector80.Y);
			Main.dust[num812].noGravity = true;
			Main.dust[num812].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
			int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, 244, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num813].velocity *= 0.5f;
			Main.dust[num813].velocity.Y = 0f - Math.Abs(Main.dust[num813].velocity.Y);
		}
		Projectile.position.X = Projectile.ai[0];
		Projectile.position.X += Main.rand.NextFloat(-1f, 1f) * Main.rand.NextFloat(160f) * (1f - Projectile.localAI[0] / maxTime);
		Projectile.position -= Projectile.velocity;
	}

	public override void Kill(int timeLeft)
	{
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<WillDeathrayBig>(), Projectile.damage, 0f, Main.myPlayer, 0f, Projectile.ai[1]);
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

    public float WidthFunction(float _) => Projectile.width * Projectile.scale * 3;

    public static Color ColorFunction(float _) => new(253, 254, 32, 100);

    public override bool PreDraw(ref Color lightColor)
    {
        // This should never happen, but just in case.
        if (Projectile.velocity == Vector2.Zero)
            return false;

        ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.WillDeathray");

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
