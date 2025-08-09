using System;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using ThoriumMod;
using Terraria.Localization;
using ThoriumMod.Utilities;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PostSetupContentThorium
    {
        public static void PostSetupContent_Thorium()
        {
            double Damage(DamageClass damageClass) => Math.Round(Main.LocalPlayer.GetTotalDamage(damageClass).Additive * Main.LocalPlayer.GetTotalDamage(damageClass).Multiplicative * 100 - 100);
            int Crit(DamageClass damageClass) => (int)Main.LocalPlayer.GetTotalCritChance(damageClass);

            int bardItem = ModContent.ItemType<GoldBugleHorn>();
            Func<string> bardDamage = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.BardDamage")}: {Damage(ModContent.GetInstance<BardDamage>())}%";
            Func<string> bardCrit = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.BardCrit")}: {Crit(ModContent.GetInstance<BardDamage>())}%";
            Func<string> bardResource = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.BardResource")}: {Main.LocalPlayer.GetThoriumPlayer().bardResourceMax2}";
            ModCompatibility.MutantMod.Mod.Call("AddStat", bardItem, bardDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", bardItem, bardCrit);
            ModCompatibility.MutantMod.Mod.Call("AddStat", bardItem, bardResource);

            int healerItem = ModContent.ItemType<PalmCross>();
            Func<string> healerDamage = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.HealerDamage")}: {Damage(ModContent.GetInstance<HealerDamage>())}%";
            Func<string> healerCrit = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.HealerCrit")}: {Crit(ModContent.GetInstance<HealerDamage>())}%";
            Func<string> healerAddHeal = () => $"{Language.GetTextValue("Mods.ssm.StatSheet.HealerAddHeal")}: {Main.LocalPlayer.GetThoriumPlayer().healBonus}%";
            ModCompatibility.MutantMod.Mod.Call("AddStat", healerItem, healerDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", healerItem, healerCrit);
            ModCompatibility.MutantMod.Mod.Call("AddStat", healerItem, healerAddHeal);
        }
    }
}
