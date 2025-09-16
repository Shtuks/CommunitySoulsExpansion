using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Armor.PreHM.ElderWood;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.BaseExtension;
using Redemption.Globals.Player;
using static ssm.Redemption.Forces.AchivementForce;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Placeable.Furniture.Misc;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class ElderWoodEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(206, 182, 95);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ElderWoodEffect>(Item);
        }

        public class ElderWoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AchivementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElderWoodEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                BuffPlayer buffPlayer = player.RedemptionPlayerBuff();
                float toAdd = player.HasEffect<AchivementEffect>() ? 1 : 0.15f;
                buffPlayer.ElementalResistance[3] += toAdd;
                buffPlayer.ElementalResistance[1] += toAdd;
                buffPlayer.ElementalResistance[2] += toAdd;
                buffPlayer.ElementalResistance[4] += toAdd;
                buffPlayer.ElementalResistance[5] += toAdd;
                buffPlayer.ElementalResistance[6] += toAdd;
                buffPlayer.ElementalResistance[7] += toAdd;
                buffPlayer.ElementalResistance[8] += toAdd;
                buffPlayer.ElementalResistance[9] += toAdd;
                buffPlayer.ElementalResistance[10] += toAdd;
                buffPlayer.ElementalResistance[11] += toAdd;
                buffPlayer.ElementalResistance[12] += toAdd;
                buffPlayer.ElementalResistance[13] += toAdd;
                buffPlayer.ElementalResistance[14] += toAdd;
                buffPlayer.ElementalResistance[0] += toAdd;
               // buffPlayer.ElementalResistance[15] += toAdd;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<ElderWoodHelmet>();
            recipe.AddIngredient<ElderWoodBreastplate>();
            recipe.AddIngredient<ElderWoodGreaves>();
            recipe.AddIngredient<ElderWoodSword>();
            recipe.AddIngredient<ElderWoodStaff>();
            recipe.AddIngredient<GathicCryoFurnace>();
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
