 using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using CalamityMod.Events;

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
                if (i == ModContent.NPCType<MutantBoss>())
                {
                    BossRushStage++;
                    //Bosses.RemoveAt(i);
                }
            }
        }
    }
}