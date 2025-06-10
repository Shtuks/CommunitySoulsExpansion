using Terraria;
using Terraria.ModLoader;
using ssm.CrossMod.CraftingStations;
using Fargowiltas.Items.Vanity;
using ssm.Content.Items.Accessories;
using ssm.Content.Items.Consumables;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueLumberjackBody : ModItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(100, 0, 0, 0);
            this.Item.defense = int.MaxValue / 1000;
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
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += int.MaxValue / 1000;
            player.GetCritChance(DamageClass.Generic) += int.MaxValue / 1000;
            player.statLifeMax2 += int.MaxValue / 100000;
            player.statManaMax2 += int.MaxValue / 100000;
            player.endurance += int.MaxValue / 100;
            //infinitie regen
            player.statLife = player.statLifeMax2;
            player.lifeRegenCount += int.MaxValue / 1000;
            player.lifeRegenTime += int.MaxValue / 1000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LumberjackBody>();

            recipe.AddIngredient<Sadism>(100);
            recipe.AddIngredient<StargateSoul>(4);
            //recipe.AddIngredient<ShardOfStarlight>(30);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
