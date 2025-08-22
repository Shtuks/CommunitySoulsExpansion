using FargowiltasSouls.Content.Items;
using ssm.Core;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework;

namespace ssm.Thorium.Emode.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ChampionHeadband : SoulsItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EmodeThorium;
        }
        public override bool Eternity => true;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = 3;
            Item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ChampionHeadbandEffect>(Item);
        }

        public class ChampionHeadbandEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EnergizedMobileDeviceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChampionHeadband>();

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<ChampionHeadbandDash>().ChampionHeadbandEquipped = true;
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ChampionHeadbandDash : ModPlayer
    {
        public const int DashDown = 0;
        public const int DashUp = 1;
        public const int DashRight = 2;
        public const int DashLeft = 3;

        public const int DashCooldown = 50; 
        public const int DashDuration = 35;

        public const float DashVelocity = 10f;

        public int DashDir = -1;

        public bool ChampionHeadbandEquipped;
        public int DashDelay = 0; 
        public int DashTimer = 0; 

        public override void ResetEffects()
        {
            ChampionHeadbandEquipped = false;

            if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
            {
                DashDir = DashDown;
            }
            else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15)
            {
                DashDir = DashUp;
            }
            else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
            {
                DashDir = DashRight;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
            {
                DashDir = DashLeft;
            }
            else
            {
                DashDir = -1;
            }
        }

        public override void PreUpdateMovement()
        {
            if (CanUseDash() && DashDir != -1 && DashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (DashDir)
                {
                    case DashUp when Player.velocity.Y > -DashVelocity:
                    case DashDown when Player.velocity.Y < DashVelocity:
                        {
                            float dashDirection = DashDir == DashDown ? 1 : -1.3f;
                            newVelocity.Y = dashDirection * DashVelocity;
                            break;
                        }
                    case DashLeft when Player.velocity.X > -DashVelocity:
                    case DashRight when Player.velocity.X < DashVelocity:
                        {
                            float dashDirection = DashDir == DashRight ? 1 : -1;
                            newVelocity.X = dashDirection * DashVelocity;
                            break;
                        }
                    default:
                        return;
                }

                DashDelay = DashCooldown;
                DashTimer = DashDuration;
                Player.velocity = newVelocity;
            }

            if (DashDelay > 0)
                DashDelay--;

            if (DashTimer > 0)
            {
                Player.eocDash = DashTimer;
                Player.armorEffectDrawShadowEOCShield = true;

                DashTimer--;
            }

            Vector2 direction = Vector2.Zero;
            switch (DashDir)
            {
                case DashDown:
                    direction = Vector2.UnitY;
                    break;
                case DashUp:
                    direction = -Vector2.UnitY;
                    break;
                case DashRight:
                    direction = Vector2.UnitX;
                    break;
                case DashLeft:
                    direction = -Vector2.UnitX;
                    break;
            }

            if (direction != Vector2.Zero)
            {
                direction *= 10f; 
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    direction,
                    ModContent.ProjectileType<BronzeSwordProj>(),
                    30,
                    3f,
                    Player.whoAmI
                );
            }
        }

        private bool CanUseDash()
        {
            return ChampionHeadbandEquipped && Player.dashType == DashID.None && !Player.mount.Active;
        }
    }
    public class BronzeSwordProj : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            AIType = ProjectileID.Bone;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
            }
        }
    }
}
