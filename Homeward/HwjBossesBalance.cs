using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using ContinentOfJourney.NPCs.Boss_TheSon;
using ContinentOfJourney.NPCs.Boss_WorldsEndEverlastingFallingWhale;
using ContinentOfJourney.NPCs.Boss_SlimeGod;
using ContinentOfJourney.NPCs.Boss_TheMaterealizer;
using ContinentOfJourney.NPCs.Boss_TheOverwatcher;
using ContinentOfJourney.NPCs.Boss_Diver;

namespace ssm.Homeward
{
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name)]
    [ExtendsFromMod(ModCompatibility.Homeward.Name)]
    public class HwjBossesBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<TheSon>())
            {
                float multiplierD = 0;
                float multiplierL = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplierL += 0.5f; multiplierD += 2f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplierL += 0.5f; multiplierD += 3f; }
                if (ModCompatibility.Calamity.Loaded) { multiplierL += 3.1f; multiplierD += 8f; }

                npc.lifeMax = (int)(2900000 + (1000000 * multiplierL));
                npc.damage = (int)(290 + (10 * multiplierD));
            }

            if (npc.type == ModContent.NPCType<WorldsEndEverlastingFallingWhale>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 3200000 : 2100000;
            }


            if (npc.type == ModContent.NPCType<SlimeGod>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 990000 : 770000;
                npc.damage = 420;
            }

            if (npc.type == ModContent.NPCType<TheMaterealizer>() || npc.type == ModContent.NPCType<TheOverwatcher>())
            {
                npc.lifeMax = (int)(ModCompatibility.Calamity.Loaded ? npc.lifeMax * 1.2f : npc.lifeMax * 1.1f);
            }

            if (npc.type == ModContent.NPCType<Diver>())
            {
                npc.lifeMax = ModCompatibility.Calamity.Loaded ? 95000 : 75000;
                npc.damage = 200;
            }
        }
    }
}