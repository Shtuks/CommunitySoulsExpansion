using Fargowiltas.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SpiritMod
{
    public class SpiritNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == ModContent.NPCType<LumberJack>())
            {
                //shop.Add(new Item(ModContent.ItemType<Emberbark>()) { shopCustomPrice = Item.buyPrice(copper: 60) }, new Condition("Mods.ssm.Conditions.AbaddonDowned", () => DownedSystem.DownedAbaddon));
            }
            base.ModifyShop(shop);
        }
    }
}
