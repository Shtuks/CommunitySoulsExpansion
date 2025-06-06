using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Systems;
using ssm.Content.Buffs;
using FargowiltasSouls.Core.Systems;
using SacredTools.NPCs.Boss.Lunarians;
using ThoriumMod;

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

    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class LostSiblingsClassImunity : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModCompatibility.Thorium.Loaded;
        }
        public override bool InstancePerEntity => true;

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (WorldSavingSystem.EternityMode)
            {
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    if (hit.DamageType != DamageClass.Magic)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    if (hit.DamageType != DamageClass.Melee)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    if (hit.DamageType != DamageClass.Throwing)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    if (hit.DamageType != DamageClass.Ranged)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    if (hit.DamageType != DamageClass.Summon)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class LostSiblingsClassImunityThorium : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (WorldSavingSystem.EternityMode)
            {
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    if (hit.DamageType != DamageClass.Magic || hit.DamageType != HealerDamage.Instance)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    if (hit.DamageType != DamageClass.Melee)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    if (hit.DamageType != DamageClass.Throwing)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    if (hit.DamageType != DamageClass.Ranged || hit.DamageType != BardDamage.Instance)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    if (hit.DamageType != DamageClass.Summon)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
            }
        }
    }
}
