using Terraria.ModLoader;
using Terraria;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using FargowiltasSouls.Content.Buffs.Boss;
using ssm.Core;
using static Terraria.ModLoader.ModContent;
using Redemption.NPCs.Bosses.Erhan;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.Bosses.Keeper;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.ADD;
using Redemption.NPCs.Bosses.PatientZero;
using Fargowiltas.NPCs;
using Redemption.Items.Placeable.Tiles;
using Redemption.Globals;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionNPCS : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Nebuleus>() || npc.type == ModContent.NPCType<Nebuleus2>())
            {
                if (Main.expertMode && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<AbomPresenceBuff>(), 2);
            }
            return base.PreAI(npc);
        }

        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCType<LumberJack>())
            {
                shop.Add(new Item(ItemType<ElderWood>()) { shopCustomPrice = Item.buyPrice(copper: 20) }, new Condition("Mods.ssm.Conditions.TheKeeperDowned", () => RedeBossDowned.downedKeeper));
                shop.Add(new Item(ItemType<PetrifiedWood>()) { shopCustomPrice = Item.buyPrice(copper: 20) }, Condition.Hardmode);
            }
            base.ModifyShop(shop);
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
                if (npc.type == NPCType<Erhan>() || npc.type == NPCType<Thorn>() || npc.type == NPCType<Keeper>() || npc.type == NPCType<SoI>())
                {
                    npc.lifeMax = num1 * ssm.SwarmItemsUsed;
                    npc.damage = num12 * ssm.SwarmItemsUsed;
                }
                if (npc.type == NPCType<KS3>() || npc.type == NPCType<OmegaCleaver>() || npc.type == NPCType<Gigapora>())
                {
                    npc.lifeMax = num2 * ssm.SwarmItemsUsed;
                    npc.damage = num22 * ssm.SwarmItemsUsed;
                }
                if (npc.type == NPCType<Ukko>() || npc.type == NPCType<Akka>() || npc.type == NPCType<PZ>() || npc.type == NPCType<Nebuleus>())
                {
                    npc.lifeMax = num3 * ssm.SwarmItemsUsed;
                    npc.damage = num32 * ssm.SwarmItemsUsed;
                }
            }
        }
    }
}
