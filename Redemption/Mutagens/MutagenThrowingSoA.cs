﻿using Redemption.Items.Materials.PostML;
using Terraria.ModLoader;
using Terraria;

using ssm.Core;
using SacredTools.Content.Items.Materials;

namespace ssm.Redemption.Mutagens
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    public class MutagenThrowingSoA : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        }
        public override void SetStaticDefaults()
        {
            base.Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 28;
            base.Item.height = 36;
            base.Item.value = Item.sellPrice(0, 12);
            base.Item.rare = 11;
            base.Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<ThrowingDamageClass>() += 0.20f;
            player.GetCritChance<ThrowingDamageClass>() += 10f;
            player.GetAttackSpeed<ThrowingDamageClass>() += 0.10f;
            player.CSE().throwerVelocity += 0.10f;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe().AddIngredient(ModContent.ItemType<FragmentQuasar>(), 10).AddIngredient(ModContent.ItemType<EmptyMutagen>()).AddIngredient(ModContent.ItemType<>())
        //        .AddTile(412)
        //        .Register();
        //}
    }
}
