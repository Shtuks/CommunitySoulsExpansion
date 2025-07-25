using Terraria;
using Terraria.ModLoader;
using ssm.CrossMod.CraftingStations;
using ssm.Content.Items.Consumables;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Accessories;
using Fargowiltas.Items.Vanity;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class TrueLumberjackPants : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return FargoSoulsUtil.AprilFools;
        }
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
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LumberjackPants>();

            if (CSEConfig.Instance.AlternativeSiblings) { recipe.AddIngredient<Sadism>(100); recipe.AddIngredient<StargateSoul>(4); }
            recipe.AddIngredient<EternalEnergy>(100);
            recipe.AddIngredient<Eridanium>(100);
            recipe.AddIngredient<AbomEnergy>(100);
            recipe.AddIngredient<DeviatingEnergy>(100);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
