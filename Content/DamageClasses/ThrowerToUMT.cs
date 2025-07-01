using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.DamageClasses
{
    public class ThrowingToUMT : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ThrowerMerge;
        }
        public override void SetDefaults(Item item)
        {
            if (item.DamageType == DamageClass.Throwing)
            {
                item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
            } 
        }
    }

    public class ThrowingToUMTProj : GlobalProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ThrowerMerge;
        }
        public override void SetDefaults(Projectile item)
        {
            if (item.DamageType == DamageClass.Throwing)
            {
                item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
            }
        }
    }
}