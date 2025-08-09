using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Lodestone;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LodestoneEnchant : BaseEnchant
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
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<LodestoneEffect2>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "LodeStoneFaceGuard").UpdateArmorSet(player);
            }
            ModContent.Find<ModItem>(this.thorium.Name, "ObsidianScale").UpdateAccessory(player, true);

            if (player.AddEffect<LodestoneEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "SandweaversTiara").UpdateAccessory(player, true);
            }
        }

        public class LodestoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LodestoneEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public class LodestoneEffect2 : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LodestoneEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LodeStoneFaceGuard>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneShinGuards>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneClaymore>()); //astrobettle husk
            recipe.AddIngredient(ModContent.ItemType<ObsidianScale>());
            recipe.AddIngredient(ModContent.ItemType<SandweaversTiara>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
