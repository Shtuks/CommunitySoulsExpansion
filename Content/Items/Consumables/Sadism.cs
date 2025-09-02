using Terraria;
using Terraria.ModLoader;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ssm.Content.Items.Materials
{
    public class ShadowEnergy : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        public override void SetDefaults()
        {
            Item.width = 33;
            Item.height = 37;
            Item.maxStack = 30;
            //no i wont change magic number to ItemRarityID
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Main.DiscoColor);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
    }
}
