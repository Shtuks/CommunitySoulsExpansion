using ssm.Items;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod.NPCs;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    internal class ThoriumCaughtNpcs : ModSystem
    {
        public static void ThoriumRegisterItems()
        {
            ssm.Add("Blacksmith", ModContent.NPCType<Blacksmith>());
            ssm.Add("Cook", ModContent.NPCType<Cook>());
            ssm.Add("Cobbler", ModContent.NPCType<Cobbler>());
            ssm.Add("ConfusedZombie", ModContent.NPCType<ConfusedZombie>());
            ssm.Add("DesertAcolyte", ModContent.NPCType<DesertAcolyte>());
            ssm.Add("Diverman", ModContent.NPCType<Diverman>());
            ssm.Add("Druid", ModContent.NPCType<Druid>());
            ssm.Add("Spiritualist", ModContent.NPCType<Spiritualist>());
            ssm.Add("Tracker", ModContent.NPCType<Tracker>());
            ssm.Add("WeaponMaster", ModContent.NPCType<WeaponMaster>());
        }
    }
}
