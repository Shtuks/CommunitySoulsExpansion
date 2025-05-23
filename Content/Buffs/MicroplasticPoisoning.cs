using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class MicroplasticPoisoning : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int stacks = GetBuffStacks(player.buffTime[buffIndex]);

            switch (stacks)
            {
                case 3:
                    player.endurance *= 0.90f;
                    break;
                case 4:
                    player.endurance *= 0.85f;
                    break;
                default:
                    if (stacks >= 5)
                        player.endurance *= 0.80f;
                    break;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int stacks = GetBuffStacks(npc.buffTime[buffIndex]);

            switch (stacks)
            {
                case 3:
                    npc.takenDamageMultiplier += 0.10f;
                    break;
                case 4:
                    npc.takenDamageMultiplier += 0.15f;
                    break;
                default:
                    if (stacks >= 5)
                        npc.takenDamageMultiplier += 0.20f;
                    break;
            }
        }

        private int GetBuffStacks(int buffTime)
        {
            return buffTime / 360;
        }
    }
}
