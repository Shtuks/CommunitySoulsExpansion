using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using CalamityMod;
using ssm.Core;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class EnrageCal : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/StatDebuffs/Enraged";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            BuffID.Sets.IsATagBuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.Calamity().enraged = true;
        }
    }
}
