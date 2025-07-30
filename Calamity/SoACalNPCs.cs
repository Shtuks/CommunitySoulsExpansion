using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs.Polterghast;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class SoACalNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.SpecificDebuffImmunity[ModContent.NPCType<Nihilus2>()][ModContent.BuffType<Enraged>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[ModContent.NPCType<Nihilus>()][ModContent.BuffType<Enraged>()] = true;
        }

        public override void SetDefaults(NPC entity)
        {
            //op flarium gear
            if(entity.type == ModContent.NPCType<Polterghast>())
            {
                entity.lifeMax *= (int)1.3;
            }
        }
    }
}
