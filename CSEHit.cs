 using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
//using ssm.Content.NPCs.MutantEX;
using ssm.Content.Buffs;
using ssm.Core;
using FargowiltasSouls.Core.Systems;
using Terraria.ID;
using ssm.Content.NPCs.RealMutantEX;

namespace ssm
{
    public partial class CSEHit : ModPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (Player.CSE().equippedPhantasmalEnchantment)
            {
                modifiers.SetMaxDamage(1000);
            }
            //if (CSEConfig.Instance.AlternativeSiblings)
            //{
            //    if (Player.HasBuff<MutantDesperationBuff>() && NPC.AnyNPCs(ModContent.NPCType<MutantEX>()))
            //    {
            //        Player.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 10);
            //    }
            //    if (NPC.AnyNPCs(ModContent.NPCType<MutantEX>()))
            //    {
            //        Player.AddBuff(ModContent.BuffType<MonstrousMaul>(), 180);
            //    }
            //    if (NPC.AnyNPCs(ModContent.NPCType<MutantEX>()) && WorldSavingSystem.MasochistModeReal)
            //    {
            //        Player.CSE().monstrosityHits++;
            //    }
            //}
            if (NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()))
            {
                Player.statLife -= Player.statLife / 5;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<RealMutantEX>()))
            {
                Player.CSE().mutantEXHits++;

                //instakill
                if (Player.name.ToLower().Contains("pucheglazik"))
                {
                    Player.CSE().mutantEXHits+=100;
                }
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (Player.HasBuff<MutantDesperationBuff>() && (proj.type == ModContent.ProjectileType<MutantGiantDeathray2>() || proj.type == ModContent.ProjectileType<MutantBomb>()))
            {
                Player.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 180);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.CSE().equippedPhantasmalEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(ModCompatibility.SoulsMod.Name, "MutantFangBuff").Type, 1000, false);
            if (Player.CSE().equippedAbominableEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(ModCompatibility.SoulsMod.Name, "AbomFangBuff").Type, 1000, false);
        }
    }

    public partial class CSENPCHit : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {

            //if (npc.type != ModContent.NPCType<MutantEX>() && Main.player[projectile.owner].CSE().lumberjackSet)
            //{
            //    npc.dontTakeDamage = false;
            //    npc.dontTakeDamageFromHostiles = false;
            //    npc.immortal = false;
            //    npc.life = 0;
            //    npc.HitEffect(0, 10.0);
            //    npc.checkDead();
            //}

            if (Main.player[projectile.owner].CSE().lumberjackSet)
            {
                npc.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 180);
            }

            if (npc.type == ModContent.NPCType<MutantBoss>() && !ModCompatibility.Inheritance.Loaded)
            {
                //modifiers.SetMaxDamage(50000);
            }

            //if(NPC.AnyNPCs(ModContent.NPCType<MutantEX>()) || NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()))
            //{
            //    if(npc.type != ModContent.NPCType<MutantEX>() && npc.type != ModContent.NPCType<MutantBoss>() && npc.type != NPCID.DukeFishron)
            //    {
            //        modifiers.FinalDamage *= 10;
            //    }
            //}
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
        //    if (npc.type != ModContent.NPCType<MutantEX>() && player.CSE().lumberjackSet)
        //    {
        //        npc.dontTakeDamage = false;
        //        npc.dontTakeDamageFromHostiles = false;
        //        npc.immortal = false;
        //        npc.life = 0;
        //        npc.HitEffect(0, 10.0);
        //        npc.checkDead();
        //    }

            //if (npc.type == ModContent.NPCType<MutantBoss>())
            //{
            //    modifiers.SetMaxDamage(50000);
            //}
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
