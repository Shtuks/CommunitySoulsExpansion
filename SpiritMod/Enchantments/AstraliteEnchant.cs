using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.BossLoot.StarplateDrops.StarArmor;
using SpiritMod.Items.BossLoot.StarplateDrops;
using SpiritMod.Items.BossLoot.StarplateDrops.SteamplateBow;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Placeable.Furniture;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class AstraliteEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(234, 197, 128);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 30000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<AstraliteArmorEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "StarMask").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "StarPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "StarLegs").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<AstraliteBoots>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "HighGravityBoots").UpdateAccessory(player, false);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<StarMask>(1);
            recipe.AddIngredient<StarPlate>(1);
            recipe.AddIngredient<StarLegs>(1);
            recipe.AddIngredient<OrionPistol>(1);
            recipe.AddIngredient<HighGravityBoots>(1);
            recipe.AddIngredient<StarplatePainting>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class AstraliteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstraliteEnchant>();
        }
        public class AstraliteBoots : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstraliteEnchant>();
        }
    }
}
