using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;

namespace ssm.CrossMod
{
    public class ThrowerMergeTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.defense > 0 || item.accessory)
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (ShtunConfig.Instance.ThrowerMerge)
                    {
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "kinetic", "thrower");
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "rogue", "thrower");

                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "Kinetic", "Thrower");
                        tooltips[i].Text = Regex.Replace(tooltips[i].Text, "Rogue", "Thrower");
                    }
                }
            }
        }
    }
}
