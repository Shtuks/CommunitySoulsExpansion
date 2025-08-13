using ssm.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class CutOpen : ModBuff
    {
        public override string Texture => "ssm/Content/Buffs/ChtuxlagorInferno";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CSEThoriumNpcs>().isCutOpen = true;
        }
    }
}
