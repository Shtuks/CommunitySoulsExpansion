using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod;
using ssm.Core;

namespace ssm.Homeward.Badges
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name, ModCompatibility.Thorium.Name)]
    public class HealerBadge : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<BardBadge>();
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
            player.GetDamage(HealerDamage.Instance) += 0.2f;
            player.GetCritChance(HealerDamage.Instance) += 5f;
        }
    }
}
