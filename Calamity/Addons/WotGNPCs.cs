using CalamityMod.Events;
using Fargowiltas.NPCs;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using NoxusBoss.Content.Items;
using NoxusBoss.Content.Items.MiscOPTools;
using NoxusBoss.Content.NPCs.Bosses.Avatar.SecondPhaseForm;
using NoxusBoss.Content.NPCs.Bosses.Draedon;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;
using ssm.Content.Items.Accessories;
using ssm.Content.NPCs;
using ssm.Content.NPCs.RealMutantEX;

//using ssm.Content.NPCs.MutantEX;
using ssm.Core;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class WotGNPCs : GlobalNPC
    {
        public override void SetDefaults(NPC entity)
        {
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<LumberJack>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Squirrel>()] = true;

            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<MutantBoss>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<DeviBoss>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<AbomBoss>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Mutant>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Abominationn>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Deviantt>()] = true;

            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<RealMutantEX>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<MonocleCat>()] = true;
            //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Industrialist>()] = true;

            if (CSEConfig.Instance.AlternativeSiblings)
            {
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<MutantEX>()] = true;
                EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Monstrosity>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<AmalgamBoss>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Amalgamtionn>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<DivergenttBoss>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Divergentt>()] = true;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if(npc.type == ModContent.NPCType<MarsBody>() && BossRushEvent.BossRushActive)
            {
                modifiers.FinalDamage *= 3;
            }
        }
        public override void OnKill(NPC npc)
        {
            if (WorldSavingSystem.EternityMode)
            {
                if (npc.type == ModContent.NPCType<AvatarOfEmptiness>())
                {
                    npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<MetallicChunk>(), 15);
                }
                if (npc.type == ModContent.NPCType<NamelessDeityBoss>())
                {
                    npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<NDMaterialPlaceholder>(), 15);
                }
            }
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.type == ModContent.NPCType<AvatarOfEmptiness>())
            {
                LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new EModeDropCondition());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MetallicChunk>(), 1, 10, 20));
            }
            if (npc.type == ModContent.NPCType<NamelessDeityBoss>())
            {
                LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new EModeDropCondition());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NDMaterialPlaceholder>(), 1, 10, 20));
            }
        }
    }
}
