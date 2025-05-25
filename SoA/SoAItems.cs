using NoxusBoss.Content.Items;
using SacredTools.Content.Items.Weapons.Dreamscape.Nihilus;
using ssm.Content.Projectiles.Enchantments;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item entity)
        {
            if(entity.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                entity.damage = (int)(entity.damage * 2f);
            }
            if (entity.type == ModContent.ItemType<Malice>())
            {
                entity.damage = (int)(entity.damage * 1.7f);
            }
            if (entity.type == ModContent.ItemType<Tenebris>())
            {
                entity.damage = (int)(entity.damage * 1.2f);
            }
            if (entity.type == ModContent.ItemType<Desperatio>())
            {
                entity.damage = (int)(entity.damage * 1.8f);
            }
            if (entity.type == ModContent.ItemType<Eschaton>())
            {
                entity.damage = (int)(entity.damage * 1.6f);
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 100%"));
            }
            if (item.type == ModContent.ItemType<Malice>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 70%"));
            }
            if (item.type == ModContent.ItemType<Tenebris>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 20%"));
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
            if (item.type == ModContent.ItemType<Desperatio>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 80%"));
            }
            if (item.type == ModContent.ItemType<Eschaton>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 60%"));
            }
        }
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
