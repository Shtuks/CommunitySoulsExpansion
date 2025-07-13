using ssm.Content.Tiles;
using Terraria.ModLoader;

namespace ssm.Content.Items.Placeable
{
    public class MonstrosityRelicItem : FargowiltasSouls.Content.Items.Placables.Relics.BaseRelic
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        protected override int TileType => ModContent.TileType<MonstrosityRelicTile>();
    }
}
