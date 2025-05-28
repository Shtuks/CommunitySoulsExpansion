using FargowiltasSouls.Core.Toggler.Content;
using ssm.Consolaria.Forces;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.SoulToggles
{
    public class HeroHeader : EnchantHeader
    {
        public override int Item => ModContent.ItemType<HeroForce>();
        public override float Priority => 0.81f;
    }
}
