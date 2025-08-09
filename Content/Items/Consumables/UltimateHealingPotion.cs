using CalamityMod.Items.Potions;
using FargowiltasSouls.Content.Items.Materials;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm.CrossMod.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Consumables
{
    public class UltimateHealingPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = 11;
            Item.value = Item.buyPrice(gold: 10);

            Item.healLife = 10000;
            Item.potion = true;
        }
        public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
            healValue = player.statLifeMax2;
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
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);

            if (ModCompatibility.SacredTools.Loaded)
            {
                ModCompatibility.SacredTools.Mod.TryFind<ModItem>("AsthraltiteHealingPotion", out ModItem soa);
                recipe.AddIngredient(soa ,50);
            }

            if (!ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded)
            {
                ModCompatibility.Calamity.Mod.TryFind<ModItem>("OmegaHealingPotion", out ModItem cal);
                recipe.AddIngredient(cal, 50);
            }

            if (!ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient(ItemID.SuperHealingPotion, 50);
            }

            recipe.AddIngredient<EternalEnergy>(1);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}