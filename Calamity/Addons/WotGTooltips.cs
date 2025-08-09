using FargowiltasSouls.Core.ItemDropRules.Conditions;
using NoxusBoss.Content.Items;
using NoxusBoss.Content.NPCs.Bosses.Avatar.SecondPhaseForm;
using NoxusBoss.Content.NPCs.Bosses.Avatar.SpecificEffectManagers;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class WotGTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<CheatPermissionSlip>())
            {
                tooltips.Add(new TooltipLine(Mod, "PostMonstrosity", $"[c/FF0000:Cross-Mod Balance:] Can only be used after defeating the Monstrosity"));
            }
        }
    }
}
