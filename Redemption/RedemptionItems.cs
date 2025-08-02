using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Redemption.BaseExtension;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Summon;
using ssm.Content.Items.Accessories;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionItems : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<MutagenMelee>())
            {
                player.GetDamage(DamageClass.Melee) += 0.05f;
                player.GetCritChance(DamageClass.Melee) += 10f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
                player.kbGlove = true;
                player.autoReuseGlove = true;
            }
            if (item.type == ModContent.ItemType<MutagenMagic>())
            {
                player.GetDamage(DamageClass.Magic) += 0.05f;
                player.GetCritChance(DamageClass.Magic) += 10f;
                player.statManaMax2 += 100;
            }
            if (item.type == ModContent.ItemType<MutagenSummon>())
            {
                player.GetDamage(DamageClass.Summon) += 0.05f;
                player.GetAttackSpeed(DamageClass.Summon) += 10f;
                player.maxMinions += 2;
            }
            if (item.type == ModContent.ItemType<MutagenRanged>())
            {
                player.GetDamage(DamageClass.Ranged) += 0.05f;
                player.GetCritChance(DamageClass.Ranged) += 10f;
                player.FargoSouls().RangedEssence = true;
            }

            if (item.type == ModContent.ItemType<ColossusSoul>() || item.type == ModContent.ItemType<DimensionSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<StargateSoul>())
            {
                HEVSuitPlayer modPlayer = player.GetModPlayer<HEVSuitPlayer>();
                modPlayer.HideVanity = hideVisual;
                modPlayer.VanityOn = true;
                player.buffImmune[ModContent.BuffType<BileDebuff>()] = true;
                player.buffImmune[ModContent.BuffType<GreenRashesDebuff>()] = true;
                player.buffImmune[ModContent.BuffType<GlowingPustulesDebuff>()] = true;
                player.buffImmune[ModContent.BuffType<FleshCrystalsDebuff>()] = true;
                player.buffImmune[ModContent.BuffType<ShockDebuff>()] = true;
                player.RedemptionRad().protectionLevel += 3;
                player.RedemptionPlayerBuff().WastelandWaterImmune = true;
                player.accDivingHelm = true;
            }
        }

        public override void SetDefaults(Item entity)
        {
            if (ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded)
            {
                if (entity.type == ModContent.ItemType<Twinklestar>())
                {
                    entity.damage = (int)(entity.damage * 1.6f);
                }
                if (entity.type == ModContent.ItemType<Constellations>())
                {
                    entity.damage = (int)(entity.damage * 1.3f);
                }
                if (entity.type == ModContent.ItemType<CosmosChains>())
                {
                    entity.damage = (int)(entity.damage * 1.2f);
                }
                if (entity.type == ModContent.ItemType<PiercingNebulaWeapon>())
                {
                    entity.damage = (int)(entity.damage * 1.5f);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded)
            {
                if (item.type == ModContent.ItemType<Twinklestar>())
                {
                    tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.DamageUP")} 60%"));
                }
                if (item.type == ModContent.ItemType<Constellations>())
                {
                    tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.DamageUP")} 30%"));
                }
                if (item.type == ModContent.ItemType<CosmosChains>())
                {
                    tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.DamageUP")} 20%"));
                }
                if (item.type == ModContent.ItemType<PiercingNebulaWeapon>())
                {
                    tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.DamageUP")} 50%"));
                }
            }
        }
    }
}
