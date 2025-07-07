using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Calamity
{
    [ExtendsFromMod(ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.WrathoftheGods.Name)]
    public class WotGAttackUtils : ModSystem
    {
        public const string GreyscaleTexturesPath = "ssm/Assets/ExtraTextures/WotGMonstrAttacks/GreyscaleTextures";

        public static readonly Texture2D BloomCircleSmall = LoadDeferred($"{GreyscaleTexturesPath}/BloomCircleSmall");
        public static readonly Texture2D DendriticNoiseZoomedOut = LoadDeferred($"{GreyscaleTexturesPath}/DendriticNoiseZoomedOut");
        public static readonly Texture2D WavyBlotchNoise = LoadDeferred($"{GreyscaleTexturesPath}/WavyBlotchNoise");
        public static readonly Texture2D FourPointedStarTexture = LoadDeferred($"{GreyscaleTexturesPath}/FourPointedStar");
        public static readonly Texture2D BloomFlare = LoadDeferred($"{GreyscaleTexturesPath}/BloomFlare");
        public static readonly Texture2D HollowCircleSoftEdge = LoadDeferred($"{GreyscaleTexturesPath}/HollowCircleSoftEdge");
        public static Texture2D LoadDeferred(string path)
        {
            if (Main.netMode == NetmodeID.Server)
                return default;

            return ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value;
        }
        public static Texture2D[] LoadDeferred(string path, int textureCount)
        {
            if (Main.netMode == NetmodeID.Server)
                return default;

            Texture2D[] textures = new Texture2D[textureCount];
            for (int i = 0; i < textureCount; i++)
                textures[i] = LoadDeferred($"{path}{i + 1}");

            return textures;
        }
    }
}
