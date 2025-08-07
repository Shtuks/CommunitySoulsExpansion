using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.gunrightsmod.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls;
using FargowiltasSouls.Core.ModPlayers;

namespace ssm.gunrightsmod.Forces
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class RadioactiveForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        public override void SetStaticDefaults()
        {
            Enchants[Type] =
            [
                ModContent.ItemType<AstatineEnchant>(),
                ModContent.ItemType<FaradayEnchant>(),
                ModContent.ItemType<PlutoniumEnchant>(),
                ModContent.ItemType<UraniumEnchant>(),
            ];
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // This is the only line you need to add to your original code.
            SetActive(player);

            player.AddEffect<RadioactiveForceEffect>(Item);
            player.AddEffect<AstatineEffect>(Item);
            //  player.AddEffect<FaradayEffect>(Item);
            //  player.AddEffect<PlutoniumEffect>(Item);
            //  player.AddEffect<UraniumEffect>(Item);
        }
    }

    public class RadioactiveForceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
        public override int ToggleItemType => ModContent.ItemType<RadioactiveForce>();


    }
}