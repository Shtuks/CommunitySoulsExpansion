using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Terraria.ID;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class ThoriumProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override void AI(Projectile projectile)
        {
            //if (projectile.type == ModContent.ProjectileType<PlasmaShot>())
            //{
            //    projectile.velocity *= 1.1f;
            //}
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<CSEThoriumPlayer>().ThunderTalonEternity)
            {
                target.AddBuff(BuffID.BoneJavelin, 300);
            }
        }
    }
}
