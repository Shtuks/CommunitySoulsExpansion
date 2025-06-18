using Microsoft.Xna.Framework;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.Terrarium;
using Terraria.ID;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumItems : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<TerrariumDefender>())
            {
                item.defense = 8;
            }
            if (item.type == ModContent.ItemType<OmniCannon>())
            {
                item.damage = (int)(item.damage * 0.8f);
            }
        }


        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            if (item.type == ModContent.ItemType<OmniCannon>())
            {
                velocity *= 1.3f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<OmniCannon>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"[c/00A36C:Cross-Mod Balance:] Damage decreased by 20% but increased projectile velocity by 20%"));
            }
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<ShtunThoriumPlayer>().ThunderTalonEternity)
            {
                target.AddBuff(BuffID.BoneJavelin, 300);
            }
        }
    }
}
