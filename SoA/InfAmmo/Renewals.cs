using Fargowiltas.Items.Renewals;
using Fargowiltas.Projectiles;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Microsoft.Xna.Framework;
using SacredTools.Items;

namespace ssm.SoA.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumRenewal : BaseRenewalItem
    {
        public FlariumRenewal()
            : base("Flarium Renewal", "Turn large radius into Flarium", ModContent.ItemType<ShrineSolution>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<FlariumNukeProj>(), 0, 0f, Main.myPlayer);
            return false;
        }
    }

    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumNukeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/SoA/InfAmmo/FlariumRenewal";
        public FlariumNukeProj()
            : base("FlariumRenewal", 145, 0, supreme: false)
        {
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumRenewalSupreme : BaseRenewalItem
    {
        public FlariumRenewalSupreme()
            : base("Flarium Renewal Supreme", "Turn the entire world into Flarium", -1, supreme: true, ModContent.ItemType<FlariumRenewal>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<FlariumNukeSupremeProj>(), 0, 0f, Main.myPlayer);
            return false;
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumNukeSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/SoA/InfAmmo/FlariumRenewalSupreme";
        public FlariumNukeSupremeProj()
            : base("FlariumRenewalSupreme", 145, 0, supreme: true)
        {
        }
    }
}
