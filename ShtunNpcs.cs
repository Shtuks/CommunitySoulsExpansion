using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Content.NPCs;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool isWaterEnemy;
        public int chtuxlagorInferno;
        public static int ECH = -1;
        public static int DukeEX = -1;
        public static int boss = -1;
        public static int mutantEX = -1;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.ImmuneToRegularBuffs[ModContent.NPCType<MutantBoss>()] = true;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (chtuxlagorInferno > 0)
                ApplyDPSDebuff(npc.lifeMax / 10, npc.lifeMax / 100, ref npc.lifeRegen, ref damage);
        }

        public override void PostAI(NPC npc)
        {
            if (chtuxlagorInferno > 0)
            {
                chtuxlagorInferno--;
            }
        }
        
        public void ApplyDPSDebuff(int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
        {
            if (lifeRegen > 0)
            {
                lifeRegen = 0;
            }

            lifeRegen -= lifeRegenValue;
            if (damage < damageValue)
            {
                damage = damageValue;
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            //if((projectile.type == ModContent.ProjectileType<ShtuxiumBlast>() || projectile.type == ModContent.ProjectileType<ShtuxiumBlast2>() || projectile.type == ModContent.ProjectileType<ShtuxiumBlast3>()) && npc.life > npc.lifeMax / 100)
            //{
            //    npc.life -= npc.lifeMax / 100;
            //}
        }

    //    public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
    //    {
    //        if(npc.type == ModContent.NPCType<Squirrel>())
    //        {
    //            int nextSlot = 0; //ignore pylon and anything else inserted into shop ( how does this work in new system?
    //            int index = 0;
    //            int startOffset = shopNum * Chest.maxItems;

    //            List<int> sellableItems = GetSellableItems();
    //            if (shopNum == 0 && ModContent.TryFind("FargowiltasSouls", "TopHatSquirrelCaught", out ModItem modItem)) //only on page 1
    //            {
    //                items[nextSlot] = new Item(modItem.Type) { shopCustomPrice = Item.buyPrice(copper: 100000) };
    //                nextSlot++;
    //            }
    //            foreach (int type in sellableItems)
    //            {
    //                if (++index < startOffset) //skip up to the minimum
    //                {
    //                    continue;
    //                }

    //                if (nextSlot >= Chest.maxItems) //only fill shop up to capacity
    //                {
    //                    break;
    //                }

    //                var item = new Item(type);
    //                int price;
    //                bool medals = false;

    //                price = item.value * 2;

    //                items[nextSlot] = new Item(type) { shopCustomPrice = Item.buyPrice(copper: price) };

    //                nextSlot++;
    //            }
    //        }
    //        base.ModifyActiveShop(npc, shopName, items);
    //    }
    }
}
