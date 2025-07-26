using SacredTools;
using SacredTools.Common.Players;
using SacredTools.Content.Items.Accessories.Wings;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Vanity.Headsets;
using ssm.Core;
using ssm.SoA;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using SoAPlayer = ssm.SoA.SoAPlayer;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAItems : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.CalWingEffects.";

            if (item.type == ModContent.ItemType<AsthraltiteWings>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Asthral")));
            }
            if (item.type == ModContent.ItemType<AuroraWings>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Aurora")));
            }
            if (item.type == ModContent.ItemType<QuasarWings>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Quasar")));
            }
            if (item.type == ModContent.ItemType<CryoditeWings>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Cryo")));
            }
            if (item.type == ModContent.ItemType<FlariumWings>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Flarium")));
            }
            if (item.type == ModContent.ItemType<DespairBoosters>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Despair")));
            }
            if (item.type == ModContent.ItemType<DevilWings>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Devil")));
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<QuasarWings>() && player.GetModPlayer<ModdedPlayer>().NovaSetEffect)
            {
                player.CSE().throwerVelocity += 0.15f;
                player.GetCritChance<ThrowingDamageClass>() += 0.1f;
            }
            if (item.type == ModContent.ItemType<FlariumWings>() && player.GetModPlayer<ModdedPlayer>().DragonSetEffect)
            {
                player.GetModPlayer<SoAPlayerTest>().flariumWings = true;
            }
            if (item.type == ModContent.ItemType<DespairBoosters>() && player.GetModPlayer<ModdedPlayer>().voidOffense)
            {
                player.GetDamage<GenericDamageClass>() += 0.15f;
            }
            if (item.type == ModContent.ItemType<DespairBoosters>() && player.GetModPlayer<ModdedPlayer>().voidDefense)
            {
                player.endurance += 0.2f;
            }
            if (item.type == ModContent.ItemType<AsthraltiteWings>() && player.GetModPlayer<ModdedPlayer>().AstralSet)
            {
                player.GetModPlayer<DebuffPlayer>().suppression += 50 / 100f;
            }
        }
    }
}
