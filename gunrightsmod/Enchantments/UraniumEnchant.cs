using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gunrightsmod.Items;
using ssm.Core;
using Terraria.DataStructures;
using gunrightsmod.Textures;

namespace ssm.gunrightmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class UraniumEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.gunrightsmod;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UraniumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<UraniumChestplate>());
            recipe.AddIngredient(ModContent.ItemType<UraniumLeggings>());
            recipe.AddIngredient(ModContent.ItemType<UraniumSword>());
            recipe.AddIngredient(ModContent.ItemType<PlasmoidWand>());
            recipe.AddIngredient(ModContent.ItemType<ParticleGun>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }

}
