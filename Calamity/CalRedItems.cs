using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Redemption.Items.Accessories.HM;
using Redemption.BaseExtension;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name)]
    public class CalRedItems : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.CalWingEffects.";

            if (item.type == ModContent.ItemType<XenomiteJetpack>() && !item.social)
            {
                tooltips.Add(new TooltipLine(Mod, "wings", Language.GetTextValue(key + "Xenomite")));
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<XenomiteJetpack>() && player.RedemptionPlayerBuff().xenomiteBonus)
            {
            }
        }
    }
}
