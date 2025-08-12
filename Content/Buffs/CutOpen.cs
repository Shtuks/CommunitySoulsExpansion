using ssm.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    internal class CutOpen : ModBuff
    {
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
