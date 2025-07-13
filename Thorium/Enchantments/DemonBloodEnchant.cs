using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.DemonBlood;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Consumable;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.NPCItems;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.GraniteEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DemonBloodEnchant : BaseEnchant
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
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //toggle
            ThoriumPlayer modPlayer = player.GetModPlayer<ThoriumPlayer>();
            if (player.AddEffect<DemonBloodEffect>(Item))
            {
                modPlayer.setDemonBlood = true;
            }
            ModContent.Find<ModItem>("ssm", "FleshEnchant").UpdateAccessory(player, true);
            ModContent.Find<ModItem>(this.thorium.Name, "VileFlailCore").UpdateAccessory(player, true);
        }

        public class DemonBloodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonBloodEnchant>();

        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DemonBloodHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DemonBloodBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<DemonBloodGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FleshEnchant>());
            recipe.AddIngredient(ModContent.ItemType<VileSpitter>());
            recipe.AddIngredient(ModContent.ItemType<VileFlailCore>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
