<<<<<<< HEAD
using ssm.Items;
=======
>>>>>>> main
using Terraria.ModLoader;
using ssm.Core;
using gunrightsmod.Content.NPCs;

<<<<<<< HEAD
namespace ssm.TerMerica
{
    [ExtendsFromMod(ModCompatibility.TerMerica.Name)]
    [JITWhenModsEnabled(ModCompatibility.TerMerica.Name)]
=======
namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    
>>>>>>> main
    internal class gunrightsmodCaughtNpcs : ModSystem
    {
        public static void gunrightsmodRegisterItems()
        {
<<<<<<< HEAD
            CaughtNPCItem.Add("Politician", ModContent.NPCType<Politician>(), "'“Might be better for society to just not release this one”'");
=======
            ssm.Add("Politician", ModContent.NPCType<Politician>());
            ssm.Add("Dumbass", ModContent.NPCType<River>());
>>>>>>> main
        }
    }
}
