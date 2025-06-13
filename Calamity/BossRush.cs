 using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using CalamityMod.Enums;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Systems;
using CalamityMod.UI.DraedonSummoning;
using CalamityMod;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;
using CalamityMod.NPCs.NormalNPCs;
using Microsoft.Xna.Framework;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.CeaselessVoid;
using System.Collections.Generic;
using SacredTools.NPCs.Boss.Abaddon;
using Redemption.NPCs.Bosses.ADD;
using SacredTools.NPCs.Boss.Araghur;
using SacredTools.NPCs.Boss.Lunarians;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using SacredTools.NPCs.Boss.Erazor;
using ThoriumMod.NPCs.BossThePrimordials;

//kluges my beloved
namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class BossRush : ModSystem
    {
        public static List<Boss> BossesOnlyCal = [
               new Boss(NPCID.KingSlime, spawnContext: type => {
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                },permittedNPCs: new int[] { NPCID.BlueSlime, NPCID.YellowSlime, NPCID.PurpleSlime, NPCID.RedSlime, NPCID.GreenSlime, NPCID.RedSlime,
                    NPCID.IceSlime, NPCID.UmbrellaSlime, NPCID.Pinky, NPCID.SlimeSpiked, NPCID.RainbowSlime, NPCType<KingSlimeJewelRuby>(),
                    NPCType<KingSlimeJewelSapphire>(), NPCType<KingSlimeJewelEmerald>() }),

                new Boss(NPCID.MoonLordCore, spawnContext: type =>{
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                    DownedBossSystem.startedBossRushAtLeastOnce = true;
                }, permittedNPCs: [NPCID.MoonLordLeechBlob, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye]),

                new Boss(NPCType<Providence>(), TimeChangeContext.Day, type =>{
                    SoundEngine.PlaySound(Providence.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    int provi = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[provi].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(provi);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<ProvSpawnDefense>(), NPCType<ProvSpawnHealer>(), NPCType<ProvSpawnOffense>(),
                    NPCType<ProfanedGuardianCommander>(), NPCType<ProfanedGuardianDefender>(), NPCType<ProfanedGuardianHealer>()]),


                new Boss(NPCType<Polterghast>(), permittedNPCs: [NPCType<PhantomFuckYou>(), NPCType<PolterghastHook>(), NPCType<PolterPhantom>()]),

                new Boss(NPCType<OldDuke>(), spawnContext: type => {
                    int od = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X + Main.rand.Next(-100, 101)), (int)Main.player[ClosestPlayerToWorldCenter].Center.Y - 300, type, 1);
                    CalamityUtils.BossAwakenMessage(od);
                    Main.npc[od].timeLeft *= 20;
                }, permittedNPCs: [NPCType<SulphurousSharkron>(), NPCType<OldDukeToothBall>()]),

                new Boss(NPCType<DevourerofGodsHead>(), spawnContext: type => {
                    SoundEngine.PlaySound(DevourerofGodsHead.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<DevourerofGodsBody>(), NPCType<DevourerofGodsTail>(), NPCType<CosmicGuardianBody>(), NPCType<CosmicGuardianHead>(), NPCType<CosmicGuardianTail>(),
                NPCType<Signus>(), NPCType<CeaselessVoid>(), NPCType<StormWeaverHead>(), NPCType<StormWeaverBody>(), NPCType<StormWeaverTail>()]),

                new Boss(NPCType<CosmosChampion>(), spawnContext: type => {
                    int erd = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[erd].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(erd);
                }),

                new Boss(NPCType<Yharon>(), permittedNPCs: NPCType<Bumblefuck>()),


                new Boss(NPCType<Draedon>(), spawnContext: type =>
                {
                    if (!NPC.AnyNPCs(NPCType<Draedon>()))
                    {
                        Player player = Main.player[ClosestPlayerToWorldCenter];

                        SoundEngine.PlaySound(CodebreakerUI.SummonSound, player.Center);
                        Vector2 spawnPos = player.Center + new Vector2(-8f, -100f);
                        int draedon = NPC.NewNPC(new EntitySource_WorldEvent("CalamityMod_BossRush"), (int)spawnPos.X, (int)spawnPos.Y, NPCType<Draedon>());
                        Main.npc[draedon].timeLeft *= 20;
                    }
                }, usesSpecialSound: true, permittedNPCs: new int[] { NPCType<Apollo>(), NPCType<AresBody>(), NPCType<AresGaussNuke>(), NPCType<AresLaserCannon>(), NPCType<AresPlasmaFlamethrower>(), NPCType<AresTeslaCannon>(), NPCType<Artemis>(), NPCType<ThanatosBody1>(), NPCType<ThanatosBody2>(), NPCType<ThanatosHead>(), NPCType<ThanatosTail>() }),

                new Boss(NPCType<SupremeCalamitas>(), spawnContext: type => {
                    SoundEngine.PlaySound(SupremeCalamitas.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    CalamityUtils.SpawnBossBetter(Main.player[ClosestPlayerToWorldCenter].Top - new Vector2(42, 84f), type);
                }, dimnessFactor: 0.5f, permittedNPCs: [NPCType<SepulcherArm>(), NPCType<SepulcherBody>(), NPCType<SepulcherHead>(), NPCType<SepulcherTail>(), NPCType<SepulcherBodyEnergyBall>(), NPCType<SoulSeekerSupreme>(), NPCType<BrimstoneHeart>(), NPCType<SupremeCataclysm>(), NPCType<SupremeCatastrophe>()]),
                new Boss(NPCType<AbomBoss>())
               ];
        public override void PostSetupContent()
        {
            if (ModCompatibility.SacredTools.Loaded && ModCompatibility.Redemption.Loaded && ModCompatibility.Thorium.Loaded)
            {
                Bosses = BossRush2.Bosses;
            }
            else if (!ModCompatibility.SacredTools.Loaded && ModCompatibility.Redemption.Loaded && ModCompatibility.Thorium.Loaded)
            {
                Bosses = BossRush3.Bosses;
            }
            else if (!ModCompatibility.SacredTools.Loaded && !ModCompatibility.Redemption.Loaded && ModCompatibility.Thorium.Loaded)
            {
                Bosses = BossRush4.Bosses;
            }
            else 
            {
                Bosses = BossesOnlyCal;
            }

            BossDeathEffects.Remove(NPCType<SupremeCalamitas>());
            BossDeathEffects.Remove(NPCType<DevourerofGodsHead>());
            BossDeathEffects.Add(NPCType<AbomBoss>(), npc => { BossRushDialogueSystem.StartDialogue(DownedBossSystem.downedBossRush ? BossRushDialoguePhase.EndRepeat : BossRushDialoguePhase.End); });
        }
    }
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class BossRush2 : ModSystem
    {
        public static List<Boss> Bosses = [
               new Boss(NPCID.KingSlime, spawnContext: type => {
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                },permittedNPCs: new int[] { NPCID.BlueSlime, NPCID.YellowSlime, NPCID.PurpleSlime, NPCID.RedSlime, NPCID.GreenSlime, NPCID.RedSlime,
                    NPCID.IceSlime, NPCID.UmbrellaSlime, NPCID.Pinky, NPCID.SlimeSpiked, NPCID.RainbowSlime, NPCType<KingSlimeJewelRuby>(),
                    NPCType<KingSlimeJewelSapphire>(), NPCType<KingSlimeJewelEmerald>() }),

                new Boss(NPCID.MoonLordCore, spawnContext: type =>{
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                    DownedBossSystem.startedBossRushAtLeastOnce = true;
                }, permittedNPCs: [NPCID.MoonLordLeechBlob, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye]),

                new Boss(NPCType<Providence>(), TimeChangeContext.Day, type =>{
                    SoundEngine.PlaySound(Providence.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    int provi = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[provi].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(provi);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<ProvSpawnDefense>(), NPCType<ProvSpawnHealer>(), NPCType<ProvSpawnOffense>(),
                    NPCType<ProfanedGuardianCommander>(), NPCType<ProfanedGuardianDefender>(), NPCType<ProfanedGuardianHealer>()]),

                new Boss(NPCType<Abaddon>(), TimeChangeContext.Night, permittedNPCs: [NPCType<NightmareClone>(), NPCType<NightmareGuardian>(), NPCType<NightmareWraithMinion>(), NPCType<NightmareSoul>(), NPCType<NightmareSoul2>()]),

                new Boss(NPCType<Polterghast>(), permittedNPCs: [NPCType<PhantomFuckYou>(), NPCType<PolterghastHook>(), NPCType<PolterPhantom>()]),

                new Boss(NPCType<Ukko>(), TimeChangeContext.Night, permittedNPCs: [NPCType<Akka>()]),

                new Boss(NPCType<AraghurHead>(), permittedNPCs: [NPCType<AraghurBody>(), NPCType<AraghurTail>(), NPCType<AraghurMinion>()]),

                new Boss(NPCType<OldDuke>(), spawnContext: type => {
                    int od = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X + Main.rand.Next(-100, 101)), (int)Main.player[ClosestPlayerToWorldCenter].Center.Y - 300, type, 1);
                    CalamityUtils.BossAwakenMessage(od);
                    Main.npc[od].timeLeft *= 20;
                }, permittedNPCs: [NPCType<SulphurousSharkron>(), NPCType<OldDukeToothBall>()]),

                new Boss(NPCType<Solarius>(), spawnContext: type => {
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, NPCType<Voxa>());
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, NPCType<Dustite>());
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, NPCType<Nuba>());
                },permittedNPCs: [NPCType<Voxa>(), NPCType<Dustite>(), NPCType<Nuba>(), NPCType<Novaniel>()]),

                new Boss(NPCType<DevourerofGodsHead>(), spawnContext: type => {
                    SoundEngine.PlaySound(DevourerofGodsHead.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<DevourerofGodsBody>(), NPCType<DevourerofGodsTail>(), NPCType<CosmicGuardianBody>(), NPCType<CosmicGuardianHead>(), NPCType<CosmicGuardianTail>(),
                NPCType<Signus>(), NPCType<CeaselessVoid>(), NPCType<StormWeaverHead>(), NPCType<StormWeaverBody>(), NPCType<StormWeaverTail>()]),

                new Boss(NPCType<CosmosChampion>(), spawnContext: type => {
                    int erd = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[erd].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(erd);
                }),

                new Boss(NPCType<DreamEater>()),

                new Boss(NPCType<Yharon>(), permittedNPCs: NPCType<Bumblefuck>()),

                new Boss(NPCType<Draedon>(), spawnContext: type =>
                {
                    if (!NPC.AnyNPCs(NPCType<Draedon>()))
                    {
                        Player player = Main.player[ClosestPlayerToWorldCenter];

                        SoundEngine.PlaySound(CodebreakerUI.SummonSound, player.Center);
                        Vector2 spawnPos = player.Center + new Vector2(-8f, -100f);
                        int draedon = NPC.NewNPC(new EntitySource_WorldEvent("CalamityMod_BossRush"), (int)spawnPos.X, (int)spawnPos.Y, NPCType<Draedon>());
                        Main.npc[draedon].timeLeft *= 20;
                    }
                }, usesSpecialSound: true, permittedNPCs: new int[] { NPCType<Apollo>(), NPCType<AresBody>(), NPCType<AresGaussNuke>(), NPCType<AresLaserCannon>(), NPCType<AresPlasmaFlamethrower>(), NPCType<AresTeslaCannon>(), NPCType<Artemis>(), NPCType<ThanatosBody1>(), NPCType<ThanatosBody2>(), NPCType<ThanatosHead>(), NPCType<ThanatosTail>() }),

                new Boss(NPCType<Nebuleus>(), permittedNPCs: NPCType<Nebuleus2>()),

                new Boss(NPCType<ErazorBoss>()),

                new Boss(NPCType<SupremeCalamitas>(), spawnContext: type => {
                    SoundEngine.PlaySound(SupremeCalamitas.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    CalamityUtils.SpawnBossBetter(Main.player[ClosestPlayerToWorldCenter].Top - new Vector2(42, 84f), type);
                }, dimnessFactor: 0.5f, permittedNPCs: [NPCType<SepulcherArm>(), NPCType<SepulcherBody>(), NPCType<SepulcherHead>(), NPCType<SepulcherTail>(), NPCType<SepulcherBodyEnergyBall>(), NPCType<SoulSeekerSupreme>(), NPCType<BrimstoneHeart>(), NPCType<SupremeCataclysm>(), NPCType<SupremeCatastrophe>()]),
                
                new Boss(NPCType<AbomBoss>())
        ];
    }
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class BossRush3 : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModCompatibility.SacredTools.Loaded;
        }

        public static List<Boss> Bosses = [
               new Boss(NPCID.KingSlime, spawnContext: type => {
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                },permittedNPCs: new int[] { NPCID.BlueSlime, NPCID.YellowSlime, NPCID.PurpleSlime, NPCID.RedSlime, NPCID.GreenSlime, NPCID.RedSlime,
                    NPCID.IceSlime, NPCID.UmbrellaSlime, NPCID.Pinky, NPCID.SlimeSpiked, NPCID.RainbowSlime, NPCType<KingSlimeJewelRuby>(),
                    NPCType<KingSlimeJewelSapphire>(), NPCType<KingSlimeJewelEmerald>() }),

                new Boss(NPCID.MoonLordCore, spawnContext: type =>{
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                    DownedBossSystem.startedBossRushAtLeastOnce = true;
                }, permittedNPCs: [NPCID.MoonLordLeechBlob, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye]),

                new Boss(NPCType<Providence>(), TimeChangeContext.Day, type =>{
                    SoundEngine.PlaySound(Providence.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    int provi = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[provi].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(provi);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<ProvSpawnDefense>(), NPCType<ProvSpawnHealer>(), NPCType<ProvSpawnOffense>(),
                    NPCType<ProfanedGuardianCommander>(), NPCType<ProfanedGuardianDefender>(), NPCType<ProfanedGuardianHealer>()]),

                new Boss(NPCType<Polterghast>(), permittedNPCs: [NPCType<PhantomFuckYou>(), NPCType<PolterghastHook>(), NPCType<PolterPhantom>()]),

                new Boss(NPCType<Ukko>(), TimeChangeContext.Night, permittedNPCs: [NPCType<Akka>()]),

                new Boss(NPCType<OldDuke>(), spawnContext: type => {
                    int od = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X + Main.rand.Next(-100, 101)), (int)Main.player[ClosestPlayerToWorldCenter].Center.Y - 300, type, 1);
                    CalamityUtils.BossAwakenMessage(od);
                    Main.npc[od].timeLeft *= 20;
                }, permittedNPCs: [NPCType<SulphurousSharkron>(), NPCType<OldDukeToothBall>()]),

                new Boss(NPCType<DevourerofGodsHead>(), spawnContext: type => {
                    SoundEngine.PlaySound(DevourerofGodsHead.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<DevourerofGodsBody>(), NPCType<DevourerofGodsTail>(), NPCType<CosmicGuardianBody>(), NPCType<CosmicGuardianHead>(), NPCType<CosmicGuardianTail>(),
                NPCType<Signus>(), NPCType<CeaselessVoid>(), NPCType<StormWeaverHead>(), NPCType<StormWeaverBody>(), NPCType<StormWeaverTail>()]),

                new Boss(NPCType<CosmosChampion>(), spawnContext: type => {
                    int erd = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[erd].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(erd);
                }),

                new Boss(NPCType<DreamEater>()),

                new Boss(NPCType<Yharon>(), permittedNPCs: NPCType<Bumblefuck>()),

                new Boss(NPCType<Draedon>(), spawnContext: type =>
                {
                    if (!NPC.AnyNPCs(NPCType<Draedon>()))
                    {
                        Player player = Main.player[ClosestPlayerToWorldCenter];

                        SoundEngine.PlaySound(CodebreakerUI.SummonSound, player.Center);
                        Vector2 spawnPos = player.Center + new Vector2(-8f, -100f);
                        int draedon = NPC.NewNPC(new EntitySource_WorldEvent("CalamityMod_BossRush"), (int)spawnPos.X, (int)spawnPos.Y, NPCType<Draedon>());
                        Main.npc[draedon].timeLeft *= 20;
                    }
                }, usesSpecialSound: true, permittedNPCs: new int[] { NPCType<Apollo>(), NPCType<AresBody>(), NPCType<AresGaussNuke>(), NPCType<AresLaserCannon>(), NPCType<AresPlasmaFlamethrower>(), NPCType<AresTeslaCannon>(), NPCType<Artemis>(), NPCType<ThanatosBody1>(), NPCType<ThanatosBody2>(), NPCType<ThanatosHead>(), NPCType<ThanatosTail>() }),

                new Boss(NPCType<Nebuleus>(), permittedNPCs: NPCType<Nebuleus2>()),

                new Boss(NPCType<SupremeCalamitas>(), spawnContext: type => {
                    SoundEngine.PlaySound(SupremeCalamitas.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    CalamityUtils.SpawnBossBetter(Main.player[ClosestPlayerToWorldCenter].Top - new Vector2(42, 84f), type);
                }, dimnessFactor: 0.5f, permittedNPCs: [NPCType<SepulcherArm>(), NPCType<SepulcherBody>(), NPCType<SepulcherHead>(), NPCType<SepulcherTail>(), NPCType<SepulcherBodyEnergyBall>(), NPCType<SoulSeekerSupreme>(), NPCType<BrimstoneHeart>(), NPCType<SupremeCataclysm>(), NPCType<SupremeCatastrophe>()]),

                new Boss(NPCType<AbomBoss>())
        ];
    }
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class BossRush4 : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Redemption.Loaded;
        }
        public static List<Boss> Bosses = [
               new Boss(NPCID.KingSlime, spawnContext: type => {
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                },permittedNPCs: new int[] { NPCID.BlueSlime, NPCID.YellowSlime, NPCID.PurpleSlime, NPCID.RedSlime, NPCID.GreenSlime, NPCID.RedSlime,
                    NPCID.IceSlime, NPCID.UmbrellaSlime, NPCID.Pinky, NPCID.SlimeSpiked, NPCID.RainbowSlime, NPCType<KingSlimeJewelRuby>(),
                    NPCType<KingSlimeJewelSapphire>(), NPCType<KingSlimeJewelEmerald>() }),

                new Boss(NPCID.MoonLordCore, spawnContext: type =>{
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                    DownedBossSystem.startedBossRushAtLeastOnce = true;
                }, permittedNPCs: [NPCID.MoonLordLeechBlob, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye]),

                new Boss(NPCType<Providence>(), TimeChangeContext.Day, type =>{
                    SoundEngine.PlaySound(Providence.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    int provi = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[provi].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(provi);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<ProvSpawnDefense>(), NPCType<ProvSpawnHealer>(), NPCType<ProvSpawnOffense>(),
                    NPCType<ProfanedGuardianCommander>(), NPCType<ProfanedGuardianDefender>(), NPCType<ProfanedGuardianHealer>()]),

                new Boss(NPCType<Polterghast>(), permittedNPCs: [NPCType<PhantomFuckYou>(), NPCType<PolterghastHook>(), NPCType<PolterPhantom>()]),

                new Boss(NPCType<OldDuke>(), spawnContext: type => {
                    int od = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X + Main.rand.Next(-100, 101)), (int)Main.player[ClosestPlayerToWorldCenter].Center.Y - 300, type, 1);
                    CalamityUtils.BossAwakenMessage(od);
                    Main.npc[od].timeLeft *= 20;
                }, permittedNPCs: [NPCType<SulphurousSharkron>(), NPCType<OldDukeToothBall>()]),

                new Boss(NPCType<DevourerofGodsHead>(), spawnContext: type => {
                    SoundEngine.PlaySound(DevourerofGodsHead.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                }, usesSpecialSound: true, permittedNPCs: [NPCType<DevourerofGodsBody>(), NPCType<DevourerofGodsTail>(), NPCType<CosmicGuardianBody>(), NPCType<CosmicGuardianHead>(), NPCType<CosmicGuardianTail>(),
                NPCType<Signus>(), NPCType<CeaselessVoid>(), NPCType<StormWeaverHead>(), NPCType<StormWeaverBody>(), NPCType<StormWeaverTail>()]),

                new Boss(NPCType<CosmosChampion>(), spawnContext: type => {
                    int erd = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[erd].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(erd);
                }),

                new Boss(NPCType<DreamEater>()),

                new Boss(NPCType<Yharon>(), permittedNPCs: NPCType<Bumblefuck>()),

                new Boss(NPCType<Draedon>(), spawnContext: type =>
                {
                    if (!NPC.AnyNPCs(NPCType<Draedon>()))
                    {
                        Player player = Main.player[ClosestPlayerToWorldCenter];

                        SoundEngine.PlaySound(CodebreakerUI.SummonSound, player.Center);
                        Vector2 spawnPos = player.Center + new Vector2(-8f, -100f);
                        int draedon = NPC.NewNPC(new EntitySource_WorldEvent("CalamityMod_BossRush"), (int)spawnPos.X, (int)spawnPos.Y, NPCType<Draedon>());
                        Main.npc[draedon].timeLeft *= 20;
                    }
                }, usesSpecialSound: true, permittedNPCs: new int[] { NPCType<Apollo>(), NPCType<AresBody>(), NPCType<AresGaussNuke>(), NPCType<AresLaserCannon>(), NPCType<AresPlasmaFlamethrower>(), NPCType<AresTeslaCannon>(), NPCType<Artemis>(), NPCType<ThanatosBody1>(), NPCType<ThanatosBody2>(), NPCType<ThanatosHead>(), NPCType<ThanatosTail>() }),

                new Boss(NPCType<SupremeCalamitas>(), spawnContext: type => {
                    SoundEngine.PlaySound(SupremeCalamitas.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    CalamityUtils.SpawnBossBetter(Main.player[ClosestPlayerToWorldCenter].Top - new Vector2(42, 84f), type);
                }, dimnessFactor: 0.5f, permittedNPCs: [NPCType<SepulcherArm>(), NPCType<SepulcherBody>(), NPCType<SepulcherHead>(), NPCType<SepulcherTail>(), NPCType<SepulcherBodyEnergyBall>(), NPCType<SoulSeekerSupreme>(), NPCType<BrimstoneHeart>(), NPCType<SupremeCataclysm>(), NPCType<SupremeCatastrophe>()]),

                new Boss(NPCType<AbomBoss>())
        ];
    }
}