using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using ssm.Core;
using ssm.Thorium.Souls;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ThoriumMod.Items.Terrarium;
using CalamityMod.Items.SummonItems;
using ThoriumMod.Items.BossLich;
using FargowiltasSouls.Content.Items.Materials;
using ContinentOfJourney.Items.ThrowerWeapons;
using Terraria.ID;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.ThrownItems;

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

                if (recipe.HasResult<GalacticaSingularity>() && !recipe.HasIngredient<CelestialFragment>())
                {
                    recipe.AddIngredient<CelestialFragment>(1);
                    recipe.AddIngredient<ShootingStarFragment>(1);
                    recipe.AddIngredient<WhiteDwarfFragment>(1);
                }

                if (ModContent.TryFind("EmpoweredGranite", out ModItem var) && ModContent.TryFind("EnchantedMarble", out ModItem var1))
                {
                    if (recipe.HasResult<OverloadedSludge>() && recipe.HasIngredient(var))
                    {
                        recipe.RemoveIngredient(var.Type);
                        recipe.RemoveIngredient(var1.Type);
                    }
                }
                if (ModContent.TryFind("StrangeAlienMotherboard", out ModItem var3))
                {
                    if ((recipe.HasResult(ItemID.MechanicalEye) || recipe.HasResult(ItemID.MechanicalSkull) || recipe.HasResult(ItemID.MechanicalWorm)) && recipe.HasIngredient(var3))
                    {
                        recipe.RemoveIngredient(var3.Type);
                    }
                }
                if (ModContent.TryFind("StormFeather", out ModItem var4))
                {
                    if (recipe.HasResult<DesertMedallion>() && recipe.HasIngredient(var4))
                    {
                        recipe.RemoveIngredient(var4.Type);
                    }
                }
                if (ModContent.TryFind("StriderFang", out ModItem var5))
                {
                    if (recipe.HasResult<CryoKey>() && recipe.HasIngredient(var5))
                    {
                        recipe.RemoveIngredient(var5.Type);
                    }
                }
                if (ModContent.TryFind("VoidseerPearl", out ModItem var6))
                {
                    if ((recipe.HasResult<CharredIdol>() || recipe.HasResult<EyeofDesolation>()) && recipe.HasIngredient(var6))
                    {
                        recipe.RemoveIngredient(var6.Type);
                    }
                }
                if (recipe.HasResult<DeathWhistle>() && recipe.HasIngredient<CursedCloth>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<CursedCloth>());
                }
            }
        }
    }
}


