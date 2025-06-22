using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Thorium.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
	public class ThunderTalonEternity : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 32;
            Item.maxStack = 1;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ShtunThoriumPlayer>().ThunderTalonEternity = true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
            float customScale = 0.92f;
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
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            scale *= 0.92f;
            return true;
        }
    }
}
