using FargowiltasSouls.Core.Systems;
using Luminance.Core.Hooking;
using Terraria.GameContent;
using Terraria.ID;

namespace ssm
{
    public class CSEDetours : ICustomDetourProvider
    {
        public void LoadDetours()
        {
            On_ShimmerTransforms.IsItemTransformLocked += IsItemTransformLocked;
        }
        public void UnloadDetours()
        {
        }

        void ICustomDetourProvider.ModifyMethods()
        {
        }
        private static bool IsItemTransformLocked(On_ShimmerTransforms.orig_IsItemTransformLocked orig, int type)
        {
            bool ret = orig(type);

            //Rod of Harmony pre Mutant
            //ofc i added 1 minute time frozen if you try to use it on bosses
            if (type == ItemID.RodofDiscord)
            {
                return !WorldSavingSystem.DownedAbom;
            }

            return ret;
        }
    }
}
