using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Enchantments.FallenPrinceEnchant;
using static ssm.SoA.Enchantments.CosmicCommanderEnchant;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;
using static ssm.SoA.Enchantments.NebulousApprenticeEnchant;
using static ssm.SoA.Enchantments.StellarPriestEnchant;
using ssm.SoA.Enchantments;
using static ssm.SoA.Enchantments.QuasarEnchant;
using System.Collections.Generic;

namespace ssm.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoranForce : BaseForce
    {
        public override List<AccessoryEffect> ActiveSkillTooltips =>
            [AccessoryEffectLoader.GetEffect<CosmicCommanderEffect>()];
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        public override void SetStaticDefaults()
        {
            Enchants[Type] = new int[5]
            {
                ModContent.ItemType<CosmicCommanderEnchant>(),
                ModContent.ItemType<BlazingBruteEnchant>(),
                ModContent.ItemType<NebulousApprenticeEnchant>(),
                ModContent.ItemType<StellarPriestEnchant>(),
                ModContent.ItemType<FallenPrinceEnchant>()
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
            player.AddEffect<CosmicCommanderEffect>(Item);
            player.AddEffect<BlazingBruteEffect>(Item);
            player.AddEffect<NebulousApprenticeEffect>(Item);
            player.AddEffect<StellarPriestEffect>(Item);
            player.AddEffect<SupernovaEffect>(Item);
            player.AddEffect<QuasarEffect>(Item);

            player.AddEffect<SoranEffect>(Item);
        }

        public class SoranEffect : AccessoryEffect
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
