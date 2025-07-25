using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ContinentOfJourney.Items.Accessories;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using SacredTools.Content.Items.Accessories;
using ContinentOfJourney.Items.Accessories.Bookmarks;
using System.Collections.Generic;
using Terraria.Localization;

namespace ssm.CrossMod.SoulsRecipes
{
    public class ArchWizardSoulRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<ArchWizardsSoul>()))
                {
                    if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("NubasBlessing"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("Armageddon"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("StoneOfResonance"), 1); recipe.RemoveIngredient(ItemID.CelestialCuffs); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("Starflower"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("DoublePlot"), 1);}
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("Petridish"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenMagic"), 1); recipe.RemoveIngredient(ModContent.ItemType<ApprenticesEssence>()); }
                    if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("NorthernLight"), 1); recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumSageStaff"), 1); }
                }
                if (ModCompatibility.Calamity.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("EtherealTalisman")))
                    {
                        if (!recipe.HasIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore")) && ModCompatibility.Thorium.Loaded)
                        {
                            recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore"), 3);
                        }
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("EruditeBookmark"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("RejuvenatedCross"), 1); }
                        if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("LuminousEnergy"), 5); }
                    }
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("NebulaSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("ArchmageBadge"), 1); recipe.RemoveIngredient(ItemID.SorcererEmblem); }
                    }
                }
            }
        }
    }
    public class ArchWizardSoulEffects : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<ArchWizardsSoul>() || Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                if (ModCompatibility.Homeward.Loaded)
                {
                    player.AddEffect<RejuvenatedCrossEffect>(Item);
                    player.AddEffect<EruditeBookmarkEffect>(Item);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    player.AddEffect<NubasBlessingEffect>(Item);
                    player.AddEffect<StoneOfResonanceEffect>(Item);
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (Item.type == ModCompatibility.Calamity.Mod.Find<ModItem>("EtherealTalisman").Type)
                {
                    if (ModCompatibility.Homeward.Loaded)
                    {
                        player.AddEffect<EruditeBookmarkEffect>(Item);
                        player.AddEffect<RejuvenatedCrossEffect>(Item);
                    }
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ArchWizardsSoul>() && !item.social)
            {
                if (ModCompatibility.Homeward.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "HWJArchWizard")));
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "SoAArchWizard")));
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (item.type == ModCompatibility.Calamity.Mod.Find<ModItem>("EtherealTalisman").Type)
                {
                    if (ModCompatibility.Homeward.Loaded)
                    {
                        tooltips.Insert(6, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "HWJArchWizard")));
                    }
                }
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class EruditeBookmarkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<EruditeBookmark>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("EruditeBookmark").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class RejuvenatedCrossEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<RejuvenatedCross>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("RejuvenatedCross").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class NubasBlessingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<NubasBlessing>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("NubasBlessing").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class StoneOfResonanceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<StoneOfResonance>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("StoneOfResonance").UpdateAccessory(player, true);
            }
        }
    }
}
