using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using SpiritMod.Items.Sets.FloranSet.FloranArmor;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Consumable.Fish;
using SpiritMod.Items.Sets.FloranSet;
using SpiritMod.Items.Sets.ClubSubclass;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class FloranEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(131, 180, 0);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<FloranWellFed>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FLegs").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<FloranGrassEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FloranCharm").UpdateAccessory(player, false);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FHelmet>(1);
            recipe.AddIngredient<FPlate>(1);
            recipe.AddIngredient<FLegs>(1);
            recipe.AddIngredient<StaminaCodex>(1);
            recipe.AddIngredient<FloranBludgeon>(1);
            recipe.AddIngredient<FloranCharm>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public class FloranWellFed : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FloranEnchant>();
        }
        public class FloranGrassEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FloranEnchant>();
        }
    }
}
