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
using NoxusBoss.Content.NPCs.Bosses.Avatar.FirstPhaseForm;
using NoxusBoss.Content.NPCs.Bosses.Avatar.SecondPhaseForm;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class BossRush : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModCompatibility.WrathoftheGods.Loaded;
        }
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
            BossDeathEffects.Add(NPCType<PrimordialWyrmHead>(), npc => { BossRushDialogueSystem.StartDialogue(DownedBossSystem.downedBossRush ? BossRushDialoguePhase.EndRepeat : BossRushDialoguePhase.End); });
        }
    }
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class WotGBossRush2 : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.BossRushPostMutant;
        }
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == NPCType<SupremeCalamitas>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(NPCType<PrimordialWyrmTail>());
                    Bosses[i].HostileNPCsToNotDelete.Add(NPCType<PrimordialWyrmBody>());
                    Bosses[i].HostileNPCsToNotDelete.Add(NPCType<PrimordialWyrmBodyAlt>());
                    Bosses.Insert(i, new Boss(NPCType<PrimordialWyrmHead>()));
                }
                if (Bosses[i].EntityID == NPCType<MutantBoss>())
                {
                    Bosses.Insert(i, new Boss(NPCType<AvatarRift>()));
                    Bosses.RemoveAt(i + 1);
                }
            }

            BossIDsAfterDeath.Add(NPCType<AvatarRift>(), [NPCType<AvatarOfEmptiness>()]);
            Bosses.Add(new Boss(NPCType<NamelessDeityBoss>()));
            //BossDeathEffects.Add(NPCType<NamelessDeityBoss>(), npc => { BossRushDialogueSystem.StartDialogue(DownedBossSystem.downedBossRush ? BossRushDialoguePhase.EndRepeat : BossRushDialoguePhase.End); });
            Bosses.Add(new Boss(NPCType<MutantBoss>()));
        }
    }
}