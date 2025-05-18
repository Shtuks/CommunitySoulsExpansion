using ssm.Items;
using Terraria.ModLoader;
using ssm.Core;
using gunrightsmod.Content.NPCs;

namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.TerMerica.Name)]
    [JITWhenModsEnabled(ModCompatibility.TerMerica.Name)]
    internal class gunrightsmodCaughtNpcs : ModSystem
    {
        public static void gunrightsmodRegisterItems()
        {
            CaughtNPCItem.Add("Politician", ModContent.NPCType<Politician>(), "'“Might be better for society to just not release this one”'");
        }
    }
}
