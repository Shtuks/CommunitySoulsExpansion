using ssm.Items;
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
            CaughtNPCItem.Add("Politician", ModContent.NPCType<Politician>(), "'“Might be better for society to just not release this one”'");
            CaughtNPCItem.Add("Dumbass", ModContent.NPCType<River>(), "'“Don't lose track of them, they might wander into lava”'");
        }
    }
}
