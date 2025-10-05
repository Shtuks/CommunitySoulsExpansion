global using LumUtils = Luminance.Common.Utilities.Utilities;
global using FargowiltasSouls.Core.ModPlayers;
global using FargowiltasSouls.Core.Toggler;
using ssm.Content.Sky;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using System;
using ssm.Core;
using ssm.Systems;
using ssm.Thorium;
using System.Collections.Generic;
using ssm.SoA;
using ssm.Redemption;
using System.Linq;
using Terraria.Localization;
using ssm.Content.Items.Summons;
//using ssm.Content.NPCs.MutantEX;
using ssm.Content.UI;
using ssm.Calamity.Addons.CalamityAmmo;
using Terraria.UI;
using ssm.CrossMod.CraftingStations;
using ssm.gunrightsmod;
using ssm.SpiritMod;
using Fargowiltas.Items.CaughtNPCs;
using System.Collections;
 using ssm.AlchemistNPC;
using ssm.Consolaria;
using ssm.Content.Items.Consumables;
using System.IO;
using FargowiltasSouls.Core.Globals;
using Terraria.Chat;
using Terraria.ID;
using FargowiltasSouls.Core.Systems;
using ssm.Content.Items.Accessories;
using ssm.Content.NPCs.Guntera;
using ssm.Content.NPCs.Ceiling;
using FargowiltasSouls.Content.Items.Materials;
//using ssm.Content.NPCs.MutantEX.HitPlayer;
//using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using ssm.Thorium.Enchantments;

namespace ssm
{
    public partial class ssm : Mod
    {
        // Swarms
        public static bool EndgameSwarmActive;
        public static bool PostMLSwarmActive;
        public static bool LateHardmodeSwarmActive;
        public static bool HardmodeSwarmActive;
        public static bool SwarmNoHyperActive;
        public static int SwarmItemsUsed;
        internal static bool SwarmSetDefaults;
        public static bool SwarmActive;
        public static int SwarmKills;
        public static int SwarmTotal;
        public static int SwarmSpawned;

        public UserInterface _bossSummonUI;
        public BossSummonUI _bossSummonState;
        public bool _showBossSummonUI;

        internal static ssm Instance;
        public static bool debug = CSEConfig.Instance.DebugMode;

        public static float OldMusicFade;

        private delegate void UIModDelegate(object instance, SpriteBatch spriteBatch);
        public static bool amiactive;
        public static readonly BindingFlags UniversalBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public static bool legit;
        public static int OS;
        public static readonly List<(string bossName, float newProgression)> bclChanges = new List<(string, float)>();
        public static string userName = Environment.UserName;
        public static string filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";

        public static bool shouldUseMacro = false;
        public override uint ExtraPlayerBuffSlots => 100u;

        public static List<int> DebuffsList;
        public static void Add(string internalName, int id)
        {
            CaughtNPCItem item = new(internalName, id);
            Instance.AddContent(item);
            FieldInfo info = typeof(CaughtNPCItem).GetField("CaughtTownies", LumUtils.UniversalBindingFlags);
            Dictionary<int, int> list = (Dictionary<int, int>)info.GetValue(info);
            list.Add(id, item.Type);
            info.SetValue(info, list);
        }

