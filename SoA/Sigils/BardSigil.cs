using SacredTools.Content.Items.Materials;
using SacredTools.Content.Rarities;
using SacredTools.Content.Tiles.CraftingStations;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ssm.Core;

namespace ssm.SoA.Sigils
{
    //no IAccessoryGroup? Fuck you john soa for making everything private
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    public class BardSigil : ModItem
    {
        public static readonly int BardDamageBonus = 25;

        public static readonly int BardCritChanceBonus = 10;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BardDamageBonus, BardCritChanceBonus);
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
            player.GetDamage(BardDamage.Instance) += BardDamageBonus / 100f;
            player.GetCritChance(BardDamage.Instance) += BardCritChanceBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<BardEmblem>().AddIngredient<TiridiumBar>(10).AddIngredient(3457, 20)
                .AddIngredient<EldritchSpark>()
                .AddIngredient<LuminousEnergy>(15)
                .AddTile<TiridiumInfuserTile>()
                .Register();
        }
    }
}