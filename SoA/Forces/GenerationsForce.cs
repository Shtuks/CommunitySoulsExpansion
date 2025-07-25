using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.SoA.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Enchantments.CairoCrusaderEnchant;
using static ssm.SoA.Enchantments.EerieEnchant;
using static ssm.SoA.Enchantments.BismuthEnchant;
using static ssm.SoA.Enchantments.DreadfireEnchant;
using static ssm.SoA.Enchantments.MarstechEnchant;
using static ssm.SoA.Enchantments.SpaceJunkEnchant;

namespace ssm.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class GenerationsForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
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

        public override void SetStaticDefaults()
        {
            Enchants[Type] =
            [
                ModContent.ItemType<EerieEnchant>(),
                ModContent.ItemType<BismuthEnchant>(),
                ModContent.ItemType<DreadfireEnchant>(),
                ModContent.ItemType<MarstechEnchant>()
            ];
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<EerieEffect>(Item);
            player.AddEffect<BismuthEffect>(Item);
            player.AddEffect<DreadfireEffect>(Item);
            player.AddEffect<MarstechEffect>(Item);
            player.AddEffect<SpaceJunkEffect>(Item);
            player.AddEffect<SpaceJunkAbilityEffect>(Item);

            player.AddEffect<GenerationsEffect>(Item);
        }

        public class GenerationsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
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
