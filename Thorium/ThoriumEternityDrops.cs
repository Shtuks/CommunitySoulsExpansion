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
using ssm.Thorium.Emode.Accessories;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossBuriedChampion;

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
            }
            if (npc.type == ModContent.NPCType<GraniteEnergyStorm>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<GraniteMaterializer>(), 1));
            }
            if (npc.type == ModContent.NPCType<BuriedChampion>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<ChampionHeadband>(), 1));
            }
            if (npc.type == ModContent.NPCType<QueenJellyfish>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AquaticDepthsCrate>(), 5));
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<JellyfishCoil>(), 1));
            }
            if (npc.type == ModContent.NPCType<Viscount>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<ScarletCrate>(), 5));
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<VampiresBlessing>(), 1));
            }
            if (npc.type == ModContent.NPCType<Lich>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<WondrousCrate>(), 5));
            }
            if (npc.type == ModContent.NPCType<ForgottenOne>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AbyssalCrate>(), 5));
            }
            if (npc.type == ModContent.NPCType<StarScouter>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ItemID.FloatingIslandFishingCrate, 5));
            }
        }
    }
}
