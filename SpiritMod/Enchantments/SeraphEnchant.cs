using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.SeraphSet.SeraphArmor;
using SpiritMod.Items.Accessory.MageTree;
using SpiritMod.Items.Sets.SeraphSet;
using SpiritMod.Items.Accessory;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SeraphEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(165, 189, 221);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<SeraphAngelicSigil>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FallenAngel").UpdateAccessory(player, false);
            }
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphimBulwark").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphHelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphLegs").UpdateArmorSet(player);
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<SeraphHelm>(1);
            recipe.AddIngredient<SeraphArmor>(1);
            recipe.AddIngredient<SeraphLegs>(1);
            recipe.AddIngredient<GlowSting>(1);
            recipe.AddIngredient<SeraphimBulwark>(1);
            recipe.AddIngredient<FallenAngel>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SeraphAngelicSigil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SeraphEnchant>();
        }
    }
}
