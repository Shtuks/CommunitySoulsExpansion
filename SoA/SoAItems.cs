using SacredTools;
using SacredTools.Common.Types;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories.Sigils;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Blightbone;
using SacredTools.Content.Items.Armor.CairoCrusader;
using SacredTools.Content.Items.Armor.Dragon;
using SacredTools.Content.Items.Armor.Lunar.Quasar;
using SacredTools.Content.Items.Armor.Marstech;
using SacredTools.Content.Items.Armor.Quasar;
using SacredTools.Content.Items.Armor.SpaceJunk;
using SacredTools.Content.Items.Potions.Recovery;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.Content.Items.Weapons.Dreamscape.Nihilus;
using ssm.Content.DamageClasses;
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
                entity.damage = (int)(entity.damage * (ModCompatibility.Calamity.Loaded ? 1.7f : 1.1f));
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
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (item.ModItem is ReloadWeapon reloadWeapon && item.type == ModContent.ItemType<Desperatio>())
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

        //More kluges YAY
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (ShtunConfig.Instance.ThrowerMerge)
            {
                if(item.type == ModContent.ItemType<HeartOfTheCaverns>())
                {
                    player.Shtun().throwerVelocity += 0.2f;
                }
                if (item.type == ModContent.ItemType<FeatherHairpin>())
                {
                    player.Shtun().throwerVelocity += 0.1f;
                    player.GetDamage<ThrowingDamageClass>() += 0.08f;
                }
                if (item.type == ModContent.ItemType<NinjaEmblem>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.15f;
                }
                if (item.type == ModContent.ItemType<QuasarSigil>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.25f;
                    player.GetCritChance<ThrowingDamageClass>() += 0.1f;
                }
            }
        }
        //I hate this
        //At least i can edit values easily to balance shit
        public override void UpdateEquip(Item item, Player player)
        {
            if (ShtunConfig.Instance.ThrowerMerge)
            {
                if (item.type == ModContent.ItemType<BlightLegs>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.05f;
                }
                if (item.type == ModContent.ItemType<SpaceJunkLegs>() || item.type == ModContent.ItemType<CairoCrusaderFaulds>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.1f;
                }
                if (item.type == ModContent.ItemType<MarstechLegs>() || item.type == ModContent.ItemType<BlightChest>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.15f;
                }
                if (item.type == ModContent.ItemType<SpaceJunkBody>() || item.type == ModContent.ItemType<MarstechHelm>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.2f;
                }
                if (item.type == ModContent.ItemType<CairoCrusaderTurban>() || item.type == ModContent.ItemType<SpaceJunkHelm>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.17f;
                }

                if (item.type == ModContent.ItemType<FallenPrinceHelm>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.24f;
                }
                if (item.type == ModContent.ItemType<BlightMask>())
                {
                    player.GetCritChance<ThrowingDamageClass>() += 0.1f;
                    player.Shtun().throwerVelocity += 0.3f;
                }
                if (item.type == ModContent.ItemType<MarstechPlate>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.25f;
                    player.Shtun().throwerVelocity += 0.27f;
                }
                if (item.type == ModContent.ItemType<FlariumCowl>())
                {
                    player.GetCritChance<ThrowingDamageClass>() += 0.21f;
                    player.GetDamage<ThrowingDamageClass>() += 0.45f;
                    player.Shtun().throwerVelocity += 0.29f;
                }
                if (item.type == ModContent.ItemType<AsthraltiteHelmetRevenant>())
                {
                    player.GetCritChance<ThrowingDamageClass>() += 0.32f;
                    player.Shtun().throwerVelocity += 0.45f;
                }
                if (item.type == ModContent.ItemType<FallenPrinceChest>())
                {
                    player.GetCritChance<ThrowingDamageClass>() += 0.26f;
                    player.GetDamage<ThrowingDamageClass>() += 0.20f;
                    player.Shtun().throwerVelocity += 0.26f;
                }
                if (item.type == ModContent.ItemType<NovaBreastplate>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.28f;
                    player.Shtun().throwerVelocity += 0.35f;
                }
                if (item.type == ModContent.ItemType<CairoCrusaderRobe>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.22f;
                    player.Shtun().throwerVelocity += 0.25f;
                }
                if (item.type == ModContent.ItemType<FallenPrinceBoots>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.17f;
                }
                if (item.type == ModContent.ItemType<NovaHelmet>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.27f;
                }
                if (item.type == ModContent.ItemType<NovaLegs>())
                {
                    player.GetDamage<ThrowingDamageClass>() += 0.18f;
                    player.GetCritChance<ThrowingDamageClass>() += 0.2f;
                }
            }
        }
    }
}
