using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.SummonItems;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Globals;
using ssm.Content.Buffs;

using ssm.Core;
using ssm.Items;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            for (int i = 0; i < Player.inventory.Length; i++)
            {
                Item item = Player.inventory[i];
                if (item.type == ModContent.ItemType<Terminus>() && item.active)
                {
                    item.SetDefaults(ModContent.ItemType<CSETerminus>());
                }
            }
        }

        //public override void PostUpdateMiscEffects()
        //{
        //    Player player = Main.LocalPlayer;
        //    var CalPlayer = player.GetModPlayer<CalamityPlayer>();

        //    Player.GetDamage<Th>() += CalPlayer.stealthDamage;
        //}

        public override void PostUpdateEquips()
        {
            if (Player.Calamity().dsSetBonus) {
                Player.GetModPlayer<CalamityPlayer>().wearingRogueArmor = true;
                Player.GetModPlayer<CalamityPlayer>().rogueStealthMax += 1.5f;
                Player.GetDamage<GenericDamageClass>() += 0.3f;
                Player.GetDamage<SummonDamageClass>() -= 0.3f;
                //yay 3k hp with all mods pre mutant
                Player.statLifeMax2 += 100;
            }
        }
        public override void PostUpdateBuffs()
        {
            if (DownedBossSystem.downedExoMechs && !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
            {
                Main.LocalPlayer.buffImmune[ModContent.BuffType<MutantFangBuff>()] = true;
            }
            if (Player.HasBuff<NihilityPresenceBuff>())
            {
                ModLoader.GetMod("CalamityMod").TryFind("Enraged", out ModBuff enrage);
                Main.LocalPlayer.buffImmune[enrage.Type] = true;
            }
        }
    }
}
