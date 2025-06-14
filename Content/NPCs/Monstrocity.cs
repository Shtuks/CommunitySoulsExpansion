using System.Collections.Generic;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using Fargowiltas.NPCs;
using System.Linq;
using ssm.Items;

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
            "Starlightwilta",
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
        private void TryAddItem(Item item, Dictionary<MonsterShopGroup, SortedSet<int>> itemCollections)
        {
            var shopGroup = MonsterSells(item, out MonsterSellType sellType);
            switch (sellType)
            {
                case MonsterSellType.SoldByMonster:
                    itemCollections[shopGroup].Add(item.type);
                    break;

                case MonsterSellType.SomeMaterialsSold:
                    foreach (var recipe in Main.recipe.Where(recipe => recipe.HasResult(item.type)))
                    {
                        foreach (var material in recipe.requiredItem)
                        {
                            if (material.ModItem is not null && material.ModItem.Name.EndsWith(shopGroup.ToString()))
                            {
                                itemCollections[shopGroup].Add(material.type);
                            }
                        }
                    }
                    break;

                case MonsterSellType.CraftableMaterialsSold:
                    var materialTypes = new HashSet<int>(Main.recipe.SelectMany(recipe => recipe.requiredItem.Select(item => item.type)).Where(type => type != ItemID.None));
                    foreach (var recipe in Main.recipe.Where(recipe => recipe.HasResult(item.type)))
                    {
                        foreach (var material in recipe.requiredItem)
                        {
                            if (material.type != ItemID.None && materialTypes.Contains(material.type))
                            {
                                itemCollections[shopGroup].Add(material.type);
                            }
                        }
                    }
                    break;

                case MonsterSellType.SoldAtThirtyStack:
                    if (item.stack >= 30)
                    {
                        itemCollections[shopGroup].Add(item.type);
                    }

                    break;

                default:
                    break;
            }
        }
        private static bool IsCSEItem(Item item)
        {
            if (item.ModItem is not null)
            {
                string modName = item.ModItem.Mod.Name;
                return modName.Equals("ssm");
            }

            return false;
        }
        public static MonsterShopGroup MonsterSells(Item item, out MonsterSellType sellType)
        {
            if (IsCSEItem(item))
            {
                if (item.ModItem.Name.EndsWith("Enchant"))
                {
                    sellType = MonsterSellType.SoldByMonster;
                    return MonsterShopGroup.Enchant;
                }
                else if (item.ModItem.Name.EndsWith("Essence"))
                {
                    sellType = MonsterSellType.SoldByMonster;
                    return MonsterShopGroup.Essence;
                }
                else if (item.ModItem.Name.EndsWith("Force"))
                {
                    sellType = MonsterSellType.SomeMaterialsSold;
                    return MonsterShopGroup.Enchant;
                }
                else if (item.ModItem.Name.EndsWith("Soul"))
                {
                    foreach (Recipe recipe in Main.recipe.Where(recipe => recipe.HasResult(item.type)))
                    {
                        foreach (Item material in recipe.requiredItem)
                        {
                            if (material.type != ItemID.None && material.ModItem != null)
                            {
                                if (material.ModItem.Name.EndsWith("Essence"))
                                {
                                    sellType = MonsterSellType.SomeMaterialsSold;
                                    return MonsterShopGroup.Essence;
                                }
                                else if (material.ModItem.Name.EndsWith("Force"))
                                {
                                    sellType = MonsterSellType.SomeMaterialsSold;
                                    return MonsterShopGroup.Force;
                                }
                                else if (material.ModItem.Name.EndsWith("Soul"))
                                {
                                    sellType = MonsterSellType.SomeMaterialsSold;
                                    return MonsterShopGroup.Soul;
                                }
                            }
                        }
                    }

                    sellType = MonsterSellType.SoldByMonster;
                    return MonsterShopGroup.Soul;
                }
            }
            sellType = MonsterSellType.End;
            return MonsterShopGroup.End;
        }
        private List<int> GetSellableItems()
        {
            Dictionary<MonsterShopGroup, SortedSet<int>> itemCollections = new();
            for (int i = 0; i < (int)MonsterShopGroup.End; i++)
            {
                itemCollections[(MonsterShopGroup)i] = new SortedSet<int>();
            }

            foreach (var player in Main.player.Where(p => p.active))
            {
                foreach (Item item in player.inventory)
                {
                    TryAddItem(item, itemCollections);
                }

                foreach (Item item in player.armor)
                {
                    TryAddItem(item, itemCollections);
                }

                foreach (Item item in player.bank.item)
                {
                    TryAddItem(item, itemCollections);
                }
            }

            return itemCollections.OrderBy(kv => kv.Key).SelectMany(kv => kv.Value).ToList();
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
            showCycleShop = GetSellableItems().Count / Chest.maxItems > 0 && !ModLoader.TryGetMod("ShopExpander", out _);

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

            if (shopNum > GetSellableItems().Count / Chest.maxItems)
            {
                shopNum = 0;
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
        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            int nextSlot = 0;
            int index = 0;
            int startOffset = shopNum * Chest.maxItems;

            List<int> sellableItems = GetSellableItems();

            foreach (int type in sellableItems)
            {
                if (++index < startOffset) 
                {
                    continue;
                }

                if (nextSlot >= Chest.maxItems)
                {
                    break;
                }

                var item = new Item(type);
                int price;
                bool medals = false;

                if (type == ItemID.RodOfHarmony)
                {
                    price = 250;
                    medals = true;
                }
                else
                {
                    price = item.value * 2;
                }

                if (medals)
                {
                    items[nextSlot] = new Item(type) { shopCustomPrice = Item.buyPrice(copper: price), shopSpecialCurrency = CustomCurrencyID.DefenderMedals };
                }
                else
                {
                    items[nextSlot] = new Item(type) { shopCustomPrice = Item.buyPrice(copper: price) };
                }

                nextSlot++;
            }
        }
    }
    public enum MonsterSellType
    {
        SoldByMonster,
        SomeMaterialsSold,
        CraftableMaterialsSold,
        SoldAtThirtyStack,
        End
    }
    public enum MonsterShopGroup
    {
        Enchant,
        Essence,
        Force,
        Soul,
        Potion,
        Other,
        End
    }
}
