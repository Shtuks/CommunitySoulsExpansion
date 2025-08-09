using Terraria.ModLoader;
using ssm.Core;
using SpiritMod.NPCs.Town;

namespace ssm.SpiritMod
{
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    internal class SpiritModCaughtNpcs : ModSystem
    {
        public static void SpiritModRegisterItems()
        {
            ssm.Add("Adventurer", ModContent.NPCType<Adventurer>());
            ssm.Add("Gambler", ModContent.NPCType<Gambler>());
            ssm.Add("Rogue", ModContent.NPCType<Rogue>());
            ssm.Add("RuneWizard", ModContent.NPCType<RuneWizard>());
        }
    }
}
