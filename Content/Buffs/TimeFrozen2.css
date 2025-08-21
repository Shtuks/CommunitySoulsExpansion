using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls;
using Luminance.Core.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Content.NPCs.RealMutantEX;

namespace ssm.Content.Buffs
{
    internal class TimeFrozenBuff2 : ModBuff
    {
        public override string Texture => "FargowiltasSouls/Content/Buffs/Souls/TimeFrozenBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.pvpBuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.Incapacitate();
            player.velocity = player.oldVelocity;
            player.position = player.oldPosition;

            player.FargoSouls().MutantNibble = true;

            ssm.ManageMusicTimestop(player.buffTime[buffIndex] < 5);

            if (!Main.dedServ && player.whoAmI == Main.myPlayer)
            {
                ManagedScreenFilter filter = ShaderManager.GetFilter("FargowiltasSouls.Invert");
                if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<CosmosChampion>())
                        && Main.npc[EModeGlobalNPC.championBoss].ai[0] == 15)
                {
                    filter.SetFocusPosition(Main.npc[EModeGlobalNPC.championBoss].Center);
                }

                if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>())
                    && WorldSavingSystem.MasochistModeReal && Main.npc[EModeGlobalNPC.mutantBoss].ai[0] == -5)
                {
                    filter.SetFocusPosition(Main.npc[EModeGlobalNPC.mutantBoss].Center);
                }

                if (FargoSoulsUtil.BossIsAlive(ref CSENpcs.RealMutantEX, ModContent.NPCType<RealMutantEX>()))
                {
                    filter.SetFocusPosition(Main.npc[CSENpcs.RealMutantEX].Center);
                }

                if (player.buffTime[buffIndex] > 60)
                    filter.Activate();

                if (player.buffTime[buffIndex] == 90)
                    SoundEngine.PlaySound(new SoundStyle("FargowiltasSouls/Assets/Sounds/Accessories/ZaWarudoResume"), player.Center);

                if (Main.WaveQuality == 0)
                    Main.WaveQuality = 1;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.FargoSouls().TimeFrozen = true;
        }
    }
}