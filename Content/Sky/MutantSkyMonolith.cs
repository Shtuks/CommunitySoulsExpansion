using FargowiltasSouls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Sky
{
    public class MutantSkyMonolith: CustomSky
    {
        private bool isActive = false;
        private float intensity = 0f;
        private float lifeIntensity = 0f;
        private float specialColorLerp = 0f;
        private Color? specialColor = null;
        private int delay = 0;
        private readonly int[] xPos = new int[50];
        private readonly int[] yPos = new int[50];

        public override void Update(GameTime gameTime)
        {
            const float increment = 0.01f;

            bool useSpecialColor = false;

            if (Main.LocalPlayer.CSE().eternalMonolith)
            {
                intensity += increment;
                lifeIntensity = 1f - 5 / 10;

                void ChangeColorIfDefault(Color color) 
                {
                    if (specialColor == null)
                        specialColor = color;
                    if (specialColor != null && specialColor == color)
                        useSpecialColor = true;
                }

                //ChangeColorIfDefault(Color.Cyan);

                if (intensity > 1f)
                    intensity = 1f;
            }
            else
            {
                lifeIntensity -= increment;
                if (lifeIntensity < 0f)
                    lifeIntensity = 0f;

                specialColorLerp -= increment * 2;
                if (specialColorLerp < 0)
                    specialColorLerp = 0;

                intensity -= increment;
                if (intensity < 0f)
                {
                    intensity = 0f;
                    lifeIntensity = 0f;
                    specialColorLerp = 0f;
                    specialColor = null;
                    delay = 0;
                    Deactivate();
                    return;
                }
            }

            if (useSpecialColor)
            {
                specialColorLerp += increment * 2;
                if (specialColorLerp > 1)
                    specialColorLerp = 1;
            }
            else
            {
                specialColorLerp -= increment * 2;
                if (specialColorLerp < 0)
                {
                    specialColorLerp = 0;
                    specialColor = null;
                }
            }
        }

        private Color ColorToUse(ref float opacity)
        {
            Color color = new(51, 255, 191);
            opacity = intensity * 0.5f + lifeIntensity * 0.5f;

            if (specialColorLerp > 0 && specialColor != null)
            {
                color = Color.Lerp(color, (Color)specialColor, specialColorLerp);
                if (specialColor == Color.Black)
                    opacity = Math.Min(1f, opacity + Math.Min(intensity, lifeIntensity) * 0.5f);
            }

            return color;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0 && minDepth < 0)
            {
                float opacity = 0f;
                Color color = ColorToUse(ref opacity);

                spriteBatch.Draw(ModContent.Request<Texture2D>($"FargowiltasSouls/Content/Sky/MutantSky{FargoSoulsUtil.TryAprilFoolsTexture}", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color * opacity);

                if (--delay < 0)
                {
                    delay = Main.rand.Next(5 + (int)(85f * (1f - lifeIntensity)));
                    for (int i = 0; i < 50; i++)
                    {
                        xPos[i] = Main.rand.Next(Main.screenWidth);
                        yPos[i] = Main.rand.Next(Main.screenHeight);
                    }
                }

                for (int i = 0; i < 50; i++) 
                {
                    int width = Main.rand.Next(3, 251);
                    spriteBatch.Draw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantStatic", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    new Rectangle(xPos[i] - width / 2, yPos[i], width, 3),
                    color * lifeIntensity * 0.75f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive;
        }

        public override Color OnTileColor(Color inColor)
        {
            float dummy = 0f;
            Color skyColor = Color.Lerp(Color.White, ColorToUse(ref dummy), 0.5f);
            return Color.Lerp(skyColor, inColor, 1f - intensity);
        }
    }
}
