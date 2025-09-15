﻿using CalamityMod.Items.Weapons.Melee;
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
using CalamityMod.Items.SummonItems;
using Terraria.Localization;
using ssm.Content.NPCs.RealMutantEX;
using FargowiltasSouls.Content.Bosses.MutantBoss;

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
                entity.damage = (int)(entity.damage * 0.5f);
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
                entity.damage = (int)(entity.damage * 0.8f);
            }
            if (entity.type == ModContent.ItemType<NanoblackReaper>())
            {
                entity.damage = (int)(entity.damage * 1.1f);
            }
            if (entity.type == ModContent.ItemType<ArkoftheCosmos>())
            {
                entity.damage = (int)(entity.damage * 1.1f);
            }
            if (entity.type == ModContent.ItemType<Ataraxia>())
            {
                entity.damage = (int)(entity.damage * 1.3f);
            }
            if (entity.type == ItemID.Zenith)
            {
                entity.damage = (int)(entity.damage * 1.2f);
            }
            if (entity.type == ModContent.ItemType<OmegaHealingPotion>() && ModCompatibility.SacredTools.Loaded)
            {
                entity.healLife = 400;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<IridescentExcalibur>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.CancelDebuff")}"));
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.VelUP")} 50%"));
            }
            if (item.type == ModContent.ItemType<Voidragon>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.CancelBuff")}"));
            }
            if (item.type == ModContent.ItemType<HalibutCannon>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.DamageDown")} 30%"));
            }
            if (item.type == ModContent.ItemType<Supernova>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.DamageDown")} 20%"));
            }
            if (item.type == ModContent.ItemType<Ataraxia>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.CancelDebuff")}"));
            }
            if (item.type == ItemID.Zenith)
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.DamageUP")} 20%"));
            }
            if (item.type == ModContent.ItemType<ArkoftheCosmos>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.DamageUP")} 10%"));
            }
            if (item.type == ModContent.ItemType<Sylvestaff>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.CancelBuff")}"));
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.DamageDown")} 30%"));
            }

            for (int i = tooltips.Count - 1; i >= 0; i--)
            {
                if (tooltips[i].Text.Contains("Support", System.StringComparison.OrdinalIgnoreCase) && item.type == ModContent.ItemType<Terminus>())
                {
                    tooltips.RemoveAt(i); 
                }
            }
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<RealMutantEX>()) || NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()))
            {
                if (item.type == ItemID.Zenith)
                {
                    damage /= 2;
                }
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
