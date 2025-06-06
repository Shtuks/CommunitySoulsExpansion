using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int chtuxlagorInferno;
        public static int ECH = -1;
        public static int DukeEX = -1;
        public static int boss = -1;
        public static int mutantEX = -1;

        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                int mutantHealth = 10000000;
                float multiplier = 0;

                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) {multiplier += 0.3f;}
                if (ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded) {multiplier += 0.7f;}
                if (ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded) {multiplier += 1f;}
                if (ModCompatibility.Thorium.Loaded) { multiplier += 0.7f; }
                if (ModCompatibility.Calamity.Loaded) { multiplier += 1.7f; }
                if (ModCompatibility.SacredTools.Loaded) { multiplier += 1.6f; }

                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = Main.getGoodWorld ? 2000 : (int)(500 + (100 * multiplier));
                    npc.lifeMax = (int)(mutantHealth + (mutantHealth * multiplier));
                }
            }

            if (npc.type == ModContent.NPCType<AbomBoss>())
            {
                int abomHealth = 1000000;
                float multiplierA = 0;

                if (ModCompatibility.Thorium.Loaded && !ModCompatibility.Calamity.Loaded) { multiplierA += 1f; }
                if (ModCompatibility.Thorium.Loaded) { multiplierA += 3f; } //post primoridals
                if (ModCompatibility.Calamity.Loaded) { multiplierA += 6f; } //same tier as cal/exos
                if (ModCompatibility.SacredTools.Loaded) { multiplierA += 1f; } //post lost sibings

                if (npc.type == ModContent.NPCType<AbomBoss>())
                {
                    npc.damage = Main.getGoodWorld ? 1000 : (int)(250 + (20 * multiplierA));
                    npc.lifeMax = (int)(2800000 + (abomHealth * multiplierA)) / 2; //like wtf
                }
            }
        }
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
