using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using Fargowiltas.Items.Tiles;
using ssm.SoA.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Enchantments.PrairieEnchant;
using static ssm.SoA.Enchantments.LapisEnchant;
using static ssm.SoA.Enchantments.FrosthunterEnchant;
using static ssm.SoA.Enchantments.BlightboneEnchant;
using static ssm.SoA.Enchantments.CairoCrusaderEnchant;

namespace ssm.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FoundationsForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        public override void SetStaticDefaults()
        {
            Enchants[Type] = new int[5]
            {
                ModContent.ItemType<CairoCrusaderEnchant>(),
                ModContent.ItemType<PrairieEnchant>(),
                ModContent.ItemType<LapisEnchant>(),
                ModContent.ItemType<FrosthunterEnchant>(),
                ModContent.ItemType<BlightboneEnchant>()
            };
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public class FoundationsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CairoEffect>(Item);
            player.AddEffect<PrairieEffect>(Item);
            player.AddEffect<LapisDefenseEffect>(Item);
            player.AddEffect<LapisSpeedEffect>(Item);
            player.AddEffect<FrosthunterEffect>(Item);
            player.AddEffect<BlightboneEffect>(Item);

            player.AddEffect<FoundationsEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            int[] array = Enchants[Type];
            foreach (int itemID in array)
            {
                recipe.AddIngredient(itemID);
            }

            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}