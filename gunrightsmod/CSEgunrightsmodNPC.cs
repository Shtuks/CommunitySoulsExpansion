using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Systems;
using ssm.Content.Buffs;
using FargowiltasSouls.Core.Systems;
using SacredTools.NPCs.Boss.Lunarians;
using ThoriumMod;
using ssm.Content.NPCs;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using Terraria.DataStructures;
using System.IO;
using Terraria.ModLoader.IO;
using SacredTools.Content.NPCs.Boss.Decree;
using static Terraria.ModLoader.ModContent;
using SacredTools.NPCs.Boss.Pumpkin;
using SacredTools.NPCs.Boss.Jensen;
using SacredTools.NPCs.Boss.Araneas;
using SacredTools.NPCs.Boss.Raynare;
using SacredTools.NPCs.Boss.Primordia;
using SacredTools.NPCs.Boss.Abaddon;
using SacredTools.NPCs.Boss.Araghur;
using FargowiltasSouls.Core.Globals;
using Terraria.ID;
/*
namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public partial class CSEgunrightsmodNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int NPCMicroplasticStack = 0;
        public int NPCMicroplasticTimer = 0;
        public bool NPCForceMicroplastic = false;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (NPCMicroplasticTimer < 0)
                NPCMicroplasticTimer--;

            if (npc.lifeRegen < 0)
            {
                damage = -npc.lifeRegen / 2; // Divide by two for DPS for some reason
                if (damage < 1)
                    damage = 1;
            }

            if (NPCMicroplasticStack > 0 && NPCMicroplasticTimer > 0)
                npc.lifeRegen -= 20 * NPCMicroplasticStack;

            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (NPCForceMicroplastic == true && NPCMicroplasticStack >= 4)
            {
                modifiers.FinalDamage *= 1.2f;
            }
            else if (NPCMicroplasticStack >= 3)
            {
                modifiers.FinalDamage *= 1.1f;
            }
        }
    }
}
*/