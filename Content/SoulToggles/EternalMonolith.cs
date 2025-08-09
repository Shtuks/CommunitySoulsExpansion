using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.Items.Accessories;
using Terraria.ModLoader;
using Terraria;
using Terraria.Graphics.Effects;

namespace ssm.Content.SoulToggles
{
    public class EternalMonolith : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<PhantaplazmalEnchant>() || entity.type == ModContent.ItemType<EternityForce>() || entity.type == ModContent.ItemType<EternitySoul>() || entity.type == ModContent.ItemType<StargateSoul>();
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (player.AddEffect<EternalMonolithEffect>(item))
            {
                player.CSE().eternalMonolith = true;
                if (!SkyManager.Instance["ssm:MutantMonolith"].IsActive())
                    SkyManager.Instance.Activate("ssm:MutantMonolith");
            }
            base.UpdateAccessory(item, player, hideVisual);
        }
        public class EternalMonolithEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhantaplazmalEnchant>();
        }
    }
}
