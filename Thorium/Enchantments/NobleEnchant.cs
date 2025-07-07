using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Misc;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.SoA.Enchantments;
using ssm.Content.Buffs;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NobleEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NobleEffect>(Item);
        }

        public class NobleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SteelEnchant>();

            private bool[] oldCoinCount = new bool[4];
            public override void PostUpdate(Player player)
            {
                CheckCoin(0, ItemID.CopperCoin, player);
                CheckCoin(1, ItemID.SilverCoin, player);
                CheckCoin(2, ItemID.GoldCoin, player);
                CheckCoin(3, ItemID.PlatinumCoin, player);
            }
            private void CheckCoin(int index, int coinType, Player player)
            {
                bool hasCoin = player.CountItem(coinType) > 0;
                if (hasCoin && !oldCoinCount[index])
                {
                    player.AddBuff(ModContent.BuffType<TheRichBuff>(), 300);
                }
                oldCoinCount[index] = hasCoin;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NoblesHat>());
            recipe.AddIngredient(ModContent.ItemType<NoblesJerkin>());
            recipe.AddIngredient(ModContent.ItemType<NoblesLeggings>());
            recipe.AddIngredient(ModContent.ItemType<RingofUnity>());
            recipe.AddIngredient(ModContent.ItemType<BrassCap>());
            recipe.AddIngredient(ModContent.ItemType<WaxyRosin>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
