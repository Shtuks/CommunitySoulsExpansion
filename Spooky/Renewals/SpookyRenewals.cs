using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Fargowiltas.Items.Renewals;
using Spooky.Content.Items.SpookyBiome.Misc;
using ssm.Core;

namespace ssm.Spooky.Renewals
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewal : BaseRenewalItem
    {
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