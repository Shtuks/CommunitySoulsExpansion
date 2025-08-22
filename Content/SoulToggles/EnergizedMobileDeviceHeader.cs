using FargowiltasSouls.Core.Toggler.Content;
using ssm.Content.Items;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class EnergizedMobileDeviceHeader : SoulHeader
    {
        public override float Priority => 8f;
        public override int Item => ModContent.ItemType<BossWatcher>();
    }
}
