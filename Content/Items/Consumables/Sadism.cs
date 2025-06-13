using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Buffs;

namespace ssm.Content.Items.Consumables
{
    public class Sadism : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 30;
            Item.rare = 11;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<SadismEX>();
            Item.buffTime = int.MaxValue;
            Item.UseSound = SoundID.Item3;
            Item.value = Item.sellPrice(10, 0, 0, 0);
        }
    }
}
