using ssm.Render.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Graphics.Shaders;

namespace ssm
{
    public class PrimDrawer
    {
        #region Fields/Properties

        public BasicEffect BaseEffect;
        public MiscShaderData Shader;
        public WidthTrailFunction WidthFunc;
        public ColorTrailFunction ColorFunc;

        /// <summary>
        /// This allows the width to dynamically change along the trail if desired.
        /// </summary>
        /// <param name="trailInterpolant">How far (0-1) the current position is on the trail</param>
        /// <returns></returns>
        public delegate float WidthTrailFunction(float trailInterpolant);

        /// <summary>
        /// This allows the color to dynamically change along the trail if desired.
        /// </summary>
        /// <param name="trailInterpolant">How far (0-1) the current position is on the trail</param>
        /// <returns></returns>
        public delegate Color ColorTrailFunction(float trailInterpolant);
        #endregion

        #region Drawing Methods
        /// <summary>
        /// Call this in PreDraw etc to draw your prims.
        /// </summary>
        /// <param name="basePoints"></param>
        /// <param name="baseOffset"></param>
        /// <param name="totalTrailPoints"></param>
        public void DrawPrims(IEnumerable<Vector2> basePoints, Vector2 baseOffset, int totalTrailPoints)
        {
            // Set the correct rasterizer state.
            Main.instance.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // First, we need to offset the points by the base offset. This is almost always going to be -Main.screenPosition, but is changeable for flexability.
            List<Vector2> drawPointsList = CorrectlyOffsetPoints(basePoints, baseOffset, totalTrailPoints);

            // If the list is too short, any points in it are NaNs, or they are all the same point, return.
            if (drawPointsList.Count < 2 || drawPointsList.Any((drawPoint) => drawPoint.HasNaNs()) || drawPointsList.All(point => point == drawPointsList[0]))
                return;

            UpdateBaseEffect(out Matrix projection, out Matrix view);

            // Get an array of primitive triangles to pass through. Color data etc is stored in the struct.
            BasePrimTriangle[] pointVertices = CreatePrimitiveVertices(drawPointsList);
            // Get an array of the indices for each primitive triangle.
            short[] triangleIndices = CreatePrimitiveIndices(drawPointsList.Count);

            // If these are too short, or the indices isnt fully completed, return.
            if (triangleIndices.Length % 6 != 0 || pointVertices.Length <= 3)
                return;

            // If the shader exists, set the correct view and apply it.
            if (Shader != null)
            {
                Shader.Shader.Parameters["uWorldViewProjection"].SetValue(view * projection);
                Shader.Apply();
            }
            // Else, apply the base effect.
            else
                BaseEffect.CurrentTechnique.Passes[0].Apply();

            // Draw the prims! Also apply the main pixel shader. Specify the type of primitives this should be expecting, and pass through the array of the struct using the correct interface.
            Main.instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, pointVertices, 0, pointVertices.Length, triangleIndices, 0, triangleIndices.Length / 3);
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        }

