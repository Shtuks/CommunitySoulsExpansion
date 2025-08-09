using Terraria.ModLoader;
using ssm.Core;
using Redemption.NPCs.Friendly.TownNPCs;
using Redemption.NPCs.Minibosses.Calavia;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    internal class RedemptionCaughtNpcs : ModSystem
    {
        public static void RedemptionRegisterItems()
        {
            ssm.Add("Fallen", ModContent.NPCType<Fallen>());
            ssm.Add("Fool", ModContent.NPCType<Newb>());
            ssm.Add("FriendlyTbot", ModContent.NPCType<TBot>());
            ssm.Add("Calavia", ModContent.NPCType<Calavia_NPC>());
            ssm.Add("Wayfarer", ModContent.NPCType<Daerel>());
        }
    }
}
