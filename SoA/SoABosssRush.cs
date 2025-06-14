using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using SacredTools.NPCs.Boss.Abaddon;
using CalamityMod.NPCs.CeaselessVoid;
using SacredTools.NPCs.Boss.Araghur;
using CalamityMod.NPCs.DevourerofGods;
using SacredTools.NPCs.Boss.Lunarians;
using CalamityMod.NPCs.Providence;
using SacredTools.NPCs.Boss.Erazor;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.OldDuke;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    public class SoABossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<Providence>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Abaddon>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Polterghast>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurMinion>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurBody>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurTail>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<AraghurHead>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<OldDuke>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Novaniel>()));
                }
                Bosses.Add(new Boss(ModContent.NPCType<ErazorBoss>()));
            }
        }
    }
}