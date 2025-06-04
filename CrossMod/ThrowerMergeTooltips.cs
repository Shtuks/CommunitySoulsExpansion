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
            for (int i = 0; i < tooltips.Count; i++)
            {
                tooltips[i].Text = Regex.Replace(tooltips[i].Text, "kinetic", "thrower", RegexOptions.IgnoreCase);
                tooltips[i].Text = Regex.Replace(tooltips[i].Text, "rogue", "thrower", RegexOptions.IgnoreCase);
            }
        }
    }
}
