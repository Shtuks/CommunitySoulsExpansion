using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Enchantments.AsthraltiteEnchant;
using static ssm.SoA.Enchantments.VoidWardenEnchant;
using static ssm.SoA.Enchantments.VulcanReaperEnchant;
using static ssm.SoA.Enchantments.ExitumLuxEnchant;
using static ssm.SoA.Enchantments.FlariumEnchant;
using ssm.SoA.Enchantments;

namespace ssm.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SyrianForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }
        public override void SetStaticDefaults()
        {
            Enchants[Type] = new int[5]
            {
                ModContent.ItemType<AsthraltiteEnchant>(),
                ModContent.ItemType<VoidWardenEnchant>(),
                ModContent.ItemType<VulcanReaperEnchant>(),
                ModContent.ItemType<ExitumLuxEnchant>(),
                ModContent.ItemType<FlariumEnchant>()
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AsthraltiteEffect>(Item);
            player.AddEffect<VoidWardenEffect>(Item);
            player.AddEffect<VulcanReaperEffect>(Item);
            player.AddEffect<ExitumLuxEffect>(Item);
            player.AddEffect<FlariumEffect>(Item);

            player.AddEffect<SyrianEffect>(Item);
        }

        public class SyrianEffect : AccessoryEffect
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
