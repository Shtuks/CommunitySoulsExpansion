using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Globals;
using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public class CSEBuffs : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if(type == ModContent.BuffType<MutantPresenceBuff>() && !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
            {
                player.FargoSouls().MutantPresence = false;
            }
        }
    }
}
