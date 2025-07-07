using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Blizzard;
using ssm.Core;
using ThoriumMod.Items.MagicItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossBoreanStrider;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.DemonBloodEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CryomancerEnchant : BaseEnchant
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
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            if (player.AddEffect<CryomancerEffect>(Item))
            {
                thoriumPlayer.setCryomancer = true;
            }
            //strider hide
            thoriumPlayer.frostBonusDamage = true;

            ModContent.Find<ModItem>("ssm", "IcyEnchant").UpdateAccessory(player, hideVisual);
        }

        public class CryomancerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryomancerEnchant>();

        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CryomancersCrown>());
            recipe.AddIngredient(ModContent.ItemType<CryomancersTabard>());
            recipe.AddIngredient(ModContent.ItemType<CryomancersLeggings>());
            recipe.AddIngredient(ModContent.ItemType<IcyEnchant>());
            recipe.AddIngredient(ModContent.ItemType<IceBoundStriderHide>());
            recipe.AddIngredient(ModContent.ItemType<IceFairyStaff>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
