using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Consolaria.Content.Items.Armor.Summon;
using Consolaria.Content.Items.Weapons.Summon;
using Consolaria.Content.Items.Pets;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class AncientWarlockEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(101, 25, 179);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientWarlockHood>(1);
            recipe.AddIngredient<AncientWarlockRobe>(1);
            recipe.AddIngredient<AncientWarlockLeggings>(1);
            recipe.AddIngredient<TurkeyFeather>(1);
            recipe.AddIngredient<MysteriousPackage>(1);
            recipe.AddIngredient<TurkeyStuff>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
    }
}

