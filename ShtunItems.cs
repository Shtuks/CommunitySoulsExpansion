using BombusApisBee.BeeDamageClass;
using ClickerClass.Utilities;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using ssm.Content.DamageClasses;
using ssm.Content.Items.Accessories;
using ssm.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace ssm
{
    public class ShtunItems : GlobalItem
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
            }
            if (entity.type == ModContent.ItemType<GuardianTome>())
            {
                entity.damage = 1500;
            }
            if (entity.type == ModContent.ItemType<TheBiggestSting>())
            {
                entity.damage = 7750;
            }
            if (entity.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>())
            {
                entity.damage = 2800;
            }
            if (entity.type == ModContent.ItemType<SlimeRain>())
            {
                entity.damage = 6000;
            }
        }

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<UniverseSoul>() && ModCompatibility.SacredTools.Loaded && ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded)
            {
                player.maxMinions += 2;
            }

            //SoU
            if (ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    ModContent.Find<ModItem>(this.Mod.Name, "StalkerSoul").UpdateAccessory(player, hideVisual);
                }
            }
            if (ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "BardSoul").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(this.Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.BeekeeperClass.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "BeekeeperSoul").UpdateAccessory(player, hideVisual);
            }

            //SoE
            if (ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "SoASoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Calamity.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "CalamitySoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "ThoriumSoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Polarities.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "WildernessForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(this.Mod.Name, "SpacetimeForce").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Spooky.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "TerrorForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(this.Mod.Name, "HorrorForce").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Redemption.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "AdvancementForce").UpdateAccessory(player, hideVisual);
            }
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()) && damage.Flat > 100000 && !ShtunUtils.IsModItem(item, "SacredTools") && !ShtunUtils.IsModItem(item, "FargowiltasSouls"))
            {
                damage *= 0.1f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<UniverseSoul>() && ModCompatibility.SacredTools.Loaded && ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded)
            {
                tooltips.Add(new TooltipLine(Mod, "balance", $"[c/00A36C:CSE Balance:] Additional 2 minion slots."));
            }
            if (ModCompatibility.Crossmod.Loaded)
            {
                if (item.type == ModContent.ItemType<SlimeRain>() || item.type == ModContent.ItemType<GuardianTome>() || item.type == ModContent.ItemType<TheBiggestSting>() || item.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>() || item.type == ModContent.ItemType<TheBiggestSting>())
                {
                    tooltips.Add(new TooltipLine(Mod, "balance", $"[c/FF0000:CSE Balance:] No."));
                }
            }
            if (item.type == ModContent.ItemType<MutantsCurse>() || item.type == ModContent.ItemType<AbominationnVoodooDoll>())
            {
                tooltips.Add(new TooltipLine(Mod, "1m", "Mutant max life and damage scales with ammount of supported mods."));
                tooltips.Add(new TooltipLine(Mod, "2m", $"Points: {Math.Round(ShtunNpcs.multiplierM, 1)}, Max Life: {10000000 + Math.Round(ShtunNpcs.multiplierM, 1) * 10000000}, Damage: {500 + Math.Round(ShtunNpcs.multiplierM, 1) * 100}"));
                tooltips.Add(new TooltipLine(Mod, "3m", "Thorium adds 0.9 points. SoA adds 1.3. Calamity 1.8"));
                tooltips.Add(new TooltipLine(Mod, "4m", "If olnly one of supported mods active Thorium - 1 SoA - 2 Calamity 2.8"));
                tooltips.Add(new TooltipLine(Mod, "5m", "If Masochist mode enabled, points multiplied by 1.5"));
                if (ModCompatibility.Calamity.Loaded)
                {
                    tooltips.Add(new TooltipLine(Mod, "6m", "Rage and Adrenaline disabled during fight."));
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    tooltips.Add(new TooltipLine(Mod, "7m", "In first phase Mutant will have Aura of Supression. After destroying aura second phase will start."));
                    tooltips.Add(new TooltipLine(Mod, "8m", "Aura can be destroyed only with Relic Weapons or Styx Armor set bonus. Mutant immune to damage if aura active."));
                }
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            if (item.CountsAsClass<UnitedModdedThrower>())
            {
                velocity *= player.Shtun().throwerVelocity;
            }
        }

        public override void UpdateEquip(Item Item, Player player)
        {
            if (player.FargoSouls().MutantSetBonusItem != null)
            {
                player.Shtun().throwerVelocity += 0.2f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(player, 20); }
                if (ModCompatibility.ClikerClass.Loaded) { Clicker(player, 0.5f); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(player, 30); }
            }

            if (player.FargoSouls().StyxSet == true)
            {
                player.Shtun().throwerVelocity += 0.1f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(player, 10); }
                if (ModCompatibility.ClikerClass.Loaded) { Clicker(player, 0.25f); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(player, 15); }
            }

            if (ModCompatibility.Calamity.Loaded)
            {
                if (Item.type == ModContent.ItemType<StyxCrown>())
                {
                    Item.defense = 35;
                    player.GetDamage(DamageClass.Generic) += 10 / 100f;
                    player.maxMinions += 5;
                }
                if (Item.type == ModContent.ItemType<StyxChestplate>())
                {
                    Item.defense = 45;
                    player.GetDamage(DamageClass.Generic) += 10 / 100f;
                }
                if (Item.type == ModContent.ItemType<StyxLeggings>())
                {
                    Item.defense = 40;
                    player.GetDamage(DamageClass.Generic) += 10 / 100f;
                }
            }
        }

        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        public void BardAndHealer(Player player, int bonus)
        {
            player.GetThoriumPlayer().healBonus += bonus;
            player.GetThoriumPlayer().bardResourceMax2 += bonus;
        }

        [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
        public void Beekeeper(Player player, int bonus)
        {
            player.GetModPlayer<BeeDamagePlayer>().BeeResourceMax2 += bonus;
        }

        [JITWhenModsEnabled(ModCompatibility.ClikerClass.Name)]
        public void Clicker(Player player, float bonus)
        {
            player.GetClickerPlayer().clickerBonusPercent += bonus;
        }
    }
}
