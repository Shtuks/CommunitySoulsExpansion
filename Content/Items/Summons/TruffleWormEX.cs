using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Fargowiltas.Items.Tiles;
using Fargowiltas.Items.Summons.VanillaCopy;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Content.Items.Summons
{
    public class TruffleWormEX : ModItem
    {
        public override string Texture => "Terraria/Images/Item_2673";

        public override void SetDefaults()
        {
            Item.maxStack = 20;
            Item.rare = 11;
            Item.width = 12;
            Item.height = 12;
            Item.bait = 1487;
            Item.value = Item.sellPrice(0, 17, 0, 0);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<TruffleWorm2>(10);
            recipe.AddIngredient(ItemID.TruffleWorm, 10);
            recipe.AddIngredient(ItemID.ShrimpyTruffle);
            recipe.AddIngredient<AbomEnergy>(5);
            //recipe.AddIngredient<EternalEnergy>(5);
            recipe.AddIngredient<DeviatingEnergy>(5);
            recipe.AddIngredient<Eridanium>(5);
            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}
