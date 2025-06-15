using Terraria.ModLoader;
using ssm.Core;
using gunrightsmod.Content.NPCs;

namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    internal class gunrightsmodCaughtNpcs : ModSystem
    {
        public static void gunrightsmodRegisterItems()
        {
            ssm.Add("Politician", ModContent.NPCType<Politician>());
            ssm.Add("Dumbass", ModContent.NPCType<River>());
        }
    }
}
