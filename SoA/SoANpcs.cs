using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Systems;
using ssm.Content.Buffs;
using FargowiltasSouls.Core.Systems;
using SacredTools.NPCs.Boss.Lunarians;
using ThoriumMod;
using ssm.Content.NPCs;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using Terraria.DataStructures;
using System.IO;
using Terraria.ModLoader.IO;
using SacredTools.Content.NPCs.Boss.Decree;
using static Terraria.ModLoader.ModContent;
using CalamityHunt.Common.Players;
using SacredTools.Content.NPCs.Boss.Jensen;
using SacredTools.NPCs.Boss.Pumpkin;
using SacredTools.NPCs.Boss.Jensen;
using SacredTools.NPCs.Boss.Araneas;
using SacredTools.NPCs.Boss.Raynare;
using SacredTools.NPCs.Boss.Primordia;
using SacredTools.NPCs.Boss.Abaddon;
using SacredTools.NPCs.Boss.Araghur;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public partial class SoANpcs : GlobalNPC
    {
        public bool summonedShieldOnce = false;
        public override bool InstancePerEntity => true;

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter writer)
        {
            writer.Write(summonedShieldOnce);
        }
        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader reader)
        {
            summonedShieldOnce = reader.ReadBoolean();
        }

        public override void SetDefaults(NPC npc)
        {
            int num1 = 30000;
            int num2 = 300000;
            int num3 = 30000000;

            int num12 = 50;
            int num22 = 100;
            int num32 = 200;

            if (ssm.SwarmSetDefaults)
            {
                if (npc.type == NPCType<DecreeLegacy>() || npc.type == NPCType<JensenLegacy>() || npc.type == NPCType<Ralnek>() || npc.type == NPCType<Ralnek2>())
                {
                    npc.lifeMax = num1 * ssm.SwarmItemsUsed;
                    npc.damage = num12 * ssm.SwarmItemsUsed;
                }
                if (npc.type == NPCType<Araneas>() || npc.type == NPCType<Raynare>() || npc.type == NPCType<Primordia>() || npc.type == NPCType<Primordia2>())
                {
                    npc.lifeMax = num2 * ssm.SwarmItemsUsed;
                    npc.damage = num22 * ssm.SwarmItemsUsed;
                }
                if (npc.type == NPCType<Abaddon>() || npc.type == NPCType<AraghurHead>() || npc.type == NPCType<Novaniel>() || npc.type == NPCType<SacredTools.NPCs.Boss.Erazor.ErazorBoss>())
                {
                    npc.lifeMax = num3 * ssm.SwarmItemsUsed;
                    npc.damage = num32 * ssm.SwarmItemsUsed;
                }
            }
        }
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
        public override void AI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                npc.dontTakeDamage = NPC.CountNPCS(ModContent.NPCType<MutantAuraOfSupression>()) > 0;
                if (!summonedShieldOnce)
                {
                    summonedShieldOnce = true;
                    IEntitySource source = npc.GetSource_FromAI();
                    int shield = NPC.NewNPC(source, (int)npc.Center.X, (int)npc.position.Y + npc.height, ModContent.NPCType<MutantAuraOfSupression>());
                    Main.npc[shield].ai[0] = npc.whoAmI;
                }
            }
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
