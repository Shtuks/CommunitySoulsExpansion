using Consolaria.Content.Items.Armor.Ranged;
using Consolaria.Content.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Consolaria.Content.Items.Weapons.Ammo;
using Consolaria.Content.Items.Weapons.Throwing;
using Consolaria.Content.Items.Accessories;
using ssm.Core;

namespace ssm.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class AncientTitanEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(60, 75, 100);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Ranged) += 8f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientTitanHelmet>(1);
            recipe.AddIngredient<AncientTitanMail>(1);
            recipe.AddIngredient<AncientTitanLeggings>(1);
            recipe.AddIngredient<Sharanga>(1);
            recipe.AddIngredient<DragonBreath>(1);
            recipe.AddIngredient<HolyHandgrenade>(5);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        private readonly Mod Consolaria = ModLoader.GetMod("Consolaria");
    }
}
