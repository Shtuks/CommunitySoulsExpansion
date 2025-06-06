using CalamityMod;
using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using MonoMod.Utils;
using SacredTools.Content.Projectiles.Armors.Nuba;
using ssm.Content.Buffs;
using ssm.Content.Projectiles.Enchantments;
using ssm.Core;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.NebulousApprenticeEnchant;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAPlayer : ModPlayer
    {
        //Bismuth Enchant
        public int bismuthEnchant; 
        public int bismuthCrystalStage = 0;
        public int bismuthFormationTimer = 0;
        public const int FormationTime = 300;
        public const int MaxStages = 3;
        public const int MaxDamageAbsorption = 90;
        public int currentDamageAbsorbed = 0;

        //Frosthunter Enchantment
        public int frosthunterEnchant;
        public int frosthunterCooldown = 0;

        //Lapis Enchant
        public int lapisSpeedTimer;
        public int lapisEnchant;

        //Blightbone Enchant
        public int blightboneEnchant;

        //Eerie Enchant
        public int eerieEnchant;

        //Eival Enchant
        public int rivalEnchant = 0;
        public int rivalKillCount = 0;
        public int rivalTimer = 0;

        //Space Junk Enchant
        public int spaceJunkEnchant = 0;

        //Fallen Prince Enchant
        public int fallenPrinceEnchant = 0;

        //Vulcan Reaper Enchant
        public int vulcanReaperEnchant;
        public int vulcanStacks;
        public int vulcanTime;

        //Flarium Enchant
        public int flariumEnchant;
        public override void ResetEffects()
        {
            vulcanReaperEnchant = 0;
            fallenPrinceEnchant = 0;
            spaceJunkEnchant = 0;
            rivalEnchant = 0;
            eerieEnchant = 0;
            blightboneEnchant = 0;
            lapisEnchant = 0;
            frosthunterEnchant = 0;
            bismuthEnchant = 0;
        }
        public override void UpdateDead()
        {
            fallenPrinceEnchant = 0;
            rivalTimer = 0;
            rivalKillCount = 0;
            frosthunterCooldown = 0;
            rivalKillCount = 0;
            currentDamageAbsorbed = 0;
            bismuthCrystalStage = 0;
        }
        public override void PostUpdateEquips()
        {
            if (rivalKillCount > 0)
            {
                rivalTimer++;
            }

            if (rivalTimer >= 300)
            {
                rivalKillCount--;
                rivalTimer=0;
            }

            if (vulcanStacks > 0)
            {
                vulcanTime++;
            }

            if (vulcanTime >= 300)
            {
                vulcanStacks--;
                vulcanTime = 0;
            }

            if (frosthunterCooldown > 0)
            {
                frosthunterCooldown--;
            }

            if (lapisSpeedTimer > 0)
            {
                lapisSpeedTimer--;
                if (lapisEnchant > 0)
                {
                    Player.moveSpeed += 0.25f;
                }
            }

            if (bismuthEnchant > 0)
            {
                bismuthFormationTimer++;

                if (bismuthFormationTimer >= FormationTime / MaxStages * (bismuthCrystalStage + 1) && bismuthCrystalStage < MaxStages)
                {
                    bismuthCrystalStage++;
                    currentDamageAbsorbed = 0;
                }
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            CreateShrapnel();
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            CreateShrapnel();
        }

        public void CreateShrapnel()
        {
            if (spaceJunkEnchant > 0 && Main.rand.NextFloat() < 0.33f)
            {
                float spread = 40f * 0.0174f;
                double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
                double deltaAngle = spread / 4f;
                double offsetAngle;

                if (Player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int dmg = (int)Player.GetDamage<GenericDamageClass>().ApplyTo(40);
                        offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                        int shard = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<SatelliteShard>(), dmg, 1, Player.whoAmI, 0f, 0f);
                        if (shard.WithinBounds(Main.maxProjectiles))
                        {
                            Main.projectile[shard].DamageType = DamageClass.Generic;
                        }
                    }
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (rivalEnchant > 0 && rivalKillCount > 0)
            {
                Player.AddBuff(ModContent.BuffType<RivalBuff>(), 60);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasEffect<NebulousApprenticeEffect>() && !target.immortal && Main.rand.NextBool(10))
            {
                float num2 = (float)Main.rand.Next(-35, 36) * 0.02f;
                float num3 = (float)Main.rand.Next(-35, 36) * 0.02f;
                num2 *= 10f;
                num3 *= 10f;
                int[] array0 = new int[3]
                {
                ModContent.ProjectileType<NubaFlameDamage>(),
                ModContent.ProjectileType<NubaFlameDefense>(),
                ModContent.ProjectileType<NubaFlameHealth>(),
                };
                int[] array = new int[5]
                {
                ModContent.ProjectileType<NubaFlameDamage>(),
                ModContent.ProjectileType<NubaFlameDefense>(),
                ModContent.ProjectileType<NubaFlameHealth>(),
                ModContent.ProjectileType<NubaFlameMana>(),
                ModContent.ProjectileType<NubaFlameSpeed>()
                };
                Projectile.NewProjectile(target.GetSource_OnHurt(base.Player), target.Center.X, target.Center.Y, num2, num3, Player.ForceEffect<NebulousApprenticeEffect>() ? array[Main.rand.Next(5)] : array0[Main.rand.Next(3)], 0, 0f, proj.owner);
            }
            if (rivalEnchant > 0 && rivalKillCount > 0)
            {
                hit.Damage = (int)(hit.Damage * (1f + 0.2f * rivalKillCount));
            }
            if (frosthunterEnchant > 0) 
            {
                bool isCluster = frosthunterCooldown <= 0;

                CreateFrostExplosion(target.Center, isCluster, proj);

                if (isCluster)
                {
                    frosthunterCooldown = 120;
                }
            }
            if (blightboneEnchant == 1 && IsBoneWeapon(proj))
            {
                target.AddBuff(BuffID.OnFire, 180);
            }

            if (blightboneEnchant == 2 && IsBoneWeapon(proj))
            {
                target.AddBuff(BuffID.OnFire, 360);
            }
        }

        private bool IsBoneWeapon(Projectile proj)
        {
            //...
            return proj.type == ProjectileID.Bone ||
                   proj.type == ProjectileID.BoneJavelin ||
                   proj.type == ProjectileID.BoneGloveProj;
        }

        public void CreateFrostExplosion(Vector2 pos, bool isCluster, Projectile proj)
        {
            float radius = isCluster ? 100f : 150f;
            int damage = (int)(Player.GetDamage(DamageClass.Generic).ApplyTo(15));
            float knockback = 3f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Distance(pos) <= radius)
                {
                    Player.ApplyDamageToNPC(npc, damage, knockback, Player.direction);
                    npc.AddBuff(frosthunterEnchant > 1 ? BuffID.Frostburn2 : BuffID.Frostburn, 180);
                }
            }

            Projectile.NewProjectile(proj.GetSource_FromThis(), pos, Vector2.Zero, ModContent.ProjectileType<FrosthunterExplosion>(), 0, 0);
            
            //for (int i = 0; i < 15; i++)
            //{
            //    Dust sust = Dust.NewDustPerfect(pos, DustID.Ice, Main.rand.NextVector2Circular(5, 5) * 3f);
            //    sust.noGravity = true;
            //    sust.scale = 1.5f;
            //}

            if (isCluster)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 clusterPos = pos + Main.rand.NextVector2Circular(radius * 0.5f, radius * 0.5f);
                    CreateSmallFrostExplosion(pos, proj);
                }
            }
        }
        public void CreateSmallFrostExplosion(Vector2 pos, Projectile proj)
        {
            float radius = 60f;
            int damage = (int)(Player.GetDamage(DamageClass.Generic).ApplyTo(10));
            float knockback = 1.5f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Distance(pos) <= radius)
                {
                    Player.ApplyDamageToNPC(npc, damage, knockback, Player.direction);
                    npc.AddBuff(frosthunterEnchant > 1 ? BuffID.Frostburn2 : BuffID.Frostburn, 180);
                }
            }

            Projectile.NewProjectile(proj.GetSource_FromThis(), pos, Vector2.Zero, ModContent.ProjectileType<FrosthunterExplosion>(), 0, 0);

            //for (int i = 0; i < 10; i++)
            //{
            //    Dust sust = Dust.NewDustPerfect(pos, DustID.Ice, Main.rand.NextVector2Circular(5, 5) * 3f);
            //    sust.noGravity = true;
            //    sust.scale = 1.5f;
            //}
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (lapisEnchant > 1 && !Player.dead)
            {
                lapisSpeedTimer = 120;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            if (rivalEnchant > 0 && target.life <= 0 && !target.friendly && target.type != NPCID.TargetDummy)
            {
                if (rivalKillCount < 5)
                {
                    rivalKillCount++;
                }
            }
            if (vulcanReaperEnchant > 0 && target.life <= 0 && !target.friendly && target.type != NPCID.TargetDummy)
            {
                vulcanStacks++;
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (bismuthEnchant > 0)
            {
                if (bismuthCrystalStage > 0 && currentDamageAbsorbed < MaxDamageAbsorption)
                {
                    float damageReduction = 0f;

                    if (ShtunUtils.AnyBossAlive())
                    {
                        damageReduction = bismuthEnchant > 1 ? 0.05f : 0.02f;
                    }
                    else
                    {
                        damageReduction = bismuthEnchant > 1 ? 0.1f : 0.05f;
                    }

                    float damageToAbsorb = modifiers.FinalDamage.Flat * damageReduction;

                    if (currentDamageAbsorbed + damageToAbsorb > MaxDamageAbsorption)
                    {
                        damageToAbsorb = MaxDamageAbsorption - currentDamageAbsorbed;
                    }

                    if (damageToAbsorb > 0)
                    {
                        modifiers.FinalDamage -= damageToAbsorb;
                        currentDamageAbsorbed += (int)damageToAbsorb;
                    }
                }
            }
        }
    }
}
