using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using ssm.gunrightsmod.Enchantments;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class RadioactiveDecay : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 10;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 450;
        }
    }
}
