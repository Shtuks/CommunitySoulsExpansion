using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ModLoader;
using Terraria;
using ssm.Redemption.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Redemption.Enchantments.CommonGuardEnchant;
using static ssm.Redemption.Enchantments.PureIronEnchant;
using static ssm.Redemption.Enchantments.DragonLeadEnchant;
using static ssm.Redemption.Enchantments.HardlightEnchant;
using static ssm.Redemption.Enchantments.XeniumEnchant;
using static ssm.Redemption.Enchantments.XenomiteEnchant;
using static ssm.Thorium.Enchantments.LivingWoodEnchant;

namespace ssm.Redemption.Forces
{
    [JITWhenModsEnabled(new string[] { "Redemption" })]
    [ExtendsFromMod(new string[] { "Redemption" })]
    public class AdvancementForce : BaseForce
    {
        public override void SetStaticDefaults()
        {
            Enchants[Type] = new int[7]
            {
                ModContent.ItemType<LivingWoodEnchant2>(),
                ModContent.ItemType<CommonGuardEnchant>(),
                ModContent.ItemType<PureIronEnchant>(),
                ModContent.ItemType<DragonLeadEnchant>(),
                ModContent.ItemType<XeniumEnchant>(),
                ModContent.ItemType<XenomiteEnchant>(),
                ModContent.ItemType<HardlightEnchant>()
            };
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CommonGuardEffect>(Item);
            player.AddEffect<DragonLeadEffect>(Item);
            player.AddEffect<PureIronEffect>(Item);
            player.AddEffect<HardlightEffect>(Item);
            player.AddEffect<XeniumEffect>(Item);
            player.AddEffect<XenomiteEffect>(Item);
            player.AddEffect<LivingWoodEffect>(Item);
            player.AddEffect<AdvancementEffect>(Item);

            player.AddEffect<LivingWoodEffect>(Item);
        }

        public class AdvancementEffect : AccessoryEffect
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
