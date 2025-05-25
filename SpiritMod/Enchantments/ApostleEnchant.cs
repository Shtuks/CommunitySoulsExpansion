using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.BossLoot.AvianDrops.ApostleArmor;
using SpiritMod.Items.BossLoot.AvianDrops;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class ApostleEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(174, 152, 132);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<ApostleEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "TalonHeaddress").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "TalonGarb").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<TalonHeaddress>(1);
            recipe.AddIngredient<TalonGarb>(1);
            recipe.AddIngredient<TalonPiercer>(1);
            recipe.AddIngredient<TalonBlade>(1);
            recipe.AddIngredient<Talonginus>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class ApostleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ApostleEnchant>();
        }
    }
}
