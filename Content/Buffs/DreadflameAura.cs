using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.DreadfireEnchant;
using static ssm.SoA.Forces.GenerationsForce;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class DreadflameAura : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<GenericDamageClass>() += player.ForceEffect<DreadfireEffect>() ? 0.3f : 0.2f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= Main.LocalPlayer.HasEffect<GenerationsEffect>() ? 300 : 30;
        }
    }
}
