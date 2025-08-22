using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Thorium.Emode.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DarkenedCloak : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EmodeThorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 6;
            //    Item.value = 100000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CSEThoriumPlayer>().DarkenedCloak = true;
        }
    }
}
