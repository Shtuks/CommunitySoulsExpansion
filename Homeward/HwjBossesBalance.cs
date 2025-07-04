using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using Redemption.NPCs.Bosses.Neb;
using ContinentOfJourney.NPCs.Boss_TheSon;
using ContinentOfJourney.NPCs.Boss_WorldsEndEverlastingFallingWhale;
using ContinentOfJourney.NPCs.Boss_SlimeGod;
using ContinentOfJourney.NPCs.Boss_TheMaterealizer;
using FargowiltasSouls.Content.Items.Armor;
using ContinentOfJourney.NPCs.Boss_TheOverwatcher;
using ContinentOfJourney.NPCs.Boss_TheLifebringer;
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
                float multiplier = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.3f; }
                if (ModCompatibility.Calamity.Loaded) { multiplier += 0.6f; }

                npc.lifeMax = (int)(2900000 + (1000000 * multiplier));
                npc.damage = 730;
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