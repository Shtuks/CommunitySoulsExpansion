using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.SoA.Forces;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using SacredTools.Content.Items.Materials;
using ssm.CrossMod.CraftingStations;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Enchantments.AsthraltiteEnchant;
using static ssm.SoA.Enchantments.ExitumLuxEnchant;
using static ssm.SoA.Enchantments.FlariumEnchant;
using static ssm.SoA.Enchantments.VoidWardenEnchant;
using static ssm.SoA.Enchantments.VulcanReaperEnchant;
using static ssm.SoA.Forces.SyranForce;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;
using static ssm.SoA.Enchantments.CosmicCommanderEnchant;
using static ssm.SoA.Enchantments.FallenPrinceEnchant;
using static ssm.SoA.Enchantments.NebulousApprenticeEnchant;
using static ssm.SoA.Enchantments.StellarPriestEnchant;
using static ssm.SoA.Forces.SoranForce;
using static ssm.SoA.Enchantments.BismuthEnchant;
using static ssm.SoA.Enchantments.CairoCrusaderEnchant;
using static ssm.SoA.Enchantments.DreadfireEnchant;
using static ssm.SoA.Enchantments.EerieEnchant;
using static ssm.SoA.Enchantments.MarstechEnchant;
using static ssm.SoA.Enchantments.SpaceJunkEnchant;
using static ssm.SoA.Forces.GenerationsForce;
using static ssm.SoA.Enchantments.BlightboneEnchant;
using static ssm.SoA.Enchantments.FrosthunterEnchant;
using static ssm.SoA.Enchantments.LapisEnchant;
using static ssm.SoA.Enchantments.PrairieEnchant;
using static ssm.SoA.Forces.FoundationsForce;
using static ssm.SoA.Enchantments.QuasarEnchant;
using Terraria.DataStructures;

namespace ssm.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoASoul : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationHorizontal(6, 60));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        public override void SetDefaults()
        {
            Item.width = 59;
            Item.height = 51;
            Item.defense = 40;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //syran
            player.AddEffect<AsthraltiteEffect>(Item);
            player.AddEffect<VoidWardenEffect>(Item);
            player.AddEffect<VulcanReaperEffect>(Item);
            player.AddEffect<ExitumLuxEffect>(Item);
            player.AddEffect<FlariumEffect>(Item);
            player.AddEffect<SyranEffect>(Item);

            //soran
            player.AddEffect<CosmicCommanderEffect>(Item);
            player.AddEffect<BlazingBruteEffect>(Item);
            player.AddEffect<NebulousApprenticeEffect>(Item);
            player.AddEffect<StellarPriestEffect>(Item);
            player.AddEffect<SupernovaEffect>(Item);
            player.AddEffect<QuasarEffect>(Item);
            player.AddEffect<SoranEffect>(Item);

            //generations
            player.AddEffect<CairoEffect>(Item);
            player.AddEffect<EerieEffect>(Item);
            player.AddEffect<BismuthEffect>(Item);
            player.AddEffect<DreadfireEffect>(Item);
            player.AddEffect<MarstechEffect>(Item);
            player.AddEffect<SpaceJunkEffect>(Item);
            player.AddEffect<SpaceJunkAbilityEffect>(Item);
            player.AddEffect<GenerationsEffect>(Item);
            
            //foundations
            player.AddEffect<PrairieEffect>(Item);
            player.AddEffect<LapisDefenseEffect>(Item);
            player.AddEffect<LapisSpeedEffect>(Item);
            player.AddEffect<FrosthunterEffect>(Item);
            player.AddEffect<BlightboneEffect>(Item);
            player.AddEffect<FoundationsEffect>(Item);

            player.buffImmune[ModContent.Find<ModBuff>(this.Mod.Name, "NihilityPresenceBuff").Type] = true;
            player.AddEffect<TwoRealmsEffect>(Item);
        }
        public class TwoRealmsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FoundationsForce>();
            recipe.AddIngredient<GenerationsForce>();
            recipe.AddIngredient<SoranForce>();
            recipe.AddIngredient<SyranForce>();
            if (!ModCompatibility.Calamity.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }
            recipe.AddIngredient<EmberOfOmen>(5);
            recipe.AddTile<SyranCraftingStationTile>();
            recipe.Register();
        }
    }
}
