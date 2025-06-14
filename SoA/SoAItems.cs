using FargowiltasSouls;
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
using SacredTools.Content.Items.Weapons.Dreamscape.Nihilus;
using ssm.Content.DamageClasses;
using ssm.Content.Projectiles.Enchantments;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.BlightboneEnchant;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<YataMirror>())
            {
                entity.damage = (int)(entity.damage * 0.5f);
            }
            if (entity.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                entity.damage = ModCompatibility.Calamity.Loaded ? (int)(entity.damage * 2.2f) : (int)(entity.damage * 2);
            }
            if (entity.type == ModContent.ItemType<Malice>())
            {
                entity.damage = ModCompatibility.Calamity.Loaded ? (int)(entity.damage * 1.8f) : (int)(entity.damage * 1.5f);
            }
            if (entity.type == ModContent.ItemType<Tenebris>())
            {
                entity.damage = ModCompatibility.Calamity.Loaded ? (int)(entity.damage * 1.1f) : (int)(entity.damage * 1);
            }
            if (entity.type == ModContent.ItemType<Desperatio>())
            {
                entity.damage = ModCompatibility.Calamity.Loaded ? (int)(entity.damage * 1.1f) : (int)(entity.damage * 1);
            }
            if (entity.type == ModContent.ItemType<Eschaton>())
            {
                entity.damage = ModCompatibility.Calamity.Loaded ? (int)(entity.damage * 1.9f) : (int)(entity.damage * 1.5);
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
            if (item.type == ModContent.ItemType<YataMirror>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage decreased by 50%"));
            }
            if (item.type == ModContent.ItemType<FlamesOfCondemnation>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 120%"));
            }
            if (item.type == ModContent.ItemType<Malice>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 80%"));
            }
            if (item.type == ModContent.ItemType<Tenebris>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 10%"));
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
            if (item.type == ModContent.ItemType<Desperatio>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 10%"));
                tooltips.Add(new TooltipLine(Mod, "nerf", $"[c/00A36C:Cross-Mod Balance:] Fire column deal 70% less damage."));
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
            if (item.type == ModContent.ItemType<Eschaton>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage increased by 90%"));
                tooltips.Add(new TooltipLine(Mod, "velocity", $"[c/00A36C:Cross-Mod Balance:] Projectile velocity increased by 10%"));
                tooltips.Add(new TooltipLine(Mod, "homing", $"[c/00A36C:Cross-Mod Balance:] Weapon's pojectiles are homing in on enemies"));
            }
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (item != null && item.damage > 0 && !item.IsAir)
            {
                if (!player.ForceEffect<BlightboneEffect>() && Main.rand.NextFloat() < 0.1f)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, player.velocity * 0.8f,
                        ModContent.ProjectileType<Blightbone>(), (int)(item.damage * 0.5f), item.knockBack, player.whoAmI);
                }
                else
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, player.velocity * 0.8f,
                        ModContent.ProjectileType<Blightbone>(), (int)(item.damage * 0.5f), item.knockBack, player.whoAmI);
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, player.velocity * 0.9f,
                        ModContent.ProjectileType<Blightbone>(), (int)(item.damage * 0.25f), item.knockBack, player.whoAmI);
                }
            }
            return base.UseItem(item, player);
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
                    player.GetDamage<UnitedModdedThrower>() += 0.08f;
                }
                if (item.type == ModContent.ItemType<NinjaEmblem>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.15f;
                }
                if (item.type == ModContent.ItemType<QuasarSigil>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.25f;
                    player.GetCritChance<UnitedModdedThrower>() += 0.1f;
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
                    player.GetDamage<UnitedModdedThrower>() += 0.05f;
                }
                if (item.type == ModContent.ItemType<SpaceJunkLegs>() || item.type == ModContent.ItemType<CairoCrusaderFaulds>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.1f;
                }
                if (item.type == ModContent.ItemType<MarstechLegs>() || item.type == ModContent.ItemType<BlightChest>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.15f;
                }
                if (item.type == ModContent.ItemType<SpaceJunkBody>() || item.type == ModContent.ItemType<MarstechHelm>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.2f;
                }
                if (item.type == ModContent.ItemType<CairoCrusaderTurban>() || item.type == ModContent.ItemType<SpaceJunkHelm>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.17f;
                }

                if (item.type == ModContent.ItemType<FallenPrinceHelm>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.24f;
                }
                if (item.type == ModContent.ItemType<BlightMask>())
                {
                    player.GetCritChance<UnitedModdedThrower>() += 0.1f;
                    player.Shtun().throwerVelocity += 0.3f;
                }
                if (item.type == ModContent.ItemType<MarstechPlate>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.25f;
                    player.Shtun().throwerVelocity += 0.27f;
                }
                if (item.type == ModContent.ItemType<FlariumCowl>())
                {
                    player.GetCritChance<UnitedModdedThrower>() += 0.21f;
                    player.GetDamage<UnitedModdedThrower>() += 0.45f;
                    player.Shtun().throwerVelocity += 0.29f;
                }
                if (item.type == ModContent.ItemType<AsthraltiteHelmetRevenant>())
                {
                    player.GetCritChance<UnitedModdedThrower>() += 0.32f;
                    player.Shtun().throwerVelocity += 0.45f;
                }
                if (item.type == ModContent.ItemType<FallenPrinceChest>())
                {
                    player.GetCritChance<UnitedModdedThrower>() += 0.26f;
                    player.GetDamage<UnitedModdedThrower>() += 0.20f;
                    player.Shtun().throwerVelocity += 0.26f;
                }
                if (item.type == ModContent.ItemType<NovaBreastplate>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.28f;
                    player.Shtun().throwerVelocity += 0.35f;
                }
                if (item.type == ModContent.ItemType<CairoCrusaderRobe>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.22f;
                    player.Shtun().throwerVelocity += 0.25f;
                }
                if (item.type == ModContent.ItemType<FallenPrinceBoots>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.17f;
                }
                if (item.type == ModContent.ItemType<NovaHelmet>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.27f;
                }
                if (item.type == ModContent.ItemType<NovaLegs>())
                {
                    player.GetDamage<UnitedModdedThrower>() += 0.18f;
                    player.GetCritChance<UnitedModdedThrower>() += 0.2f;
                }
            }
        }
    }
}