        /// <summary>
        /// Use this in the <see cref="IPixelPrimitiveDrawer.DrawPrixelPrimitives(SpriteBatch)"/> method only.
        /// </summary>
        /// <param name="basePoints"></param>
        /// <param name="baseOffset"></param>
        /// <param name="totalTrailPoints"></param>
        public void DrawPixelPrims(IEnumerable<Vector2> basePoints, Vector2 baseOffset, int totalTrailPoints)
        {
            // Set the correct rasterizer state.
            Main.instance.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // First, we need to offset the points by the base offset. This is almost always going to be -Main.screenPosition, but is changeable for flexability.
            List<Vector2> drawPointsList = CorrectlyOffsetPoints(basePoints, baseOffset, totalTrailPoints);

            // If the list is too short, any points in it are NaNs, or they are all the same point, return.
            if (drawPointsList.Count < 2 || drawPointsList.Any((drawPoint) => drawPoint.HasNaNs()) || drawPointsList.All(point => point == drawPointsList[0]))
                return;

            UpdateBaseEffectPixel(out var effectProjetion, out Matrix view);
            // Get an array of primitive triangles to pass through. Color data etc is stored in the struct.
            BasePrimTriangle[] pointVertices = CreatePrimitiveVertices(drawPointsList);
            // Get an array of the indices for each primitive triangle.
            short[] triangleIndices = CreatePrimitiveIndices(drawPointsList.Count);

            // If these are too short, or the indices isnt fully completed, return.
            if (triangleIndices.Length % 6 != 0 || pointVertices.Length <= 3)
                return;

            // If the shader exists, set the correct view and apply it.
            if (Shader != null)
            {
                Shader.Shader.Parameters["uWorldViewProjection"].SetValue(view * effectProjetion);
                Shader.Apply();
            }
            // Else, apply the base effect.
            else
                BaseEffect.CurrentTechnique.Passes[0].Apply();

            // Draw the prims! Also apply the main pixel shader. Specify the type of primitives this should be expecting, and pass through the array of the struct using the correct interface.
            Main.instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, pointVertices, 0, pointVertices.Length, triangleIndices, 0, triangleIndices.Length / 3);
            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        }
        #endregion

        #region Helper Methods
        public PrimDrawer(WidthTrailFunction widthFunc, ColorTrailFunction colorFunc, MiscShaderData shader = null)
        {
            WidthFunc = widthFunc;
            ColorFunc = colorFunc;
            Shader = shader;
            // Create a basic effect.
            BaseEffect = new BasicEffect(Main.instance.GraphicsDevice)
            {
                VertexColorEnabled = true,
                TextureEnabled = false
            };
            UpdateBaseEffect(out _, out _);
        }

        private void UpdateBaseEffect(out Matrix effectProjection, out Matrix effectView)
        {
            int height = Main.instance.GraphicsDevice.Viewport.Height;

            Vector2 zoom = Main.GameViewMatrix.Zoom;
            Matrix zoomScaleMatrix = Matrix.CreateScale(zoom.X, zoom.Y, 1f);

            effectView = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up);

            effectView *= Matrix.CreateTranslation(0f, -height, 0f);

            effectView *= Matrix.CreateRotationZ(MathHelper.Pi);

            if (Main.LocalPlayer.gravDir == -1f)
                effectView *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, height, 0f);

            effectView *= zoomScaleMatrix;

            effectProjection = Matrix.CreateOrthographicOffCenter(0f, Main.screenWidth * zoom.X, 0f, Main.screenHeight * zoom.Y, 0f, 1f) * zoomScaleMatrix;
            BaseEffect.View = effectView;
            BaseEffect.Projection = effectProjection;
        }

        private void UpdateBaseEffectPixel(out Matrix effectProjetion, out Matrix effectView)
        {
            effectProjetion = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, -1, 1);
            effectView = Matrix.Identity;
            BaseEffect.Projection = effectProjetion;
            BaseEffect.View = effectView;
        }

        private static List<Vector2> CorrectlyOffsetPoints(IEnumerable<Vector2> basePoints, Vector2 baseOffset, int totalPoints)
        {
            List<Vector2> newList = new();
            for (int i = 0; i < basePoints.Count(); i++)
            {
                if (basePoints.ElementAt(i) == Vector2.Zero)
                    continue;

                newList.Add(basePoints.ElementAt(i) + baseOffset);
            }

            if (newList.Count <= 1)
                return newList;

            List<Vector2> controlPoints = new();
            for (int i = 0; i < basePoints.Count(); i++)
            {
                if (basePoints.ElementAt(i) == Vector2.Zero)
                    continue;

                Vector2 offset = baseOffset;
                controlPoints.Add(basePoints.ElementAt(i) + offset);
            }
            List<Vector2> points = new();

            if (controlPoints.Count <= 4)
                return controlPoints;

            for (int j = 0; j < totalPoints; j++)
            {
                float splineInterpolant = j / (float)totalPoints;
                float localSplineInterpolant = splineInterpolant * (controlPoints.Count - 1f) % 1f;
                int localSplineIndex = (int)(splineInterpolant * (controlPoints.Count - 1f));

                Vector2 farLeft;
                Vector2 left = controlPoints[localSplineIndex];
                Vector2 right = controlPoints[localSplineIndex + 1];
                Vector2 farRight;

                if (localSplineIndex <= 0)
                {
                    Vector2 mirrored = left * 2f - right;
                    farLeft = mirrored;
                }
                else
                    farLeft = controlPoints[localSplineIndex - 1];

                if (localSplineIndex >= controlPoints.Count - 2)
                {
                    Vector2 mirrored = right * 2f - left;
                    farRight = mirrored;
                }
                else
                    farRight = controlPoints[localSplineIndex + 2];

                points.Add(Vector2.CatmullRom(farLeft, left, right, farRight, localSplineInterpolant));
            }

            points.Insert(0, controlPoints.First());
            points.Add(controlPoints.Last());

            return points;
        }

        private BasePrimTriangle[] CreatePrimitiveVertices(List<Vector2> points)
        {
            List<BasePrimTriangle> rectPrims = new();

            for (int i = 0; i < points.Count - 1; i++)
            {
                float trailCompletionRatio = i / (float)points.Count;

                float width = WidthFunc(trailCompletionRatio);
                Color color = ColorFunc(trailCompletionRatio);

                Vector2 point = points[i];
                Vector2 aheadPoint = points[i + 1];

                Vector2 directionToAhead = (aheadPoint - point).SafeNormalize(Vector2.Zero);

                Vector2 leftCurrentTextureCoord = new(trailCompletionRatio, 0f);
                Vector2 rightCurrentTextureCoord = new(trailCompletionRatio, 1f);

                Vector2 sideDirection = new(-directionToAhead.Y, directionToAhead.X);

                rectPrims.Add(new BasePrimTriangle(point - sideDirection * width, color, leftCurrentTextureCoord));
                rectPrims.Add(new BasePrimTriangle(point + sideDirection * width, color, rightCurrentTextureCoord));
            }

            return rectPrims.ToArray();
        }

        private static short[] CreatePrimitiveIndices(int totalPoints)
        {
            int totalIndices = (totalPoints - 1) * 6;

            short[] indices = new short[totalIndices];

            for (int i = 0; i < totalPoints - 2; i++)
            {
                int startingTriangleIndex = i * 6;
                int connectToIndex = i * 2;
                indices[startingTriangleIndex] = (short)connectToIndex;
                indices[startingTriangleIndex + 1] = (short)(connectToIndex + 1);
                indices[startingTriangleIndex + 2] = (short)(connectToIndex + 2);
                indices[startingTriangleIndex + 3] = (short)(connectToIndex + 2);
                indices[startingTriangleIndex + 4] = (short)(connectToIndex + 1);
                indices[startingTriangleIndex + 5] = (short)(connectToIndex + 3);
            }
            return indices;
        }
        #endregion
    }
}
