using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ArcaneArmor;
using ssm.Core;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.ThrownItems;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossBuriedChampion;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FlightEnchant : BaseEnchant
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoSoulsPlayer fargoPlayer = player.GetModPlayer<FargoSoulsPlayer>();
            fargoPlayer.WingTimeModifier += 0.1f;
            player.AddEffect<FlightEffect>(Item);
        }

        public class FlightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FlightMasteryHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlightEnchant>();

            public int featherTimer;
            public int featherCount;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FlightMask>());
            recipe.AddIngredient(ModContent.ItemType<FlightMail>());
            recipe.AddIngredient(ModContent.ItemType<FlightBoots>());
            recipe.AddIngredient(ModContent.ItemType<ChampionWing>());
            recipe.AddIngredient(ModContent.ItemType<FabergeEgg>());
            recipe.AddIngredient(ModContent.ItemType<Bolas>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
