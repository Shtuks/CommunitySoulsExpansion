using ssm.SpiritMod.Forces;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class AtlantisForceHeader : SoulHeader
    {
        public override float Priority => 7.4f;
        public override int Item => ModContent.ItemType<AtlantisForce>();
    }
}