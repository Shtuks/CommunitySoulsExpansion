using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Items.Accessories
{
    public class MicroverseSoul : BaseSoul
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return (ModCompatibility.Redemption.Loaded || ModCompatibility.Polarities.Loaded || ModCompatibility.Spooky.Loaded || ModCompatibility.Homeward.Loaded);
        }
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.value = 1000000;
            Item.rare = 11;
            Item.accessory = true;
            Item.defense = 10;
        }

        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            if (ModCompatibility.Spooky.Loaded)
            {
                tooltips.Add(new TooltipLine(Mod, "Spooky1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Spooky1")));
                tooltips.Add(new TooltipLine(Mod, "Spooky1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Spooky2")));
            }
            if (ModCompatibility.Polarities.Loaded)
            {
                tooltips.Add(new TooltipLine(Mod, "Polarities1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Polarities1")));
                tooltips.Add(new TooltipLine(Mod, "Polarities1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Polarities2")));
            }
            if (ModCompatibility.Redemption.Loaded)
            {
                tooltips.Add(new TooltipLine(Mod, "Redemption1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Redemption1")));
                tooltips.Add(new TooltipLine(Mod, "Redemption1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Redemption2")));
            }
            if (ModCompatibility.Homeward.Loaded)
            {
                tooltips.Add(new TooltipLine(Mod, "Homeward1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Homeward1")));
                tooltips.Add(new TooltipLine(Mod, "Homeward1", Language.GetTextValue("Mods.ssm.Items.MicroverseSoul.Homeward2")));
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModCompatibility.Spooky.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "HorrorForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "TerrorForce").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Polarities.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "SpacetimeForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "WildernessForce").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Redemption.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "AdvancementForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "AchivementForce").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Homeward.Loaded)
            {
                //ModContent.Find<ModItem>(Mod.Name, "AdvancementForce").UpdateAccessory(player, false);
                //ModContent.Find<ModItem>(Mod.Name, "AchivementForce").UpdateAccessory(player, false);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            if (ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("ShadowspecBar"), 5);
            }
            if (!ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient<AbomEnergy>(10);
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("EmberOfOmen"), 1);
            }
            if (ModCompatibility.Redemption.Loaded)
            {
                recipe.AddIngredient(ModContent.Find<ModItem>(Mod.Name, "AdvancementForce"), 1);
                recipe.AddIngredient(ModContent.Find<ModItem>(Mod.Name, "AchivementForce"), 1);
            }
            if (ModCompatibility.Spooky.Loaded)
            {
                recipe.AddIngredient(ModContent.Find<ModItem>(Mod.Name, "HorrorForce"), 1);
                recipe.AddIngredient(ModContent.Find<ModItem>(Mod.Name, "TerrorForce"), 1);
            }
            if (ModCompatibility.Polarities.Loaded)
            {
                recipe.AddIngredient(ModContent.Find<ModItem>(Mod.Name, "SpacetimeForce"), 1);
                recipe.AddIngredient(ModContent.Find<ModItem>(Mod.Name, "WildernessForce"), 1);
            }

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}
