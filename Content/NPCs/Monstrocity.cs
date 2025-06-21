using System.Collections.Generic;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using Fargowiltas.NPCs;

namespace ssm.Content.NPCs
{
    [AutoloadHead]
    public class Monstrocity : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.AlternativeSiblings;
        }

        private static int shopNum;
        private static bool showCycleShop;
        public static List<string> Names = new() {
            "Neko",
            "Starlight",
            "Palug",
            "Shtux",
            "Echson",
            "Doggo",
            "Bingus",
            "Spoinkers",
            "La creatura",
            "DrMutant",
            "Herobrine",
            "Wargofilwta",
            "Mutant",
            "TheLorde",
            "Bakarim",
            "Apotheosis",
            "Thanatos",
            "Spamton"
        };
        
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 10;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Mods.ssm.Bestiary.Monstrocity")});
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 40;
            NPC.height = 54;
            NPC.aiStyle = 7;
            NPC.damage = 500;
            NPC.defense = int.MaxValue;
            NPC.lifeMax = int.MaxValue;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.dontTakeDamage = true;
            AnimationType = 22;
            NPC.Happiness
                    .SetNPCAffection<Mutant>(AffectionLevel.Dislike)
                    .SetNPCAffection<Deviantt>(AffectionLevel.Like)
                    .SetNPCAffection<Abominationn>(AffectionLevel.Like);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (WorldSavingSystem.EternityMode)
            {
                return NPC.downedMoonlord;
            }
            return false;
        }
        public override List<string> SetNPCNameList()
        {
            return Names;
        }
        public override string GetChat()
        {
            if (WorldSavingSystem.DownedMutant)
            {
                return "[c/FF0000:You are ready.]";
            }

            return Main.rand.Next(3) switch
            {
                0 => "...",
                1 => "...",
                _ => "...",
            };
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            if (showCycleShop)
            {
                button += $" {shopNum + 1}";
                button2 = "Cycle Shop";
            }
        }
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
            else
            {
                shopNum++;
            }
        }
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "Shop");

            npcShop.Register();
        }
        public override bool CanGoToStatue(bool toKingStatue)
        {
            return toKingStatue;
        }
        public override bool UsesPartyHat()
        {
            return false;
        }
    }
}
