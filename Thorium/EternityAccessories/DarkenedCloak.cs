using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Thorium.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DarkenedCloak : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 6;
            //    Item.value = 100000;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
            float customScale = 1.2f;
            spriteBatch.Draw(
                texture,
                position,
                null,
                drawColor,
                0f,
                origin,
                customScale,
                SpriteEffects.None,
                0f
            );
            return false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ShtunThoriumPlayer>().DarkenedCloak = true;
        }
    }
}
