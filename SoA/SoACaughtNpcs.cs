using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Content.NPCs.Town;
using FargowiltasSouls.Content.Items.Armor;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    internal class SoACaughtNpcs : ModSystem
    {
        public static void SoARegisterItems()
        {
            ssm.Add("PureNymph", ModContent.NPCType<RNGNymph>());
            ssm.Add("Scavenger", ModContent.NPCType<Scavenger>());
            ssm.Add("PandolarSalvager", ModContent.NPCType<Pandolar>());
            ssm.Add("Decorationist", ModContent.NPCType<Decorationist>());
            ssm.Add("CloakedAlchemist", ModContent.NPCType<Neil>());
        }
    }
}
