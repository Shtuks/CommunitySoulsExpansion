using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Consolaria.Content.Items.Accessories;
using Consolaria.Content.Items.Armor.Summon;
using Consolaria.Content.Items.Weapons.Summon;
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
    public class WarlockEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(164, 108, 187);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 3;
            player.jumpSpeedBoost += 2.5f;
            ModContent.Find<ModItem>(this.Consolaria.Name, "WarlockHood").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.Consolaria.Name, "WarlockRobe").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.Consolaria.Name, "WarlockLeggings").UpdateArmorSet(player);
            
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<WarlockHood>(1);
            recipe.AddIngredient<WarlockRobe>(1);
            recipe.AddIngredient<WarlockLeggings>(1);
            recipe.AddIngredient<AncientWarlockEnchant>(1);
            recipe.AddIngredient<EternityStaff>(1);
            recipe.AddIngredient<ValentineRing>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
    }
}
