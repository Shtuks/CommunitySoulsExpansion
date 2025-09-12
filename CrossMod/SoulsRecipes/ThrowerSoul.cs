using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ssm.Redemption.Mutagens;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Essences;
using Clamity.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod;

namespace ssm.CrossMod.SoulsRecipes
{
    [ExtendsFromMod(ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Crossmod.Name)]
    public class ThrowerSoulRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()))
                {
                    if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("DreadflameEmblem"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("QuasarSigil"), 1); recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("BlindJustice"), 1); }
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModContent.ItemType<MutagenThrowingCal>(), 1); recipe.RemoveIngredient(ModContent.ItemType<OutlawsEssence>()); }
                    if (ModCompatibility.Clamity.Loaded) { recipe.AddIngredient(ModCompatibility.Clamity.Mod.Find<ModItem>("DraculasCharm"), 1); }
                    if (ModCompatibility.Thorium.Loaded) { recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("PiratesPurse"), 1); recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumRippleKnife"), 1); }
                    if (ModCompatibility.Entropy.Loaded) { recipe.AddIngredient(ModCompatibility.Entropy.Mod.Find<ModItem>("ThiefsPocketwatchOfEclipse"), 1); }
                }
                if (ModCompatibility.Calamity.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.Calamity.Mod.Find<ModItem>("Nanotech")))
                    {
                        if (ModCompatibility.Thorium.Loaded)
                        {
                            if (!recipe.HasIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore")))
                            {
                                recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("TerrariumCore"), 3);
                            }
                        }
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Crossmod.Name)]
    public class ThrowerSoulEffects : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<VagabondsSoul>() || Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                if (ModCompatibility.Thorium.Loaded)
                {
                    player.AddEffect<PiratesPurseEffect>(Item);
                    player.AddEffect<ThrowerSoulEffect>(Item);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    player.AddEffect<DreadflameEmblemEffect>(Item);
                }
                if (ModCompatibility.Clamity.Loaded)
                {
                    player.AddEffect<DraculasCharmEffect>(Item);
                }
                //if (ModCompatibility.Entropy.Loaded)
                //{
                //    ModCompatibility.Entropy.Mod.Find<ModItem>("ThiefsPocketwatchOfEclipse").UpdateAccessory(player, true);
                //}
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<VagabondsSoul>() && !item.social)
            {
                if (ModCompatibility.Clamity.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo1", Language.GetTextValue(key + "ClamThrower")));
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "SoAThrower")));
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "ThoriumThrower")));
                    tooltips.Insert(6, new TooltipLine(Mod, "mayo2", Language.GetTextValue(key + "ThoriumThrower2")));
                }
            }
        }
        [ExtendsFromMod(ModCompatibility.Clamity.Name)]
        public class DraculasCharmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<DraculasCharm>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Clamity.Mod.Find<ModItem>("DraculasCharm").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
        public class DreadflameEmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadflameEmblem>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.SacredTools.Mod.Find<ModItem>("DreadflameEmblem").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        public class PiratesPurseEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<PiratesPurse>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Thorium.Mod.Find<ModItem>("PiratesPurse").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        public class ThrowerSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<UniverseSoul>();

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<ThoriumPlayer>().throwerExhaustion = 0;
            }
        }
    }
}
