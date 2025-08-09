using FargowiltasSouls.Content.Items.Summons;
using NoxusBoss.Content.Items;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;
using NoxusBoss.Core.World.WorldSaving;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
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
                tooltips.Add(new TooltipLine(Mod, "PostMonstrosity", $"{Language.GetTextValue("Mods.ssm.Balance.PostMonstrosity")}"));
            }
            if (item.type == ModContent.ItemType<MutantsCurse>())
            {
                tooltips.Add(new TooltipLine(Mod, "PostND", $"{Language.GetTextValue("Mods.ssm.Balance.PostND")}"));
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<MutantsCurse>())
            {
                return BossDownedSaveSystem.HasDefeated<NamelessDeityBoss>();
            }
            return base.CanUseItem(item, player);
        }
    }
}
