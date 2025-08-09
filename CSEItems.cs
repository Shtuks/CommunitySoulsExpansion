﻿using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Accessories;
using ssm.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm
{
    public class CSEItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<Penetrator>() || entity.type == ModContent.ItemType<SparklingLove>() || entity.type == ModContent.ItemType<StyxGazer>())
            {
                entity.damage *= 2;
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (entity.type == ModContent.ItemType<StyxCrown>())
                {
                    entity.defense = 35;
                }
                if (entity.type == ModContent.ItemType<StyxChestplate>())
                {
                    entity.defense = 45;
                }
                if (entity.type == ModContent.ItemType<StyxLeggings>())
                {
                    entity.defense = 40;
                }
                if (entity.type == ModContent.ItemType<MutantBody>())
                {
                    entity.defense = 100;
                }
                if (entity.type == ModContent.ItemType<MutantMask>())
                {
                    entity.defense = 70;
                }
                if (entity.type == ModContent.ItemType<MutantPants>())
                {
                    entity.defense = 70;
                }
            }
            if (entity.type == ModContent.ItemType<GuardianTome>())
            {
                entity.damage = 1500;
            }
            if (entity.type == ModContent.ItemType<TheBiggestSting>())
            {
                entity.damage = 9750;
            }
            if (entity.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>())
            {
                entity.damage = 7800;
            }
            if (entity.type == ModContent.ItemType<SlimeRain>())
            {
                entity.damage = 6800;
            }

            if (entity.type == ModContent.ItemType<ColossusSoul>())
            {
                entity.defense = 15;
            }
            if (entity.type == ModContent.ItemType<UniverseSoul>())
            {
                entity.defense = 5;
            }
            if (entity.type == ModContent.ItemType<DimensionSoul>())
            {
                entity.defense = 20;
            }
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ModContent.ItemType<MutantBag>())
            {
                itemLoot.Add(ItemDropRule.ByCondition(new EModeDropCondition(), ModContent.ItemType<EternalEnergy>(), 1, 20, 30));
            }
        }
        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            //if (Item.type == ModContent.ItemType<UniverseSoul>() && ModCompatibility.SacredTools.Loaded && ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded)
            //{
            //    player.maxMinions += 2;
            //}

            if (Item.type == ModContent.ItemType<BerserkerSoul>())
            {
                player.GetDamage<MeleeDamageClass>() += 0.03f;
            }
            if (Item.type == ModContent.ItemType<SnipersSoul>())
            {
                player.GetDamage<RangedDamageClass>() += 0.03f;
            }
            if (Item.type == ModContent.ItemType<ConjuristsSoul>())
            {
                player.GetDamage<MeleeDamageClass>() += 0.03f;
            }
            if (Item.type == ModContent.ItemType<ArchWizardsSoul>())
            {
                player.GetDamage<RangedDamageClass>() += 0.03f;
            }

            //SoU
            if (ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    ModContent.Find<ModItem>(Mod.Name, "StalkerSoul").UpdateAccessory(player, hideVisual);
                }
            }
            if (ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "BardSoul").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.BeekeeperClass.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "BeekeeperSoul").UpdateAccessory(player, hideVisual);
            }

            //SoE
            if (ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "SoASoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Crossmod.Loaded && ModCompatibility.Calamity.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "CalamitySoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "ThoriumSoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Polarities.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "WildernessForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "SpacetimeForce").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Spooky.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "TerrorForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "HorrorForce").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Redemption.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "AdvancementForce").UpdateAccessory(player, hideVisual);
            }
            if (Item.type == ModContent.ItemType<EternitySoul>() && !CSEConfig.Instance.AlternativeSiblings)
            {
                if (CSEConfig.Instance.SecretBosses)
                {
                    ModContent.Find<ModItem>(Mod.Name, "CyclonicFin").UpdateAccessory(player, hideVisual);
                }
                ModContent.Find<ModItem>(Mod.Name, "EternityForce").UpdateAccessory(player, hideVisual);
            }
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            //just in case
            //no cal inheritance because mutant enrages
            if (item.damage < 100000 && item.damage > 10000 && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
            {
                damage *= 0.1f;
            }
            if (item.damage > 100000 && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
            {
                damage *= 0.01f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (CSEConfig.Instance.DebugMode)
            {
                string internalName = CSEUtils.GetItemInternalName(item);
                TooltipLine internalNameLine = new TooltipLine(Mod, "internalName", $"ItemID:{item.type}\nInternalName:{internalName}");
                internalNameLine.OverrideColor = Color.Red;
                tooltips.Add(internalNameLine);
            }

            if (item.ModItem is BaseSoul)
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    tooltips[i].Text = Regex.Replace(tooltips[i].Text, "22%", "25%", RegexOptions.IgnoreCase);
                }
            }
            //if (item.type == ModContent.ItemType<UniverseSoul>() && ModCompatibility.SacredTools.Loaded && ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded)
            //{
            //    tooltips.Add(new TooltipLine(Mod, "balance", $"[c/00A36C:CSE Balance:] Additional 2 minion slots."));
            //}
            if (ModCompatibility.Crossmod.Loaded)
            {
                if (item.type == ModContent.ItemType<SlimeRain>() || item.type == ModContent.ItemType<GuardianTome>() || item.type == ModContent.ItemType<TheBiggestSting>() || item.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>() || item.type == ModContent.ItemType<TheBiggestSting>())
                {
                    tooltips.Add(new TooltipLine(Mod, "balance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} No."));
                }
            }
            if (item.type == ModContent.ItemType<MutantsCurse>() || item.type == ModContent.ItemType<AbominationnVoodooDoll>())
            {
                //tooltips.Add(new TooltipLine(Mod, "1m", "Mutant max life and damage scales with ammount of supported mods."));
                //tooltips.Add(new TooltipLine(Mod, "2m", $"Points: {Math.Round(CSENpcs.multiplierM, 1)}, Max Life: {10000000 + Math.Round(CSENpcs.multiplierM, 1) * 10000000}, Damage: {500 + Math.Round(CSENpcs.multiplierM, 1) * (ModCompatibility.Calamity.Loaded ? 125 : 100)}"));
                //tooltips.Add(new TooltipLine(Mod, "3m", "Thorium adds 0.9 points. SoA adds 1.3. Calamity 1.8"));
                //tooltips.Add(new TooltipLine(Mod, "4m", "If olnly one of supported mods active Thorium - 1 SoA - 2 Calamity 2.8"));
                //tooltips.Add(new TooltipLine(Mod, "5m", "If Masochist mode enabled, points multiplied by 1.5"));
                if (ModCompatibility.SacredTools.Loaded && CSEConfig.Instance.ExperimentalContent)
                {
                    tooltips.Add(new TooltipLine(Mod, "7m", "In first phase Mutant has Aura of Supression. After destroying aura second phase will start."));
                    tooltips.Add(new TooltipLine(Mod, "8m", "Aura can be destroyed only with Relic Weapons or Styx Armor set bonus. Mutant immune to damage if aura active."));
                }
            }
            if (ModCompatibility.Thorium.Loaded)
            {
                if (item.type == ModContent.ItemType<NekomiHood>() || item.type == ModContent.ItemType<NekomiHoodie>() || item.type == ModContent.ItemType<NekomiLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 5%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 5 and max inspiration by 10"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 1 and max bard buffs duration"));
                }
                if (item.type == ModContent.ItemType<StyxCrown>() || item.type == ModContent.ItemType<StyxChestplate>() || item.type == ModContent.ItemType<StyxLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 20%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 10 and max inspiration by 30"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 2 and max bard buffs duration"));
                }
                if (item.type == ModContent.ItemType<MutantBody>() || item.type == ModContent.ItemType<MutantMask>() || item.type == ModContent.ItemType<MutantPants>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 100%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 50 and max inspiration by 100"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 2 and max bard buffs duration"));
                }
                if (item.type == ModContent.ItemType<GaiaGreaves>() || item.type == ModContent.ItemType<GaiaHelmet>() || item.type == ModContent.ItemType<GaiaPlate>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 20%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 10 and max inspiration by 30"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 2 and max bard buffs duration"));
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (item.type == ModContent.ItemType<NekomiHood>() || item.type == ModContent.ItemType<NekomiHoodie>() || item.type == ModContent.ItemType<NekomiLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 70"));
                }
                if (item.type == ModContent.ItemType<StyxChestplate>() || item.type == ModContent.ItemType<StyxCrown>() || item.type == ModContent.ItemType<StyxLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 200"));
                }
                if (item.type == ModContent.ItemType<MutantBody>() || item.type == ModContent.ItemType<MutantMask>() || item.type == ModContent.ItemType<MutantPants>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 500"));
                }
                if (item.type == ModContent.ItemType<GaiaPlate>() || item.type == ModContent.ItemType<GaiaHelmet>() || item.type == ModContent.ItemType<GaiaGreaves>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 110"));
                }
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            if (item.CountsAsClass<ThrowingDamageClass>())
            {
                velocity *= player.CSE().throwerVelocity;
            }
        }
        public override void UpdateEquip(Item Item, Player player)
        {
            if (ModCompatibility.Calamity.Loaded)
            {
                if (Item.type == ModContent.ItemType<StyxCrown>())
                {
                    player.GetCritChance(DamageClass.Generic) += 10f;
                    player.GetDamage(DamageClass.Generic) += 5 / 100f;
                    player.maxMinions += 5;
                }
                if (Item.type == ModContent.ItemType<StyxChestplate>())
                {
                    player.GetDamage(DamageClass.Generic) += 5 / 100f;
                }
                if (Item.type == ModContent.ItemType<StyxLeggings>())
                {
                    player.GetDamage(DamageClass.Generic) += 5 / 100f;
                }
            }
        }
    }
}
