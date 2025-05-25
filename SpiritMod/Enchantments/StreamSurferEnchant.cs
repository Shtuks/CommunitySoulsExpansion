using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.TideDrops.StreamSurfer;
using SpiritMod.Items.Sets.TideDrops.Whirltide;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.ClubSubclass;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class StreamSurferEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(30, 142, 185);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<StreamSurferFishJump>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "Flying_Fish_Fin").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<StreamSurferSpout>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "StreamSurferHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "StreamSurferChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "StreamSurferLeggings").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<StreamSurferHelmet>(1);
            recipe.AddIngredient<StreamSurferChestplate>(1);
            recipe.AddIngredient<StreamSurferLeggings>(1);
            recipe.AddIngredient<Whirltide>(1);
            recipe.AddIngredient<BassSlapper>(1);
            recipe.AddIngredient<Flying_Fish_Fin>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class StreamSurferFishJump : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StreamSurferEnchant>();
        }
        public class StreamSurferSpout : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StreamSurferEnchant>();
        }
    }
}
