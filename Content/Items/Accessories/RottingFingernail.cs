using ssm.Thorium;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Buffs;


namespace ssm.Content.Items.Accessories
{
    public class RottingFingernail : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 32;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 1);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<GraveLimbDebuff>()] = true;
        }
        
	}
}
