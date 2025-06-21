using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;

namespace ssm.Content.DamageClasses
{
    public partial class UnitedModdedThrower : DamageClass, ColoredDamageTypesSupport.IDamageColor
    {
        internal static UnitedModdedThrower Instance;
        Color ColoredDamageTypesSupport.IDamageColor.DamageColor => new Color(255, 100, 100);

        public override void Load()
        {
            Instance = this;
        }

        public override void Unload()
        {
            Instance = null;
        }

        public override bool GetPrefixInheritance(DamageClass damageClass)
        {
            return damageClass == Ranged;
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Throwing || damageClass == Generic)
            {
                return StatInheritanceData.Full;
            }

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if(damageClass == Throwing)
            {
                return true;
            }

            if (ModCompatibility.SacredTools.Loaded)
            {
                if (damageClass == SoAUMT.GetEffectInheritance(damageClass))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
