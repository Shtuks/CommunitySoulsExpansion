using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ssm.Content.NPCs
{
    public class DPSLimitGlobalNPC : GlobalNPC
    {
        public static Dictionary<int, int> BossDPSLimits = new Dictionary<int, int>();

        private static float[] accumulatedDamage;
        private static int[] lastHitTime;
        private static bool[] initialized;

        public override void Load()
        {
            accumulatedDamage = new float[Main.maxNPCs];
            lastHitTime = new int[Main.maxNPCs];
            initialized = new bool[Main.maxNPCs];
        }

        public override void Unload()
        {
            accumulatedDamage = null;
            lastHitTime = null;
            initialized = null;
            BossDPSLimits.Clear();
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers hit)
        {
            ProcessDamage(npc, ref hit);
        }

        private void ProcessDamage(NPC npc, ref NPC.HitModifiers hit)
        {
            int id = npc.whoAmI;

            if (!BossDPSLimits.TryGetValue(npc.type, out int maxDPS))
                return;

            if (!initialized[id])
            {
                lastHitTime[id] = (int)Main.GameUpdateCount;
                accumulatedDamage[id] = 0;
                initialized[id] = true;
            }

            int currentTime = (int)Main.GameUpdateCount;
            if (currentTime - lastHitTime[id] >= 60) 
            {
                accumulatedDamage[id] = 0;
                lastHitTime[id] = currentTime;
            }

            float damage = hit.FinalDamage.Flat;
            float allowedDamage = maxDPS - accumulatedDamage[id];

            if (allowedDamage <= 0)
            {
                hit.FinalDamage *= 0;
            }
            else if (damage > allowedDamage)
            {
                hit.FinalDamage.Flat = (int)allowedDamage;
                accumulatedDamage[id] = maxDPS; 
            }
            else
            {
                accumulatedDamage[id] += damage;
            }

            lastHitTime[id] = currentTime;
        }

        public override void ResetEffects(NPC npc)
        {
            if (!npc.active)
            {
                int id = npc.whoAmI;
                accumulatedDamage[id] = 0;
                lastHitTime[id] = 0;
                initialized[id] = false;
            }
        }
    }
}