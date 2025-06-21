using System;
using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using SacredTools.Common.Players;
using Terraria;
using SacredTools;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class PostSetupContentSoA
    {
        public static void PostSetupContent_Thorium()
        {
            int bossdmgItem = ModContent.ItemType<RageSuppressor>();
            int accuracyItem = ModContent.ItemType<CasterArcanum>();
            Func<string> bardDamage = () => $"Boss Damage: {Main.LocalPlayer.GetModPlayer<MiscEffectsPlayer>().bossDamage}%";
            Func<string> bardCrit = () => $"Accuracy: {Main.LocalPlayer.GetModPlayer<ModdedPlayer>().accuracy}";
            ModCompatibility.MutantMod.Mod.Call("AddStat", bossdmgItem, bardDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", accuracyItem, bardCrit);
        }
    }
}
