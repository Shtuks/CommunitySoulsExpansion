using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SacredTools.Common.Systems;
using SacredTools.Content.Items.Materials;
using SacredTools.Content.Items.Placeable.MusicBoxes;
using SacredTools.Content.Items.Placeable.Obelisks;
using SacredTools.Content.Items.Vanity.Masks;
using SacredTools.Items.Consumable.BossSummon;
using SacredTools.Items.Placeable;
using SacredTools.NPCs.Boss.Araghur;
using SacredTools.NPCs.Boss.Erazor;
using SacredTools.NPCs.Boss.Lunarians.Defeat;
using SacredTools.NPCs.Boss.Lunarians.Minion;
using SacredTools.NPCs.Boss.Lunarians;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using SacredTools.NPCs.Boss.Abaddon;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.BossChecklist.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.BossChecklist.Name)]
    public class SoABCLEdits : ModSystem
    {
        public Dictionary<string, float> BossChecklistValues = new Dictionary<string, float>
        {
            { "Abaddon", 18.9f },
            { "Araghur", 19.9f },
            { "Siblings", 21.1f },
            { "Erazor", 24f },
            { "Nihilus", 26.998f }
        };
        public override void PostSetupContent()
        {
            CSEUtils.RemoveFromChecklist(22.16f); //nih
            CSEUtils.RemoveFromChecklist(20.5f); //siblings
            CSEUtils.RemoveFromChecklist(19.5f); //araghur
            CSEUtils.RemoveFromChecklist(21.5f); //erazor
            CSEUtils.RemoveFromChecklist(18.5f); //araghur

            //List<int> abaddonCollection = new List<int>
            //{
            //    ModContent.ItemType<TrophyOblivion>(),
            //    ModContent.ItemType<OblivionMask>(),
            //    ModContent.ItemType<AbaddonMusicBox>()
            //};
            //AddBoss(new List<int> { ModContent.NPCType<Abaddon>() }, BossChecklistValues["Abaddon"], () => DownedSystem.DownedAbaddon, () => true, abaddonCollection, new List<int> { ModContent.ItemType<AbaddonSummon>() }, "Mods.SacredTools.CrossMod.BossChecklist.SpawnInfo.Abaddon");
            //List<int> araghurNPCs = new List<int>
            //{
            //    ModContent.NPCType<AraghurHead>(),
            //    ModContent.NPCType<AraghurBody>(),
            //    ModContent.NPCType<AraghurTail>(),
            //    ModContent.NPCType<AraghurMinion>()
            //};
            //List<int> araghurCollection = new List<int>
            //{
            //    ModContent.ItemType<TrophySerpent_Lame>(),
            //    ModContent.ItemType<SerpentMask>(),
            //    ModContent.ItemType<AraghurMusicBox>()
            //};
            //AddBoss(araghurNPCs, BossChecklistValues["Araghur"], () => DownedSystem.DownedAraghur, () => true, araghurCollection, new List<int> { ModContent.ItemType<SerpentSummon>() }, "Mods.SacredTools.CrossMod.BossChecklist.SpawnInfo.Araghur", null, delegate (SpriteBatch spriteBatch, Rectangle rect, Color color)
            //{
            //    DrawTextureCenteredAndCropped("SacredTools/Assets/Textures/CrossMod/BossChecklist/Araghur", spriteBatch, rect, color);
            //}, new List<string> { "SacredTools/Assets/Textures/CrossMod/BossChecklist/AraghurHead" });
            //List<int> siblingsNPCs = new List<int>
            //{
            //    ModContent.NPCType<Solarius>(),
            //    ModContent.NPCType<SolariusDefeat>(),
            //    ModContent.NPCType<Voxa>(),
            //    ModContent.NPCType<VoxaDefeat>(),
            //    ModContent.NPCType<VoxaTrackingGrenade>(),
            //    ModContent.NPCType<Nuba>(),
            //    ModContent.NPCType<NubaDefeat>(),
            //    ModContent.NPCType<ShadowNuba>(),
            //    ModContent.NPCType<Dustite>(),
            //    ModContent.NPCType<DustiteDefeat>(),
            //    ModContent.NPCType<DustiteClone>(),
            //    ModContent.NPCType<DustiteDragonHead>(),
            //    ModContent.NPCType<DustiteStarReaver>(),
            //    ModContent.NPCType<DustiteStarstreamWyrmHead>(),
            //    ModContent.NPCType<DustiteStarstreamWyrmBody>(),
            //    ModContent.NPCType<DustiteStarstreamWyrmBody2>(),
            //    ModContent.NPCType<DustiteStarstreamWyrmBody3>(),
            //    ModContent.NPCType<DustiteStarstreamWyrmTail>(),
            //    ModContent.NPCType<Novaniel>(),
            //    ModContent.NPCType<NovanielDefeat>()
            //};
            //List<int> siblingsSummon = new List<int>
            //{
            //    ModContent.ItemType<SoranEmblem>(),
            //    ModContent.ItemType<HeirsAuthority>()
            //};
            //List<int> siblingsCollection = new List<int>
            //{
            //    ModContent.ItemType<TrophyLunarians>(),
            //    ModContent.ItemType<SolariusMask>(),
            //    ModContent.ItemType<VoxaMask>(),
            //    ModContent.ItemType<NubaMask>(),
            //    ModContent.ItemType<DustiteMask>(),
            //    ModContent.ItemType<NovanielMask>(),
            //    ModContent.ItemType<LostSiblingsMusicBox>()
            //};
            //AddBoss(siblingsNPCs, BossChecklistValues["Siblings"], () => DownedSystem.DownedSiblings, () => true, siblingsCollection, siblingsSummon, "Mods.SacredTools.CrossMod.BossChecklist.SpawnInfo.LostSiblings", null, delegate (SpriteBatch spriteBatch, Rectangle rect, Color color)
            //{
            //    DrawTextureCenteredAndCropped("SacredTools/Assets/Textures/CrossMod/BossChecklist/Siblings", spriteBatch, rect, color);
            //}, null, Language.GetText("Mods.SacredTools.CrossMod.BossChecklist.NameOverride.LostSiblings"));
            //List<int> erazorNPCs = new List<int>
            //{
            //    ModContent.NPCType<ErazorBoss>(),
            //    ModContent.NPCType<ErazorEnergySphere>(),
            //    ModContent.NPCType<SeekingShadow>()
            //};
            //List<int> erazorCollection = new List<int>
            //{
            //    ModContent.ItemType<TrophyErazor>(),
            //    ModContent.ItemType<ErazorMask>(),
            //    ModContent.ItemType<ErazorMusicBox>()
            //};
            //AddBoss(erazorNPCs, BossChecklistValues["erazor"], () => DownedSystem.DownedErazor, () => Main.expertMode, erazorCollection, new List<int> { ModContent.ItemType<ErosionSample>() }, "Mods.SacredTools.CrossMod.BossChecklist.SpawnInfo.Erazor");

            //List<int> nihilusNPCs = new List<int>
            //{
            //    ModContent.NPCType<Nihilus>(),
            //    ModContent.NPCType<Nihilus2>(),
            //    ModContent.NPCType<Nihilus2Hand>(),
            //    ModContent.NPCType<NihilusAbyssRock>(),
            //    ModContent.NPCType<NihilusAwaken>(),
            //    ModContent.NPCType<NihilusAwakenSpirit>(),
            //    ModContent.NPCType<NihilusBarricade>(),
            //    ModContent.NPCType<NihilusChainBomb>(),
            //    ModContent.NPCType<NihilusChainBombVolatile>(),
            //    ModContent.NPCType<NihilusCrystal>(),
            //    ModContent.NPCType<NihilusCrystalArrive>(),
            //    ModContent.NPCType<NihilusHandBeam>(),
            //    ModContent.NPCType<NihilusLantern>(),
            //    ModContent.NPCType<NihilusLanternBig>(),
            //    ModContent.NPCType<NihilusLanternRisen>(),
            //    ModContent.NPCType<RelicShieldNihilus>()
            //};
            //List<int> nihilusCollection = new List<int>
            //{
            //    ModContent.ItemType<TrophyNihilus>(),
            //    ModContent.ItemType<NihilusMask>(),
            //    ModContent.ItemType<Dreamscape1MusicBox>()
            //};
            //AddBoss(new List<int> { ModContent.NPCType<NihilusAwaken>() }, BossChecklistValues["Nihilus"], () => false, () => Main.expertMode && DownedSystem.DownedErazor && !DownedSystem.DownedNihilus, null, new List<int> { ModContent.ItemType<NihilusObelisk>() }, "Mods.SacredTools.CrossMod.BossChecklist.SpawnInfo.Nihilus", null, delegate (SpriteBatch spriteBatch, Rectangle rect, Color color)
            //{
            //    DrawTextureCenteredAndCropped("SacredTools/Assets/Textures/CrossMod/BossChecklist/NihilusFace", spriteBatch, rect, color);
            //}, new List<string> { "SacredTools/Assets/Textures/CrossMod/BossChecklist/Doem" }, Language.GetText("Mods.SacredTools.CrossMod.BossChecklist.NameOverride.UnknownBoss"));
            //AddBoss(nihilusNPCs, BossChecklistValues["Nihilus"], () => DownedSystem.DownedNihilus, () => Main.expertMode && DownedSystem.DownedErazor && DownedSystem.DownedNihilus, nihilusCollection, new List<int> { ModContent.ItemType<NihilusObelisk>() }, "Mods.SacredTools.CrossMod.BossChecklist.SpawnInfo.Nihilus", null, delegate (SpriteBatch spriteBatch, Rectangle rect, Color color)
            //{
            //    DrawTextureCenteredAndCropped("SacredTools/Assets/Textures/CrossMod/BossChecklist/Nihilus", spriteBatch, rect, color);
            //}, new List<string> { "SacredTools/Assets/Textures/CrossMod/BossChecklist/NihilusHead" });
        }

        private void AddBoss(List<int> npcIDs, float progression, Func<bool> downed, Func<bool> visible, List<int> specialItems, List<int> spawnItems, string spawnInfo = null, Func<NPC, LocalizedText> despawnMessage = null, Action<SpriteBatch, Rectangle, Color> portrait = null, List<string> headTextures = null, LocalizedText nameOverride = null)
        {
            ModNPC mainNPC = NPCLoader.GetNPC(npcIDs[0]);
            Dictionary<string, object> additionalData = new Dictionary<string, object>();
            additionalData["spawnItems"] = spawnItems;
            additionalData["collectibles"] = specialItems;
            if (nameOverride != null)
            {
                additionalData.Add("displayName", nameOverride);
            }
            if (visible != null)
            {
                additionalData["availability"] = visible;
            }
            if (headTextures != null)
            {
                additionalData["overrideHeadTextures"] = headTextures;
            }
            if (despawnMessage != null)
            {
                additionalData["despawnMessage"] = despawnMessage;
            }
            if (portrait != null)
            {
                additionalData["customPortrait"] = portrait;
            }
            ModCompatibility.BossChecklist.Mod.Call("LogBoss", Mod, mainNPC.Name, progression, downed, npcIDs, additionalData);
        }

        private static void DrawTextureCenteredAndCropped(string texturePath, SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            Texture2D texture = ModContent.Request<Texture2D>(texturePath, (AssetRequestMode)2).Value;
            int frameWidth = Math.Min(rect.Width, texture.Width);
            int frameHeight = Math.Min(rect.Height, texture.Height);
            int frameX = Math.Max(0, texture.Width - rect.Width) / 2;
            int frameY = Math.Max(0, texture.Height - rect.Height) / 2;
            spriteBatch.Draw(sourceRectangle: new Rectangle(frameX, frameY, frameWidth, frameHeight), texture: texture, position: rect.Center.ToVector2() + new Vector2(frameX, frameY), color: color, rotation: 0f, origin: texture.Size() * 0.5f, scale: Vector2.One, effects: SpriteEffects.None, layerDepth: 0f);
        }
    }
}