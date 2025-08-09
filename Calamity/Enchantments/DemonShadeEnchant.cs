﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using CalamityMod.Items.Armor.Demonshade;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using CalamityMod.Buffs.StatDebuffs;
using ssm.Content.Buffs;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonShadeEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 5000000;
        }

        public override Color nameColor => new(173, 52, 70);


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();

            if (player.AddEffect<RedDevil>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DemonshadeHelm").UpdateArmorSet(player);
            }

            if (player.AddEffect<SoulCrystal>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ProfanedSoulCrystal").UpdateAccessory(player, hideVisual);
            }

            player.AddEffect<Enrage>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonshadeHelm>());
            recipe.AddIngredient(ModContent.ItemType<DemonshadeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DemonshadeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ProfanedSoulCrystal>());
            recipe.AddIngredient(ModContent.ItemType<Apotheosis>());
            recipe.AddIngredient(ModContent.ItemType<Eternity>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }

        public class RedDevil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
        }

        public class Enrage : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
            public override bool ActiveSkill => true;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                player.AddBuff(CSEConfig.Instance.DebugMode ? ModContent.BuffType<EnrageCal>() : ModContent.BuffType<Enraged>(), 1200);
            }
        }

        public class SoulCrystal : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
        }
    }
}
