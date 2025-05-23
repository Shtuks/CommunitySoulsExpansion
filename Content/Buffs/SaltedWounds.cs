using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.TerMerica.Name)]
    [JITWhenModsEnabled(ModCompatibility.TerMerica.Name)]
    public class SaltedWounds : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = false;
            Main.pvpBuff[this.Type] = true;
            Main.buffNoSave[this.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Mod mod = ModLoader.GetMod("TerMerica");
            player.GetDamage(DamageClass.Generic) += 10 / 100f;
            player.lifeRegen += 5;
        }
    }
}
