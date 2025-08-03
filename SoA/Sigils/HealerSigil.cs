using SacredTools.Content.Items.Materials;
using SacredTools.Content.Rarities;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Items.HealerItems;
using ThoriumMod;
using ssm.Core;
using SacredTools.Content.Tiles.CraftingStations;

namespace ssm.SoA.Sigils
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    public class HealerSigil : ModItem
    {
        public static readonly int HealerDamageBonus = 25;

        public static readonly int HealerCritChanceBonus = 10;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(HealerDamageBonus, HealerCritChanceBonus);
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(16, 16);
            Item.value = Item.sellPrice(0, 25);
            Item.rare = ModContent.RarityType<LunaticRarity>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(HealerDamage.Instance) += HealerDamageBonus / 100f;
            player.GetCritChance(HealerDamage.Instance) += HealerCritChanceBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<ClericEmblem>().AddIngredient<TiridiumBar>(10).AddIngredient<CelestialFragment>(20)
                .AddIngredient<EldritchSpark>()
                .AddIngredient<LuminousEnergy>(15)
                .AddTile<TiridiumInfuserTile>()
                .Register();
        }
    }
}
