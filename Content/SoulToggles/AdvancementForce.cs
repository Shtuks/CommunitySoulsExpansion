using FargowiltasSouls.Core.Toggler.Content;
using ssm.Redemption.Forces;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class AdvancementForceHeader : SoulHeader
    {
        public override float Priority => 8f;
        public override int Item => ModContent.ItemType<AdvancementForce>();
    }
}
