using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Guntera
{
    public class Gun : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}