        private void LoadAllDebuffs()
        {
            DebuffsList.Clear();

            for (int i = 1; i < BuffID.Count; i++)
            {
                if (Main.debuff[i] && !BuffID.Sets.NurseCannotRemoveDebuff[i] && i != BuffID.Lovestruck)
                {
                    DebuffsList.Add(i);
                }
            }

            for (int i = BuffID.Count; i < BuffLoader.BuffCount; i++)
            {
                if (Main.debuff[i])
                {
                    DebuffsList.Add(i);
                }
            }
        }
        public static void ManageMusicTimestop(bool playMusicAgain)
        {
            if (Main.dedServ)
                return;

            if (playMusicAgain)
            {
                if (OldMusicFade > 0)
                {
                    Main.musicFade[Main.curMusic] = OldMusicFade;
                    OldMusicFade = 0;
                }
            }
            else
            {
                if (OldMusicFade == 0)
                {
                    OldMusicFade = Main.musicFade[Main.curMusic];
                }
                else
                {
                    for (int i = 0; i < Main.musicFade.Length; i++)
                        Main.musicFade[i] = 0f;
                }
            }
        }
        public enum PacketID : byte
        {
            SpawnFishronEX,
            HealthDataSync,
            RequestCyberOrbs
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte data = reader.ReadByte();
            if (Enum.IsDefined(typeof(PacketID), data))
            {
                switch ((PacketID)data)
                {
                    case PacketID.SpawnFishronEX:
                        HandleSpawnFishronEX(reader, whoAmI);
                        break;

                    case PacketID.HealthDataSync:
                        HandleHealthDataSync(reader, whoAmI);
                        break;
                    case PacketID.RequestCyberOrbs:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            byte p = reader.ReadByte();
                            int multiplier = reader.ReadByte();
                            int n = NPC.NewNPC(NPC.GetBossSpawnSource(p), (int)Main.player[p].Center.X, (int)Main.player[p].Center.Y, ModContent.NPCType<CyberneticOrb>(), 0,
                                p, 0f, multiplier, 0);
                            if (n != Main.maxNPCs)
                            {
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                            }
                        }
                        break;
                }
            }
        }

        private void HandleSpawnFishronEX(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                byte target = reader.ReadByte();
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                EModeGlobalNPC.spawnFishronEX = true;
                NPC.NewNPC(NPC.GetBossSpawnSource(target), x, y, NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, target);
                ChatHelper.BroadcastChatMessage(
                    NetworkText.FromKey("Announcement.HasAwoken",
                    Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DukeFishronEX.DisplayName")),
                    new Color(50, 100, 255));
            }
        }

        private void HandleHealthDataSync(BinaryReader reader, int whoAmI)
        {
            byte playerId = reader.ReadByte();
            Player player = Main.player[playerId];

            //if (player.TryGetModPlayer(out MonstrHealthPlayer modPlayer))
            //{
            //    modPlayer.OriginalMaxLife = reader.ReadInt32();
            //    modPlayer.HealthReduction = reader.ReadInt32();

            //    if (Main.netMode == NetmodeID.Server)
            //    {
            //        modPlayer.SyncData();
            //    }

            //    if (Main.netMode == NetmodeID.MultiplayerClient)
            //    {
            //        player.statLifeMax = modPlayer.OriginalMaxLife - modPlayer.HealthReduction;
            //        if (player.statLife > player.statLifeMax)
            //        {
            //            player.statLife = player.statLifeMax;
            //        }
            //    }
            //}
        }
        public static int SwarmMinDamage
        {
            get
            {
                float num = ((!ssm.HardmodeSwarmActive) ? ((float)(50 + 3 * ssm.SwarmItemsUsed)) : ((float)(60 + 40 * ssm.SwarmItemsUsed)));
                return (int)num;
            }
        }

        public void AddBCL(string type, string bossName, float priority, List<int> npcIDs, Func<bool> downed, Func<bool> available, List<int> collectibles, List<int> spawnItems, bool hasKilledAllMessage, string portrait = null)
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                static bool AllPlayersAreDead() => Main.player.All(plr => !plr.active || plr.dead);

