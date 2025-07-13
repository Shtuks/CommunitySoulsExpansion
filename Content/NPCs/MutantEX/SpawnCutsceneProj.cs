using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX
{
    public class SpawnCutsceneProj : ModProjectile
    {
        public override string Texture => "ssm/Content/NPCs/MutantEX/MutantEX";
        private enum Phase { Ascend, SummonBossSprites, Implode }
        private Phase CurrentPhase = Phase.Ascend;

        private const int AscendTime = 120;
        private const int SummonTime = 600;
        private const int ImplodeTime = 60;

        private int Timer = 0;
        private Vector2 InitialPosition;
        private Player Owner => Main.player[Projectile.owner];

        private readonly List<Vector2> bossParticles = new List<Vector2>();
        private readonly List<Vector2> bossParticleVelocities = new List<Vector2>();
        private readonly List<int> particleTextureIndices = new List<int>();
        private List<int> availableTextureIndices = new List<int>();

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.scale = 1.5f;
        }

        public override void AI()
        {
            if (Timer == 0)
            {
                InitialPosition = Owner.Center;
                Projectile.position = InitialPosition - new Vector2(Projectile.width / 2, Projectile.height / 2);
                Projectile.velocity = Vector2.Zero;
                InitializeAvailableTextureIndices();
                BlockerSystem.Start(true, true, () => Projectile.active);
            }

            Timer++;

            switch (CurrentPhase)
            {
                case Phase.Ascend:
                    HandleAscendPhase();
                    break;

                case Phase.SummonBossSprites:
                    HandleSummonPhase();
                    break;

                case Phase.Implode:
                    HandleImplodePhase();
                    break;
            }

            UpdateCamera();
        }

        private void InitializeAvailableTextureIndices()
        {
            availableTextureIndices = Enumerable.Range(1, 9).ToList();

            if (ModLoader.TryGetMod("CalamityMod", out _))
                availableTextureIndices.AddRange(Enumerable.Range(9, 9));

            if (ModLoader.TryGetMod("CatalystMod", out _))
                availableTextureIndices.AddRange(Enumerable.Range(18, 0));

            if (ModLoader.TryGetMod("NoxusBoss", out _))
                availableTextureIndices.AddRange(Enumerable.Range(21, 2));

            if (ModLoader.TryGetMod("SacredTools", out _))
                availableTextureIndices.AddRange(Enumerable.Range(23, 9));

            if (ModLoader.TryGetMod("Redemption", out _))
                availableTextureIndices.AddRange(Enumerable.Range(32, 11));

            if (ModLoader.TryGetMod("ThoriumMod", out _))
                availableTextureIndices.AddRange(Enumerable.Range(43, 10));

            availableTextureIndices = availableTextureIndices.Distinct().ToList();
        }
        private void HandleAscendPhase()
        {
            float ascendSpeed = 16 * 50f / AscendTime; 
            Projectile.position.Y -= ascendSpeed;  

            if (Timer >= AscendTime)
            {
                CurrentPhase = Phase.SummonBossSprites;
                Timer = 0;
            }
        }

        private void HandleSummonPhase()
        {
            Owner.whoAmI.ToPlayer().CSE().DiscordWhiteTheme(60, 2);

            if (Timer % 10 == 0 && availableTextureIndices.Count > 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    SpawnBossParticle();
                }
            }

            for (int i = 0; i < bossParticles.Count; i++)
            {
                Vector2 direction = Projectile.Center - bossParticles[i];
                direction.Normalize();

                float speed = MathHelper.Lerp(2f, 10f, Timer / (float)SummonTime);
                bossParticleVelocities[i] = Vector2.Lerp(bossParticleVelocities[i], direction * speed, 0.1f);
                bossParticles[i] += bossParticleVelocities[i];
            }

            if (Timer >= SummonTime)
            {
                CurrentPhase = Phase.Implode;
                Timer = 0;
                Projectile.velocity = Vector2.Zero;
            }
        }

        private void SpawnBossParticle()
        {
            Vector2 spawnPosition = Vector2.Zero;
            int side = Main.rand.Next(4);

            switch (side)
            {
                case 0: // Top
                    spawnPosition = new Vector2(Main.rand.Next(Main.screenWidth), -100);
                    break;
                case 1: // Bottom
                    spawnPosition = new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 100);
                    break;
                case 2: // Left
                    spawnPosition = new Vector2(-100, Main.rand.Next(Main.screenHeight));
                    break;
                case 3: // Right
                    spawnPosition = new Vector2(Main.screenWidth + 100, Main.rand.Next(Main.screenHeight));
                    break;
            }

            spawnPosition += Main.screenPosition;
            bossParticles.Add(spawnPosition);
            bossParticleVelocities.Add(Vector2.Zero);

            particleTextureIndices.Add(availableTextureIndices[Main.rand.Next(availableTextureIndices.Count)]);
        }

        private void HandleImplodePhase()
        {
            Projectile.scale = MathHelper.Lerp(1f, 0.01f, Timer / (float)ImplodeTime);

            if (Timer >= ImplodeTime)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int bossType = ModContent.NPCType<MutantEX>();
                    NPC.SpawnOnPlayer(Owner.whoAmI, bossType);
                }

                for (int i = 0; i < 50; i++)
                {
                    Dust dust = Dust.NewDustPerfect(
                        Projectile.Center,
                        DustID.GoldFlame,
                        Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat(5f, 10f)
                    );
                    dust.noGravity = true;
                }

                SoundEngine.PlaySound(new("FargowiltasSouls/Assets/Sounds/DifficultyEmode"), Projectile.Center);
                BlockerSystem.WorldEnterAndExitClearing();
                Projectile.Kill();
            }
        }

        private void UpdateCamera()
        {
            if (Main.myPlayer != Projectile.owner) return;

            Vector2 targetPosition = Projectile.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Main.screenPosition = Vector2.Lerp(Main.screenPosition, targetPosition, 0.1f);
        }

        public override void Kill(int timeLeft)
        {
            BlockerSystem.WorldEnterAndExitClearing();
        }

        //public override bool PreDraw(ref Color lightColor) => false;

        public override void PostDraw(Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            for (int i = 0; i < bossParticles.Count; i++)
            {
                int textureIndex = particleTextureIndices[i];
                Texture2D texture = ModContent.Request<Texture2D>($"ssm/Assets/ExtraTextures/MonstrositySpawnAnimation/boss{textureIndex}").Value;

                Main.spriteBatch.Draw(
                    texture,
                    bossParticles[i] - Main.screenPosition,
                    null,
                    Color.White * 0.7f,
                    0f,
                    texture.Size() / 2f,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}