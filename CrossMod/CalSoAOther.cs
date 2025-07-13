using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Potions.Recovery;
using CalamityMod.Items.Potions;

namespace ssm.CrossMod
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //flora fist to gaunlet
                if (recipe.HasResult(ModContent.ItemType<ElementalGauntlet>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<FloraFist>(1);
                }

                if (recipe.HasResult(ModContent.ItemType<AsthraltiteHealingPotion>()) && !recipe.HasIngredient<OmegaHealingPotion>())
                {
                    recipe.AddIngredient<OmegaHealingPotion>(20);
                }

                if (recipe.HasResult(ModContent.ItemType<OmegaHealingPotion>()) && !recipe.HasIngredient<MegaHealingPotion>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<SupremeHealingPotion>());
                    recipe.AddIngredient<MegaHealingPotion>(20);
                }
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Shields;
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<FloraFist>())
            {
                player.GetDamage<MeleeDamageClass>() += 0.02f;
            }
        }
    }

}
