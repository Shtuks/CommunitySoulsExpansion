﻿using Terraria.ModLoader;
using Terraria;
using ThoriumMod;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using ssm.Thorium.Essences;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.BossFallenBeholder;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ThoriumMod.Items.BossThePrimordials.Omni;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OlympiansSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            player.GetDamage<ThrowingDamageClass>() += 0.25f;
            player.GetCritChance<ThrowingDamageClass>() += 10f;
            player.GetAttackSpeed<ThrowingDamageClass>() += 0.10f;
            player.CSE().throwerVelocity += 0.15f;
            player.GetModPlayer<ThoriumPlayer>().throwerExhaustionRegenBonus += 10;
            player.GetModPlayer<ThoriumPlayer>().throwerExhaustionMax += 1000;
            player.GetModPlayer<ThoriumPlayer>().throwGuide3 = true;
            player.GetModPlayer<ThoriumPlayer>().canteenCadet = true;

            if (player.AddEffect<ThiefsWalletEffect>(Item))
            {
                player.GetModPlayer<ThoriumPlayer>().accPiratesPurse = true;
            }
            player.GetModPlayer<ThoriumPlayer>().throwConsume = 0.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<SlingerEssence>();
            if (ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }

            recipe.AddIngredient<MermaidCanteen>();
            recipe.AddIngredient<MagnetoGrip>();
            recipe.AddIngredient<PiratesPurse>();
            recipe.AddIngredient<ThrowingGuideVolume3>();
            recipe.AddIngredient<TidalWave>();
            recipe.AddIngredient<TerrariumRippleKnife>();
            recipe.AddIngredient<DragonFang>();
            recipe.AddIngredient<TerraKnife>();
            recipe.AddIngredient<HellRoller>();

            recipe.AddIngredient<OceanEssence>(5);
            recipe.AddIngredient<InfernoEssence>(5);
            recipe.AddIngredient<DeathEssence>(5);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }

        public class ThiefsWalletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<PiratesPurse>();
        }
    }
}