using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MonstrositySuit : ModItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 10;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
        }

        public override void AddRecipes()
        {
        }
    }
}
