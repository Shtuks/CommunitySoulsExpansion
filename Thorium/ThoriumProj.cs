using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Terraria.ID;
using ssm.Thorium.Buffs;

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
            if (player.GetModPlayer<ShtunThoriumPlayer>().ThunderTalonEternity)
            {
                target.AddBuff(BuffID.BoneJavelin, 300);
            }
            if (player.GetModPlayer<ShtunThoriumPlayer>().DarkenedCloak)
            {
                if (Main.rand.NextBool(4))
                {
                    player.AddBuff(ModContent.BuffType<SoulStrength>(), 120);
                }
            }
        }
    }
}
