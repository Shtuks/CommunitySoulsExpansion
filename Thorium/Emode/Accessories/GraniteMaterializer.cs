using FargowiltasSouls.Content.Items;
using ssm.Core;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Buffs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework;

namespace ssm.Thorium.Emode.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GraniteMaterializer : SoulsItem
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
            player.buffImmune[ModContent.BuffType<GraniteSurge>()] = true;
            player.AddEffect<GraniteMaterializerEffect>(Item);
        }

        public class GraniteMaterializerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EnergizedMobileDeviceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteMaterializer>();

            private int graniteTimer = 0;
            public override void PostUpdate(Player player)
            {
                graniteTimer++;
                if (graniteTimer >= 300) 
                {
                    Vector2 direction = Main.MouseWorld - player.Center;
                    direction.Normalize();
                    direction *= 12f; 

                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        direction,
                        ModContent.ProjectileType<GraniteChunkProj>(),
                        50, 
                        2f, 
                        player.whoAmI
                    );
                    graniteTimer = 0;
                }
            }
        }
    }

    public class GraniteChunkProj: ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;

            Projectile.rotation += 0.1f;

            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Granite, 0f, 0f, 100);
                dust.velocity *= 0.3f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Vector2 velocity = new Vector2(
                    Main.rand.NextFloat(-4f, 4f),
                    Main.rand.NextFloat(-4f, 4f)
                );

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    velocity,
                    ModContent.ProjectileType<GraniteShardProj>(),
                    Projectile.damage / 2,
                    1f,
                    Projectile.owner
                );
            }


            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Granite, 0f, 0f, 100);
                dust.velocity *= 1.5f;
            }
        }
    }

    public class GraniteShardProj : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 180;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            CSEUtils.HomeInOnNPC(Projectile, false, 800, 8, 5);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100);
                dust.noGravity = true;
                dust.scale = 0.8f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100);
                dust.velocity *= 1.2f;
                dust.noGravity = true;
            }
        }
    }
}
