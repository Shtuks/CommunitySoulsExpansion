using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Buffs;

namespace ssm.Content.Items.Consumables
{
    public class Sadism : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.AlternativeSiblings;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 30;
            Item.rare = 11;
            Item.value = Item.sellPrice(10, 0, 0, 0);
        }
    }
}
