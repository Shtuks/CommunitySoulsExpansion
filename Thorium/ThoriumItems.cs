using Microsoft.Xna.Framework;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.Terrarium;
using Terraria.ID;
using ThoriumMod.Items.Donate;
using Terraria.Localization;

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
                item.damage = (int)(item.damage * 0.7f);
            }
        }


        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            if (item.type == ModContent.ItemType<OmniCannon>())
            {
                velocity += velocity *= 1.6f;
            }
            if (item.type == ModContent.ItemType<QuasarsFlare>())
            {
                velocity += velocity *= 1.5f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<OmniCannon>())
            {
                tooltips.Add(new TooltipLine(Mod, "rebalance", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.VelUP")} 50%"));
                tooltips.Add(new TooltipLine(Mod, "rebalance2", $"{Language.GetTextValue("Mods.ssm.Balance.Nerf")} {Language.GetTextValue("Mods.ssm.Balance.DamageDown")} 30%"));
            }
            if (item.type == ModContent.ItemType<TerrariumParticleSprinters>())
            {
                tooltips.Insert(8, new TooltipLine(Mod, "compat", $"{Language.GetTextValue("Mods.ssm.AddedEffects.Aeolus")}"));
            }
            if (item.type == ModContent.ItemType<QuasarsFlare>())
            {
                tooltips.Add(new TooltipLine(Mod, "buff", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.VelUP")} 50%"));
            }
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<CSEThoriumPlayer>().ThunderTalonEternity)
            {
                target.AddBuff(BuffID.BoneJavelin, 300);
            }
        }
    }
}
