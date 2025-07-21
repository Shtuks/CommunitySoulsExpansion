using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;

namespace ssm.CrossMod
{
    public class ThrowerMergeTooltips : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod("ThrowerUnification");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.defense > 0 || item.accessory)
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (i == 0) continue;

                    //if (CSEConfig.Instance.ThrowerMerge)
                    //{
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "kinetic", "thrower");
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "rogue", "thrower");

                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "Kinetic", "Thrower");
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "Rogue", "Thrower");
                    //}
                }
            }
        }
    }
}