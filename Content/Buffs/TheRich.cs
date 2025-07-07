using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Buffs
{
    public class TheRichBuff : ModBuff
    {
        public override string Texture => "ssm/Content/Buffs/ChtuxlagorInferno";
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.05f;
            player.GetDamage(DamageClass.Generic) += 0.15f;
        }
    }
}
