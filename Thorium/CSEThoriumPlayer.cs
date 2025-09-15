using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.IllumiteEnchant;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]

    public partial class CSEThoriumPlayer : ModPlayer
    {
        public bool tripleDamageNextHit;
        public bool illumiteNightVision;
        public override void ResetEffects()
        {
            tripleDamageNextHit = false;
            if (!Player.HasEffect<IllumiteEffect>())
            {
                illumiteNightVision = false;
            }
        }

        public override void UpdateDead()
        {
            illumiteNightVision = false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (tripleDamageNextHit)
            {
                modifiers.FinalDamage *= 3;
                tripleDamageNextHit = false;
            }
        }
    }
}