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

namespace ssm
{
    public partial class CSENpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int genTimer = 0;
        public bool mayo;

        public int chtuxlagorInferno;
        public static int ECH = -1;
        public static int DukeEX = -1;
        public static int boss = -1;
        public static int mutantEX = -1;

        public static float multiplierM = 0;
        public static float multiplierA = 0;

        public bool SwarmActive;
        public bool SwarmHealth;
        private int go = 1;
        public override void Load()
        {
            if (ModCompatibility.Homeward.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplierM += 0.8f; }

            if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierM += 0.8f; }

            if (ModCompatibility.Thorium.Loaded) { multiplierM += 0.6f; multiplierA += 1f; }
            if (ModCompatibility.Calamity.Loaded) { multiplierM += CSEConfig.Instance.DebugMode ? 8.8f : 2.8f; multiplierA += 6f; }
            if (ModCompatibility.SacredTools.Loaded) { multiplierM += 1.6f; multiplierA += 2f; }
        }
        public override void SetDefaults(NPC npc)
        {
            //devi max hp - 40 k
            //divergentt max hp - 600 k
            //abom max hp - 12.8 mil
            //amalgamationn max hp - 30 mil
            //mutant max hp - 70 mil
            //monstrosity max hp - 600 mil

            if (npc.type == ModContent.NPCType<MutantBoss>() && !ModCompatibility.Inheritance.Loaded)
            {
                //cal omly
                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 750;
                    npc.lifeMax = 45000000;
                }

                //soa only
                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 700;
                    npc.lifeMax = 35000000;
                }

                //thorium only
                if (ModCompatibility.Thorium.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
                {
                    npc.damage = 600;
                    npc.lifeMax = 18000000;
                }

                //thorium and soa
                if (ModCompatibility.Thorium.Loaded && ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
                {
                    npc.damage = 750;
                    npc.lifeMax = 45000000;
                }

                //thorium and cal
                if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    npc.damage = 800;
                    npc.lifeMax = 50000000;
                }

                //soa and cal
                if (ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    npc.damage = 900;
                    npc.lifeMax = 60000000;
                }

                //all mods
                if (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded && ModCompatibility.SacredTools.Loaded)
                {
                    npc.damage = 1000;
                    npc.lifeMax = 70000000;
                }

                //npc.damage = Main.getGoodWorld ? 2000 : (int)(500 + ((ModCompatibility.Calamity.Loaded && CSEConfig.Instance.DebugMode ? 90 : 100) * (Math.Round(multiplierM, 1))));
                //npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1))) / (Main.expertMode ? 1 : 2);
            }

            if (npc.type == ModContent.NPCType<AbomBoss>() && !ModCompatibility.Inheritance.Loaded)
            {
                npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (15 * multiplierA));
                npc.lifeMax = (int)(1400000 + (1000000 * multiplierA)) / (Main.expertMode ? 2 : 4);
            }
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantBoss>()] = true;
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantEX>()] = true;
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<DeviBoss>()] = true;
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<AbomBoss>()] = true;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (chtuxlagorInferno > 0)
                ApplyDPSDebuff(npc.lifeMax / 10, npc.lifeMax / 100, ref npc.lifeRegen, ref damage);
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>() && Main.npc[EModeGlobalNPC.mutantBoss].ai[0] > 10 && ModCompatibility.IEoR.Loaded)
            {
                float LRM = Utilities.Saturate((float)npc.life / (float)npc.lifeMax);
                float maxTimeNormal = 12000; // 4 min
                float maxTimeMaso = 18000; // 4.5 min
                float intendedDuration = WorldSavingSystem.MasochistModeReal ? maxTimeMaso : maxTimeNormal;

                // 0 = as intended, 1 = instakill
                float fightProgress = Utilities.InverseLerp(0f, intendedDuration, genTimer);
                float aheadOfSchedule = MathF.Max(0f, 1f - fightProgress - LRM);

                float resistanceFactor = (float)Math.Pow(aheadOfSchedule, 0.1f); // lower value - sharper applying

                if (aheadOfSchedule > 0.8f)
                {
                    modifiers.FinalDamage *= 0.01f;
                }
                else
                {
                    float damageMultiplier = 1f - resistanceFactor;
                    modifiers.FinalDamage *= damageMultiplier;
                }
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
            if (!mayo)
            {
                if (npc.type == ModContent.NPCType<Mutant>())
                {
                    npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1))) / (Main.expertMode ? 1 : 2) / 10;
                    npc.life = npc.lifeMax;
                }
                if (npc.type == ModContent.NPCType<Abominationn>())
                {
                    npc.lifeMax = (int)(2800000 + (1000000 * multiplierA)) / (Main.expertMode ? 2 : 4) / 10;
                    npc.life = npc.lifeMax;
                }
                mayo = true;
            }

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
    }
}
