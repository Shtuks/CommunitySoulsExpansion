using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using System.Collections.Generic;
using Terraria.Localization;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using SacredTools.Content.Items.Accessories;
using ContinentOfJourney.Items.Accessories;
using Redemption.Items.Accessories.PostML;

namespace ssm.CrossMod.SoulsRecipes
{
    public class ColossusSoulRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()))
                {
                    if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("RoyalGuard"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("NightmareBlindfold"), 1); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("OneGiantLeap"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("MasterShield"), 1); }
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("HEVSuit"), 1); }
                    if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("BlastShield"), 1); }
                    if (ModCompatibility.Clamity.Loaded) { recipe.AddIngredient(ModCompatibility.Clamity.Mod.Find<ModItem>("SkullOfTheBloodGod"), 1); }
                }
            }
        }
    }
    public class ColossusSoulEffects : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<ColossusSoul>() || Item.type == ModContent.ItemType<DimensionSoul>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                if (ModCompatibility.Redemption.Loaded)
                {
                    player.AddEffect<HEVEffect>(Item);
                }
                if (ModCompatibility.Homeward.Loaded)
                {
                    player.AddEffect<OneGiantLeapEffect>(Item);
                    ModCompatibility.Homeward.Mod.Find<ModItem>("MasterShield").UpdateAccessory(player, true);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    player.AddEffect<RoyalGuardEffect>(Item);
                    ModCompatibility.SacredTools.Mod.Find<ModItem>("NightmareBlindfold").UpdateAccessory(player, true);
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    player.AddEffect<BlastShieldEffect>(Item);
                }
                if (ModCompatibility.Clamity.Loaded)
                {
                    ModCompatibility.Clamity.Mod.Find<ModItem>("SkullOfTheBloodGod").UpdateAccessory(player, true);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                if (ModCompatibility.Redemption.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "RedColossus")));
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo5", Language.GetTextValue(key + "SoAColossus")));
                }
                if (ModCompatibility.Homeward.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo4", Language.GetTextValue(key + "HWJColossus")));
                }
                if (ModCompatibility.Clamity.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo3", Language.GetTextValue(key + "ClamColossus")));
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "ThoriumColossus")));
                }
            }
        }

        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class RoyalGuardEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override int ToggleItemType => ModContent.ItemType<RoyalGuard>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("RoyalGuard").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class NightmareBlindfoldhEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<NightmareBlindfold>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("NightmareBlindfold").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        public class BlastShieldEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModCompatibility.Thorium.Mod.Find<ModItem>("BlastShield").Type;
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Thorium.Mod.Find<ModItem>("BlastShield").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Redemption.Name)]
        public class HEVEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<HEVSuit>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Redemption.Mod.Find<ModItem>("HEVSuit").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class OneGiantLeapEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OneGiantLeap>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("OneGiantLeap").UpdateAccessory(player, true);
            }
        }
    }
}
