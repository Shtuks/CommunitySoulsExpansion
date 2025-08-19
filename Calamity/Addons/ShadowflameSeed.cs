using NoxusBoss.Content.Rarities;
using NoxusBoss.Core.GlobalInstances;
using SacredTools.Content.Items.Materials;
using ssm.Core;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.WrathoftheGods.Name)]
    public class ShadowflameSeed : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.ItemNoGravity[Type] = true;

        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 0;
            Item.rare = ModContent.RarityType<GenesisComponentRarity>();
            //Item.DefaultToPlaceableTile(ModContent.TileType<ShadowflameSeedTile>());
            Item.Wrath().GenesisComponent = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).
                AddTile(TileID.WorkBenches).
                AddIngredient<EmberOfOmen>(5).
                AddIngredient(ItemID.Seed).
                Register();
        }
    }
    public class SoAWotGItem : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ShadowflameSeed>(), 1));
        }
    }
}