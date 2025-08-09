//using SacredTools.Content.DamageClasses;
//using ssm.Core;
//using Terraria.ModLoader;

//namespace ssm.Content.DamageClasses
//{
//    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
//    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
//    public class SoAUMT
//    {
//        public static StatInheritanceData SoAUMTAdd(DamageClass damageClass)
//        {
//            if (damageClass == ModContent.GetInstance<KineticDamageClass>())
//            {
//                return StatInheritanceData.Full;
//            }

//            return StatInheritanceData.None;
//        }

//        public static DamageClass GetEffectInheritance(DamageClass damageClass)
//        {
//            return ModContent.GetInstance<KineticDamageClass>();
//        }
//    }
//}
