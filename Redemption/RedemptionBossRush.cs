﻿using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using Redemption.NPCs.Bosses.ADD;
using Redemption.NPCs.Bosses.Neb;
using CalamityMod.NPCs.Polterghast;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    public class RedemptionBossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<Polterghast>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Akka>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Ukko>()));
                }
                Bosses.Add(new Boss(ModContent.NPCType<Nebuleus>(), TimeChangeContext.Night));
            }

            //BossIDsAfterDeath.Add(ModContent.NPCType<Nebuleus>(), [ModContent.NPCType<Nebuleus2>()]);
        }
    }
}