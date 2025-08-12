using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs;
using ContinentOfJourney.NPCs.Boss_TheSon;
using ContinentOfJourney.NPCs.Boss_ScarabBelief;
using CalamityMod.NPCs.Bumblebirb;
using ContinentOfJourney.NPCs.Boss_SlimeGod;
using ContinentOfJourney.NPCs.Boss_TheOverwatcher;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name, ModCompatibility.Calamity.Name)]
    public class HwjBossRush : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.HWJBR;
        }
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<Bumblefuck>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<SlimeGod>(), TimeChangeContext.Night));
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Overseer>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<ScarabBelief>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Draedon>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<TheSon>()));
                }
            }
        }
    }
}