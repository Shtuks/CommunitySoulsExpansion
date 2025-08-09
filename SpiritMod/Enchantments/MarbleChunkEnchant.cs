using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.MarbleSet.MarbleArmor;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.MarbleSet;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class MarbleChunkEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(206, 182, 95);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<MarbleChunkEffect>(player, base.Item))
            {
            }
            ModContent.Find<ModItem>(this.SpiritMod.Name, "MarbleHelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "MarbleChest").UpdateArmorSet(player);
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<MarbleHelm>(1);
            recipe.AddIngredient<MarbleChest>(1);
            recipe.AddIngredient<DefenseCodex>(1);
            recipe.AddIngredient<MarbleBident>(1);
            recipe.AddIngredient<MarbleStaff>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class MarbleChunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarbleChunkEnchant>();
        }
    }
}
