using Terraria.ModLoader;
using ssm.Core;
using Consolaria.Content.NPCs.Friendly.McMoneypants;

namespace ssm.Calamity.Addons.CalamityAmmo
{
    [JITWhenModsEnabled("CalamityAmmo")]
    [ExtendsFromMod("CalamityAmmo")]
    internal class CalamityAmmoCaughtNPCs : ModSystem
    {
        public static void CalamityAmmoRegisterItems()
        {
            if (ModLoader.TryGetMod("CalamityAmmo", out Mod? mod))
            {
                ssm.Add("AmmoDealer", mod.Find<ModNPC>("QianXing").Type);
            }
        }
    }
}