using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Dreadfire;
using SacredTools.Content.Items.Weapons.Dreadfire;
using SacredTools.Content.Items.Armor.Exodus;
using SacredTools.Content.Items.LuxShards;
using SacredTools.Items.Weapons.Luxite;
using SacredTools.Common.GlobalItems;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class ExitumLuxEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(137, 154, 178);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ExitumLuxEffect>(Item);
        }

        public class ExitumLuxEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ExitumLuxEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                if(player.HeldItem.ModItem is IRelicItem)
                {
                    player.GetDamage<GenericDamageClass>() += 0.1f;
                    player.GetCritChance<GenericDamageClass>() += 0.1f;
                    player.GetAttackSpeed<GenericDamageClass>() += 0.1f;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<ExodusHelmet>();
            recipe.AddIngredient<ExodusChest>();
            recipe.AddIngredient<ExodusLegs>();
            recipe.AddIngredient<LuxDustThrown>();
            recipe.AddIngredient<Claymarine>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
