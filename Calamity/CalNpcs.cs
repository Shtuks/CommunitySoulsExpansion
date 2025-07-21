using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using FargowiltasCrossmod.Content.Calamity.Buffs;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using CalamityMod;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using CalamityMod.Items.Weapons.Ranged;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalNPCs : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if (ModCompatibility.Goozma.Loaded)
            {
                if (npc.type == ModCompatibility.Goozma.GooBoss.Type)
                {
                    if (Main.expertMode && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                        Main.LocalPlayer.AddBuff(ModContent.BuffType<CalamitousPresenceBuff>(), 2);
                }
            }
            return base.PreAI(npc);
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.type == ModContent.NPCType<AbomBoss>() && npc.target.ToPlayer().HeldItem.type == ModContent.ItemType<HalibutCannon>())
            {
                modifiers.FinalDamage *= 0.8f;
            }
        }
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>() && ModCompatibility.SacredTools.Loaded)
            {
                npc.DR_NERD(0.1f, 0.2f, 0.3f);
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                npc.DR_NERD(0.05f, 0.1f, 0.15f, 0.2f);
            }

            if (npc.type == ModContent.NPCType<SupremeCalamitas>())
            {
                float multiplier = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplier += 0.6f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.2f; }

                npc.lifeMax = (int)(2800000 + (1000000 * multiplier));
                npc.damage += (int)(npc.damage * multiplier);
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                if (npc.type == ModCompatibility.Goozma.GooBoss.Type)
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.3f; }

                    npc.lifeMax = (int)(4300000 + (1000000 * multiplier));
                    npc.damage += (int)(npc.damage * multiplier);
                }
            }

            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                if (npc.type == ModCompatibility.WrathoftheGods.NamelessDeityBoss.Type)
                {
                    float multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded) { multiplier += 5f; }
                    if (ModCompatibility.SacredTools.Loaded) { multiplier += 5f; }

                    npc.lifeMax = (int)(25000000 + (1000000 * multiplier));
                }
            }

            if (npc.type == ModContent.NPCType<AresBody>() || npc.type == ModContent.NPCType<AresGaussNuke>() || npc.type == ModContent.NPCType<AresLaserCannon>() || npc.type == ModContent.NPCType<AresPlasmaFlamethrower>() || npc.type == ModContent.NPCType<AresTeslaCannon>())
            {
                float multiplier = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplier += 0.5f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.1f; }

                npc.lifeMax = (int)(4400000 + (1000000 * multiplier));
                npc.damage += (int)(npc.damage * multiplier);
            }
            if (npc.type == ModContent.NPCType<Apollo>() || npc.type == ModContent.NPCType<Artemis>())
            {
                float multiplier = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplier += 0.5f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.1f; }

                npc.lifeMax = (int)(4300000 + (1000000 * multiplier));
                npc.damage += (int)(npc.damage * multiplier);
            }
            if (npc.type == ModContent.NPCType<ThanatosBody1>() || npc.type == ModContent.NPCType<ThanatosBody2>() || npc.type == ModContent.NPCType<ThanatosHead>() || npc.type == ModContent.NPCType<ThanatosTail>())
            {
                float multiplier = 0;

                if (ModCompatibility.Thorium.Loaded) { multiplier += 0.5f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplier += 0.1f; }

                npc.lifeMax = (int)(4300000 + (1000000 * multiplier));
                npc.damage += (int)(npc.damage * multiplier);
            }
        }
    }
}
