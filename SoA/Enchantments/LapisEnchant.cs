using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SacredTools;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Lapis;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Special;
using ssm.Core;
using FargowiltasSouls;
using SacredTools.Content.Buffs;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class LapisEnchant : BaseEnchant
    {
        private int attackCounter = 0;
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(46, 66, 163);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<LapisSpeedEffect>(Item))
            {
                player.moveSpeed += player.ForceEffect<LapisSpeedEffect>() ? 0.2f : 0.15f;
                player.GetModPlayer<SoAPlayer>().lapisEnchant = player.ForceEffect<LapisSpeedEffect>() ? 2 : 1;
            }
            if (player.AddEffect<LapisSpeedEffect>(Item))
            {
                if (player.itemAnimation > 0 && player.HeldItem.damage > 0)
                {
                    attackCounter++;

                    if (attackCounter >= (player.ForceEffect<LapisDefenseEffect>() ? 3 : 5))
                    {
                        attackCounter = 0;
                        player.AddLeveledBuff<LapisShieldBuff>(600);
                    }
                }
            }

            //ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "LapisPendant").UpdateAccessory(player, false);
        }

        public class LapisSpeedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();
        }

        public class LapisDefenseEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<LapisHelmet>();
            recipe.AddIngredient<LapisChest>();
            recipe.AddIngredient<LapisLegs>();
            recipe.AddIngredient<LapisPendant>();
            recipe.AddIngredient<LapisStaff>();
            recipe.AddIngredient<Haven>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
