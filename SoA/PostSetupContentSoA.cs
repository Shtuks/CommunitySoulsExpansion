using System;
using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using SacredTools.Common.Players;
using Terraria;
using SacredTools;
using FargowiltasSouls.Content.Items;
using static Terraria.ModLoader.ModContent;
using System.Linq;
using SacredTools.Content.Items.Weapons.Harpy;
using SacredTools.Content.Items.Weapons.Dreadfire;
using SacredTools.Content.Items.Weapons.Mechs;
using SacredTools.Items.Dev;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Items.Weapons.Luxite;
using SacredTools.Items.Weapons.Marstech;
using SacredTools.Items.Weapons.Oblivion;
using SacredTools.Items.Weapons.Pigman;
using SacredTools.Items.Weapons.Primordia;
using SacredTools.Items.Weapons.Special;
using SacredTools.Items.Weapons.Venomite;
using Terraria.Localization;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class PostSetupContentSoA
    {
        public static void PostSetupContent_Thorium()
        {
            int bossdmgItem = ItemType<RageSuppressor>();
            int accuracyItem = ItemType<CasterArcanum>();
            Func<string> bardDamage = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.BossDamage")}: {Main.LocalPlayer.GetModPlayer<MiscEffectsPlayer>().bossDamage.Multiplicative}%";
            Func<string> bardCrit = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.Accuracy")}: {Main.LocalPlayer.GetModPlayer<ModdedPlayer>().accuracy}";
            ModCompatibility.MutantMod.Mod.Call("AddStat", bossdmgItem, bardDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", accuracyItem, bardCrit);

            int[] SoASwordsToApplyRework = [ItemType<Feathersword>(), ItemType<RedSword>(), ItemType<ChromaUltima>(),
            ItemType<DragonslayerPandolarra>(), ItemType<PumpkinCarver>(), ItemType<CrimsonVeins>(),
            ItemType<Eredhun>(),ItemType<NvidiaSword>(),ItemType<MidnightBlade>(),
            ItemType<FlariumSword>(),ItemType<LapisSword>(),ItemType<Nyanmere>(),
            ItemType<StarShower>(),ItemType<Claymarine>(),ItemType<PhaseSlasher>(),
            ItemType<MoonEdgedPandolarra>(),ItemType<TrueMoonEdgedPandolarra>(),ItemType<Evanescense>(),
            ItemType<OversizedFang>(),ItemType<Brandblade>(),ItemType<Pandolarra>(),
            ItemType<Shaytnajima>(),ItemType<Skill_FuryForged>(),ItemType<Meenmourne>(),
            ItemType<Yaldabaoth>(),ItemType<TruePandolarra>(),
            ItemType<VenomiteSword>(),ItemType<HoariHemonga>(),];
            SwordGlobalItem.AllowedModdedSwords = SwordGlobalItem.AllowedModdedSwords.Union(SoASwordsToApplyRework).ToArray();
        }
    }
}
