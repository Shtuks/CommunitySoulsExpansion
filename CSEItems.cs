using BombusApisBee;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Accessories;
using ssm.Content.NPCs.RealMutantEX;
using ssm.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
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
                entity.damage *= 5;
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
                    entity.defense = 90;
                }
                if (entity.type == ModContent.ItemType<MutantMask>())
                {
                    entity.defense = 70;
                }
                if (entity.type == ModContent.ItemType<MutantPants>())
                {
                    entity.defense = 70;
                }
                if (entity.type == ModContent.ItemType<NekomiHood>())
                {
                    entity.defense = 7;
                }
                if (entity.type == ModContent.ItemType<NekomiHoodie>())
                {
                    entity.defense = 11;
                }
                if (entity.type == ModContent.ItemType<NekomiLeggings>())
                {
                    entity.defense = 7;
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
                entity.damage = 9000;
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

            if (ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded)
            {
                if (entity.type == ModContent.ItemType<NukeFishron>())
                {
                    entity.shootSpeed = 30;
                    entity.useTime = 30;
                    entity.damage = (int)(entity.damage * 1.2f);
                }
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.RodOfHarmony) {
                if (NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()) || NPC.AnyNPCs(ModContent.NPCType<RealMutantEX>()))
                {
                    player.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 6000);
                }
            }
            return base.UseItem(item, player);
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

            if (ModCompatibility.Calamity.Loaded && Item.type == ModContent.ItemType<UniverseSoul>())
            {
                player.GetDamage<GenericDamageClass>() += 0.1f;
                player.GetCritChance<GenericDamageClass>() += 5f;
            }

            //SoU
            if (CSEConfig.Instance.SacredTools && ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    ModContent.Find<ModItem>(Mod.Name, "StalkerSoul").UpdateAccessory(player, hideVisual);
                }
            }
            if (CSEConfig.Instance.Thorium && ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "BardSoul").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, hideVisual);
            }
            if (CSEConfig.Instance.Beekeeper && ModCompatibility.BeekeeperClass.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "BeekeeperSoul").UpdateAccessory(player, hideVisual);
            }

            //SoE
            if (CSEConfig.Instance.SacredTools && ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "SoASoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Crossmod.Loaded && ModCompatibility.Calamity.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "CalamitySoul").UpdateAccessory(player, hideVisual);
            }
            if (CSEConfig.Instance.Thorium && ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "ThoriumSoul").UpdateAccessory(player, hideVisual);
            }
            if (CSEConfig.Instance.SpiritMod && ModCompatibility.SpiritMod.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "SpiritSoul").UpdateAccessory(player, hideVisual);
            }
            if (CSEConfig.Instance.Polarities && ModCompatibility.Polarities.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "WildernessForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "SpacetimeForce").UpdateAccessory(player, hideVisual);
            }
            if (CSEConfig.Instance.Spooky && ModCompatibility.Spooky.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "TerrorForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "HorrorForce").UpdateAccessory(player, hideVisual);
            }
            if (CSEConfig.Instance.Redemption && ModCompatibility.Redemption.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(Mod.Name, "AdvancementForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(Mod.Name, "AchivementForce").UpdateAccessory(player, hideVisual);
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
            //if (item.damage > 10000 && !CSEUtils.IsModItem(item, "CalamityHunt") && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
            //{
            //    damage *= 0.1f;
            //}
            if (item.damage > 0) {
                if (ModCompatibility.Infernum.Loaded && item.ModItem is ModItem)
                {
                    if (item.ModItem.Name == "StormMaidensRetribution")
                    {
                        damage *= 0.1f;
                    }
                }
                if (item.damage < 100000 && item.damage > 10000 && !CSEUtils.IsModItem(item, "CalamityHunt") && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
                {
                    damage *= 0.1f;
                }
                if (item.damage > 100000 && !CSEUtils.IsModItem(item, "CalamityHunt") && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
                {
                    damage *= 0.04f;
                }
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

            if (item.type == ModContent.ItemType<UniverseSoul>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (ModCompatibility.Calamity.Loaded)
                    {
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "50%", "60%", RegexOptions.IgnoreCase);
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "25%", "30%", RegexOptions.IgnoreCase);
                    }
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
                    tooltips.Add(new TooltipLine(Mod, "balance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.CancelDebuff")}"));
                }
            }
            if (item.type == ModContent.ItemType<MutantsCurse>() || item.type == ModContent.ItemType<AbominationnVoodooDoll>())
            {
                tooltips.Add(new TooltipLine(Mod, "1m", "Mutant max life and damage scales with ammount of supported mods."));
                tooltips.Add(new TooltipLine(Mod, "2m", $"Current Max Life: {10000000 + Math.Round(CSENpcs.multiplierML, 1) * 10000000}, Current Damage: {500 + Math.Round(CSENpcs.multiplierMD, 1) * 100}"));
                tooltips.Add(new TooltipLine(Mod, "5m", "If Masochist mode enabled, stats multiplied by 1.5"));
                if (ModCompatibility.SacredTools.Loaded && CSEConfig.Instance.ExperimentalContent)
                {
                    tooltips.Add(new TooltipLine(Mod, "7m", "In first phase Mutant has Aura of Supression. After destroying aura second phase will start."));
                    tooltips.Add(new TooltipLine(Mod, "8m", "Aura can be destroyed only with Relic Weapons or Styx Armor set bonus. Mutant immune to damage if aura active."));
                }
            }
            if (item.type == ModContent.ItemType<MutantsCurseEX>())
            {
                tooltips.Add(new TooltipLine(Mod, "1m", $"Current Max Life: {10000000 + Math.Round(CSENpcs.multiplierML, 1) * 10000000}, Current Damage: {500 + Math.Round(CSENpcs.multiplierMD, 1) * 100}"));
                if (ModCompatibility.SacredTools.Loaded && CSEConfig.Instance.ExperimentalContent)
                {
                    tooltips.Add(new TooltipLine(Mod, "2m", "In first phase Mutant has Aura of Supression. After destroying aura second phase will start."));
                    tooltips.Add(new TooltipLine(Mod, "3m", "Aura can be destroyed only with Relic Weapons or Styx Armor set bonus. Mutant immune to damage if aura active."));
                    tooltips.Add(new TooltipLine(Mod, "3m", "Aura appears again after transition to second phase."));
                }
                if (ModCompatibility.Calamity.Loaded)
                {
                    tooltips.Add(new TooltipLine(Mod, "4m", "Rage and Adrenaline disabled during fight."));
                }
                tooltips.Add(new TooltipLine(Mod, "5m", "Transition to 2nd phase happens on 1/2 of HP instead of 2/3."));
                tooltips.Add(new TooltipLine(Mod, "6m", "Arena will be acitve regardless of phase."));
                tooltips.Add(new TooltipLine(Mod, "7m", "You will die immideately if you get more than 20 (10 if maso mode) hits."));
                tooltips.Add(new TooltipLine(Mod, "8m", "Your hitbox always displayed during fight."));
                tooltips.Add(new TooltipLine(Mod, "9m", "Stardust and Gold enchantments abilities disabled."));
                tooltips.Add(new TooltipLine(Mod, "10m", "Masochism."));
            }
            if (item.damage < 100000 && item.damage > 10000 && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.DamageDown")} 90%"));
            }
            if (item.damage > 100000 && !CSEUtils.IsModItem(item, "CalamityInheritance") && !CSEUtils.IsModItem(item, "SacredTools") && !CSEUtils.IsModItem(item, "FargowiltasSouls") && !CSEUtils.IsModItem(item, "ThoriumMod") && !CSEUtils.IsModItem(item, "CaamityMod"))
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.DamageDown")} 95%"));
            }
            if (ModCompatibility.Thorium.Loaded)
            {
                if (item.type == ModContent.ItemType<NekomiHood>() || item.type == ModContent.ItemType<NekomiHoodie>() || item.type == ModContent.ItemType<NekomiLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium1")}"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium2")} 5%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium4")} 5"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium3")} 2"));
                }
                if (item.type == ModContent.ItemType<StyxCrown>() || item.type == ModContent.ItemType<StyxChestplate>() || item.type == ModContent.ItemType<StyxLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium1")}"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium2")} 20%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium4")} 20"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium3")} 2"));
                }
                if (item.type == ModContent.ItemType<MutantBody>() || item.type == ModContent.ItemType<MutantMask>() || item.type == ModContent.ItemType<MutantPants>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium1")}"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium2")} 100%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium4")} 100"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium3")} 2"));
                }
                if (item.type == ModContent.ItemType<GaiaGreaves>() || item.type == ModContent.ItemType<GaiaHelmet>() || item.type == ModContent.ItemType<GaiaPlate>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium1")}"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium2")} 10%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium4")} 10"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Thorium3")} 2"));
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (item.type == ModContent.ItemType<NekomiHood>() || item.type == ModContent.ItemType<NekomiHoodie>() || item.type == ModContent.ItemType<NekomiLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Cal1")} 70"));
                }
                if (item.type == ModContent.ItemType<StyxChestplate>() || item.type == ModContent.ItemType<StyxCrown>() || item.type == ModContent.ItemType<StyxLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Cal1")} 200"));
                }
                if (item.type == ModContent.ItemType<MutantBody>() || item.type == ModContent.ItemType<MutantMask>() || item.type == ModContent.ItemType<MutantPants>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Cal1")} 500"));
                }
                if (item.type == ModContent.ItemType<GaiaPlate>() || item.type == ModContent.ItemType<GaiaHelmet>() || item.type == ModContent.ItemType<GaiaGreaves>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"{Language.GetTextValue("Mods.ssm.Balance.SetBonus")} {Language.GetTextValue("Mods.ssm.Balance.Cal1")} 110"));
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
