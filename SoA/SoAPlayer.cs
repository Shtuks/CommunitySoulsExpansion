using SacredTools;

using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAPlayer : ModPlayer
    {
        public int rivalStreak;

        public override void PostUpdateEquips()
        {
            //if (Player.GetModPlayer<ModdedPlayer>().AstralSet)
            //{
            //    Player.GetDamage<UnitedModdedThrower>() += 0.7f;
            //}
        }
    }
}
