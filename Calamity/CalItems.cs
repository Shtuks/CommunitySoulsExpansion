using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Potions;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalItems : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<Sylvestaff>())
            {
                entity.damage = (int)(entity.damage * 0.4f);
            }
            if (entity.type == ModContent.ItemType<Voidragon>())
            {
                entity.damage = (int)(entity.damage * 0.9f);
            }
            if (entity.type == ModContent.ItemType<HalibutCannon>())
            {
                entity.damage = (int)(entity.damage * 0.7f);
            }
            if (entity.type == ModContent.ItemType<IridescentExcalibur>())
            {
                entity.damage = (int)(entity.damage * 1.5f);
            }
            if (entity.type == ModContent.ItemType<Supernova>())
            {
                entity.damage = (int)(entity.damage * 0.7f);
            }
            if (entity.type == ModContent.ItemType<NanoblackReaper>())
            {
                entity.damage = (int)(entity.damage * 1.1f);
            }
            //if (entity.type == ItemID.Zenith)
            //{
            //    entity.damage = (int)(entity.damage * 1.3f);
            //}
            if (entity.type == ModContent.ItemType<OmegaHealingPotion>() && ModCompatibility.SacredTools.Loaded)
            {
                entity.healLife = 400;
            }
            if (entity.type == ModContent.ItemType<ColossusSoul>())
            {
                entity.defense = 15;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<IridescentExcalibur>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FFFF00:CSE Balance:] Canceled DLC debuff."));
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:CSE Balance:] Increased projectile velocity."));
            }
            if (item.type == ModContent.ItemType<Voidragon>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FFFF00:CSE Balance:] Canceled DLC buff."));
            }
            if (item.type == ModContent.ItemType<HalibutCannon>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FF0000:CSE Balance:] Damage decreased by 30%."));
            }
            if (item.type == ModContent.ItemType<Supernova>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FF0000:CSE Balance:] Damage decreased by 30%."));
            }
            //if (item.type == ItemID.Zenith)
            //{
            //    tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:CSE Balance:] Damage increased by 30%."));
            //}
            if (item.type == ModContent.ItemType<Sylvestaff>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FFFF00:CSE Balance:] Canceled DLC buff."));
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/FF0000:CSE Balance:] Damage decreased by 40%."));
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<VagabondsSoul>())
            {
                player.GetDamage<ThrowingDamageClass>() += 3;
            }
        }
    }
}
