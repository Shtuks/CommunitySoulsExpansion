using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Globals;

namespace ssm.Content.NPCs.MutantEX.HitPlayer
{
    internal class MonstrHealthPlayer : ModPlayer
    {
        internal int OriginalMaxLife = 0;
        internal int HealthReduction = 0;
        internal int iFrames = 0;

        public override void OnRespawn()
        {
            OriginalMaxLife = 0;
            HealthReduction = 0;
        }

        public override void ResetEffects()
        {
            iFrames--;
        }

        public override void UpdateEquips()
        {
            if (!FargoSoulsUtil.BossIsAlive(ref CSENpcs.mutantEX, ModContent.NPCType<MutantEX>()) && !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
            {
                HealthReduction = 0;
            }
        }
        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            mana = StatModifier.Default;
            health = StatModifier.Default;
            if (OriginalMaxLife > 0)
            {
                health.Base = OriginalMaxLife - HealthReduction;
            }
        }

        public void SyncData()
        {
            if (Main.netMode == NetmodeID.SinglePlayer) return;

            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)ssm.PacketID.HealthDataSync);
            packet.Write((byte)Player.whoAmI);
            packet.Write(OriginalMaxLife);
            packet.Write(HealthReduction);
            packet.Send();
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            if (newPlayer && OriginalMaxLife > 0)
            {
                SyncData();
            }
        }
    }
}
