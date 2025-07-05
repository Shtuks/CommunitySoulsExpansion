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

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int genTimer = 0;
        
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
            //soa 34
            //cal 48
            //thor 16
            //HWJ 18
            //soa-cal 55
            //soa-thor 40
            //soa-cal-thor 60
            //thor-cal 52
            if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded) { multiplierA += 1.1f; }
            if (ModCompatibility.Thorium.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierA += 1f; }
            if (ModCompatibility.Homeward.Loaded && !ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) { multiplierM += 0.8f; }

            if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierM += 0.8f; }

            if (ModCompatibility.Thorium.Loaded) { multiplierM += 0.6f; multiplierA += 3f; }
            if (ModCompatibility.Calamity.Loaded) { multiplierM += ShtunConfig.Instance.DebugMode ? 8.8f : 2.8f; multiplierA += 6f; }
            if (ModCompatibility.SacredTools.Loaded) { multiplierM += 1.6f; multiplierA += 1f; }
        }
        public override void SetDefaults(NPC npc)
        {
            //devi max hp - 40 k
            //divergentt max hp - 600 k
            //abom max hp - 12.8 mil
            //amalgamationn max hp - 30 mil
            //mutant max hp - 60 mil //120 mil if debug mode
            //monstrosity max hp - 600 mil

            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                npc.damage = Main.getGoodWorld ? 2000 : (int)(500 + (ModCompatibility.Calamity.Loaded ? 80 : 100 * (Math.Round(multiplierM, 1))));
                npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1))) / (Main.expertMode ? 1 : 2);
            }
            if (npc.type == ModContent.NPCType<Mutant>())
            {
                npc.lifeMax = (int)(10000000 + (10000000 * Math.Round(multiplierM, 1))) / (Main.expertMode ? 1 : 2) / 10;
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (15 * multiplierA));
                npc.lifeMax = (int)(2800000 + (1000000 * multiplierA)) / (Main.expertMode ? 2 : 4);
            }
            if (npc.type == ModContent.NPCType<Abominationn>())
            {
                npc.lifeMax = (int)(2800000 + (1000000 * multiplierA)) / (Main.expertMode ? 2 : 4)/10;
            }
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantBoss>()] = true;
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

                float resistanceFactor = (float)Math.Pow(aheadOfSchedule, 0.3f); // lower value - sharper applying

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
