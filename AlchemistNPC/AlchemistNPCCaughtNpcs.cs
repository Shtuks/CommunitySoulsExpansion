using Terraria.ModLoader;
using ssm.Core;
using AlchemistNPCLite.NPCs;

namespace ssm.AlchemistNPC
{
    [ExtendsFromMod(ModCompatibility.AlchNPCs.Name)]
    [JITWhenModsEnabled(ModCompatibility.AlchNPCs.Name)]
    internal class AlchemistNPCCaughtNpcs : ModSystem
    {
        public static void AlchemistNPCCaughtNpcsRegisterItems()
        {
            ssm.Add("Alchemist", ModContent.NPCType<Alchemist>());
            ssm.Add("Architect", ModContent.NPCType<Architect>());
            ssm.Add("Brewer", ModContent.NPCType<Brewer>());
            ssm.Add("Jeweler", ModContent.NPCType<Jeweler>());
            ssm.Add("Musician", ModContent.NPCType<Musician>());
            ssm.Add("Operator", ModContent.NPCType<Operator>());
            ssm.Add("Tinkerer", ModContent.NPCType<Tinkerer>());
            ssm.Add("YoungBrewer", ModContent.NPCType<YoungBrewer>());
        }
    }
}
