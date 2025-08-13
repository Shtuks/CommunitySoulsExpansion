using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.EarlyMagic;
using ssm.Core;
using ThoriumMod.Items.BasicAccessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.SummonItems;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.ThoriumEnchant;
using ssm.Content.SoulToggles;
using SpiritMod;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SilkEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 0;
            Item.value = 20000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SilkEffect>(Item);
        }

        public class SilkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();

            public int manaStacks;
            public int manaAccumulator;
            public int effectTimer;
            private int previousMana;

            public void ResetEffect()
            {
                manaStacks = 0;
                manaAccumulator = 0;
                effectTimer = 0;
            }
            public override void PostUpdateMiscEffects(Player player)
            {
                if (player.statMana < previousMana)
                {
                    int manaSpent = previousMana - player.statMana;
                    manaAccumulator += manaSpent;
                    effectTimer = 600;

                    while (manaAccumulator >= 50 && manaStacks < 10)
                    {
                        manaStacks++;
                        manaAccumulator -= 50;
                    }
                }

                previousMana = player.statMana;

                if (effectTimer > 0)
                {
                    effectTimer--;
                    if (effectTimer == 0)
                    {
                        ResetEffect();
                    }
                }

                player.GetDamage<GenericDamageClass>() += 0.02f * manaStacks;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SilkHat>());
            recipe.AddIngredient(ModContent.ItemType<SilkTabard>());
            recipe.AddIngredient(ModContent.ItemType<SilkLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersFocus>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersShield>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersRocketeers>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
