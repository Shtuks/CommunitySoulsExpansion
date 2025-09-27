using System;
using System.Text;
using Daybreak.Common.Features.ModPanel;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

// This looks really ugly right now, if I'm being honest. And I don't want to be the one to push to newer builds until it doesn't look this way. You can though.

//namespace ssm.Core
//{
    //    internal sealed class CSEModPanel : ModPanelStyleExt
    //    {
    //        public sealed class ModName : UIText
    //        {
    //            private readonly string originalText;

    //            public ModName(string text, float textScale = 1f, bool large = false)
    //                : base(text, textScale, large)
    //            {
    //                if (ChatManager.Regexes.Format.Matches(text).Count != 0)
    //                {
    //                    throw new InvalidOperationException("The text cannot contain formatting.");
    //                }
    //                originalText = text;
    //            }

    //            protected override void DrawSelf(SpriteBatch spriteBatch)
    //            {
    //                string formattedText = ModName.GetPulsatingText(this.originalText, Main.GlobalTimeWrappedHourly);
    //                SetText(formattedText);
    //                base.DrawSelf(spriteBatch);
    //            }

    //            public static string GetPulsatingText(string text, float time)
    //            {
    //                Color color_ = CSEModPanel.color_1;
    //                color_.A = 200;
    //                Color lightOrange = color_;
    //                color_ = CSEModPanel.color_2;
    //                color_.A = 200;
    //                Color darkOrange = color_;
    //                StringBuilder sb = new StringBuilder(12 * text.Length);
    //                for (int i = 0; i < text.Length; i++)
    //                {
    //                    float wave = MathF.Sin(time * 3f + (float)i * 0.3f);
    //                    Color color = Color.Lerp(lightOrange, darkOrange, (wave + 1f) / 2f);
    //                    StringBuilder stringBuilder = sb;
    //                    StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(5, 2, stringBuilder);
    //                    handler.AppendLiteral("[c/");
    //                    handler.AppendFormatted(color.Hex3());
    //                    handler.AppendLiteral(":");
    //                    handler.AppendFormatted(text[i]);
    //                    handler.AppendLiteral("]");
    //                    stringBuilder.Append(ref handler);
    //                }
    //                return sb.ToString();
    //            }
    //        }

    //        private sealed class ModIcon : UIImage
    //        {
    //            private readonly Texture2D animationTexture;
    //            private readonly Texture2D overlayTexture;
    //            private const int frameCount = 4;
    //            private float animationTimer;
    //            private int currentFrame;

    //            public ModIcon()
    //                : base(TextureAssets.MagicPixel)
    //            {
    //                animationTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/ModIconMutantFly", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
    //                overlayTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/LifeStar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
    //            }

    //            public override void Update(GameTime gameTime)
    //            {
    //                base.Update(gameTime);
    //                animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
    //                if (animationTimer > 0.15f)
    //                {
    //                    animationTimer = 0f;
    //                    currentFrame = (currentFrame + 1) % frameCount;
    //                }
    //            }

    //            protected override void DrawSelf(SpriteBatch spriteBatch)
    //            {
    //                Rectangle dims = GetDimensions().ToRectangle();

    //                int frameWidth = animationTexture.Width / frameCount;
    //                int frameHeight = animationTexture.Height;

    //                Rectangle sourceRect = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);

    //                Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
    //                Vector2 position = dims.Center.ToVector2();

    //                spriteBatch.Draw(animationTexture, position, sourceRect, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

    //                spriteBatch.Draw(overlayTexture, position, null, Color.White * 0.8f, 0f, overlayTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
    //            }
    //        }

    //        private static readonly Color color_1 = Color.Teal;

    //        private static readonly Color color_2 = Color.Cyan;

    //        private static float hoverIntensity;

    //        public override void Load()
    //        {
    //            base.Load();
    //        }

    //        public override UIImage ModifyModIcon(UIPanel element, UIImage modIcon, ref int modIconAdjust)
    //        {
    //            return new ModIcon
    //            {
    //                Left = modIcon.Left,
    //                Top = modIcon.Top,
    //                Width = modIcon.Width,
    //                Height = modIcon.Height
    //            };
    //        }

    //        public override UIText ModifyModName(Terraria.ModLoader.UI.UIModItem element, UIText modName)
    //        {
    //            return new ModName(Language.GetTextValue("Mods.ssm.ModIcon.Name") + $" v{element._mod.Version}")
    //            {
    //                Left = modName.Left,
    //                Top = modName.Top
    //            };
    //        }

    //        public override bool PreDrawPanel(Terraria.ModLoader.UI.UIModItem element, SpriteBatch sb, ref bool drawDivider)
    //        {
    //            if (((UIPanel)element)._needsTextureLoading)
    //            {
    //                ((UIPanel)element)._needsTextureLoading = false;
    //                ((UIPanel)element).LoadTextures();
    //            }

    //            sb.End(out var ss);

    //            CalculatedStyle dims = element.GetDimensions();

    //            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, ss.RasterizerState, null, Main.UIScaleMatrix);

    //            Texture2D panelTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantSky", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
    //            sb.Draw(panelTexture, dims.ToRectangle(), Color.Teal);

    //            Texture2D staticTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantStatic", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

    //            int[] xPos = new int[50];
    //            int[] yPos = new int[50];
    //            Random rand = new Random(12345);

    //            for (int i = 0; i < 50; i++)
    //            {
    //                xPos[i] = rand.Next((int)dims.X, (int)(dims.X + dims.Width));
    //                yPos[i] = rand.Next((int)dims.Y, (int)(dims.Y + dims.Height));
    //            }

    //            Color color = Color.White;
    //            float lifeIntensity = 1f;

    //            for (int i = 0; i < 50; i++)
    //            {
    //                int width = Main.rand.Next(3, 251);
    //                Rectangle rect = new Rectangle(xPos[i] - width / 2, yPos[i], width, 3);
    //                sb.Draw(staticTexture, rect, color * lifeIntensity * 0.75f);
    //            }

    //            sb.End();
    //            sb.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, ss.RasterizerState, null, Main.UIScaleMatrix);

    //            return false;
    //        }

    //        public override Color ModifyEnabledTextColor(bool enabled, Color color)
    //        {
    //            if (!enabled)
    //            {
    //                return CSEModPanel.color_1;
    //            }
    //            return CSEModPanel.color_2;
    //        }

    //        private static Vector4 Transform(Vector4 vector)
    //        {
    //            Vector2 value = Vector2.Transform(new Vector2(vector.X, vector.Y), Main.UIScaleMatrix);
    //            Vector2 vec2 = Vector2.Transform(new Vector2(vector.Z, vector.W), Main.UIScaleMatrix);
    //            return new Vector4(value, vec2.X, vec2.Y);
    //        }
    //    }
    /*
    [Autoload(Side = ModSide.Client)]
    public class CustomModPanel : ModPanelStyle
    {
        public class CSEPanelIcon : UIImage
        {
            private readonly Texture2D animationTexture;
            private readonly Texture2D overlayTexture;
            private readonly Texture2D headTexture;
            private const int frameCount = 1;
            private float animationTimer;
            private int currentFrame;
            public CSEPanelIcon() : base(TextureAssets.MagicPixel.Value) 
            {
                headTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/ModIconMutantHead", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                animationTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/ModIconMutantFly", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                overlayTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/LifeStar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                //animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if (animationTimer > 0.15f)
                //{
                //    animationTimer = 0f;
                //    currentFrame = (currentFrame + 1) % frameCount;
                //}
            }
            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                Rectangle dims = GetDimensions().ToRectangle();

                int frameWidth = headTexture.Width / frameCount;
                int frameHeight = headTexture.Height;

                Rectangle sourceRect = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);

                Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
                Vector2 position = dims.Center.ToVector2();

                spriteBatch.Draw(headTexture, position, sourceRect, Color.White, 0f, origin, 2.5f, SpriteEffects.None, 0f);

                spriteBatch.Draw(overlayTexture, position, null, Color.White * 0.8f, 0f, overlayTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
            }
        }
        public class CSEPanelModName : UIText
        {
            private readonly string originalText;

            public CSEPanelModName(string text, float textScale = 1, bool large = false) : base(text, textScale, large)
            {
                originalText = text;
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                var formattedText = GetCSEPanelText(originalText, Main.GlobalTimeWrappedHourly);
                SetText(formattedText);
                base.DrawSelf(spriteBatch);
            }

            private static string GetCSEPanelText(string text, float time)
            {
                var sb = new StringBuilder(text.Length * 12);

                Color darkTeal = new Color(0, 80, 80);
                Color brightCyan = new Color(0, 255, 255);

                for (var i = 0; i < text.Length; i++)
                {
                    float wave = MathF.Sin(time * 1.0f + i * 0.2f) * 0.5f + 0.5f;

                    var color = Color.Lerp(darkTeal, brightCyan, wave);
                    color.A = 255;

                    sb.Append($"[c/{color.Hex3()}:{text[i]}]");
                }

                return sb.ToString();
            }
        }

        private static float hoverIntensity;
        private static Effect csePanelShader;
        public override void Load()
        {
            base.Load();

            //if (Main.netMode != Terraria.ID.NetmodeID.Server)
            //{
            //    try
            //    {
            //        csePanelShader = ModContent.Request<Effect>("CSEPanel/Effects/CSEPanelGradient", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            //    }
            //    catch
            //    {
            //        csePanelShader = null;
            //        ModContent.GetInstance<CSEPanel>()?.Logger?.Warn("Failed to load CSEPanelGradient shader, using fallback rendering");
            //    }
            //}
        }

        public override void Unload()
        {
            csePanelShader = null;
            base.Unload();
        }

        public override UIImage ModifyModIcon(UIPanel element, UIImage modIcon, ref int modIconAdjust)
        {
            return new CSEPanelIcon()
            {
                Left = modIcon.Left,
                Top = modIcon.Top,
                Width = modIcon.Width,
                Height = modIcon.Height,
            };
        }

        public override UIText ModifyModName(UIPanel element, UIText modName)
        {
            return new CSEPanelModName(Language.GetTextValue("Mods.ssm.ModIcon.Name") + $" v{Mod.Version}")
            {
                Left = modName.Left,
                Top = modName.Top,
            };
        }

        public override bool PreDrawPanel(UIPanel element, SpriteBatch sb, ref bool drawDivider)
        {
            sb.End(out var ss);

            CalculatedStyle dims = element.GetDimensions();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, ss.RasterizerState, null, Main.UIScaleMatrix);

            Texture2D panelTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantSky", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            sb.Draw(panelTexture, dims.ToRectangle(), Color.Teal);

            Texture2D staticTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantStatic", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            int[] xPos = new int[10];
            int[] yPos = new int[10];
            Random rand = new Random(12345);

            for (int i = 0; i < 10; i++)
            {
                xPos[i] = rand.Next((int)dims.X, (int)(dims.X + dims.Width));
                yPos[i] = rand.Next((int)dims.Y, (int)(dims.Y + dims.Height));
            }

            Color color = Color.White;
            float lifeIntensity = 0.5f;

            for (int i = 0; i < 10; i++)
            {
                int width = Main.rand.Next(3, 251);
                Rectangle rect = new Rectangle(xPos[i] - width / 2, yPos[i], width, 3);
                sb.Draw(staticTexture, rect, color * lifeIntensity * 0.75f);
            }

            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, ss.RasterizerState, null, Main.UIScaleMatrix);

            return false;
        }

        public override Color ModifyEnabledTextColor(bool enabled, Color color)
        {
            if (enabled)
            {
                return new Color(0, 191, 191);
            }
            else
            {
                return new Color(0, 128, 128);
            }
        }

        public override bool PreDrawModStateTextPanel(UIElement self, bool enabled)
        {
            return false;
        }
    }
}
*/