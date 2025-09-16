using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ssm.Core
{
    public class DrawAnimationHorizontal : DrawAnimation
    {
        public int FrameSpeed;
        private int _frameCounter; 

        public DrawAnimationHorizontal(int frameCount, int frameSpeed)
        {
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
        }

        public override void Update()
        {
            if (++_frameCounter >= FrameSpeed)
            {
                _frameCounter = 0;
                Frame = (Frame + 1) % FrameCount;
            }
        }

        public override Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
        {
            int frame = frameCounterOverride != -1 ? frameCounterOverride : Frame;
            int frameWidth = texture.Width / FrameCount;
            return new Rectangle(frameWidth * frame, 0, frameWidth, texture.Height);
        }
    }
}
