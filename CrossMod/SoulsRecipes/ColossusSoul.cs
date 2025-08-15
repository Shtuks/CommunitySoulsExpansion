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
using FargowiltasSouls.Content.Items.Accessories.Essences;
using Terraria.ID;
using ThoriumMod.Items.Donate;
using static ssm.CrossMod.SoulsRecipes.BerserkerSoulEffects;
using ThoriumMod.Items.Terrarium;
using CalamityMod.Items.Accessories;

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
                    if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("RoyalGuard"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("NightmareBlindfold"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("ReflectionShield"), 1); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("OneGiantLeap"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("MasterShield"), 1); }
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("HEVSuit"), 1); }
                    if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("BlastShield"), 1); }
                    if (ModCompatibility.Clamity.Loaded) { recipe.AddIngredient(ModCompatibility.Clamity.Mod.Find<ModItem>("SkullOfTheBloodGod"), 1); }
                }

                if (ModCompatibility.Thorium.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumDefender")))
                    {
                        if (!ModCompatibility.WHummus.Loaded)
                        {
                            if (ModCompatibility.Calamity.Loaded) { recipe.RemoveIngredient(ItemID.AnkhShield); recipe.AddIngredient(ItemID.FrozenShield); }
                            recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("LifeQuartzShield"), 1);
                        }
                    }
                }

                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("CelestialShield")))
                    {
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("AncientBlessing"), 1); recipe.RemoveIngredient(ItemID.CelestialShell); }
                        if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded) { recipe.RemoveIngredient(ItemID.FrozenShield); }
                    }
                }

                if (ModCompatibility.Homeward.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Homeward.Mod.Find<ModItem>("VanguardBreastpiece")))
                    {
                        if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumDefender"), 1); recipe.RemoveIngredient(ItemID.AnkhShield); }
                    }
                }

                if (ModCompatibility.Calamity.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("RampartofDeities")))
                    {
                        if (ModCompatibility.Thorium.Loaded && !ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumDefender"), 1); recipe.RemoveIngredient(ItemID.FrozenShield); }
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("VanguardBreastpiece"), 1); recipe.RemoveIngredient(ItemID.FrozenShield); }
                    }
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
                    player.AddEffect<MasterShieldEffect>(Item);
                    ModCompatibility.Homeward.Mod.Find<ModItem>("VanguardBreastpiece").UpdateAccessory(player, true);
                    ModCompatibility.Homeward.Mod.Find<ModItem>("AncientBlessing").UpdateAccessory(player, true);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    player.AddEffect<RoyalGuardEffect>(Item);
                    player.AddEffect<ReflectionsEffect>(Item);
                    ModCompatibility.SacredTools.Mod.Find<ModItem>("NightmareBlindfold").UpdateAccessory(player, true);
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    player.AddEffect<BlastShieldEffect>(Item);
                    player.AddEffect<LifeQuartzEffect>(Item);
                    player.AddEffect<DefenderEffect>(Item);
                }
                if (ModCompatibility.Clamity.Loaded)
                {
                    ModCompatibility.Clamity.Mod.Find<ModItem>("SkullOfTheBloodGod").UpdateAccessory(player, true);
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (Item.type == ModCompatibility.Calamity.Mod.Find<ModItem>("RampartofDeities").Type)
                {
                    if (ModCompatibility.SacredTools.Loaded)
                    {
                        player.AddEffect<ReflectionsEffect>(Item);
                    }
                    if (ModCompatibility.Thorium.Loaded)
                    {
                        player.AddEffect<LifeQuartzEffect>(Item);
                        player.AddEffect<DefenderEffect>(Item);
                    }
                    if (ModCompatibility.Homeward.Loaded)
                    {
                        ModCompatibility.Homeward.Mod.Find<ModItem>("VanguardBreastpiece").UpdateAccessory(player, true);
                    }
                }
            }
            if (ModCompatibility.Homeward.Loaded)
            {
                if (Item.type == ModCompatibility.Homeward.Mod.Find<ModItem>("VanguardBreastpiece").Type)
                {
                    if (ModCompatibility.Thorium.Loaded)
                    {
                        player.AddEffect<LifeQuartzEffect>(Item);
                        player.AddEffect<DefenderEffect>(Item);
                    }
                }
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                if (Item.type == ModCompatibility.SacredTools.Mod.Find<ModItem>("CelestialShield").Type)
                {
                    if (ModCompatibility.Homeward.Loaded)
                    {
                        ModCompatibility.Homeward.Mod.Find<ModItem>("AncientBlessing").UpdateAccessory(player, true);
                    }
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
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo4", Language.GetTextValue(key + "HWJColossus2")));
                }
                if (ModCompatibility.Clamity.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo3", Language.GetTextValue(key + "ClamColossus")));
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "ThoriumColossus3")));
                }
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                if (item.type == ModCompatibility.SacredTools.Mod.Find<ModItem>("CelestialShield").Type && !item.social)
                {
                    if (ModCompatibility.Homeward.Loaded)
                    {
                        tooltips.Insert(5, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "HWJBlessing")));
                    }
                }
            }
            if (ModCompatibility.Homeward.Loaded)
            {
                if (item.type == ModCompatibility.Homeward.Mod.Find<ModItem>("VanguardBreastpiece").Type && !item.social)
                {
                    if (ModCompatibility.Thorium.Loaded)
                    {
                        tooltips.Insert(5, new TooltipLine(Mod, "mayo3", Language.GetTextValue(key + "ThoriumColossus2")));
                    }
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (item.type == ModCompatibility.Calamity.Mod.Find<ModItem>("RampartofDeities").Type && !item.social)
                {
                    if (ModCompatibility.Thorium.Loaded)
                    {
                        tooltips.Insert(5, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "ThoriumColossus2")));
                    }
                    if (ModCompatibility.SacredTools.Loaded)
                    {
                        tooltips.Insert(5, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "SoARampart")));
                    }
                    if (ModCompatibility.Homeward.Loaded)
                    {
                        tooltips.Insert(5, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "HWJRampart")));
                    }
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
        public class ReflectionsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReflectionShield>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("ReflectionShield").UpdateAccessory(player, true);
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
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        public class DefenderEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumDefender").Type;
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumDefender").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        public class LifeQuartzEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<LifeQuartzShield>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Thorium.Mod.Find<ModItem>("LifeQuartzShield").UpdateAccessory(player, true);
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
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class MasterShieldEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<MasterShield>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("MasterShield").UpdateAccessory(player, true);
            }
        }
    }
}
