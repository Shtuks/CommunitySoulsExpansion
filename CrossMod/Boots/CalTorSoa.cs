using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using ThoriumMod.Items.Terrarium;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using CalamityMod.Items.Accessories.Wings;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ID;

namespace ssm.CrossMod.Boots
{
    /*
        * Progression look like this:
        * 
        * terraspark
        * zephyr boots
        * angel treads
        * royal runners
        * aeolus boots
        * terrarium particle sprinters
        * celestial treads
        * void spurs
        * elysean tracers
        * seraph tracers.
        * 
        * redemption?
        * no
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class BootsRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Boots;
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // zephyr to treads (if no dlc)
                if (recipe.HasResult(ModContent.ItemType<AngelTreads>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<ZephyrBoots>(1);
                }
                // treads to runners
                if (recipe.HasResult(ModContent.ItemType<RoyalRunners>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient(ItemID.SoulofFlight, 5);
                    recipe.AddIngredient<AngelTreads>(1);
                }
                // runners to aeolus
                if (recipe.HasResult(ModContent.ItemType<AeolusBoots>()) && (recipe.HasIngredient<AngelTreads>() || recipe.HasIngredient<ZephyrBoots>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<ZephyrBoots>());
                    recipe.RemoveIngredient(ModContent.ItemType<AngelTreads>());
                    recipe.AddIngredient<RoyalRunners>(1);
                }
                // aeolus to sprinters
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                }
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && !recipe.HasIngredient<AeolusBoots>())
                {
                    recipe.AddIngredient<AeolusBoots>(1);
                }
                //sprinters to celestial
                if (recipe.HasResult(ModContent.ItemType<TracersCelestial>()) && !recipe.HasIngredient<TerrariumParticleSprinters>())
                {
                    recipe.AddIngredient<TerrariumParticleSprinters>(1);
                }
                if (recipe.HasResult(ModContent.ItemType<TracersCelestial>()) && recipe.HasIngredient<AeolusBoots>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                }
                //celestial to spurs
                if (recipe.HasResult(ModContent.ItemType<VoidSpurs>()) && recipe.HasIngredient<RoyalRunners>() && !ModCompatibility.Homeward.Loaded)
                {
                    recipe.RemoveIngredient(ModContent.ItemType<RoyalRunners>());
                    recipe.AddIngredient<TracersCelestial>(1);
                }
                //spurs to elysian
                if (recipe.HasResult(ModContent.ItemType<TracersElysian>()) && recipe.HasIngredient<TracersCelestial>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<TracersCelestial>());
                    recipe.AddIngredient<VoidSpurs>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class BootsEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Boots;
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<AngelTreads>()
                || Item.type == ModContent.ItemType<RoyalRunners>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                    ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "ZephyrBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<RoyalRunners>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "AngelTreads").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "RoyalRunners").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                || Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "AeolusBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumParticleSprinters").UpdateAccessory(player, false);
            }

            if (//Item.type == ModContent.ItemType<VoidSpurs>()
                Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "TracersCelestial").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "VoidSpurs").UpdateAccessory(player, false);
            }
        }
    }

}

