using CalamityMod.DataStructures;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity.SpecificEffectManagers;
using NoxusBoss.Core.BaseEntities;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using NoxusBoss.Content.Particles;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ssm.Core;
using System;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Calamity
{
    [ExtendsFromMod(ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.WrathoftheGods.Name)]
    public class NDLaserbeam : BaseTelegraphedPrimitiveLaserbeam, IPixelatedPrimitiveRenderer, IAdditiveDrawer
    {
        public override bool UseStandardDrawing => false;

        public override int TelegraphPointCount => 33;

        public override int LaserPointCount => 45;

        public override float MaxLaserLength => 8000f;

        public override float LaserExtendSpeedInterpolant => 0.081f;

        public override ManagedShader TelegraphShader => ShaderManager.GetShader("NoxusBoss.SideStreakShader");

        public override ManagedShader LaserShader => ShaderManager.GetShader("NoxusBoss.NamelessDeityPortalLaserShader");

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = (int)MaxLaserLength + 400;
        }

        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 112;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hostile = true;
            Projectile.timeLeft = 60000;
        }
        
        public override void PostAI()
        {
            Projectile.Opacity = LumUtils.InverseLerp(TelegraphTime + LaserShootTime - 1f, TelegraphTime + LaserShootTime - 11f, Time);

            bool laserIsFiring = Time >= TelegraphTime && Time <= TelegraphTime + LaserShootTime - 4f;
            if (laserIsFiring && Projectile.WithinRange(Main.LocalPlayer.Center, 1000f))
            {
                if (Time % 4f == 3f)
                {
                    PulseRing ring = new(Projectile.Center, Vector2.Zero, new(229, 60, 90), 0.5f, 2.75f, 12);
                    ring.Spawn();
                }
            }
        }

        public override void OnLaserFire()
        {
            NamelessDeityKeyboardShader.BrightnessIntensity += 0.4f;

            SoundEngine.PlaySound(new SoundStyle("NoxusBoss/Assets/Sounds/Custom/NamelessDeity/PortalLaserShoot") with { Volume = 1.32f, PitchVariance = 0.1f, MaxInstances = 1, SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest }, Main.LocalPlayer.Center);

            Main.LocalPlayer.Shtun().Screenshake = 10;
        }

        public override float TelegraphWidthFunction(float completionRatio) => Projectile.Opacity * Projectile.width / 3;

        public override Color TelegraphColorFunction(float completionRatio)
        {
            float timeFadeOpacity = LumUtils.InverseLerpBump(TelegraphTime - 1f, TelegraphTime - 7f, TelegraphTime - 20f, 0f, Time);
            float endFadeOpacity = LumUtils.InverseLerpBump(0f, 0.15f, 0.67f, 1f, completionRatio);
            Color baseColor = Color.Lerp(new(206, 46, 164), Color.OrangeRed, Projectile.identity / 9f % 0.7f);
            return baseColor * endFadeOpacity * timeFadeOpacity * Projectile.Opacity * 0.3f;
        }

        public override float LaserWidthFunction(float completionRatio) => Projectile.Opacity * Projectile.width / 3;

        public override Color LaserColorFunction(float completionRatio)
        {
            float timeFade = LumUtils.InverseLerp(LaserShootTime - 1f, LaserShootTime - 8f, Time - TelegraphTime);
            float startFade = LumUtils.InverseLerp(0f, 0.065f, completionRatio);
            Color baseColor = Color.Lerp(new(206, 46, 164), Color.Orange, Projectile.identity / 9f % 0.7f);

            return baseColor * Projectile.Opacity * timeFade * startFade * 0.75f;
        }

        public override void PrepareTelegraphShader(ManagedShader telegraphShader)
        {
            telegraphShader.TrySetParameter("generalOpacity", Projectile.Opacity);
        }

        public override void PrepareLaserShader(ManagedShader laserShader)
        {
            laserShader.TrySetParameter("darknessNoiseScrollSpeed", 2.5f);
            laserShader.TrySetParameter("brightnessNoiseScrollSpeed", 1.7f);
            laserShader.TrySetParameter("darknessScrollOffset", Vector2.UnitY * (Projectile.identity * 0.3358f % 1f));
            laserShader.TrySetParameter("brightnessScrollOffset", Vector2.UnitY * (Projectile.identity * 0.3747f % 1f));
            laserShader.TrySetParameter("drawAdditively", false);
            laserShader.SetTexture(WotGAttackUtils.WavyBlotchNoise, 1);
            laserShader.SetTexture(WotGAttackUtils.DendriticNoiseZoomedOut, 2);
        }

        public void RenderPixelatedPrimitives(SpriteBatch spriteBatch) => DrawTelegraphOrLaser(true);

        public void AdditiveDraw(SpriteBatch spriteBatch)
        {
            float sourceOpacity = LumUtils.InverseLerp(LaserShootTime - 1f, LaserShootTime - 6f, Time - TelegraphTime) * 0.92f;
            Vector2 sourceScale = Projectile.scale * new Vector2(1f, 3f);
            Vector2 sourceDrawPosition = Projectile.Center - Main.screenPosition + Projectile.velocity * 16f;
            Color sourceColor = Color.White * LumUtils.InverseLerp(-3f, 0f, Time - TelegraphTime) * sourceOpacity;
            spriteBatch.Draw(WotGAttackUtils.BloomCircleSmall, sourceDrawPosition, null, sourceColor, Projectile.velocity.ToRotation(), WotGAttackUtils.BloomCircleSmall.Size() * 0.5f, sourceScale, 0, 0f);

            float glimmerCompletion = LumUtils.InverseLerp(8f, TelegraphTime, Time);
            if (glimmerCompletion <= 0f || glimmerCompletion >= 1f)
                return;

            // Calculate draw information for the glimmer and glow.
            float glimmerScale = LumUtils.InverseLerpBump(0f, 0.45f, 0.95f, 1f, glimmerCompletion);
            float glimmerOpacity = (float)(Math.Pow(LumUtils.InverseLerp(0f, 0.32f, glimmerCompletion), 2f) * 0.5f);
            float glimmerRotation = MathHelper.Lerp(MathHelper.PiOver4, MathHelper.Pi * 4f + MathHelper.PiOver4, (float)Math.Pow(glimmerCompletion, 0.15f));

            // Draw the glimmer.
            Vector2 glimmerDrawPosition = Projectile.Center - Main.screenPosition;
            Color glimmerDrawColor = Projectile.GetAlpha(Color.Wheat) * glimmerOpacity;
            Color circularGlowDrawColor = Projectile.GetAlpha(Color.Pink) * glimmerOpacity;
            spriteBatch.Draw(WotGAttackUtils.FourPointedStarTexture, glimmerDrawPosition, null, glimmerDrawColor, glimmerRotation, WotGAttackUtils.FourPointedStarTexture.Size() * 0.5f, glimmerScale, 0, 0f);
            spriteBatch.Draw(WotGAttackUtils.BloomFlare, glimmerDrawPosition, null, glimmerDrawColor * 0.3f, glimmerRotation, WotGAttackUtils.BloomFlare.Size() * 0.5f, glimmerScale * 1.5f, 0, 0f);

            spriteBatch.Draw(WotGAttackUtils.HollowCircleSoftEdge, glimmerDrawPosition, null, circularGlowDrawColor, Projectile.velocity.ToRotation(), WotGAttackUtils.HollowCircleSoftEdge.Size() * 0.5f, glimmerScale * new Vector2(0.9f, 1.25f) * 0.35f, 0, 0f);
        }
    }
}
