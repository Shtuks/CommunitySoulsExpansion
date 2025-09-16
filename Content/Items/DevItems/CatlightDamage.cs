using Terraria.ModLoader;

namespace ssm.Content.Items.DevItems
{
    internal class CatlightDamage : DamageClass
    {
        internal static CatlightDamage Instance;

        public override void Load()
        {
            Instance = this;
        }

        public override void Unload()
        {
            Instance = null;
        }

        //public override bool GetPrefixInheritance(DamageClass damageClass)
        //{
        //    return true;
        //}
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return true;
        }
    }
}
