using ssm.Content.Items.Accessories;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class EternityForceHeader : SoulHeader
    {
        public override float Priority => 6.3f;
        public override int Item => ModContent.ItemType<EternityForce>();
    }
}
