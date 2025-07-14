using SacredTools.Common.Systems;
using ssm.Core;
using Terraria.ModLoader;

namespace ssm.SoA
{
    //:sob:
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public static class TrueModeManager
    {
        public static void setTrueMode(bool v)
        {
            var trueModeProperty = typeof(TrueModeSystem).GetProperty("TrueMode");
            var setMethod = trueModeProperty.GetSetMethod(nonPublic: v);
            setMethod.Invoke(null, new object[] { v });
        }
    }
}
