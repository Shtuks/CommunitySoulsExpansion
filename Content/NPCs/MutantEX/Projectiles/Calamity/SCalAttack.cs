using CalamityMod.Buffs.DamageOverTime;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Calamity
{
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class MutantSCal : ModProjectile
    {
        public static SoundStyle SCalDashSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SCalDash", (SoundType)0);
        public override string Texture => "CalamityMod/NPCs/SupremeCalamitas/SupremeShieldTop";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 75;
            Projectile.height = 75;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }
        public override void AI()
        {
            ref float timer = ref Projectile.ai[1];
            ref float telegraphTime = ref Projectile.ai[0];
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 7;
            }
            else
            {
                Projectile.alpha = 0;
            }
            if (timer == telegraphTime)
            {
                SoundEngine.PlaySound(SCalDashSound, Projectile.Center);
            }
            if (timer > telegraphTime)
            {
                if (Projectile.velocity.Length() < 50)
                {
                    Projectile.velocity *= 1.2f;
                }
            }
            else
            {
                Projectile.position -= Projectile.velocity;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
            timer++;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (WorldSavingSystem.EternityMode)
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 60 * 5);
            target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 60 * 5);
            base.OnHitPlayer(target, info);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                float opacity = Projectile.Opacity * ((float)ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 pos = Projectile.oldPos[i];
                float rot = Projectile.oldRot[i];
                DrawShield(pos, rot, opacity);
            }
            DrawShield(Projectile.Center, Projectile.rotation, Projectile.Opacity);

            return false;
        }
        private void DrawShield(Vector2 center, float rotation, float opacity)
        {
            float shieldOpacity = opacity;
            float shieldRotation = Projectile.velocity.ToRotation();

            float jawRotation = shieldRotation;
            float jawRotationOffset = 0f;

            jawRotationOffset += MathHelper.Lerp(0.04f, -0.82f, (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 17.2f)) * 0.5f + 0.5f);

            Color shieldColor = Color.White * shieldOpacity;
            Texture2D shieldSkullTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SupremeShieldTop", (AssetRequestMode)2).Value;
            Texture2D shieldJawTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SupremeShieldBottom", (AssetRequestMode)2).Value;
            Vector2 drawPosition = center + Utils.ToRotationVector2(shieldRotation) * 24f - Main.screenPosition;
            Vector2 jawDrawPosition = drawPosition;
            SpriteEffects direction = (SpriteEffects)((!(Math.Cos((double)shieldRotation) > 0.0)) ? 2 : 0);
            if ((int)direction == 2)
            {
                jawDrawPosition += Utils.ToRotationVector2(shieldRotation - (float)Math.PI / 2f) * 42f;
            }
            else
            {
                jawDrawPosition += Utils.ToRotationVector2(shieldRotation + (float)Math.PI / 2f) * 42f;
                jawRotationOffset *= -1f;
            }
            Main.EntitySpriteDraw(shieldJawTexture, jawDrawPosition, (Rectangle?)null, shieldColor, jawRotation + jawRotationOffset, Utils.Size(shieldJawTexture) * 0.5f, 1f, direction, 0f);
            Main.EntitySpriteDraw(shieldSkullTexture, drawPosition, (Rectangle?)null, shieldColor, shieldRotation, Utils.Size(shieldSkullTexture) * 0.5f, 1f, direction, 0f);
        }
    }
}
