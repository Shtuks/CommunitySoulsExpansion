using ssm.Core;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace ssm.Homeward.Badges
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name, ModCompatibility.Thorium.Name)]
    public class BardBadge : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<HealerBadge>();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = 10;
            Item.value = Item.sellPrice(0, 15);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(BardDamage.Instance) += 0.2f;
            player.GetCritChance(BardDamage.Instance) += 5f;
        }
    }
}
