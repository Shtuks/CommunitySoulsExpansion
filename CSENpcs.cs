using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.Globals;
using Luminance.Common.Utilities;
using FargowiltasSouls.Core.Systems;
using Fargowiltas.NPCs;
using Terraria.DataStructures;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Terraria.GameContent.ItemDropRules;
using ssm.Content.Items.Accessories;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ssm.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Projectiles;
using ssm.Content.NPCs.RealMutantEX;

namespace ssm
{
    public partial class CSENpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int genTimer = 0;
        public int mayo;

        public int chtuxlagorInferno;
        public static int ECH = -1;
        public static int boss = -1;
        public static int mutantEX = -1;
        public static int RealMutantEX = -1;

        public static float multiplierMD = 0;
        public static float multiplierML = 0;
        public static float multiplierAD = 0;
        public static float multiplierAL = 0;

        public bool SwarmActive;
        public bool SwarmHealth;
        private int go = 1;

        public bool dukeEX;
        public override void Load()
        {
            if (ModCompatibility.Thorium.Loaded) { multiplierML += 0.5f; multiplierMD += 1f; multiplierAL += 0.7f; multiplierAD += 2f; }
            if (ModCompatibility.Calamity.Loaded) { multiplierML += 3f; multiplierMD += 2.5f; multiplierAL += 5f; multiplierAD += 5f; }
            if (ModCompatibility.SacredTools.Loaded) { multiplierML += 1f; multiplierMD += 1.5f; multiplierAL += 0.5f; multiplierAD += 2f; }
            if (ModCompatibility.Homeward.Loaded) { multiplierML += 0.5f; multiplierMD += 1f; multiplierAL += 0.5f; multiplierAD += 1f; }
            if (ModCompatibility.Calamity.Loaded && ModCompatibility.SacredTools.Loaded) { multiplierML += 0.5f;}
            if (ModCompatibility.CatTech.Loaded) { multiplierML += 9f; multiplierMD += 10f; multiplierAL += 7f; multiplierAD += 7f; }

            if (CSEConfig.Instance.SecretBosses) { multiplierML += 0.5f;}
            if (CSEConfig.Instance.DebugMode && ModCompatibility.Calamity.Loaded) { multiplierML += 1.5f; }

            if (ModCompatibility.Inheritance.Loaded) { multiplierAL = 16f; multiplierAD = 20f; }
        }
        public override bool CheckDead(NPC npc)
        {
            if (!(npc.ai[0] <= 9))
            {
                if (EModeGlobalNPC.fishBossEX == npc.whoAmI)
                {
                    if (WorldSavingSystem.EternityMode)
                    {
                        npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<CyclonicFin>());
                    }
                    //if (ModCompatibility.Goozma.Loaded && ModCompatibility.Calamity.Loaded)
                    //{
                    //    if (!Main.LocalPlayer.GetModPlayer<CSEAuricSoulPlayer>().eternalSoul)
                    //    {
                    //        npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<EternalAuricSoul>());
                    //    }
                    //}
                    //if (ModCompatibility.Calamity.Loaded)
                    //{
                    //    npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<DukeEXLore>());
                    //}
                    int maxEX = Main.rand.Next(5) + 10;
                    for (int i = 0; i < maxEX; i++)
                        npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<EternalScale>());
                    int maxAbom = Main.rand.Next(50) + 100;
                    for (int i = 0; i < maxAbom; i++)
                        npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<AbomEnergy>());
                    int maxDevi = Main.rand.Next(100) + 200;
                    for (int i = 0; i < maxDevi; i++)
                        npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<DeviatingEnergy>());

                    return false;
                }
            }
            return base.CheckDead(npc);
        }
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            //if (npc.type == NPCID.DukeFishron && dukeEX && CSEConfig.Instance.AlternativeSiblings)
            //{
            //    target.AddBuff(ModContent.BuffType<MonstrousMaul>(), 180);
            //}
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.type == NPCID.DukeFishron && EModeGlobalNPC.spawnFishronEX)
            {
                dukeEX = true;
                EModeGlobalNPC.spawnFishronEX = false;
            }
            if (npc.type == ModContent.NPCType<MutantBoss>() && !Main.zenithWorld && CSEConfig.Instance.MutantWontShutUp)
            {
                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<MutantYap>(), 0, 0, -1, npc.whoAmI);
            }
            if (npc.type == ModContent.NPCType<RealMutantEX>())
            {
                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<MutantYap>(), 0, 0, -1, npc.whoAmI);
            }
        }
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>() || npc.type == ModContent.NPCType<RealMutantEX>())
            {
                npc.defense = 300;

                npc.damage = Main.getGoodWorld ? 2000 : (int)((500 + (100 * Math.Round(multiplierMD, 1))) * (WorldSavingSystem.AngryMutant ? WorldSavingSystem.MasochistModeReal ? 15 : 10 : WorldSavingSystem.MasochistModeReal ? 1.5f : 1));
                npc.lifeMax = (int)((10000000 + ((10000000 * Math.Round(multiplierML, 1))) / (Main.expertMode ? 1 : 2)) * (WorldSavingSystem.AngryMutant ? WorldSavingSystem.MasochistModeReal ? 15 : 10 : WorldSavingSystem.MasochistModeReal ? 1.5f : 1));

                if (ModCompatibility.Inheritance.Loaded && !Main.zenithWorld && !Main.getGoodWorld)
                {
                    npc.damage = 3000;
                    npc.lifeMax = 440000000;
                }
            }

            if (npc.type == NPCID.DukeFishron && (EModeGlobalNPC.spawnFishronEX || dukeEX))
            {
                npc.defense = 0;
                npc.defDefense = 0;

                npc.damage = (Main.getGoodWorld ? 2000 : (int)((500 + (100 * Math.Round(multiplierMD, 1))) * (WorldSavingSystem.MasochistModeReal ? 1.5f : 1))) / 2;
                npc.lifeMax = ((int)((10000000 + ((10000000 * Math.Round(multiplierML, 1))) / (Main.expertMode ? 1 : 2)) * (WorldSavingSystem.MasochistModeReal ? 1.5f : 1))) / 2;

                if (ModCompatibility.Inheritance.Loaded && !Main.zenithWorld && !Main.getGoodWorld)
                {
                    npc.damage = 2000;
                    npc.lifeMax = 300000000;
                }
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (10 * multiplierAD));
                npc.lifeMax = (int)(2800000 + (1000000 * multiplierAL)) / (Main.expertMode ? 2 : 4);
            }
        }
        public override void SetStaticDefaults()
        {
            //NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantBoss>()] = true;
            //NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantEX>()] = true;
            //NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<DeviBoss>()] = true;
            //NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<AbomBoss>()] = true;

            //if (EModeGlobalNPC.spawnFishronEX || dukeEX)
            //{
            //    NPCID.Sets.ImmuneToRegularBuffs[NPCID.DukeFishron] = true;
            //}
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (chtuxlagorInferno > 0)
                ApplyDPSDebuff(npc.lifeMax / 10, npc.lifeMax / 100, ref npc.lifeRegen, ref damage);
        }

        //    float LRM = Utilities.Saturate((float)npc.life / (float)npc.lifeMax);
        //    float maxTimeNormal = 12000; // 4 min
        //    float maxTimeMaso = 18000; // 4.5 min
        //    float intendedDuration = WorldSavingSystem.MasochistModeReal ? maxTimeMaso : maxTimeNormal;

        //    // 0 = as intended, 1 = instakill
        //    float fightProgress = Utilities.InverseLerp(0f, intendedDuration, genTimer);
        //    float aheadOfSchedule = MathF.Max(0f, 1f - fightProgress - LRM);

        //    float resistanceFactor = (float)Math.Pow(aheadOfSchedule, 0.1f); // lower value - sharper applying

        //    if (aheadOfSchedule > 0.8f)
        //    {
        //        modifiers.FinalDamage *= 0.01f;
        //    }
        //    else
        //    {
        //        float damageMultiplier = 1f - resistanceFactor;
        //        modifiers.FinalDamage *= damageMultiplier;
        //    }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            //if (npc.type == ModContent.NPCType<MutantBoss>() && Main.npc[EModeGlobalNPC.mutantBoss].ai[0] > 10)
            //{
            //    float desiredLifeRatio = 1f - LumUtils.InverseLerp(0f, 4 * 60 * 60, genTimer);
            //    float aheadLifeRatioInterpolant = Utilities.Saturate((desiredLifeRatio - Utilities.Saturate(npc.life / npc.lifeMax)) * 2f);

            //    float damageReductionInterpolant = (float)Math.Pow(aheadLifeRatioInterpolant, 1f / 2.5f);

            //    float damageReductionFactor = MathHelper.Lerp(1f, 1f - 0.9f, damageReductionInterpolant);
            //    modifiers.FinalDamage *= damageReductionFactor;
            //}
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule emodeRule = new(new EModeDropCondition());
            npcLoot.Add(emodeRule);

            //if (npc.type == NPCID.DukeFishron && WorldSavingSystem.DownedAbom)
            //{
            //    emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<CyclonicFin>(), 1));
            //}
        }
        public override void PostAI(NPC npc)
        {
            if (chtuxlagorInferno > 0)
            {
                chtuxlagorInferno--;
            }
            if (go == 2)
            {
                go = 1;
            }
        }

        public override void AI(NPC npc)
        {
            if (mayo == 20)
            {
                if (npc.type == NPCID.DukeFishron && (EModeGlobalNPC.spawnFishronEX || dukeEX))
                {
                    npc.defense = 0;
                    npc.defDefense = 0;

                    npc.damage = (Main.getGoodWorld ? 2000 : (int)((500 + (100 * Math.Round(multiplierMD, 1))) * (WorldSavingSystem.MasochistModeReal ? 1.5f : 1))) / 2;
                    npc.lifeMax = ((int)((10000000 + ((10000000 * Math.Round(multiplierML, 1))) / (Main.expertMode ? 1 : 2)) * (WorldSavingSystem.MasochistModeReal ? 1.5f : 1))) / 2;

                    if (ModCompatibility.Inheritance.Loaded && !Main.zenithWorld && !Main.getGoodWorld)
                    {
                        npc.damage = 2000;
                        npc.lifeMax = 300000000;
                    }
                }

                //here because maso breaks scaling for some reason
                if (npc.type == ModContent.NPCType<MutantBoss>() || npc.type == ModContent.NPCType<RealMutantEX>())
                {
                    npc.defense = 300;

                    npc.damage = Main.getGoodWorld ? 2000 : (int)((500 + (100 * Math.Round(multiplierMD, 1))) * (WorldSavingSystem.AngryMutant ? WorldSavingSystem.MasochistModeReal ? 15 : 10 : WorldSavingSystem.MasochistModeReal ? 1.5f : 1));
                    npc.lifeMax = (int)((10000000 + ((10000000 * Math.Round(multiplierML, 1))) / (Main.expertMode ? 1 : 2)) * (WorldSavingSystem.AngryMutant ? WorldSavingSystem.MasochistModeReal ? 15 : 10 : WorldSavingSystem.MasochistModeReal ? 1.5f : 1));

                    if (ModCompatibility.Inheritance.Loaded && !Main.zenithWorld && !Main.getGoodWorld)
                    {
                        npc.damage = 3000;
                        npc.lifeMax = 440000000;
                    }
                }

                if (npc.type == ModContent.NPCType<Mutant>())
                {
                    npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierML, 1))) / (Main.expertMode ? 1 : 2) / 10;
                }
                if (npc.type == ModContent.NPCType<Abominationn>())
                {
                    npc.lifeMax = (int)(1800000 + (1000000 * multiplierAL)) / 10;
                }

                npc.life = npc.lifeMax;
            }
            mayo++;

            if (npc.type == ModContent.NPCType<MutantBoss>() && Main.npc[EModeGlobalNPC.mutantBoss].ai[0] > 10)
            {
                genTimer++;
            }
            base.AI(npc);
        }
        public void ApplyDPSDebuff(int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
        {
            if (lifeRegen > 0)
            {
                lifeRegen = 0;
            }

            lifeRegen -= lifeRegenValue;
            if (damage < damageValue)
            {
                damage = damageValue;
            }
        }
        public override bool PreAI(NPC npc)
        {
            if (ssm.SwarmNoHyperActive)
            {
                return true;
            }

            if (ssm.EndgameSwarmActive && Main.GameUpdateCount % 5 == 0)
            {
                return true;
            }

            if (ssm.PostMLSwarmActive && Main.GameUpdateCount % 4 == 0)
            {
                return true;
            }

            if (ssm.LateHardmodeSwarmActive && Main.GameUpdateCount % 3 == 0)
            {
                return true;
            }

            if (ssm.HardmodeSwarmActive && Main.GameUpdateCount % 2 == 0)
            {
                return true;
            }

            if (ssm.SwarmActive && !npc.townNPC && npc.lifeMax > 1 && go < 2)
            {
                go++;
                npc.AI();
                float num = 0.5f;
                Vector2 position = npc.position + npc.velocity * num;
                if (!Collision.SolidCollision(position, npc.width, npc.height))
                {
                    npc.position = position;
                }
            }

            return true;
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.type == NPCID.DukeFishron && (EModeGlobalNPC.spawnFishronEX || dukeEX))
            {
                Texture2D texture = TextureAssets.Npc[npc.type].Value;
                Rectangle frame = npc.frame;
                Vector2 origin = frame.Size() * 0.5f;
                Vector2 position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);

                Color outlineColor = Color.Teal;

                float outlineOffset = 2f;

                for (int i = 0; i < 8; i++)
                {
                    Vector2 offset = Vector2.UnitX.RotatedBy(MathHelper.PiOver4 * i) * outlineOffset;
                    Main.spriteBatch.Draw(
                        texture,
                        position + offset,
                        frame,
                        outlineColor,
                        npc.rotation,
                        origin,
                        npc.scale,
                        npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                        0f
                    );
                }
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}
