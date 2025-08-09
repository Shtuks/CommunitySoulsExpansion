﻿using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Tracker;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MotDE : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
            Item.defense = 5;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<PocketFusionGeneratorEffect>(Item))
                ModContent.GetInstance<PocketFusionGenerator>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<InfernoLordsFocusEffect>(Item))
                ModContent.GetInstance<InfernoLordsFocus>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<LihzahrdTailEffect>(Item))
                ModContent.GetInstance<LihzahrdTail>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<SerpentShieldEffect>(Item))
                ModContent.GetInstance<SerpentShield>().UpdateAccessory(player, hideVisual);

            ModContent.GetInstance<MetabolicPills>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MonsterCharm>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<HexingTalisman>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FlawlessChrysalis>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TheRing>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SteamkeeperWatch>().UpdateAccessory(player, hideVisual);

            if (player.AddEffect<CapeoftheSurvivorEffect>(Item))
                ModContent.GetInstance<CapeoftheSurvivor>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<CrystalScorpionEffect>(Item))
                ModContent.GetInstance<CrystalScorpion>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);

            recipe.AddIngredient<SteamkeeperWatch>();
            recipe.AddIngredient<CrystalScorpion>();
            recipe.AddIngredient<CapeoftheSurvivor>();
            recipe.AddIngredient<InfernoLordsFocus>();
            recipe.AddIngredient<PocketFusionGenerator>();
            recipe.AddIngredient<LihzahrdTail>();
            recipe.AddIngredient<SerpentShield>();
            recipe.AddIngredient<MetabolicPills>();
            recipe.AddIngredient<MonsterCharm>();
            recipe.AddIngredient<HexingTalisman>();
            recipe.AddIngredient<FlawlessChrysalis>();
            recipe.AddIngredient<TheRing>();

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        public class InfernoLordsFocusEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<InfernoLordsFocus>();
            public override Header ToggleHeader => Header.GetHeader<MotDEHeader>();
        }
        public class CapeoftheSurvivorEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<CapeoftheSurvivor>();

            public override Header ToggleHeader => Header.GetHeader<MotDEHeader>();
        }
        public class PocketFusionGeneratorEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<PocketFusionGenerator>();

            public override Header ToggleHeader => Header.GetHeader<MotDEHeader>();
        }
        public class CrystalScorpionEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<CrystalScorpion>();

            public override Header ToggleHeader => Header.GetHeader<MotDEHeader>();
        }
        public class LihzahrdTailEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<LihzahrdTail>();

            public override Header ToggleHeader => Header.GetHeader<MotDEHeader>();
        }

        public class SerpentShieldEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<SerpentShield>();

            public override Header ToggleHeader => Header.GetHeader<MotDEHeader>();
        }
    }
}
