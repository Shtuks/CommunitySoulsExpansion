using Fargowiltas.Items.Tiles;
using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using CalamityMod.Items.Accessories;
using ThoriumMod.Items.ThrownItems;
using CalamityMod.Items.Materials;
using ThoriumMod.Items.NPCItems;
using CalamityMod.CalPlayer;
using CalamityMod;
using ThoriumMod.Utilities;

namespace ssm.CrossMod.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class GtTETFinal : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer calamityPlayer = player.Calamity();
            calamityPlayer.nanotech = true;
            calamityPlayer.raiderTalisman = true;
            calamityPlayer.electricianGlove = true;
            calamityPlayer.filthyGlove = true;
            calamityPlayer.bloodyGlove = true;
            player.GetDamage<ThrowingDamageClass>() += 0.2f;
            player.Shtun().throwerVelocity += 0.25f;
            player.GetThoriumPlayer().throwGuide3 = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<Nanotech>();
            recipe.AddIngredient<ThrowingGuideVolume3>();
            recipe.AddIngredient<SuspiciousScrap>(5);
            recipe.AddIngredient<StrangeAlienTech>(5);
            recipe.AddIngredient<AuricBar>(5);

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}