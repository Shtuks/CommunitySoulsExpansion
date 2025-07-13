using Microsoft.Xna.Framework.Graphics;
using ssm.Core;
using System.Collections.Generic;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials;
using ThoriumMod.Items.MasterMode;
using ThoriumMod.Items.Placeable.Relics;
using ThoriumMod.Items.Placeable;
using ThoriumMod.NPCs.BossThePrimordials;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using ReLogic.Content;
using ThoriumMod;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.BossChecklist.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.BossChecklist.Name)]
    public class ThoriumBCLEdits : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod("WHummusMultiModBalancing");
        }

        public Dictionary<string, float> BossChecklistValues = new Dictionary<string, float>
        {
            { "ThePrimordials", 21.5f },
        };
        public override void PostSetupContent()
        {
            CSEUtils.RemoveFromChecklist(19.5f); //ragnarok

            DoBossChecklistSupport_AddEntry("LogBoss", "ThePrimordials", BossChecklistValues["ThePrimordials"], () => ThoriumWorld.downedThePrimordials, new List<int>
            {
                ModContent.NPCType<DreamEater>(),
                ModContent.NPCType<SlagFury>(),
                ModContent.NPCType<Omnicide>(),
                ModContent.NPCType<Aquaius>()
            }, new List<int>
            {
                ModContent.ItemType<ThePrimordialsRelic>(),
                ModContent.ItemType<ThePrimordialsTrophy>(),
                ModContent.ItemType<SlagMask>(),
                ModContent.ItemType<OmniMask>(),
                ModContent.ItemType<AquaMask>(),
                ModContent.ItemType<ThePrimordialsMusicBox>(),
                ModContent.ItemType<DoomSayersPenny>()
            }, ModContent.ItemType<DoomSayersCoin>(), null, delegate (SpriteBatch spriteBatch, Rectangle rect, Color color)
            {
                DrawSingleImage(spriteBatch, ModContent.Request<Texture2D>("ThoriumMod/ModSupport/ModSupportModules/BossChecklistPortraits/ThePrimordials", (AssetRequestMode)2), rect, color);
            });
        }

        private static void DrawSingleImage(SpriteBatch spriteBatch, Asset<Texture2D> asset, Rectangle rect, Color color)
        {
            Texture2D texture = asset.Value;
            Vector2 centered = new Vector2(rect.X + rect.Width / 2 - texture.Width / 2, rect.Y + rect.Height / 2 - texture.Height / 2);
            spriteBatch.Draw(texture, centered, color);
        }
        private void DoBossChecklistSupport_AddEntry(string message, string internalName, float weight, Func<bool> downed, int bossType, List<int> collectibles, int summonItemType, LocalizedText spawnInfoOverride = null, Action<SpriteBatch, Rectangle, Color> customPortrait = null, string overrideHeadTexture = null)
        {
            DoBossChecklistSupport_AddEntry(message, internalName, weight, downed, new List<int> { bossType }, collectibles, new List<int> { summonItemType }, spawnInfoOverride, customPortrait, overrideHeadTexture);
        }

        private void DoBossChecklistSupport_AddEntry(string message, string internalName, float weight, Func<bool> downed, List<int> bossTypes, List<int> collectibles, int summonItemType, LocalizedText spawnInfoOverride = null, Action<SpriteBatch, Rectangle, Color> customPortrait = null, string overrideHeadTexture = null)
        {
            DoBossChecklistSupport_AddEntry(message, internalName, weight, downed, bossTypes, collectibles, new List<int> { summonItemType }, spawnInfoOverride, customPortrait, overrideHeadTexture);
        }

        private void DoBossChecklistSupport_AddEntry(string message, string internalName, float weight, Func<bool> downed, int bossType, List<int> collectibles, List<int> summonItemTypes, LocalizedText spawnInfoOverride = null, Action<SpriteBatch, Rectangle, Color> customPortrait = null, string overrideHeadTexture = null)
        {
            this.DoBossChecklistSupport_AddEntry(message, internalName, weight, downed, new List<int> { bossType }, collectibles, summonItemTypes, spawnInfoOverride, customPortrait, overrideHeadTexture);
        }

        private void DoBossChecklistSupport_AddEntry(string message, string internalName, float weight, Func<bool> downed, List<int> bossTypes, List<int> collectibles, List<int> summonItemTypes, LocalizedText spawnInfoOverride = null, Action<SpriteBatch, Rectangle, Color> customPortrait = null, string overrideHeadTexture = null)
        {
            Dictionary<string, object> bonusData = new Dictionary<string, object>
            {
                ["spawnItems"] = summonItemTypes,
                ["collectibles"] = collectibles
            };
            if (spawnInfoOverride != null)
            {
                bonusData["spawnInfo"] = spawnInfoOverride;
            }
            if (customPortrait != null)
            {
                bonusData["customPortrait"] = customPortrait;
            }
            if (overrideHeadTexture != null)
            {
                bonusData["overrideHeadTextures"] = overrideHeadTexture;
            }
            ModCompatibility.BossChecklist.Mod.Call(message, base.Mod, internalName, weight, downed, bossTypes, bonusData);
        }
    }
}
