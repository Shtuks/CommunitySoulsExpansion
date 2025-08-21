using System;
using FargowiltasSouls.Assets.ExtraTextures;
using System.Collections.Generic;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class AbomRitual : BaseArena
{
	private const float realRotation = (float)Math.PI / 180f;

    public float VisualScale = 0f;
    public override string Texture => "Terraria/Images/Projectile_274";

	public AbomRitual()
		: base(realRotation, 1400f, ModContent.NPCType<RealMutantEX>(), 87)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	protected override void Movement(NPC npc)
	{
		if (npc.ai[0] < 9f)
		{
			Projectile.velocity = npc.Center - Projectile.Center;
			if (npc.ai[0] != 8f)
			{
				Projectile.velocity /= 40f;
			}
			rotationPerTick = (float)Math.PI / 180f;
		}
		else
		{
			Projectile.velocity = Vector2.Zero;
			rotationPerTick = -0.0017453292f;
		}
	}

	public override void AI()
	{
		base.AI();
		Projectile.rotation += 1f;
	}

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
		base.OnHitPlayer(target, info);
		if (WorldSavingSystem.EternityMode)
		{
			target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300);
			target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120);
		}
		target.AddBuff(30, 600);
	}

    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
    {
        if (Projectile.hide)
            behindNPCsAndTiles.Add(index);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 auraPos = Projectile.Center;

        float leeway = Projectile.width / 2 * Projectile.scale;
        leeway *= 0.75f;
        float radius = threshold - leeway;

        var target = Main.LocalPlayer;

        var blackTile = TextureAssets.MagicPixel;
        var diagonalNoise = FargosTextureRegistry.WavyNoise;

        if (!blackTile.IsLoaded || !diagonalNoise.IsLoaded)
            return false;

        var maxOpacity = Projectile.Opacity;
        float scale = MathF.Sqrt(VisualScale);

        ManagedShader borderShader = ShaderManager.GetShader("FargowiltasSouls.AbomRitualShader");
        borderShader.TrySetParameter("colorMult", 7.35f);
        borderShader.TrySetParameter("time", Main.GlobalTimeWrappedHourly);
        borderShader.TrySetParameter("radius", radius * scale);
        borderShader.TrySetParameter("anchorPoint", auraPos);
        borderShader.TrySetParameter("screenPosition", Main.screenPosition);
        borderShader.TrySetParameter("screenSize", Main.ScreenSize.ToVector2());
        borderShader.TrySetParameter("playerPosition", target.Center);
        borderShader.TrySetParameter("maxOpacity", maxOpacity);

        Main.spriteBatch.GraphicsDevice.Textures[1] = diagonalNoise.Value;

        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, borderShader.WrappedEffect, Main.GameViewMatrix.TransformationMatrix);
        Rectangle rekt = new(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
        Main.spriteBatch.Draw(blackTile.Value, rekt, null, default, 0f, blackTile.Value.Size() * 0.5f, 0, 0f);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        return false;
    }
}