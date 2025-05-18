using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ssm.CrossMod.Accessories;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Content.Items.Accessories;

namespace ssm.CrossMod
{
    [ExtendsFromMod(ModCompatibility.Crossmod.Name, ModCompatibility.Thorium.Name)]
    public class DlcTorOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()) && recipe.HasIngredient(ModContent.ItemType<Nanotech>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<Nanotech>());
                    recipe.AddIngredient<GtTETFinal>(1);
                }
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.Crossmod.Name, ModCompatibility.Thorium.Name)]
    public class DlcTorOtherItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<VagabondsSoul>() || item.type == ModContent.ItemType<UniverseSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<StargateSoul>())
            {
                ModContent.Find<ModItem>(this.Mod.Name, "GtTETFinal").UpdateAccessory(player, hideVisual);
            }
        }
    }
}
