using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LifeBloomEnchant : BaseEnchant
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
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer modPlayer = player.GetModPlayer<ThoriumPlayer>();
            modPlayer.setLifeBloom = true;

            ModContent.Find<ModItem>("ssm", "LivingWoodEnchant").UpdateAccessory(player, hideVisual);

            if (player.AddEffect<LifeBloomEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "HeartOfTheJungle").UpdateAccessory(player, hideVisual);
            }
        }

        public class LifeBloomEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LifeBloomEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LifeBloomMask>());
            recipe.AddIngredient(ModContent.ItemType<LifeBloomMail>());
            recipe.AddIngredient(ModContent.ItemType<LifeBloomLeggings>());
            recipe.AddIngredient(ModContent.ItemType<HeartOfTheJungle>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
