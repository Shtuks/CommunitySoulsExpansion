using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using ssm.SoA.Enchantments;
using ssm.SoA.Forces;
using ssm.Systems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using SacredTools.Content.Items.Materials;
using ssm.CrossMod.CraftingStations;

namespace ssm.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoASoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.defense = 40;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FoundationsForce").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "GenerationsForce").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SoranForce").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SyrianForce").UpdateAccessory(player, hideVisual);

            player.buffImmune[ModContent.Find<ModBuff>(this.Mod.Name, "NihilityPresenceBuff").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FoundationsForce>();
            recipe.AddIngredient<GenerationsForce>();
            recipe.AddIngredient<SoranForce>();
            recipe.AddIngredient<SyrianForce>();
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<EmberOfOmen>(5);
            recipe.AddTile<SyranCraftingStationTile>();
            recipe.Register();
        }
    }
}
