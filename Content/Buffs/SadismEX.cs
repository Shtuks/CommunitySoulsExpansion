using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    internal class SadismEX : ModBuff
    {
        private List<int> debuffIndexes = new List<int>();
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.persistentBuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                {
                    player.buffImmune[index] = true;
                    if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
                    {
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "RageMode").Type] = false;
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "AdrenalineMode").Type] = false;
                    }
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (player.buffType[i] > 0 && Main.debuff[player.buffType[i]])
                {
                    debuffIndexes.Add(i);
                }
            }

            for (int i = debuffIndexes.Count - 1; i >= 0; i--)
            {
                player.DelBuff(debuffIndexes[i]);
            }
        }
    }
}
