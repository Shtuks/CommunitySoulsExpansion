using Consolaria.Content.Items.Armor.Ranged;
using Consolaria.Content.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Consolaria.Content.Items.Armor.Melee;
using static FargoMoreSoulsCompat.Content.Items.Consolaria.DragonEnchant;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class TitanEnchant2 : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExperimentalContent;
        }
        public override Color nameColor => new Color(107, 135, 135);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<TitanRecoil>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.Consolaria.Name, "TitanHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Consolaria.Name, "TitanMail").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Consolaria.Name, "TitanLeggings").UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<TitanHelmet>(1);
            recipe.AddIngredient<TitanMail>(1);
            recipe.AddIngredient<TitanLeggings>(1);
            recipe.AddIngredient<AncientTitanEnchant>(1);
            recipe.AddIngredient<SpicySauce>(1);
            recipe.AddIngredient<VolcanicRepeater>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
        public class TitanRecoil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanEnchant2>();
        }
    }
}
