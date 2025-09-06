using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SacredTools.Content.Projectiles.Weapons.Dreamscape.Nihilus;
using SacredTools.Projectiles.Dreamscape;
using SacredTools.Projectiles.Lunar;
using ssm.Content.NPCs;
using ssm.Content.Projectiles.Enchantments;
using ssm.Content.Projectiles.Minions;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.FlariumEnchant;
using static ssm.SoA.Enchantments.QuasarEnchant;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAProjectiles : GlobalProjectile
    {
        public int auraFrameMutant;
        public int eerieBoost;
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            if (proj.type == ModContent.ProjectileType<DesperatioFlame>())
            {
                proj.damage -= (int)(proj.damage * 0.3f);
            }
            if (proj.type == ModContent.ProjectileType<EnergyBlade>())
            {
                proj.damage += (int)(proj.damage * 0.5f);
            }
        }
        public override void AI(Projectile projectile)
        {
            if(eerieBoost > 0 && projectile.minion)
            {
                projectile.damage = (int)(projectile.damage * 1.1);
                eerieBoost--;
            }

            if (projectile.type == ModContent.ProjectileType<TenebrisLink2>())
            {
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    CSEUtils.HomeInOnNPC(projectile, true, 1600, 10, 2);
                }
            }

            if (projectile.type == ModContent.ProjectileType<SpookGrenade>())
            {
                projectile.velocity *= 1.05f;
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    CSEUtils.HomeInOnNPC(projectile, true, 700, 10, 2);
                }
            }

            if (projectile.type == ModContent.ProjectileType<DesperatioBullet>())
            {
                if ((projectile.ai[2] += 1f) >= 20f)
                {
                    CSEUtils.HomeInOnNPC(projectile, true, 300, 10, 2);
                }
            }

            if (projectile.DamageType == DamageClass.Throwing && projectile.owner.ToPlayer().HasEffect<QuasarEffect>())
            {
                CreateGravityField(projectile, projectile.damage);
            }
        }

        public void CreateGravityField(Projectile proj, int damage)
        {
            //Funniest shit i ever made
            float gravityStrength = MathHelper.Clamp(damage / 50f, 1f, 20f);
            float radius = 300f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.CanBeChasedBy() && !npc.boss)
                {
                    float distance = Vector2.Distance(npc.Center, proj.Center);
                    if (distance < radius)
                    {
                        Vector2 direction = proj.Center - npc.Center;
                        direction.Normalize();
                        float pullStrength = gravityStrength * (1f - distance / radius);
                        npc.velocity += direction * pullStrength;

                        if (Main.rand.NextBool(5))
                        {
                            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                            dust.velocity = direction * pullStrength * 2f;
                            dust.noGravity = true;
                        }
                    }
                }
            }
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if(projectile.owner.ToPlayer().ownedProjectileCounts[ModContent.ProjectileType<FlariumTornado>()] < 6) {
                if (projectile.owner.ToPlayer().HasEffect<FlariumEffect>() && Main.rand.NextFloat() < 0.15f && projectile.damage > 0 && !projectile.minion && projectile.tileCollide)
                {
                    Vector2 spawnPosition = projectile.Center;
                    Projectile.NewProjectile(
                        projectile.GetSource_FromThis(),
                        spawnPosition,
                        Vector2.Zero,
                        ModContent.ProjectileType<FlariumTornado>(),
                        100,
                        0,
                        projectile.owner
                    );
                }
            }

            return base.OnTileCollide(projectile, oldVelocity);
        }

        public override bool PreDraw(Projectile npc, ref Color lightColor)
        {
            if (npc.type == ModContent.ProjectileType<MutantBossProjectile>())
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                if (NPC.AnyNPCs(ModContent.NPCType<MutantAuraOfSupression>()))
                {
                    Texture2D suppTexture = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantSupression", (AssetRequestMode)2).Value;
                    Texture2D HexagonTexture = ModContent.Request<Texture2D>("SacredTools/Effects/Hexagons", (AssetRequestMode)2).Value;
                    int frameHeightSupp = suppTexture.Height / 4;
                    int startYSupp = frameHeightSupp * auraFrameMutant;
                    Rectangle suppRect = new Rectangle(0, startYSupp, suppTexture.Width, frameHeightSupp);
                    Color borderColor = Color.Multiply(new Color(191, 48, 135), 1f);
                    Color innerColor = Color.Multiply(new Color(252, 98, 119), 0.25f);
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                    Effect value = ModContent.Request<Effect>("SacredTools/Effects/Shield", (AssetRequestMode)1).Value;
                    value.Parameters["offset"].SetValue(Vector2.Zero);
                    value.Parameters["sampleTexture"].SetValue(HexagonTexture);
                    value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 6f);
                    value.Parameters["border"].SetValue(borderColor.ToVector4());
                    value.Parameters["inner"].SetValue(innerColor.ToVector4());
                    value.Parameters["sinMult"].SetValue(5f);
                    value.Parameters["spriteRatio"].SetValue(new Vector2(suppTexture.Width / 2 / HexagonTexture.Width, suppTexture.Height / 16 / HexagonTexture.Height));
                    value.Parameters["conversion"].SetValue(new Vector2(1f / (float)(suppTexture.Width / 2), 1f / (float)(suppTexture.Height / 2)));
                    value.Parameters["frameAmount"].SetValue(4f);
                    value.CurrentTechnique.Passes[0].Apply();
                    Main.spriteBatch.Draw(suppTexture, drawCenter - new Vector2(2f, 0f) - Main.screenPosition, suppRect, Color.Teal, npc.rotation, suppRect.Size() / 2f, npc.scale * 1.2f, SpriteEffects.None, 0f);
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
                }
            }
            return base.PreDraw(npc, ref lightColor);
        }
    }
}
