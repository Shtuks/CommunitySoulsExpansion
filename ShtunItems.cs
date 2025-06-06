using BombusApisBee.BeeDamageClass;
using ClickerClass.Utilities;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using Microsoft.Xna.Framework;
using ssm.Content.DamageClasses;
using ssm.Core;
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

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModCompatibility.Crossmod.Loaded)
            {
                if (item.type == ModContent.ItemType<SlimeRain>() || item.type == ModContent.ItemType<GuardianTome>() || item.type == ModContent.ItemType<TheBiggestSting>() || item.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>() || item.type == ModContent.ItemType<TheBiggestSting>())
                {
                    tooltips.Add(new TooltipLine(Mod, "balance", $"[c/FF0000:CSE Balance:] No."));
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
