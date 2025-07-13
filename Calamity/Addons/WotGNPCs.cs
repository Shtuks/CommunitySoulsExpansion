using Fargowiltas.NPCs;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using NoxusBoss.Content.Items.MiscOPTools;
using ssm.Content.NPCs;
using ssm.Content.NPCs.MutantEX;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class WotGNPCs : GlobalNPC
    {
        public override void SetDefaults(NPC entity)
        {
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<LumberJack>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Squirrel>()] = true;

            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<MutantBoss>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<DeviBoss>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<AbomBoss>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Mutant>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Abominationn>()] = true;
            EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Deviantt>()] = true;

            if (CSEConfig.Instance.AlternativeSiblings) 
            {
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Industrialist>()] = true;
                EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<MonocleCat>()] = true;

                EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<MutantEX>()] = true;
                EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Monstrosityy>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<AmalgamBoss>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Amalgamtionn>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<DivergenttBoss>()] = true;
                //EmptinessSprayer.NPCsThatReflectSpray[ModContent.NPCType<Divergentt>()] = true;
            }
        }
    }
}
