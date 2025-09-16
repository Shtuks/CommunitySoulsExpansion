using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.DataStructures;
using Terraria.ID;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using ssm.Calamity.Addons;
using ssm.Core;

namespace ssm.CrossMod.CraftingStations
{
    public class MutantsForgeItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.value = Item.buyPrice(10, 0, 0, 0);
            Item.CloneDefaults(ModContent.ItemType<CrucibleCosmos>());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(16, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
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

        public override void SetDefaults()
        {
            Item.width = 110;
            Item.height = 80;
            Item.rare = 10;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(2, 0, 0, 0);
            Item.createTile = ModContent.TileType<MutantsForgeTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (ModLoader.HasMod("CalamityMod"))
            {
                recipe.AddIngredient<DemonshadeWorkbenchItem>();
            }

            if (ModLoader.HasMod("SacredTools"))
            {
                recipe.AddIngredient<SyranCraftingStationItem>();
            }

            if (ModLoader.HasMod("ThoriumMod"))
            {
                recipe.AddIngredient<DreamersForgeItem>();
            }

            if (ModLoader.HasMod("Redemption"))
            {
                recipe.AddIngredient<RedemptionCraftingStationItem>();
            }

            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.WrathoftheGods.Mod.Find<ModItem>("StarlitForge"), 1);
            }

            recipe.AddIngredient<EternalEnergy>(30);
            recipe.AddIngredient<CrucibleCosmos>();
            recipe.Register();
        }
    }
}
