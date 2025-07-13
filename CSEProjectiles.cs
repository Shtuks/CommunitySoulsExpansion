using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public class CSEProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            if(projectile.type == 55 && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<TheBiggestSting>())
            {
                projectile.Kill();
            }

            base.AI(projectile);
        }
    }
}
