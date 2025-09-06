using Terraria.ModLoader;
using ssm.Core;
using Consolaria.Content.NPCs.Bosses.Ocram;
using Terraria;
using Consolaria.Content.NPCs.Bosses.Turkor;
using CalamityMod.Events;
using Redemption.NPCs.Bosses.ADD;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Erhan;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.Keeper;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Neb.Clone;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Obliterator;
using Redemption.NPCs.Bosses.PatientZero;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.Thorn;

namespace ssm.Consolaria
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class ConsolariaBossesBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool mayo;
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        bool CheckBossRush()
        {
            return BossRushEvent.BossRushActive;
        }
        public override void AI(NPC npc)
        {
            bool num = false;
            if (ModCompatibility.Calamity.Loaded)
            {
                num = CheckBossRush();
            }

            if (mayo)
            {
                if (!ssm.SwarmActive && !num)
                {
                    if (npc.type == ModContent.NPCType<Ocram>())
                    {
                        npc.lifeMax = (int)(54000 * (ModCompatibility.Calamity.Loaded ? 2f : 1.7));
                        npc.damage = (int)(55 * 2f);
                    }
                    if (npc.type == ModContent.NPCType<TurkortheUngrateful>())
                    {
                        npc.lifeMax = (int)(7000 * (ModCompatibility.Calamity.Loaded ? 1.5f : 1.2f));
                        npc.damage = (int)(40 * 1.3f);
                    }
                }

                npc.life = npc.lifeMax;
                mayo = true;
            }
        }
    }
}