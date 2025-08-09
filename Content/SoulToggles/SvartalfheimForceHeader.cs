using ssm.Thorium.Forces;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class SvartalfheimForceHeader : SoulHeader
    {
        public override float Priority => 70.4f;
        public override int Item => ModContent.ItemType<SvartalfheimForce>();
    }
}
