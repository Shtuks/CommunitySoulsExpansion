using Terraria;
using Terraria.ModLoader;
using ssm.CrossMod.CraftingStations;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Content.Items.Armor
{
    //[ExtendsFromMod(ModCompatibility.Calamity.Name)]
    //[JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Legs)]
    public class TrueMonstrosityPants : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(100, 0, 0, 0);
            this.Item.defense = 200;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 2f;
            player.GetCritChance(DamageClass.Generic) += 100f;
            player.moveSpeed += 0.5f;
            player.statLifeMax2 += 500;
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
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<EternalEnergy>(15);
            recipe.AddIngredient<Sadism>(15);
            recipe.AddIngredient<MonstrosityPants>();

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
