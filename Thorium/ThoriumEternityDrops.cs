using FargowiltasSouls;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using ssm.Core;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Thorium;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossViscount;
using ssm.Thorium.EternityAccessories;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumEternityDrops : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        
        {
            LeadingConditionRule emodeRule = new(new EModeDropCondition());
            npcLoot.Add(emodeRule);

            if (npc.type == ModContent.NPCType<TheGrandThunderBird>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<StrangeCrate>(), 5));
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<ThunderTalonEternity>(), 1));
            }
            else if (npc.type == ModContent.NPCType<QueenJellyfish>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AquaticDepthsCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<Viscount>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<ScarletCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<Lich>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<WondrousCrate>(), 5));
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<DarkenedCloak>(), 1));
            }
            else if (npc.type == ModContent.NPCType<ForgottenOne>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AbyssalCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<StarScouter>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ItemID.FloatingIslandFishingCrate, 5));
            }
        }
    }
}
