//using Fargowiltas.Common.Configs;
//using FargowiltasSouls.Assets.ExtraTextures;
//using FargowiltasSouls.Content.Items.Accessories.Enchantments;
//using FargowiltasSouls.Content.Items.Accessories.Forces;
//using FargowiltasSouls;
//using Luminance.Core.Graphics;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria.GameContent;
//using Terraria.ModLoader;
//using Terraria;
//using Microsoft.Xna.Framework;
//using FargowiltasSouls.Core.AccessoryEffectSystem;
//using ssm.Core;
//using ssm.gunrightsmod.Enchantments;
//using static FargowiltasSouls.Content.Items.Accessories.Forces.TimberForce;
//using ssm.SoA.Enchantments;
//using ssm.SoA.Forces;
//using static ssm.gunrightsmod.Enchantments.UraniumEnchant;
//
//namespace ssm.Content.Projectiles.Enchantments
//{
//    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
//    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
//    public class UraniumAuraProjectile : ModProjectile
//    {
//        public override string Texture => FargoSoulsUtil.EmptyTexture;
//
//        public override Color? GetAlpha(Color lightColor) => lightColor * Projectile.Opacity;
//
//        public override void SetDefaults()
//        {
//            Projectile.width = 32;
//            Projectile.height = 32;
//            Projectile.friendly = false;
//            Projectile.penetrate = -1;
//            Projectile.scale = 1f;
//            Projectile.timeLeft = 60;
//            Projectile.ignoreWater = true;
//            Projectile.tileCollide = false;
//        }
//        public override void AI()
//        {
//            if (!Projectile.owner.IsWithinBounds(Main.maxPlayers))
//            {
//                Projectile.Kill();
//                return;
//            }
//            Player player = Main.player[Projectile.owner];
//            if (!player.Alive() || !player.HasEffect<UraniumEffect>())
//            {
//                Projectile.Kill();
//                return;
//            }
//            Projectile.Center = player.Center;
//            Projectile.timeLeft = 60;
//            Projectile.ai[0] = UraniumEffect.Range(player, player.ForceEffect<UraniumEffect>());
//            Projectile.damage = player.FargoSouls().ForceEffect<AstatineEnchant>() ? 15 * 60 : 6 * 60;
//
//        }
//        public static bool CombinedAura(Player player) => player.HasEffect<NatureEffect>() && player.HasEffect<MoltenEffect>() && (player.HasEffect<EbonwoodEffect>() || player.HasEffect<EbonwoodEffect>()) && player.HasEffect<TimberEffect>();
//        public override bool PreDraw(ref Color lightColor)
//        {
//            if (!Projectile.owner.IsWithinBounds(Main.maxPlayers))
//            {
//                Projectile.Kill();
//                return false;
//            }
//            Player player = Main.player[Projectile.owner];
//            if (!player.Alive() || !player.HasEffect<UraniumEffect>())
//            {
//                Projectile.Kill();
//                return false;
//            }
//
//            if (CombinedAura(player))
//            {
//                return false;
//            }
//
//            Color darkColor = Color.DarkGreen;
//            Color mediumColor = Color.Green;
//            Color lightColor2 = Color.Lerp(Color.GreenYellow, Color.SeaGreen, 0.35f);
//
//            Vector2 auraPos = player.Center;
//            float radius = Projectile.ai[0];
//            var target = Main.LocalPlayer;
//            var blackTile = TextureAssets.MagicPixel;
//            var diagonalNoise = FargosTextureRegistry.WavyNoise;
//            if (!blackTile.IsLoaded || !diagonalNoise.IsLoaded)
//                return false;
//            var maxOpacity = Projectile.Opacity * ModContent.GetInstance<FargoClientConfig>().TransparentFriendlyProjectiles;
//
//            ManagedShader borderShader = ShaderManager.GetShader("FargowiltasSouls.GenericInnerAura");
//            borderShader.TrySetParameter("colorMult", 7.35f);
//            borderShader.TrySetParameter("time", Main.GlobalTimeWrappedHourly);
//            borderShader.TrySetParameter("radius", radius);
//            borderShader.TrySetParameter("anchorPoint", auraPos);
//            borderShader.TrySetParameter("screenPosition", Main.screenPosition);
//            borderShader.TrySetParameter("screenSize", Main.ScreenSize.ToVector2());
//            borderShader.TrySetParameter("playerPosition", target.Center);
//            borderShader.TrySetParameter("maxOpacity", maxOpacity);
//            borderShader.TrySetParameter("darkColor", darkColor.ToVector4());
//            borderShader.TrySetParameter("midColor", mediumColor.ToVector4());
//            borderShader.TrySetParameter("lightColor", lightColor2.ToVector4());
//            borderShader.TrySetParameter("opacityAmp", 1f);
//
//            Main.spriteBatch.GraphicsDevice.Textures[1] = diagonalNoise.Value;
//
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, borderShader.WrappedEffect, Main.GameViewMatrix.TransformationMatrix);
//            Rectangle rekt = new(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
//            Main.spriteBatch.Draw(blackTile.Value, rekt, null, default, 0f, blackTile.Value.Size() * 0.5f, 0, 0f);
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//    }
//}