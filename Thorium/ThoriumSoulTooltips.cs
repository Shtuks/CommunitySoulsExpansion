using FargowiltasSouls.Content.Items.Accessories.Souls;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class ThoriumSoulsTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";
            
            if (!ModCompatibility.IEoR.Loaded)
            {
                if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
                {
                    tooltips.Insert(8, new TooltipLine(Mod, "ThoriumColossusSoul", Language.GetTextValue(key + "ThoriumColossus")));
                    int index = tooltips.FindIndex((TooltipLine t) => t.Mod.Equals("Terraria") && t.Name.Equals("ItemName"));
                    if (index != -1)
                    {
                        tooltips.Insert(index + 1, new TooltipLine(base.Mod, "AccessoryWarning", "-Omni Shield-")
                        {
                            OverrideColor = new Color(102, 255, 255)
                        });
                    }
                }
            }

            if (item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "ThoriumUniverseSoul",
                    Language.GetTextValue(key + "ThoriumUniverse")));
            }

            if (item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "ThoriumDimestionSoul",
                    Language.GetTextValue(key + "ThoriumColossus")));
            }
        }
    }
}
