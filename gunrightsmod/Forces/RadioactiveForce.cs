using Terraria;
using Terraria.ID;
/*
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.gunrightsmod.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.gunrightsmod.Enchantments.AstatineEnchant;
using static ssm.gunrightsmod.Enchantments.UraniumEnchant;
using static ssm.gunrightsmod.Enchantments.PlutoniumEnchant;


namespace ssm.gunrightsmod.Forces
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class RadioactiveForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        public override void SetStaticDefaults()
        {
            Enchants[Type] =
            [
                ModContent.ItemType<AstatineEnchant>(),
                ModContent.ItemType<PlutoniumEnchant>(),
                ModContent.ItemType<UraniumEnchant>(),
            ];
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SetActive(player);
            player.AddEffect<RadioactiveEffect>(Item);
            player.AddEffect<AstatineEffect>(Item);
            player.AddEffect<UraniumEffect>(Item);
            player.AddEffect<PlutoniumEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            foreach (int ench in Enchants[Type])
            recipe.AddIngredient(ench);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public class RadioactiveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<RadioactiveForce>();
        }
    }
}
*/