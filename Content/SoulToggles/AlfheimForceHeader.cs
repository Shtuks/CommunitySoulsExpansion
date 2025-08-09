using ssm.Thorium.Forces;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class AlfheimForceHeader : SoulHeader
    {
        public override float Priority => 60.8f;
        public override int Item => ModContent.ItemType<AlfheimForce>();
    }
}
