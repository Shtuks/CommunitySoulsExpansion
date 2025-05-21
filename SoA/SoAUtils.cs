using SacredTools.Common.Types;
using System;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public static class SoAUtils
    {
        //Just make that methods public
        //Why the fuck i have to copy them
        public static void AddLeveledBuff(this Player player, int type, int time, bool quiet = false, bool foodHack = false)
        {
            player.AddBuff(type, time, quiet, foodHack);
            if (BuffLoader.GetBuff(type) is LeveledBuff leveledBuff)
            {
                leveledBuff.BuffLevel(player) = Math.Max(leveledBuff.BuffLevel(player), 1);
            }
        }
        internal static void AddLeveledBuff<T>(this Player player, int time, bool quiet = false, bool foodHack = false) where T : LeveledBuff
        {
            player.AddLeveledBuff(ModContent.BuffType<T>(), time, quiet, foodHack);
        }
    }
}
