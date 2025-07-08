//using Fargowiltas.NPCs;
//using FargowiltasSouls;
//using FargowiltasSouls.Content.Bosses.AbomBoss;
//using FargowiltasSouls.Content.Bosses.DeviBoss;
//using FargowiltasSouls.Content.Bosses.MutantBoss;
//using FargowiltasSouls.Core.Globals;
//using NoxusBoss.Content.Projectiles.Typeless;
//using ssm.Content.NPCs;
//using ssm.Content.NPCs.MutantEX;
//using ssm.Core;
//using Terraria;
//using Terraria.ModLoader;

//namespace ssm.Calamity
//{
//    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
//    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
//    public class WotGProj : GlobalProjectile
//    {
//        public override bool InstancePerEntity => true;
//        public override bool PreAI(Projectile projectile)
//        {
//            NPC targetMutant = null;
//            NPC targetMutantNPC = null;
//            NPC targetAbom = null;
//            NPC targetAbomNPC = null;
//            NPC targetDevi = null;
//            NPC targetDeviNPC = null;
//            NPC targetMonster = null;
//            NPC targetMonsterNPC = null;

//            for (int i = 0; i < Main.maxNPCs; i++)
//            {
//                NPC npc = Main.npc[i];
//                if (npc.active && npc.type == ModContent.NPCType<MutantBoss>())
//                {
//                    targetMutant = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<Mutant>())
//                {
//                    targetMutantNPC = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<AbomBoss>())
//                {
//                    targetAbom = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<Abominationn>())
//                {
//                    targetAbomNPC = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<DeviBoss>())
//                {
//                    targetDevi = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<Deviantt>())
//                {
//                    targetDeviNPC = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<MutantEX>())
//                {
//                    targetDevi = npc;
//                    break;
//                }
//                if (npc.active && npc.type == ModContent.NPCType<Monstrosity>())
//                {
//                    targetDeviNPC = npc;
//                    break;
//                }
//            }


//            if (projectile.type == ModContent.ProjectileType<EmptinessSprayerGas>() && (ShtunUtils.ProjGetDistanceToNPC(projectile, targetMutantNPC) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetMutant) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetAbom) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetAbomNPC) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetDeviNPC) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetDevi) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetMonster) < 200 ||
//                                                                                        ShtunUtils.ProjGetDistanceToNPC(projectile, targetMonsterNPC) < 200))
//            {
//                projectile.Kill();
//            }
//            if (projectile.type == ModContent.ProjectileType<EmptinessSprayerGas>() && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()) ||
//                                                                                       FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantEX>()) ||
//                                                                                       FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<AbomBoss>()) ||
//                                                                                       FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<DeviBoss>()))
//            {
//                projectile.Kill();
//            }
//            return base.PreAI(projectile);
//        }
//    }
//}
