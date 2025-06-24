using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Fargowiltas.Items.Renewals;
using ssm.Core;
using Spooky.Content.Items.SpookyBiome.Misc;
using Spooky.Content.Items.Cemetery.Misc;



namespace ssm.Spooky.Renewals
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewal : BaseRenewalItem
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewal";
        public SpookyRenewal() : base("Spooky Renewal", "Spookifies a large radius", ModContent.ItemType<SpookySolution>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<SpookyRenewalProj>(), 0, 0, Main.myPlayer);

            return false;
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewalSupreme : BaseRenewalItem
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewalSupreme";
        public SpookyRenewalSupreme() : base("Spooky Renewal Supreme", "Spookifies the entire world", -1, true, ModContent.ItemType<SpookyRenewal>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<SpookyRenewalSupremeProj>(), 0, 0, Main.myPlayer);

            return false;
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewal : BaseRenewalItem
    {
        public override string Texture => "ssm/Spooky/Renewals/SwampyRenewal";

        public SwampyRenewal() : base("Swampy Renewal", "Swampifies a large radius", ModContent.ItemType<CemeterySolution>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<SwampyRenewalProj>(), 0, 0, Main.myPlayer);

            return false;
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalSupreme : BaseRenewalItem
    {
        public override string Texture => "ssm/Spooky/Renewals/SwampyRenewalSupreme";

        public SwampyRenewalSupreme() : base("Swampy Renewal Supreme", "Swampifies the entire world", -1, true, ModContent.ItemType<SwampyRenewal>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<SwampyRenewalSupremeProj>(), 0, 0, Main.myPlayer);

            return false;
        }
    }
}