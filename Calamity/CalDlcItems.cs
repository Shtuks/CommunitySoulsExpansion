using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using FargowiltasSouls.Content.Items.Summons;
using NoxusBoss.Core.World.WorldSaving;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name, ModCompatibility.WrathoftheGods.Name)]
    internal class CalDlcItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<MutantsCurse>())
            {
                tooltips.Add(new TooltipLine(Mod, "PostND", $"[c/FF0000:Cross-mod Balance:] Can be used after defeating the Nameless Deity"));
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<MutantsCurse>())
                return BossDownedSaveSystem.HasDefeated<NamelessDeityBoss>();
            return base.CanUseItem(item, player);
        }
    }
}
