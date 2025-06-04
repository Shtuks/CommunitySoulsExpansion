using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Content.Items.Armor.Lunar.Solar;
using SacredTools.Items.Weapons.Lunatic;
using ssm.Core;
using FargowiltasSouls;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class BlazingBruteEnchant : BaseEnchant
    {
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
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(249, 75, 7);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<BlazingBruteEffect>(Item))
            {
                player.GetModPlayer<SoAPlayer>().rivalEnchant = player.ForceEffect<BlazingBruteEffect>() ? 2 : 1;
            }
        }
        public class BlazingBruteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlazingBruteEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<BlazingBruteHelm>();
            recipe.AddIngredient<BlazingBrutePlate>();
            recipe.AddIngredient<BlazingBruteLegs>();
            recipe.AddIngredient<Nyanmere>();
            recipe.AddIngredient<StarShower>();
            recipe.AddIngredient<AsteroidShower>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
