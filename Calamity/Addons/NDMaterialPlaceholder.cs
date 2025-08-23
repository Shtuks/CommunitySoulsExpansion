using CalamityMod.Items;
using NoxusBoss.Content.Items;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class NDMaterialPlaceholder : ModItem
    {
        public bool onFirstTick;
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<MetallicChunk>());
            Item.rare = ModCompatibility.WrathoftheGods.Mod.Find<ModRarity>("NamelessDeityRarity").Type;
        }
        public override void UpdateInventory(Player player)
        {
            if(onFirstTick)
            {
                CSEUtils.RemoveItem(ModContent.ItemType<Rock>());
                onFirstTick = true;
            }
            base.UpdateInventory(player);
        }
    }
}
