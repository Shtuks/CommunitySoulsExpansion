using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.gunrightsmod.Enchantments;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.gunrightsmod.Forces
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class IdeocracyForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 1183376;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
        }
    }
}
