using ssm.Content.Projectiles.Enchantments;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool? UseItem(Item item, Player player)
        {
            if (item != null && item.DamageType.CountsAsClass<ThrowingDamageClass>())
            {
                int enchant = player.GetModPlayer<SoAPlayer>().blightboneEnchant;

                if (enchant > 0 && item.CountsAsClass(DamageClass.Throwing))
                {
                    if (enchant == 1 && Main.rand.NextFloat() < 0.1f)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, player.velocity * 0.8f,
                            ModContent.ProjectileType<Blightbone>(), (int)(item.damage * 0.5f), item.knockBack, player.whoAmI);
                    }

                    if (enchant == 2 && Main.rand.NextFloat() < 0.15f)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, player.velocity * 0.8f,
                            ModContent.ProjectileType<Blightbone>(), (int)(item.damage * 0.5f), item.knockBack, player.whoAmI);
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, player.velocity * 0.9f,
                            ModContent.ProjectileType<Blightbone>(), (int)(item.damage * 0.25f), item.knockBack, player.whoAmI);
                    }
                }
            }
            return base.UseItem(item, player);
        }
    }
}
