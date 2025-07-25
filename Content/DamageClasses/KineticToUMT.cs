//using SacredTools.Content.DamageClasses;
//using Terraria;
//using Terraria.ModLoader;
//using ssm.Core;
//

//namespace ssm.Reworks
//{
//    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
//    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
//    public class KineticToUMT : GlobalItem
//    {
//        public override bool IsLoadingEnabled(Mod mod)
//        {
//            return CSEConfig.Instance.ThrowerMerge && !ModLoader.HasMod("ThrowerUnification");
//        }
//        public override void SetDefaults(Item item)
//        {
//            if (item.DamageType == ModContent.GetInstance<KineticDamageClass>())
//            {
//                item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
//            }
//        }
//    }
//    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
//    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
//    public class KineticToUMTProj : GlobalProjectile
//    {
//        public override bool IsLoadingEnabled(Mod mod)
//        {
//            return CSEConfig.Instance.ThrowerMerge && !ModLoader.HasMod("ThrowerUnification");
//        }
//        public override void SetDefaults(Projectile item)
//        {
//            if (item.DamageType == ModContent.GetInstance<KineticDamageClass>())
//            {
//                item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
//            }
//        }
//    }
//}