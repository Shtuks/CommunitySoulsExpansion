using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Calamity.Addons;
using ssm.Content.Items.Materials;
using ssm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Items.Accessories
{
    public class MacroverseSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return (ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded || ModCompatibility.Spirit.Loaded || ModCompatibility.Thorium.Loaded);
        }
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.value = 2500000;
            Item.rare = 11;
            Item.accessory = true;
            Item.defense = 50;
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModCompatibility.Redemption.Loaded || ModCompatibility.Polarities.Loaded || ModCompatibility.Spooky.Loaded || ModCompatibility.Homeward.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "MicroverseSoul").UpdateAccessory(player, false);
            }
            ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "TerrariaSoul").UpdateAccessory(player, false);

            if (ModCompatibility.Calamity.Loaded && ModCompatibility.Crossmod.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "CalamitySoul").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Spooky.Loaded && CSEConfig.Instance.Spooky)
            {
                ModContent.Find<ModItem>(Mod.Name, "HorrorForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "TerrorForce").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Polarities.Loaded && CSEConfig.Instance.Polarities)
            {
                ModContent.Find<ModItem>(Mod.Name, "SpacetimeForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "WildernessForce").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Redemption.Loaded && CSEConfig.Instance.Redemption)
            {
                ModContent.Find<ModItem>(Mod.Name, "AdvancementForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "AchivementForce").UpdateAccessory(player, false);
            }
            if (ModCompatibility.SacredTools.Loaded && CSEConfig.Instance.SacredTools)
            {
                ModContent.Find<ModItem>(Mod.Name, "SoASoul").UpdateAccessory(player, false);
            }
            if (ModCompatibility.Thorium.Loaded && CSEConfig.Instance.Thorium)
            {
                ModContent.Find<ModItem>(Mod.Name, "ThoriumSoul").UpdateAccessory(player, false);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<TerrariaSoul>(1);
            if (CSEConfig.Instance.SecretBosses)
            {
                recipe.AddIngredient<EternalScale>(5);
            }
            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                recipe.AddIngredient<NDMaterialPlaceholder>(5);
                recipe.AddIngredient(ModCompatibility.WrathoftheGods.Mod.Find<ModItem>("MetallicChunk"), 5);
            }
            if (ModCompatibility.Calamity.Loaded && ModCompatibility.Crossmod.Loaded)
            {
                recipe.AddIngredient(Mod.Find<ModItem>("CalamitySoul"), 1);
            }
            if (ModCompatibility.Thorium.Loaded && CSEConfig.Instance.Thorium)
            {
                recipe.AddIngredient(Mod.Find<ModItem>("ThoriumSoul"), 1);
            }
            if (ModCompatibility.SacredTools.Loaded && CSEConfig.Instance.SacredTools)
            {
                recipe.AddIngredient(Mod.Find<ModItem>("SoASoul"), 1);
            }
            if (ModCompatibility.Spirit.Loaded && CSEConfig.Instance.SpiritMod)
            {
                recipe.AddIngredient(Mod.Find<ModItem>("SpiritSoul"), 1);
            }
            if (ModCompatibility.Redemption.Loaded || ModCompatibility.Polarities.Loaded || ModCompatibility.Spooky.Loaded || ModCompatibility.Homeward.Loaded)
            {
                recipe.AddIngredient(Mod.Find<ModItem>("MicroverseSoul"), 1);
            }

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
            recipe.Register();
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Item.social)
            {
                return;
            }

            const int linesToShow = 5;

            string description = Language.GetTextValue("Mods.ssm.Items.MacroverseSoul.Extra.Effects");
            description += "                                                                                                                                                       ";

            if (Main.GameUpdateCount % 10 == 0 || MacroverseSoulSystem.TooltipLines == null)
            {
                MacroverseSoulSystem.TooltipLines = [];
                for (int i = 0; i < linesToShow; i++)
                {
                    string line = Main.rand.NextFromCollection(MacroverseSoulSystem.Tooltips.Where(s => s.Length < description.Length).ToList());
                    if (MacroverseSoulSystem.TooltipLines.Contains(line)) 
                    {
                        i--;
                        continue;
                    }
                    MacroverseSoulSystem.TooltipLines.Add(line);
                }
            }
            for (int i = 0; i < MacroverseSoulSystem.TooltipLines.Count; i++)
            {
                description += "\n" + MacroverseSoulSystem.TooltipLines[i];
            }
            tooltips.Add(new TooltipLine(Mod, "tooltip", description));
            tooltips.Add(new TooltipLine(Mod, "FlavorText", Language.GetTextValue("Mods.ssm.Items.MacroverseSoul.Extra.Flavor")));
        }

    }
    public class MacroverseSoulSystem : ModSystem
    {
        public static List<string> Tooltips = new List<string>();

        public static List<string> TooltipLines = new List<string>();

        public override void OnLocalizationsLoaded()
        {
            Tooltips.Clear();
            PostAddRecipes();
        }
        public override void PostAddRecipes()
        {
            string[] startsWithFilter = Language.GetTextValue("Mods.ssm.Items.MacroverseSoul.Extra.StartsWithFilter").Split("|", StringSplitOptions.RemoveEmptyEntries);
            string[] containsFilter = Language.GetTextValue("Mods.ssm.Items.MacroverseSoul.Extra.ContainsFilter").Split("|", StringSplitOptions.RemoveEmptyEntries);
            foreach (Recipe item in Main.recipe.Where((Recipe r) => r.createItem != null && r.createItem.ModItem is BaseSoul))
            {
                foreach (Item item2 in item.requiredItem)
                {
                    ModItem modItem = item2.ModItem;
                    if (modItem != null)
                    {
                        IEnumerable<string> collection = from line in modItem.Tooltip.Value.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                                         where !startsWithFilter.Any(line.StartsWith) && !containsFilter.Any(line.Contains)
                                                         select line;
                        Tooltips.AddRange(collection);
                    }
                }
            }
        }
    }
}
