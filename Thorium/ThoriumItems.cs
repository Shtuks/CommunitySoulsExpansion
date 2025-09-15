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
using ThoriumMod;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.IridescentEnchant;
using static ssm.Thorium.Enchantments.LifeBinderEnchant;

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

        public override void OnConsumeItem(Item item, Player player)
        {
            if(item.healLife > 0 && player.HasEffect<IridescentEffect>())
            {
                ApplyLifeShield(player, item.healLife);
            }

            if (item.healLife > 0 && player.HasEffect<LifeBinderEffect>())
            {
                player.GetModPlayer<ThoriumPlayer>().MetalShieldMax += item.healLife/2;
            }
        }
        private void ApplyLifeShield(Player target, int healValue)
        {
            ThoriumPlayer thoriumPlayer = target.GetModPlayer<ThoriumPlayer>();
            if (thoriumPlayer == null) return;

            int shieldAmount = 0;
            int newMax = thoriumPlayer.MetalShieldMax;

            if (healValue <= 25)
            {
                shieldAmount = 1;
                if (newMax < 5) newMax = 5;
            }
            else if (healValue <= 50)
            {
                shieldAmount = 2;
                if (newMax < 10) newMax = 10;
            }
            else if (healValue <= 150)
            {
                shieldAmount = 3;
                if (newMax < 15) newMax = 15;
            }

            if (shieldAmount > 0)
            {
                thoriumPlayer.MetalShieldMax = newMax;
                thoriumPlayer.shieldHealth += shieldAmount;
                if (thoriumPlayer.shieldHealth > thoriumPlayer.MetalShieldMax)
                {
                    thoriumPlayer.shieldHealth = thoriumPlayer.MetalShieldMax;
                }

                CombatText.NewText(target.Hitbox, new Color(100, 200, 255), shieldAmount);
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
                tooltips.Insert(8, new TooltipLine(Mod, "compat", $"{Language.GetTextValue("Mods.ssm.Aeolus")}"));
            }
            if (item.type == ModContent.ItemType<QuasarsFlare>())
            {
                tooltips.Add(new TooltipLine(Mod, "buff", $"{Language.GetTextValue("Mods.ssm.Balance.Buff")} {Language.GetTextValue("Mods.ssm.Balance.VelUP")} 50%"));
            }
        }
    }
}
