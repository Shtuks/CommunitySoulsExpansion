﻿using FargowiltasSouls;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class MonstrosityPresenceBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.CSE().MonstrosityPresence = true;
            player.FargoSouls().OceanicMaul = true;
            player.FargoSouls().TinEternityDamage = 0;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().noSupersonic = true;
            player.FargoSouls().MutantPresence = true;
            player.FargoSouls().TinEternityDamage = 0;
            player.FargoSouls().GrazeRadius *= 0.5f;
            player.moonLeech = true;
        }
    }
}
