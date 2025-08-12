using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using ThoriumMod.NPCs.BossThePrimordials;
using CalamityMod.NPCs.DevourerofGods;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class ThoriumBossRush : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.ThoriumBR;
        }
        public override void PostSetupContent()
        {
            if (!ModLoader.HasMod("RagnarokMod") && !ModLoader.HasMod("ThoriumRework"))
            {
                for (int i = Bosses.Count - 1; i >= 0; i--)
                {
                    if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<DreamEater>()));
                    }
                }
            }
        }
    }
}