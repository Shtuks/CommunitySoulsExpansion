using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Buffs
{
    public class PureFlameBuff : ModBuff
    {
        public override string Texture => "ssm/Content/Buffs/ChtuxlagorInferno";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = -10; 
        }
    }
}
