using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Projectiles.Masomode;
using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public class CSEProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool PreAI(Projectile projectile)
        {
            //dont mess with my scaling
            if (projectile.type == ModContent.ProjectileType<FishronRitual>())
            {
                projectile.ai[2] = 1;
            }
            return base.PreAI(projectile);
        }
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
