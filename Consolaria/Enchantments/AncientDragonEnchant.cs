using Consolaria.Content.Items.Armor.Melee;
using Consolaria.Content.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Consolaria.Content.Items.Consumables;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class AncientDragonEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(151, 191, 241);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
            Item.defense = 7;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 0.10f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientDragonMask>(1);
            recipe.AddIngredient<AncientDragonBreastplate>(1);
            recipe.AddIngredient<AncientDragonGreaves>(1);
            recipe.AddIngredient<GreatDrumstick>(1);
            recipe.AddIngredient<AlbinoMandible>(1);
            recipe.AddIngredient<Wishbone>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