                bossChecklist.Call(
                $"Log{type}",
                this,
                bossName,
                priority,
                downed,
                npcIDs,
                new Dictionary<string, object>()
                {
                            { "spawnItems", spawnItems },
                            { "availability", available },
                            { "despawnMessage", hasKilledAllMessage ? new Func<NPC, LocalizedText>(npc =>
                                        AllPlayersAreDead() ? Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.KilledAllMessage") : Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.DespawnMessage")) :
                                    Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.DespawnMessage") },
                            {
                                "customPortrait",
                                portrait == null ? null : new Action<SpriteBatch, Rectangle, Color>((spriteBatch, rect, color) =>
                                {
                                    Texture2D tex = Assets.Request<Texture2D>(portrait, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                                    Rectangle sourceRect = tex.Bounds;
                                    float scale = Math.Min(1f, (float)rect.Width / sourceRect.Width);
                                    spriteBatch.Draw(tex, rect.Center.ToVector2(), sourceRect, color, 0f, sourceRect.Size() / 2, scale, SpriteEffects.None, 0);
                                })
                            }
                    }
                );
            }
        }
        public static void ChangeBossProgressions(params (string name, float newProgression)[] changes)
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                // get access to bossTracker
                object bossTracker = ModCompatibility.BossChecklist.Mod.GetType()
                    .GetField("bossTracker", BindingFlags.NonPublic | BindingFlags.Static)
                    .GetValue(null);

                // get entries list
                FieldInfo sortedEntriesField = bossTracker.GetType()
                    .GetField("SortedEntries", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                IList entries = (IList)sortedEntriesField.GetValue(bossTracker);

                // prepare for reflection
                PropertyInfo displayNameProperty = null;
                FieldInfo progressionField = null;
                var entriesToChange = new List<(object entry, float newProg)>();

                // find all entries
                foreach (object entry in entries)
                {
                    if (displayNameProperty == null)
                    {
                        displayNameProperty = entry.GetType().GetProperty("DisplayName",
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        progressionField = entry.GetType().GetField("progression",
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        if (displayNameProperty == null || progressionField == null)
                            throw new InvalidOperationException("Required fields was not found.");
                    }

                    string currentName = (string)displayNameProperty.GetValue(entry);
                    foreach (var change in changes)
                    {
                        if (currentName == change.name)
                        {
                            entriesToChange.Add((entry, change.newProgression));
                            break;
                        }
                    }
                }

                // apply edits
                foreach (var change in entriesToChange)
                {
                    progressionField.SetValue(change.entry, change.newProg);
                }

                // re-sort
                List<object> sortedList = new List<object>();
                foreach (object entry in entries)
                    sortedList.Add(entry);

                sortedList.Sort((x, y) =>
                {
                    float xProg = (float)progressionField.GetValue(x);
                    float yProg = (float)progressionField.GetValue(y);
                    return xProg.CompareTo(yProg);
                });

                // update original list
                entries.Clear();
                foreach (object entry in sortedList)
                    entries.Add(entry);
            }
        }

        //basicaly same but for removing
        private static void RemoveFromChecklist(params float[] progressions)
        {
            object bossTracker = ModCompatibility.BossChecklist.Mod.GetType()
                .GetField("bossTracker", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

            FieldInfo sortedEntriesField = bossTracker.GetType()
                .GetField("SortedEntries", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            object originalEntries = sortedEntriesField.GetValue(bossTracker);

            Type entryCollectionType = originalEntries.GetType();
            object newEntries = Activator.CreateInstance(entryCollectionType);
            IList newEntriesCasted = (IList)newEntries;

            bool isFirst = true;
            FieldInfo progressionField = null;

            foreach (object entry in (IEnumerable)originalEntries)
            {
                if (isFirst)
                {
                    progressionField = entry.GetType()
                        .GetField("progression", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (progressionField == null)
                    {
                        throw new InvalidOperationException("�� ������� ���� 'progression' � ������� ������");
                    }
                    isFirst = false;
                }

                float entryProgression = (float)progressionField.GetValue(entry);

                bool keepEntry = true;
                foreach (float progression in progressions)
                {
                    if (Math.Abs(entryProgression - progression) < 0.001f)
                    {
                        keepEntry = false;
                        break;
                    }
                }

                if (keepEntry)
                {
                    newEntriesCasted.Add(entry);
                }
            }

            sortedEntriesField.SetValue(bossTracker, newEntries);
        }
        private void BossChecklistCompatibility()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {

                //AddBCL("Boss",
                //    "CeilingOfMoonlord",
                //    19.5f,
                //    new List<int> { ModContent.NPCType<CeilingOfMoonLord>() },
                //    () => WorldSavingSystem.DownedFishronEX,
                //    () => true,
                //    new List<int> {
                //        ModContent.ItemType<DeviatingEnergy>(),
                //    },
                //    new List<int> { /*ModContent.ItemType<TruffleWormEX>()*/ },
                //    true
                //);
                //AddBCL("Boss",
                //    "Guntera",
                //    22.91f,
                //    new List<int> { ModContent.NPCType<Guntera>() },
                //    () => WorldSavingSystem.DownedFishronEX,
                //    () => true,
                //    new List<int> {
                //        ModContent.ItemType<AbomEnergy>(),
                //    },
                //    new List<int> { /*ModContent.ItemType<TruffleWormEX>()*/ },
                //    true
                //);

                AddBCL("Boss",
                    "DukeFishronEX",
                    27.5f,
                    new List<int> { NPCID.DukeFishron },
                    () => WorldSavingSystem.DownedFishronEX,
                    () => true,
                    new List<int> {
                        ModContent.ItemType<CyclonicFin>(),
                    },
                    new List<int> { ModContent.ItemType<TruffleWormEX>() },
                    true
                );

                //if (CSEConfig.Instance.AlternativeSiblings) {
                //    AddBCL("Boss",
                //        "MutantEX",
                //        float.MaxValue - 1,
                //        new List<int> { ModContent.NPCType<MutantEX>() },
                //        () => WorldSaveSystem.downedMutantEX,
                //        () => true,
                //        new List<int> {
                //        ModContent.ItemType<Sadism>(),
                //        },
                //        new List<int> { ModContent.ItemType<MutantsForgeItem>() },
                //        true
                //    );
                //}

                //Add("Boss",
                //    "Echdeath",
                //    float.MaxValue,
                //    new List<int> { ModContent.NPCType<Echdeath>() },
                //    () => WorldSaveSystem.downedMutantEX,
                //    () => true,
                //    new List<int> {
                //        ModContent.ItemType<Sadism>(),
                //    },
                //    new List<int> { ModContent.ItemType<MutantsForgeItem>() },
                //    true
                //);
            }
        }

        public override void Load()
        {
            DebuffsList = new List<int>();

            int enabledCount = 0;
            if (ModCompatibility.SpiritMod.Loaded) enabledCount++;
            if (ModCompatibility.Thorium.Loaded) enabledCount++;
            if (ModCompatibility.Calamity.Loaded) enabledCount++;
            if (ModCompatibility.SacredTools.Loaded) enabledCount++;
            if (ModCompatibility.Redemption.Loaded || ModCompatibility.Polarities.Loaded || ModCompatibility.Spooky.Loaded || ModCompatibility.Homeward.Loaded) enabledCount++;
            shouldUseMacro = enabledCount >= 2;

            _bossSummonUI = new UserInterface();
            ModIntergationSystem.BossChecklist.AdjustValues();

            Instance = this;
            OS = OSType();

            if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
            {
                ThoriumCaughtNpcs.ThoriumRegisterItems();
            }
            if (ModLoader.TryGetMod("SacredTools", out Mod soa))
            {
                SoACaughtNpcs.SoARegisterItems();
            }
            if (ModLoader.TryGetMod("Redemption", out Mod red))
            {
                RedemptionCaughtNpcs.RedemptionRegisterItems();
            }
            if (ModLoader.TryGetMod("gunrightsmod", out Mod grm))
            {
                gunrightsmodCaughtNpcs.gunrightsmodRegisterItems();
            }
            if (ModLoader.TryGetMod("SpiritMod", out Mod spr))
            {
                SpiritModCaughtNpcs.SpiritModRegisterItems();
            }
            if (ModLoader.TryGetMod("AlchemistNPC", out Mod alch))
            {
                AlchemistNPCCaughtNpcs.AlchemistNPCCaughtNpcsRegisterItems();
            }
            if (ModLoader.TryGetMod("Consolaria", out Mod _))
            {
                ConsolariaCaughtNpcs.ConsolariaRegisterItems();
            }
        //    if (ModLoader.TryGetMod("XDContentMod", out Mod XD))
        //    {
        //        XDContentModCaughtNPCs.XDContentModRegisterItems();
        //    }
            if (ModLoader.TryGetMod("CalamityAmmo", out Mod _))
            {
                CalamityAmmoCaughtNPCs.CalamityAmmoRegisterItems();
            }
            SkyManager.Instance["ssm:MutantEX"] = new MutantEXSky();
            SkyManager.Instance["ssm:RealMutantEX"] = new RealMutantEXSky();
            SkyManager.Instance["ssm:MutantMonolith"] = new MutantSkyMonolith();
        }
        public override void Unload()
        {
            _bossSummonUI = null;
            Instance = null;
        }
        public void ShowBossSummonUI()
        {
            if (_bossSummonState == null)
            {
                _bossSummonState = new BossSummonUI();
                _bossSummonUI.SetState(_bossSummonState);
            }
            _showBossSummonUI = true;
        }
        public override void PostSetupContent()
        {
            LoadAllDebuffs();
            //if (!ModCompatibility.Inheritance.Loaded)
            //{
            //    DPSLimitGlobalNPC.BossDPSLimits.Add(ModContent.NPCType<MutantBoss>(), 600000);
            //}
            if (ModLoader.TryGetMod("Wikithis", out Mod wikithis) && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "https://terrariamods.wiki.gg/wiki/Community_Souls_Expansion/{}");
            }
            BossChecklistCompatibility();
            if (ModCompatibility.Thorium.Loaded)
            {
                //bclChanges.Union(ThoriumBCLEdits.BossChecklistValues);
                PostSetupContentThorium.PostSetupContent_Thorium();
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                //bclChanges.Union(SoABCLEdits.BossChecklistValues);
                PostSetupContentSoA.PostSetupContent_Thorium();
            }

            if (ModCompatibility.BossChecklist.Loaded)
            {
                var changes = new List<(string, float)>
                {
                    ("Slime God", 18.7f),
                    ("3rd Omega Prototype", 18.99f),
                    ("Ordeals", 20.4f),
                    ("Patient Zero", 20.0002f),
                    ("The Lifebringer", 19.57f),
                    ("The Overwatcher", 19.58f),
                    ("The Materealizer", 19.59f),
                    ("Scarab Belief", 20.9f),
                    ("World's End Everlasting Falling Whale", 21.9f),
                    ("Nebuleus", 26f),
                    ("ThePrimordials", 19.5f),
                    ("Nihilus", 26.99f),
                    ("The Entropic God, Noxus", 27.001f),
                    ("Erazor", 25f),
                    ("Abaddon, the Source of the Affliction", 18.9f),
                    ("Araghur, the Flare Serpent", 19.9f),
                    ("The Lost Siblings", 21.1f)
                };

                ChangeBossProgressions(changes.ToArray());
                RemoveFromChecklist(22.16f);
            }

            //ChangeBossProgressions(bclChanges.ToArray());

            //if (CSEConfig.Instance.DevItems)
            //{
            //    int[] SwordsToApplyRework = [ModContent.ItemType<Pucheblade>()];
            //    SwordGlobalItem.AllowedModdedSwords = SwordGlobalItem.AllowedModdedSwords.Union(SwordsToApplyRework).ToArray();
            //}
        }
        public int OSType()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID platform = os.Platform;
            switch (platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";
                    return 0;
                case PlatformID.Unix:
                    filePath = "/home/" + userName + "/.local/share/Terraria/tModLoader/StarlightSouls";
                    return 1;
                case PlatformID.MacOSX:
                    filePath = "/Users/" + userName + "/Library/Application Support/Terraria/tModLoader/StarlightSouls";
                    return 2;
                default:
                    filePath = Main.SavePath + "/ModLoader/StarlightSouls";
                    break;
            }
            return -1;
        }
    }
}
