using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Systems
{
    public class ModIntergationSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            if (ModCompatibility.HEROSMod.Loaded || ModCompatibility.Dragonlens.Loaded || ModCompatibility.CheatSheet.Loaded)
            {
                //PrivateClassEdits.LoadAntiCheats();
            }
        }
        public static class BossChecklist
        {
            public static void AdjustValues()
            {
                ModCompatibility.SoulsMod.Mod.BossChecklistValues["CosmosChampion"] = 21.2f;
                ModCompatibility.SoulsMod.Mod.BossChecklistValues["MutantBoss"] = int.MaxValue - 1;
                ModCompatibility.SoulsMod.Mod.BossChecklistValues["AbomBoss"] = 22.9f;
            }
        }
    }
}