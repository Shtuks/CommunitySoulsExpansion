using Terraria.ModLoader;
using ssm.Core;
using Consolaria.Content.NPCs.Friendly.McMoneypants;

namespace ssm.Consolaria
{
    // Okay, so I know these two aren't consolaria, but they are kind of similar? One's consolaria and one's Heartbeatraria, which is just chinese instead of console
    // I just don't really want to make a whole folder for them.
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    internal class ConsolariaCaughtNpcs : ModSystem
    {
        public static void ConsolariaRegisterItems()
        {
            ssm.Add("McMoneypants", ModContent.NPCType<McMoneypants>());
        }
    }
    [JITWhenModsEnabled("XDContentMod")]
    [ExtendsFromMod("XDContentMod")]
    internal class XDContentModCaughtNPCs : ModSystem
    {
        public static void XDContentModRegisterItems()
        {
            if (ModLoader.TryGetMod("XDContentMod", out Mod XD))
            {
                ssm.Add("StarMerchantNPC", XD.Find<ModNPC>("StarMerchantNPC").Type);
            }
        }
    }
}