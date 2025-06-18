using Terraria;
using Terraria.ModLoader;
using ssm.Thorium;

namespace ssm.Content.Items.Accessories
{
	public class ThunderTalonEternity : ModItem
	{
		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 32;
			Item.maxStack = 1;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ShtunThoriumPlayer>().ThunderTalonEternity = true;
		}
	}
}
