using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Systems;
using ssm.Content.Buffs;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public partial class SoANpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnKill(NPC npc)
        {
            if(npc.type == ModContent.NPCType<Nihilus>() && !WorldSaveSystem.downedNihilus)
            {
                WorldSaveSystem.downedNihilus = true;
            }

            if (npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanReaperEnchant > 0 && !npc.friendly && npc.lifeMax > 5 && !npc.SpawnedFromStatue)
            {
                if (ShtunUtils.AnyBossAlive())
                {
                    if (npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanReaperEnchant == 1)
                    {
                        npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanKillCount++;
                        npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanDamageBonus = 0.05f;
                        npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanBuffTimer = 120;
                    }
                    else if (npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanReaperEnchant > 1)
                    {
                        npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanKillCount++;
                        if (npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanKillCount >= 3)
                        {
                            npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanKillCount = 0;
                            npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanDamageBonus = 0.10f;
                            npc.lastInteraction.ToPlayer().GetModPlayer<SoAPlayer>().vulkanBuffTimer = 180;
                        }
                    }
                }
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            projectile.owner.ToPlayer().GetModPlayer<SoAPlayer>().ModifyDamageWithVulkan(ref modifiers.FinalDamage.Base);
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            player.GetModPlayer<SoAPlayer>().ModifyDamageWithVulkan(ref modifiers.FinalDamage.Base);
        }
        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Nihilus>() || npc.type == ModContent.NPCType<Nihilus2>())
            {
                if (Main.expertMode && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<NihilityPresenceBuff>(), 2);
            }
            return base.PreAI(npc);
        }
    }
}
