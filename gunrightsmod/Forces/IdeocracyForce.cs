using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.gunrightsmod.Enchantments;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.gunrightsmod.Forces
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class IdeocracyForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 1183376;
        }

        public new static int[] Enchants => new int[]
        {
            ModContent.ItemType<SuperCeramicEnchant>(),
            ModContent.ItemType<RockSaltEnchant>(),
            ModContent.ItemType<PurifiedSaltEnchant>(),
            ModContent.ItemType<PlasticEnchant>(),
            ModContent.ItemType<KevlarEnchant>(),
        };

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var CSEgunrightsmodPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();

            CSEgunrightsmodPlayer.IdeocracyForceEquipped = true;
            CSEgunrightsmodPlayer.SuperCeramicEnchantEquipped = true;
            CSEgunrightsmodPlayer.RockSaltEnchantEquipped = true;
            CSEgunrightsmodPlayer.PurifiedSaltEnchantEquipped = true;
            CSEgunrightsmodPlayer.PlasticEnchantEquipped = true;
            CSEgunrightsmodPlayer.KevlarEnchantEquipped = true;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            foreach (int ench in Enchants)
                recipe.AddIngredient(ench);

            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
