using CalamityMod.Items.Weapons.Melee;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalItems : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item entity)
        {
            //if (entity.type == ModContent.ItemType<Sylvestaff>())
            //{
            //    entity.damage = (int)(entity.damage * 0.3f);
            //}
            if (entity.type == ModContent.ItemType<IridescentExcalibur>())
            {
                entity.damage = (int)(entity.damage * 1.3f);
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<IridescentExcalibur>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FFFF00:CSE Balance:] Increased projectile velocity."));
            }
            //if (item.type == ModContent.ItemType<Sylvestaff>())
            //{
            //    tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FFFF00:CSE Balance:] No."));
            //    tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FFFF00:CSE Balance:] Damage decreased by 50%."));
            //}
        }
    }
}
