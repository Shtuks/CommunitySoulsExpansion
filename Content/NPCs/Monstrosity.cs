using System.Collections.Generic;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using Fargowiltas.NPCs;
using FargowiltasSouls;
using ssm.Core;

namespace ssm.Content.NPCs
{
    [AutoloadHead]
    public class Monstrosity : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }

        public static List<string> Names = new() {
            "Neko",
            "Starlight",
            "Palug",
            "Shtux",
            "Echson",
            "Doggo",
            "Bingus",
            "Greg",
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
                new FlavorTextBestiaryInfoElement("Mods.ssm.Bestiary.Monstrosity")});
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 40;
            NPC.height = 54;
            NPC.aiStyle = 7;
            NPC.damage = 500;
            NPC.defense = int.MaxValue/10;
            NPC.lifeMax = int.MaxValue/10;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.dontTakeDamage = true;
            AnimationType = 22;
            NPC.Happiness
                    .SetNPCAffection<Mutant>(AffectionLevel.Dislike)
                    .SetNPCAffection<Deviantt>(AffectionLevel.Like)
                    .SetNPCAffection<Abominationn>(AffectionLevel.Like);

            if (FargoSoulsUtil.AprilFools)
                NPC.GivenName = Language.GetTextValue("Mods.ssm.NPCs.Monstrosity.DisplayNameApril");
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
            List<string> dialogue = new List<string>
            {
                "You know the rules! And you will die. Im not going to rickroll you, that's only 0,00001% chance.",
                "Yippe!",
                "Why would i be afraid of a cat? Well, it is long story.",
                "Can I jump? Yes, I have a 'spacebar'.",
                "It would be a calamity for you not to buy my products.",
                "How old am i? I'm some Eternity years old.",
                "When Abomination went to register at the registry office they put an extra 'N' in his name.",
                "You don't want to see me in boss form.",
                "My creator? Don't mention it if you don't want to see me angry.",
                "I'm the reason why Seth got his face covered with that bandana.",
                "1.4.5? maybe in a few years.",
                "There's an imposter among us, he's me! I don't really belong anywhere.",
                "Galactica has a friend of mine, you should visit him.",
                "I heard rumors about a creature with 'EX' in its name, that doesn't sound so good.",
                "Deviantt doesn't like my look.",
                "I am the real evil presence that watches you.",
                "Split me? nah that would be a calamity , imagine if some Elements awoken from my dead body, that would be a cataclysm, and this world would not be worthy to witness this.",
                "I heard about Whips, they boost your damage, I don't like it.",
                "Why don't I attack? Ah that's a nice question, I don't think it's necessary.",
                "My hp is limited to this world, but I would prob get 22^22^22^22 times this.",
                "Eternity doesn't last that long when you are old enough.",
                "Sunflowers are cool, not for your health tho.",
                "Are you approaching me? Can't but my stuff if you don't get closer.",
                "There is a pipe bomb inside your walls.",
                "A lot of things explode for no reason.",
                "Also try Infernal Eclipse of Ragnarok!",
                "Once I put Grand Thunder Bird, Scarabeus, Desert Scourge, Cursed Coffin and Grand Antlion in one room to see who is strongest. Instead they started talking about politics.",
            };

            if (WorldSavingSystem.DownedMutant)
            {
                dialogue.Add("[c/FF0000:You are ready.]");
            }

            if (ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded && ModCompatibility.Thorium.Loaded)
            {
                dialogue.Add("Go touch some grass.");
            }

            if (Main.LocalPlayer.FargoSouls().Eternity)
            {
                dialogue.Add("Soul of Eternity is not enough to face me, let alone 2 of it.");
            }

            return Main.rand.Next(dialogue);
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
