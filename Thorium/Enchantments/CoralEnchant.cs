using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Painting;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Coral;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.CyberPunkEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CoralEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CoralEffect>(Item);
        }

        public class CoralEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CoralEnchant>();
            public override bool ActiveSkill => true;

            public bool active;
            public int wetBuffTimer;
            public int cooldownTimer;
            public override void PostUpdate(Player player)
            {
                if (active)
                {
                    if (cooldownTimer > 0) cooldownTimer--;

                    if (wetBuffTimer > 0)
                    {
                        wetBuffTimer--;
                        player.wet = true; 
                        player.wetCount = 10; 
                    }
                }
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (cooldownTimer <= 0 && wetBuffTimer <= 0)
                {
                    wetBuffTimer = 30 * 60; 
                    cooldownTimer = (30 + 15) * 60; 

                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item86, player.position);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CoralHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CoralChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<CoralGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SeaBreezePendant>());
            recipe.AddIngredient(ModContent.ItemType<BubbleMagnet>());
            recipe.AddIngredient(ItemID.Swordfish);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
