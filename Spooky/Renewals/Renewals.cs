using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Fargowiltas.Items.Renewals;
using Spooky.Content.Items.SpookyBiome.Misc;

namespace ssm.Spooky.Renewals
{
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
}