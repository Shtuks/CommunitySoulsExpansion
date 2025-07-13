using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Tracker;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.BronzeEnchant;
using ssm.Content.Projectiles.Enchantments;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AssassinEnchant : BaseEnchant
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
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AssasinEffect>(Item);
        }

        public class AssasinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override bool ActiveSkill => true;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                //player.Teleport(Main.mouse)
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AssassinsWalkers>());
            recipe.AddIngredient(ModContent.ItemType<AssassinsGuard>());
            recipe.AddIngredient(ModContent.ItemType<MasterArbalestHood>());
            recipe.AddIngredient(ModContent.ItemType<DartPouch>());
            recipe.AddIngredient(ModContent.ItemType<TheBlackBow>());
            recipe.AddIngredient(ModContent.ItemType<WyrmDecimator>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
