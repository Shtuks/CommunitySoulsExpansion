using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ModLoader;
using Terraria;
using ssm.Redemption.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Redemption.Enchantments.CommonGuardEnchant;
using static ssm.Redemption.Enchantments.HardlightEnchant;
using static ssm.Redemption.Enchantments.XeniumEnchant;
using static ssm.Redemption.Enchantments.XenomiteEnchant;
using static ssm.Redemption.Enchantments.ElderWoodEnchant;

namespace ssm.Redemption.Forces
{
    [JITWhenModsEnabled(new string[] { "Redemption" })]
    [ExtendsFromMod(new string[] { "Redemption" })]
    public class AdvancementForce : BaseForce
    {
        public override void SetStaticDefaults()
        {
            Enchants[Type] = new int[4]
            {
                ModContent.ItemType<CommonGuardEnchant>(),
                ModContent.ItemType<XeniumEnchant>(),
                ModContent.ItemType<HardlightEnchant>(),
                ModContent.ItemType<XenomiteEnchant>()
            };
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CommonGuardEffect>(Item);
            player.AddEffect<HardlightEffect>(Item);
            player.AddEffect<XeniumEffect>(Item);
            player.AddEffect<XenomiteEffect>(Item);

            player.AddEffect<AdvancementEffect>(Item);
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
    [JITWhenModsEnabled(new string[] { "Redemption" })]
    [ExtendsFromMod(new string[] { "Redemption" })]
    public class AchivementForce : BaseForce
    {
        public override void SetStaticDefaults()
        {
            Enchants[Type] =
            [
                ModContent.ItemType<LivingWoodEnchant2>(),
                ModContent.ItemType<PureIronEnchant>(),
                ModContent.ItemType<DragonLeadEnchant>(),
                ModContent.ItemType<ElderWoodEnchant>()
            ];
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ElderWoodEffect>(Item);
            player.AddEffect<CommonGuardEffect>(Item);
            player.AddEffect<HardlightEffect>(Item);
            player.AddEffect<XeniumEffect>(Item);
            player.AddEffect<XenomiteEffect>(Item);

            player.AddEffect<AchivementEffect>(Item);
        }
        public class AchivementEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            foreach (int ench in Enchants[Type])
            recipe.AddIngredient(ench);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
