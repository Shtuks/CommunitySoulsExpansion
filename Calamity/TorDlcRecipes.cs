using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using CalamityMod.Items.Materials;
using ssm.Core;
using ssm.Calamity.Souls;
using ssm.Thorium.Souls;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ThoriumMod.Items.Terrarium;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Crossmod.Name)]
    public class TorDlcRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ThoriumSoul>() && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                    recipe.AddIngredient<MiracleMatter>();
                }

                if (recipe.HasResult<MiracleMatter>() && !recipe.HasIngredient<TerrariumCore>())
                {
                    recipe.AddIngredient<TerrariumCore>(5);
                }

                if ((recipe.HasResult<VagabondsSoul>()) && !recipe.HasIngredient<OceanEssence>())
                {
                    recipe.AddIngredient<OceanEssence>(5);
                    recipe.AddIngredient<InfernoEssence>(5);
                    recipe.AddIngredient<DeathEssence>(5);
                }
            }
        }
    }
}


