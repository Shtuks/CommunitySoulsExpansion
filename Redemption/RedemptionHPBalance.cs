using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Core.Systems;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.ADD;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionHPBalance : GlobalNPC
    {
        public bool fullHP = false;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (WorldSavingSystem.EternityMode)
            {
                if (npc.type == ModContent.NPCType<Nebuleus>())
                {
                    npc.lifeMax = 2700000;
                    npc.damage = 790;
                }

                if (npc.type == ModContent.NPCType<Nebuleus2>())
                {
                    npc.lifeMax = 4100000;
                    npc.damage = 890;
                }

                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = 1700000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = 1900000;
                    npc.damage = 470;
                }
                if (!fullHP) { npc.life = npc.lifeMax; fullHP = true; }
            }
            return base.PreAI(npc);
        }
    }
}
