using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Lunar.Vortex;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Weapons.Oblivion;
using Terraria.WorldBuilding;
using ssm.Content.Buffs;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class CosmicCommanderEnchant : BaseEnchant
    {
        public override List<AccessoryEffect> ActiveSkillTooltips =>
            [AccessoryEffectLoader.GetEffect<CosmicCommanderEffect>()];
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(21, 142, 100);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CosmicCommanderEffect>(Item);
        }

        public class CosmicCommanderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<CosmicCommanderEnchant>();
            public override bool ActiveSkill => true;

            public bool sniperStateActive;
            public int sniperStateTimer;
            public int cooldownTimer;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (!player.HasBuff(ModContent.BuffType<SniperCooldownBuff>()))
                {
                    sniperStateActive = true;
                    sniperStateTimer = 15 * 60;
                    player.AddBuff(ModContent.BuffType<SniperBuff>(), sniperStateTimer);
                }
            }

            private void DeactivateSniperState(Player player)
            {
                sniperStateActive = false;
                cooldownTimer = 15 * 60; // 15 seconds cooldown
                player.AddBuff(ModContent.BuffType<SniperCooldownBuff>(), cooldownTimer);
            }
            public override void PostUpdate(Player player)
            {
                if (sniperStateActive)
                {
                    if (sniperStateTimer-- <= 0)
                    {
                        DeactivateSniperState(player);
                    }
                }
                else if (cooldownTimer > 0)
                {
                    cooldownTimer--;
                }
            }
            public override void PostUpdateEquips(Player player)
            {
                if (sniperStateActive)
                {
                    player.aggro -= (int)(player.aggro * 0.5f);
                    player.statDefense = player.statDefense *= 0.75f; // -25% defense
                }
                else if (cooldownTimer > 0)
                {
                    player.statDefense = player.statDefense *= 1.30f; // +30% defense
                }
            }

            public override void OnHitByEither(Player player, NPC npc, Projectile proj)
            {
                if (sniperStateActive)
                {
                    DeactivateSniperState(player);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VortexCommanderHat>();
            recipe.AddIngredient<VortexCommanderSuit>();
            recipe.AddIngredient<VortexCommanderGreaves>();
            recipe.AddIngredient<DolphinGun>();
            recipe.AddIngredient<LightningRifle>();
            recipe.AddIngredient<PGMUltimaRatioHecateII>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
