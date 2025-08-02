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
using ssm.Content.NPCs.MutantEX;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using Terraria.DataStructures;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls;
using Terraria.GameContent.ItemDropRules;
using ssm.Content.Items.Accessories;
using ssm.Content.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ssm.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.NPCs;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Spooky.Core;

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

        public static float multiplierM = 0;
        public static float multiplierA = 0;

        public bool SwarmActive;
        public bool SwarmHealth;
        private int go = 1;

        public bool dukeEX;
        public override void Load()
        {
            if (ModCompatibility.Homeward.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplierM += 0.8f; }

            if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierM += 0.8f; }

            if (ModCompatibility.Thorium.Loaded) { multiplierM += 0.6f; multiplierA += 0.7f; }
            if (ModCompatibility.Calamity.Loaded) { multiplierM += CSEConfig.Instance.DebugMode ? 8.8f : 2.8f; multiplierA += 6f; }
            if (ModCompatibility.SacredTools.Loaded) { multiplierM += 1.6f; multiplierA += 1f; }
            if (ModCompatibility.Inheritance.Loaded) { multiplierA = 16f; }
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
            if (npc.type == NPCID.DukeFishron && (EModeGlobalNPC.spawnFishronEX || dukeEX))
            {
                target.AddBuff(ModContent.BuffType<MonstrousMaul>(), 180);
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.type == NPCID.DukeFishron && EModeGlobalNPC.spawnFishronEX)
            {
                dukeEX = true;
                EModeGlobalNPC.spawnFishronEX = false;
            }
        }
        public override void SetDefaults(NPC npc)
        {
            //devi max hp - 40 k
            //divergentt max hp - 500 k
            //abom max hp - 10.8 mil 14.4 in total
            //amalgamationn max hp - 40 mil 53 in total
            ////duke ex - 30 mil 60 in total
            //mutant max hp - 60 mil 80 in total
            //monstrosity max hp - 800 mil

            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                npc.defense = 300;

                //cal omly
                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 750;
                    npc.lifeMax = CSEConfig.Instance.SecretBosses ? 40000000 : 35000000;
                }

                //soa only
                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 700;
                    npc.lifeMax = CSEConfig.Instance.SecretBosses ? 35000000 : 30000000;
                }

                //thorium only
                if (ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 600;
                    npc.lifeMax = CSEConfig.Instance.SecretBosses ? 20000000 : 15000000;
                }

                //thorium and soa
                if (ModCompatibility.Thorium.Loaded && ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 750;
                    npc.lifeMax = CSEConfig.Instance.SecretBosses ? 40000000 : 35000000;
                }

                //thorium and cal
                if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 800;
                    npc.lifeMax = CSEConfig.Instance.SecretBosses ? 4500000 : 40000000;
                }

                //soa and cal
                if (ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 900;
                    npc.lifeMax = 50000000;
                }

                //all mods
                if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && ModCompatibility.SacredTools.Loaded && !ModCompatibility.Inheritance.Loaded)
                {
                    npc.damage = 1000;
                    npc.lifeMax = 60000000;
                }

                //no mods (idk why are you even playing this)
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 500;
                    npc.lifeMax = CSEConfig.Instance.SecretBosses ? 15000000 : 10000000;
                }

                //funnies
                if (ModCompatibility.Inheritance.Loaded && !Main.zenithWorld && !Main.getGoodWorld)
                {
                    npc.damage = 3000;
                    npc.lifeMax = 440000000;
                }

                //npc.damage = Main.getGoodWorld ? 2000 : (int)(500 + ((ModCompatibility.Calamity.Loaded && CSEConfig.Instance.DebugMode ? 90 : 100) * (Math.Round(multiplierM, 1))));
                //npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1))) / (Main.expertMode ? 1 : 2);
            }

            if (npc.type == NPCID.DukeFishron && (EModeGlobalNPC.spawnFishronEX || dukeEX))
            {
                npc.defense = 0;
                npc.defDefense = 0;

                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 200;
                    npc.lifeMax = 20000000;
                }
                else if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 200;
                    npc.lifeMax = 15000000;
                }
                else if (ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
                {
                    npc.damage = 200;
                    npc.lifeMax = 10000000;
                }
                else if (ModCompatibility.Thorium.Loaded && ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
                {
                    npc.damage = 300;
                    npc.lifeMax = 20000000;
                }
                else if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    npc.damage = 300;
                    npc.lifeMax = 22500000;
                }
                else if (ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 300;
                    npc.lifeMax = 25000000;
                }
                else if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && ModCompatibility.SacredTools.Loaded)
                {
                    npc.damage = 400;
                    npc.lifeMax = 30000000;
                }
                else if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 100;
                    npc.lifeMax = 5000000;
                }
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (20 * multiplierA));
                npc.lifeMax = (int)(2800000 + (1000000 * multiplierA)) / (Main.expertMode ? 2 : 4);
            }
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantBoss>()] = true;
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantEX>()] = true;
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<DeviBoss>()] = true;
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<AbomBoss>()] = true;

            if (EModeGlobalNPC.spawnFishronEX || dukeEX)
            {
                NPCID.Sets.ImmuneToRegularBuffs[NPCID.DukeFishron] = true;
            }
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

            if (npc.type == NPCID.DukeFishron && WorldSavingSystem.DownedAbom)
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<CyclonicFin>(), 1));
            }
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

                    if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                    {
                        npc.defDamage = 200;
                        npc.lifeMax = 20000000;
                    }
                    else if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                    {
                        npc.defDamage = 200;
                        npc.lifeMax = 15000000;
                    }
                    else if (ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
                    {
                        npc.defDamage = 200;
                        npc.lifeMax = 10000000;
                    }
                    else if (ModCompatibility.Thorium.Loaded && ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
                    {
                        npc.defDamage = 300;
                        npc.lifeMax = 20000000;
                    }
                    else if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                    {
                        npc.defDamage = 300;
                        npc.lifeMax = 22500000;
                    }
                    else if (ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                    {
                        npc.defDamage = 300;
                        npc.lifeMax = 25000000;
                    }
                    else if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && ModCompatibility.SacredTools.Loaded)
                    {
                        npc.defDamage = 400;
                        npc.lifeMax = 30000000;
                    }
                    else if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                    {
                        npc.defDamage = 100;
                        npc.lifeMax = 5000000;
                    }
                }
                if (npc.type == ModContent.NPCType<Mutant>())
                {
                    npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1))) / (Main.expertMode ? 1 : 2) / 10;
                    npc.life = npc.lifeMax;
                }
                if (npc.type == ModContent.NPCType<Abominationn>())
                {
                    npc.lifeMax = (int)(1800000 + (1000000 * multiplierA)) / 10;
                    npc.life = npc.lifeMax;
                }
                if (npc.type == NPCID.DukeFishron && (EModeGlobalNPC.spawnFishronEX || dukeEX))
                {
                    npc.life = npc.lifeMax;
                }
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
