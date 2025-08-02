using CalamityMod.Items.Accessories;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Accessories.MeleeExpansion;
using ContinentOfJourney.Items.Accessories.SummonerRings;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using SacredTools.Content.Items.Accessories;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.CrossMod.SoulsRecipes
{
    public class BerserkerSoulRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<BerserkerSoul>()))
                {
                    if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("TrueMoonEdgedPandolarra"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("SolarSigil"), 1); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("CommandersGaunlet"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("PhilosophersStone"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("TrueDawnsBorder"), 1); }
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("PZGauntlet"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenMelee"), 1); recipe.RemoveIngredient(ModContent.ItemType<BarbariansEssence>()); }
                    if (ModCompatibility.Calamity.Loaded) { recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("ArkoftheCosmos"), 1); }
                    if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("BlizzardPouch"), 1); recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariansLastKnife"), 1); }
                }
                if (ModCompatibility.Calamity.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("ArkoftheCosmos")))
                    {
                        if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("LuminousEnergy"), 5); }
                        if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("OceanEssence"), 1); recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("InfernoEssence"), 1); recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("DeathEssence"), 1); }
                        recipe.AddIngredient<Eridanium>(5);
                    }
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("BadgeofBravery")))
                    {
                        if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("TraceOfChaos"), 5); }
                        if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore"), 3); }
                    }
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("ElementalGauntlet")))
                    {
                        if (!recipe.HasIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore")) && ModCompatibility.Thorium.Loaded)
                        {
                            recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore"), 3);
                        }
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("DivineTouch"), 1); recipe.RemoveIngredient(ItemID.FireGauntlet); }
                        if (!ModCompatibility.Homeward.Loaded && ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("FloraFist"), 1); recipe.RemoveIngredient(ItemID.FireGauntlet); }
                        //if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("LuminousEnergy"), 5); }
                    }
                }
                if (ModCompatibility.Homeward.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Homeward.Mod.Find<ModItem>("PhilosophersStone")))
                    {
                        if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("OblivionBar"), 8); }
                    }
                    if (recipe.HasResult(ModCompatibility.Homeward.Mod.Find<ModItem>("CommandersGaunlet")))
                    {
                        recipe.AddIngredient(ItemID.BerserkerGlove, 1);
                        recipe.RemoveIngredient(ItemID.PowerGlove);
                        if (ModCompatibility.Calamity.Loaded) { recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("LifeAlloy"), 3); }
                    }
                    if (recipe.HasResult(ModCompatibility.Homeward.Mod.Find<ModItem>("DivineTouch")))
                    {
                        recipe.RemoveIngredient(ItemID.FireGauntlet);
                        if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("FloraFist"), 1); }
                    }
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("SolarSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("SwordmasterBadge"), 1); recipe.RemoveIngredient(ItemID.WarriorEmblem); }
                    }
                }
            }
        }
    }
    public class BerserkerSoulEffects : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<BerserkerSoul>() || Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                if (ModCompatibility.Homeward.Loaded)
                {
                    player.AddEffect<CommandersGauntletEffect>(Item);
                    player.AddEffect<PhilosophersStoneEffect>(Item);
                    player.AddEffect<GodlyTouchEffect>(Item);
                    player.AddEffect<BerserkerGloveEffect>(Item);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    player.AddEffect<FloraFistEffect>(Item);
                }
            }
            if (ModCompatibility.Calamity.Loaded) 
            {
                if (Item.type == ModCompatibility.Calamity.Mod.Find<ModItem>("ElementalGauntlet").Type)
                {
                    if (ModCompatibility.SacredTools.Loaded)
                    {
                        player.AddEffect<FloraFistEffect>(Item);
                    }
                }
            }
            if (ModCompatibility.Homeward.Loaded)
            {
                if (Item.type == ModCompatibility.Homeward.Mod.Find<ModItem>("CommandersGaunlet").Type)
                {
                    player.AddEffect<BerserkerGloveEffect>(Item);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<BerserkerSoul>() && !item.social)
            {
                if (ModCompatibility.Homeward.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "HWJBerserker")));
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "SoABerserker")));
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo3", Language.GetTextValue(key + "ThoriumBerserker")));
                }
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class PhilosophersStoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhilosophersStone>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("PhilosophersStone").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class CommandersGauntletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<CommandersGaunlet>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("CommandersGaunlet").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class GodlyTouchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineTouch>();

            public override void PostUpdateEquips(Player player)
            {
                //no free 15% stat boosts
                ModCompatibility.Homeward.Mod.Find<ModItem>("DivineEmblem").UpdateAccessory(player, true);
            }
        }
        public class BerserkerGloveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ItemID.BerserkerGlove;
            public override void PostUpdateEquips(Player player)
            {
                player.statDefense += 8;
                player.kbGlove = true;
                player.meleeScaleGlove = true;
                player.autoReuseGlove = true;
            }
        }
        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class FloraFistEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<FloraFist>();

            public override void PostUpdateEquips(Player player)
            {
                //nooooo
                ModCompatibility.SacredTools.Mod.Find<ModItem>("FloraFist").UpdateAccessory(player, true);
                player.GetDamage(DamageClass.Melee) -= 0.12f;
                player.GetAttackSpeed(DamageClass.Melee) -= 0.12f;
            }
        }
    }
}