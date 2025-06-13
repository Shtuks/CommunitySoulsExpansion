using CalamityMod.NPCs.Providence;
using static CalamityMod.Events.BossRushEvent;
using Terraria.ModLoader;
using CatalystMod.NPCs.Boss.Astrageldon;
using ssm.Core;
using NoxusBoss.Content.NPCs.Bosses.Draedon;
using CalamityMod.NPCs.ExoMechs;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name, ModCompatibility.Calamity.Name)]
    public class CatalystBossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModCompatibility.IEoR.Loaded)
            {
                for (int i = Bosses.Count - 1; i >= 0; i--)
                {
                    if (Bosses[i].EntityID == ModContent.NPCType<Providence>())
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<Astrageldon>(), TimeChangeContext.Night));
                    }
                }
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.WrathoftheGods.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.WrathoftheGods.Name, ModCompatibility.Calamity.Name)]
    public class WotGBossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<Draedon>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<MarsBody>(), TimeChangeContext.Night));
                }
            }
        }
    }
}
