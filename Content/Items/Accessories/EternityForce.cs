using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Buffs.Minions;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.Items.Accessories
{
    public class EternityForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(10, 0, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<MutantSoulEffect>(Item))
            {
                player.AddBuff(ModContent.BuffType<MutantSoulBuff>(), 2);
            }
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "StyxEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "PhantaplazmalEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "NekomiEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "EridanusEnchant").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "GaiaEnchant").UpdateAccessory(player, false);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<GaiaEnchant>(1);
            recipe.AddIngredient<EridanusEnchant>(1);
            recipe.AddIngredient<StyxEnchant>(1);
            recipe.AddIngredient<PhantaplazmalEnchant>(1);
            recipe.AddIngredient<NekomiEnchant>(1);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }

        public abstract class EternityForceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
        }

        public class MutantSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EternityForce>();
            public override bool MinionEffect => true;
        }
    }
}
