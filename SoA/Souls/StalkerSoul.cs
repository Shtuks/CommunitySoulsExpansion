﻿using Fargowiltas.Items.Tiles;

using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Core;
using ssm.SoA.Essences;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Weapons.Oblivion;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Primordia;
using FargowiltasSouls.Content.Items.Materials;
using SacredTools.Content.Items.Accessories.Sigils;

namespace ssm.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StalkerSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name) && CSEConfig.Instance.SacredTools;
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
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            player.GetDamage<ThrowingDamageClass>() += 0.25f;
            player.GetCritChance<ThrowingDamageClass>() += 10f;
            player.GetAttackSpeed<ThrowingDamageClass>() += 0.15f;
            player.CSE().throwerVelocity += 0.2f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<StalkerEssence>();
            recipe.AddIngredient<QuasarSigil>();

            recipe.AddIngredient<AsthralSaber>();
            recipe.AddIngredient<LunaticsGamble>();
            recipe.AddIngredient<FlariumDisc>();
            recipe.AddIngredient<SinisterKnives>();
            recipe.AddIngredient<FairGame>();
            recipe.AddIngredient<NovaknifePack>();
            recipe.AddIngredient<Ainfijarnar>();
            recipe.AddIngredient<TerraLance>();
            recipe.AddIngredient<TrueDecapitator>();
            recipe.AddIngredient<OrbFlayer>();

            recipe.AddIngredient<AbomEnergy>(10);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
