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
using System.Collections.Generic;
using Terraria.Localization;

namespace ssm.CrossMod.SoulsRecipes
{
    public class ConjuristSoulRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<ConjuristsSoul>()))
                {
                    if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("StarstreamVeil"), 1); recipe.RemoveIngredient(ItemID.MonkBelt); recipe.RemoveIngredient(ItemID.SquireShield); recipe.RemoveIngredient(ItemID.HuntressBuckler); recipe.RemoveIngredient(ItemID.ApprenticeScarf); }
                    if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("IncitingIncident"), 1); recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("DivineNecklace"), 1);}
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("PortableHoloProjector"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("CruxCardMossyGoliath"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenSummon"), 1); recipe.RemoveIngredient(ModContent.ItemType<OccultistsEssence>()); }
                    if (ModCompatibility.Catalyst.Loaded) { recipe.AddIngredient(ModCompatibility.Catalyst.Mod.Find<ModItem>("UnrelentingTorment"), 1);}
                }
                if (ModCompatibility.Calamity.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("Nucleogenesis")))
                    {
                        if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("LuminousEnergy"), 5); }
                    }
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("StardustSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                        if (ModCompatibility.Homeward.Loaded) { recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("CounsellorBadge"), 1); recipe.RemoveIngredient(ItemID.SummonerEmblem); }
                    }
                }
            }
        }
    }
    public class ConjuristSoulEffects : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<ConjuristsSoul>() || Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                if (ModCompatibility.Homeward.Loaded)
                {
                    player.AddEffect<DivineNecklaceEffect>(Item);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    player.AddEffect<StarstreamVeilEffect>(Item);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ConjuristsSoul>() && !item.social)
            {
                if (ModCompatibility.Homeward.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "HWJConjurist")));
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    tooltips.Insert(5, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "SoAConjurist")));
                }
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class DivineNecklaceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineNecklace>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("DivineNecklace").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class StarstreamVeilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<StarstreamVeil>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("StarstreamVeil").UpdateAccessory(player, true);
            }
        }
    }
}
