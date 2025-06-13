using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Content.NPCs.MutantEX;
using ssm.Content.Buffs;
using ssm.Core;
using FargowiltasSouls.Core.Systems;

namespace ssm
{
    public partial class ShtunHit : ModPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (Player.Shtun().equippedPhantasmalEnchantment)
            {
                modifiers.SetMaxDamage(1000);
            }
            if (Player.HasBuff<MutantDesperationBuff>() && NPC.AnyNPCs(ModContent.NPCType<MutantEX>()))
            {
                Player.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 10);
            }
            if (NPC.AnyNPCs(ModContent.NPCType<MutantEX>()))
            {
                Player.statLife -= Player.statLife / 5;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()))
            {
                Player.statLife -= Player.statLife / 10;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.Shtun().equippedPhantasmalEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(ModCompatibility.SoulsMod.Name, "MutantFangBuff").Type, 1000, false);
            if (Player.Shtun().equippedAbominableEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(ModCompatibility.SoulsMod.Name, "AbomFangBuff").Type, 1000, false);
        }
    }

    public partial class ShtunNPCHit : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(npc.type != ModContent.NPCType<MutantEX>() && Main.player[projectile.owner].Shtun().lumberjackSet)
            {
                npc.dontTakeDamage = false;
                npc.dontTakeDamageFromHostiles = false;
                npc.immortal = false;
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.checkDead();
            }

            if (Main.player[projectile.owner].Shtun().lumberjackSet)
            {
                npc.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 180);
            }

            if (npc.type == ModContent.NPCType<MutantBoss>() && !WorldSavingSystem.DownedMutant)
            {
                modifiers.SetMaxDamage(40000);
            }
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (npc.type != ModContent.NPCType<MutantEX>() && player.Shtun().lumberjackSet)
            {
                npc.dontTakeDamage = false;
                npc.dontTakeDamageFromHostiles = false;
                npc.immortal = false;
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.checkDead();
            }

            if (npc.type == ModContent.NPCType<MutantBoss>() && !WorldSavingSystem.DownedMutant)
            {
                modifiers.SetMaxDamage(40000);
            }
        }
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                hit.InstantKill = false;
            }
        }
    }
}
