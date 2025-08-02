using ssm.Thorium.Souls;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using SacredTools.Content.Items.Materials;
using SacredTools.Content.Items.Accessories.Sigils;
using ssm.SoA.Sigils;
using Terraria.ID;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.BardItems;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    public class TorSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ThoriumSoul>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }

                if (recipe.HasResult<MementoMori>() && !recipe.HasIngredient<BardSigil>())
                {
                    recipe.AddIngredient<BardSigil>();
                    recipe.AddIngredient<HealerSigil>();
                }

                if (recipe.HasResult<BardSoul>() && !recipe.HasIngredient<BardSigil>())
                {
                    recipe.AddIngredient<BardSigil>();
                }
                if (recipe.HasResult<GuardianAngelsSoul>() && !recipe.HasIngredient<HealerSigil>())
                {
                    recipe.AddIngredient<HealerSigil>();
                }

                if (recipe.HasResult<HealerSigil>())
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(Mod.Find<ModItem>("HealerBadge"), 1); recipe.RemoveIngredient(ModContent.ItemType<ClericEmblem>()); }
                }

                if (recipe.HasResult<BardSigil>())
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(Mod.Find<ModItem>("BardBadge"), 1); recipe.RemoveIngredient(ModContent.ItemType<BardEmblem>()); }
                }
            }
        }
    }
}
