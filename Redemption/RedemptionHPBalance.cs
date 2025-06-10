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
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            //if (WorldSavingSystem.EternityMode)
            //{
                if (npc.type == ModContent.NPCType<Nebuleus>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 1f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.6f; }

                    npc.lifeMax = (int)(3400000 + (1000000 * multiplier));
                    npc.damage = 730;
                }

                if (npc.type == ModContent.NPCType<Nebuleus2>())
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 1f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.6f; }

                    npc.lifeMax = (int)(3700000 + (1000000 * multiplier));
                    npc.damage = 800;
                }

                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = 1600000;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = 1800000;
                    npc.damage = 470;
                }
            //}
        }
    }
}
