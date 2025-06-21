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
    public class WastelandRenewal : BaseRenewalItem
    {
        public WastelandRenewal()
            : base("Wasteland Renewal", "Turn large radius into wasteland", 780)
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<WastelandNukeProj>(), 0, 0f, Main.myPlayer);
            return false;
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class WastelandNukeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Redemption/InfAmmo/WastelandRenewal";
        public WastelandNukeProj()
            : base("WastelandRenewal", 145, 0, supreme: false)
        {
        }
    }
}