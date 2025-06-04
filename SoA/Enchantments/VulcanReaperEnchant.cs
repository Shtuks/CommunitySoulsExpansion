using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Content.Items.Armor.Vulcan;
using SacredTools.Items.Potions;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Placeable.Paintings;
using ssm.Core;
using SacredTools.Common.Players;
using FargowiltasSouls;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VulcanReaperEnchant : BaseEnchant
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

        public override Color nameColor => new(138, 36, 58);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<VulcanReaperEffect>(Item))
            {
                player.GetModPlayer<SoAPlayer>().vulcanReaperEnchant = player.ForceEffect<VulcanReaperEffect>() ? 2 : 1;
            }
            player.buffImmune[ModContent.Find<ModBuff>(ModCompatibility.SacredTools.Name, "ObsidianCurse").Type] = true;
        }

        public class VulcanReaperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VulcanReaperEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<MiscEffectsPlayer>().bossDamage *= 1f + (player.GetModPlayer<SoAPlayer>().vulcanStacks * 0.05f);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VulcanHelm>();
            recipe.AddIngredient<VulcanChest>();
            recipe.AddIngredient<VulcanLegs>();
            recipe.AddIngredient<SmolderingSpicyCurry>();
            recipe.AddIngredient<SerpentChain>();
            recipe.AddIngredient<Warmth>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
