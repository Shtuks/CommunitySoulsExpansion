using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.HuskstalkSet.ElderbarkArmor;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.BriarDrops;
using SpiritMod.Items.Sets.BriarChestLoot;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class ElderbarkEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(82, 103, 51);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic).Flat += 2f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ElderbarkHead>(1);
            recipe.AddIngredient<ElderbarkChest>(1);
            recipe.AddIngredient<ElderbarkLegs>(1);
            recipe.AddIngredient<ReachChestMagic>(1);
            recipe.AddIngredient<SanctifiedStabber>(1);
            recipe.AddIngredient<ReachPainting>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}

