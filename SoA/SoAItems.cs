using FargowiltasSouls;
using SacredTools.Common.Types;
using SacredTools.Content.Items.Armor.Lunar.Nebula;
using SacredTools.Content.Items.Armor.Lunar.Vortex;
using SacredTools.Content.Items.Potions.Recovery;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.Content.Items.Weapons.Dreamscape.Nihilus;
using SacredTools.Items.Weapons.Lunatic;
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
            if (entity.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 3f : 2.2f));
            }
            if (entity.type == ModContent.ItemType<AsthralSaber>())
            {
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 1.5f : 1));
            }
            if (entity.type == ModContent.ItemType<Malice>())
            {
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 2.5 : 1.8f));
            }
            if (entity.type == ModContent.ItemType<Tenebris>())
            {
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.1f));
            }
            if (entity.type == ModContent.ItemType<Desperatio>())
            {
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 2f : 1.2f));
            }
            if (entity.type == ModContent.ItemType<Eschaton>())
            {
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 2f : 1.7f));
            }
            if (entity.type == ModContent.ItemType<AsthraltiteHealingPotion>() && ModCompatibility.Calamity.Loaded)
            {
                entity.healLife = 500;
            }
            if (entity.type == ModContent.ItemType<DolphinGun>())
            {
                entity.useAnimation = ModCompatibility.Calamity.Loaded ? 3 : 4;
            }
            if (entity.type == ModContent.ItemType<VortexCommanderHat>())
            {
                entity.defense = ModCompatibility.Calamity.Loaded ? 33 : 22;
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<NubaHood>())
            {
                player.GetDamage<MagicDamageClass>() += (ModCompatibility.Calamity.Loaded ? 0.1f : 0);
            }
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.ModItem is ReloadWeapon reloadWeapon && (player.FargoSouls().RangedSoul || player.FargoSouls().Eternity || player.FargoSouls().UniverseSoul))
            {
                reloadWeapon.RefillMagazine(player);
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int n = 0;
            if (item.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                n = ModCompatibility.Calamity.Loaded ? 150 : 120;
            }
            if (item.type == ModContent.ItemType<Malice>())
            {
                n = ModCompatibility.Calamity.Loaded ? 100 : 80;
            }
            if (item.type == ModContent.ItemType<Tenebris>())
            {
                n = ModCompatibility.Calamity.Loaded ? 50 : 10;
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
            if (item.type == ModContent.ItemType<Desperatio>())
            {
                n = ModCompatibility.Calamity.Loaded ? 90 : 20;
                tooltips.Add(new TooltipLine(Mod, "nerf", $"[c/00A36C:Cross-Mod Balance:] Fire column deal 70% less damage."));
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
            if (item.type == ModContent.ItemType<Eschaton>())
            {
                n = ModCompatibility.Calamity.Loaded ? 90 : 70;
                tooltips.Add(new TooltipLine(Mod, "velocity", $"[c/00A36C:Cross-Mod Balance:] Projectile velocity increased by 10%"));
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
            if (item.type == ModContent.ItemType<Eschaton>() || item.type == ModContent.ItemType<Desperatio>() || item.type == ModContent.ItemType<Tenebris>() || item.type == ModContent.ItemType<Malice>() || item.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by {n}%"));
            }
        }
    }
}
