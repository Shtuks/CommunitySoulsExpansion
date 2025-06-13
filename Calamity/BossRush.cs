using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using CalamityMod.Enums;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Systems;
using CalamityMod;
using static Terraria.ModLoader.ModContent;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using CalamityMod.NPCs.PrimordialWyrm;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class BossRush : ModSystem
    {
        public override void PostSetupContent()
        {


            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == NPCType<MutantBoss>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(NPCType<PrimordialWyrmTail>());
                    Bosses[i].HostileNPCsToNotDelete.Add(NPCType<PrimordialWyrmBody>());
                    Bosses[i].HostileNPCsToNotDelete.Add(NPCType<PrimordialWyrmBodyAlt>());
                    Bosses.Insert(i, new Boss(NPCType<PrimordialWyrmHead>()));
                    Bosses.RemoveAt(i + 1);
                }
            }

            BossDeathEffects.Remove(NPCType<SupremeCalamitas>());
            BossDeathEffects.Remove(NPCType<DevourerofGodsHead>());
            BossDeathEffects.Add(NPCType<PrimordialWyrmHead>(), npc => { BossRushDialogueSystem.StartDialogue(DownedBossSystem.downedBossRush ? BossRushDialoguePhase.EndRepeat : BossRushDialoguePhase.End); });
        }
    }
}