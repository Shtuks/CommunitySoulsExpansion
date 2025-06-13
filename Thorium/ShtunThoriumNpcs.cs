using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.NPCs.BossMini;
using ssm.Core;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossBuriedChampion;
using static Terraria.ModLoader.ModContent;
using ThoriumMod.NPCs.BossBoreanStrider;
using ThoriumMod.NPCs.BossFallenBeholder;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossThePrimordials;
using ThoriumMod.NPCs.BossViscount;
using ThoriumMod.NPCs.BossStarScouter;


namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public partial class ShtunThoriumNpcs : GlobalNPC
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override bool InstancePerEntity => true;
        public override bool PreKill(NPC npc)
        {
            bool doDeviText = false;
            if (npc.type == ModContent.NPCType<PatchWerk>() && !ThoriumWorldSave.downedPatchWrek)
            {
                doDeviText = true;
                ThoriumWorldSave.downedPatchWrek = true;
            }
            if (npc.type == ModContent.NPCType<Illusionist>() && !ThoriumWorldSave.downedIllusionist)
            {
                doDeviText = true;
                ThoriumWorldSave.downedIllusionist = true;
            }
            if (npc.type == ModContent.NPCType<CorpseBloom>() && !ThoriumWorldSave.downedCorpseBloom)
            {
                doDeviText = true;
                ThoriumWorldSave.downedCorpseBloom = true;
            }
            if (doDeviText && Main.netMode != NetmodeID.Server)
            {
                Main.NewText("A new item has been unlocked in Deviantt's shop!", Color.HotPink);
            }
            return true;
        }
        public override void SetDefaults(NPC npc)
        {
            int num1 = ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded ? 30000 : 20000;
            int num2 = ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded ? 300000 : 150000;
            int num3 = 30000000;

            int num12 = 50;
            int num22 = 100;
            int num32 = 200;

            if (ssm.SwarmSetDefaults)
            {
                if (npc.type == NPCType<QueenJellyfish>() || npc.type == NPCType<TheGrandThunderBird>() || npc.type == NPCType<GraniteEnergyStorm>() || npc.type == NPCType<BuriedChampion>() || npc.type == NPCType<Viscount>() || npc.type == NPCType<StarScouter>())
                {
                    npc.lifeMax = num1 * ssm.SwarmItemsUsed;
                    npc.damage = num12 * ssm.SwarmItemsUsed;
                }
                if (npc.type == NPCType<BoreanStrider>() || npc.type == NPCType<FallenBeholder>() || npc.type == NPCType<FallenBeholder2>() || npc.type == NPCType<Lich>() || npc.type == NPCType<LichHeadless>() || npc.type == NPCType<ForgottenOne>() || npc.type == NPCType<ForgottenOneCracked>() || npc.type == NPCType<ForgottenOneReleased>())
                {
                    npc.lifeMax = num2 * ssm.SwarmItemsUsed;
                    npc.damage = num22 * ssm.SwarmItemsUsed;
                }
                if (npc.type == NPCType<DreamEater>())
                {
                    npc.lifeMax = num3 * ssm.SwarmItemsUsed;
                    npc.damage = num32 * ssm.SwarmItemsUsed;
                }
            }
        }
    }
}