using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.GraniteSet.GraniteArmor;
using SpiritMod.Items.Accessory.ShurikenLauncher;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Accessory.Leather;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class GraniteChunkEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(116, 112, 169);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<GraniteChunkStomp>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "GraniteHelm").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "GraniteChest").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "GraniteLegs").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<GraniteBoots>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "TechBoots").UpdateAccessory(player, false);
            }
            ModContent.Find<ModItem>(this.SpiritMod.Name, "ShurikenLauncher").UpdateAccessory(player, false);
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<GraniteHelm>(1);
            recipe.AddIngredient<GraniteChest>(1);
            recipe.AddIngredient<GraniteLegs>(1);
            recipe.AddIngredient<ShurikenLauncher>(1);
            recipe.AddIngredient<RageBlazeDecapitator>(1);
            recipe.AddIngredient<TechBoots>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class GraniteChunkStomp : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteChunkEnchant>();
        }
        public class GraniteBoots : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteChunkEnchant>();
        }
    }
}
