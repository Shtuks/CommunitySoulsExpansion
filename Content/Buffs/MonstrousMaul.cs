using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls;

namespace ssm.Content.Buffs
{
    public class MonstrousMaul : ModBuff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.FargoSouls().FlamesoftheUniverse = true;
            player.FargoSouls().MutantNibble = true;
            player.FargoSouls().CurseoftheMoon = true;
            player.FargoSouls().Defenseless = true;
            player.FargoSouls().GodEater = true;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().MutantPresence = true;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().Infested = true;
            player.FargoSouls().MutantNibble = true;
            player.FargoSouls().OceanicMaul = true;
            player.FargoSouls().noSupersonic = true;

            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
        }
    }
}
