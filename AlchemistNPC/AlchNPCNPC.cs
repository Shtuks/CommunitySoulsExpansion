using AlchemistNPCLite.NPCs;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Core.Systems;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.AlchemistNPC
{
    [ExtendsFromMod(ModCompatibility.AlchNPCs.Name)]
    [JITWhenModsEnabled(ModCompatibility.AlchNPCs.Name)]
    public class AlchNPCNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == ModContent.NPCType<Operator>() && shopName == "ModBags2")
            {
                int slot = 0;

                if (WorldSavingSystem.DownedDevi)
                {
                    CSEUtils.AddToNextEmptySlot(ref slot, items, ModContent.ItemType<DeviBag>(), 200000);
                }
                if (WorldSavingSystem.DownedAbom)
                {
                    CSEUtils.AddToNextEmptySlot(ref slot, items, ModContent.ItemType<AbomBag>(), 150000000);
                }
                if (WorldSavingSystem.DownedMutant)
                {
                    CSEUtils.AddToNextEmptySlot(ref slot, items, ModContent.ItemType<MutantBag>(), 500000000);
                }
            }
        }
    }
}
