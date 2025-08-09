using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Armor.LeatherArmor;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Weapon.Thrown;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class MarksmanEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(206, 182, 95);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 4f;
            if (AccessoryEffectLoader.AddEffect<MarksmanCrit>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "LeatherHood").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "LeatherPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "LeatherLegs").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<LeatherHood>(1);
            recipe.AddIngredient<LeatherPlate>(1);
            recipe.AddIngredient<LeatherLegs>(1);
            recipe.AddIngredient<Kunai_Throwing>(50);
            recipe.AddIngredient<Dartboard>(1);
            recipe.AddIngredient<MagnifyingGlass>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class MarksmanCrit : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarksmanEnchant>();
        }
    }
}