using Fargowiltas.Items.Renewals;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Fargowiltas.Projectiles;
using ssm.Core;

namespace ssm.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class WastelandRenewalSupreme : BaseRenewalItem
    {
        public WastelandRenewalSupreme()
            : base("Wasteland Renewal Supreme", "Turn the entire world into wasteland", -1, supreme: true, ModContent.ItemType<WastelandRenewal>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<WastelandNukeSupremeProj>(), 0, 0f, Main.myPlayer);
            return false;
        }
    }
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class WastelandNukeSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Redemption/InfAmmo/WastelandRenewalSupreme";
        public WastelandNukeSupremeProj()
            : base("WastelandRenewalSupreme", 145, 0, supreme: true)
        {
        }
    }
}