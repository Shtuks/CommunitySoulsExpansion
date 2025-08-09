using CalamityHunt.Content.Items.BossBags;
using CalamityHunt.Content.Items.Materials;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using ssm.Core;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Goozma.Name)]
    [JITWhenModsEnabled(ModCompatibility.Goozma.Name)]
    public class HotGItems : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if(item.type == ModContent.ItemType<TreasureTrunk>() || item.type == ModContent.ItemType<TreasureBucket>())
            {
                itemLoot.Add(ItemDropRule.ByCondition(new EModeDropCondition(), ModContent.ItemType<ChromaticMass>(), 1, 20, 30));
            }
        }
    }
}
