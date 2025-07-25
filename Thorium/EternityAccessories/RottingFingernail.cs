using ssm.Thorium;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Core;



namespace ssm.Thorium.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class RottingFingernail : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.ExperimentalContent;
        }
        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 22;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 1);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<GraveLimbDebuff>()] = true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
            float customScale = 1.1f;
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
            scale *= 1.1f;
            return true;
        }
    }
}
