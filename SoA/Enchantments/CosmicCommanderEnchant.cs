using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using SacredTools.Content.Items.Armor.Lunar.Vortex;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Weapons.Oblivion;
using ssm.Content.Buffs;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.UI.Elements;

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
            public bool SniperStateActive;
            public bool SniperStateRecharging;
            public int SniperStateCooldown;
            public int SniperStateCharge;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (!player.HasBuff(ModContent.BuffType<SniperCooldownBuff>()))
                {
                    SniperStateActive = true;
                    SniperStateCharge = 15 * 60;
                    player.AddBuff(ModContent.BuffType<SniperBuff>(), 15 * 60);
                }
            }

            private void DeactivateSniperState(Player player)
            {
                SniperStateActive = false;
                SniperStateCharge = 0;
                player.AddBuff(ModContent.BuffType<SniperCooldownBuff>(), 15 * 60);
                SniperStateRecharging = true;
            }
            public override void PostUpdate(Player player)
            {
                if (SniperStateActive)
                {
                    SniperStateCharge--;
                    
                    if (SniperStateCharge <= 0)
                    {
                        DeactivateSniperState(player);
                        SniperStateRecharging = true;
                    }
                }
                if (SniperStateRecharging)
                {
                    if (SniperStateCooldown++ < 15 * 60)
                    {
                        SniperStateCharge++;
                    }
                    else
                    {
                        SniperStateRecharging = false;
                    }
                }
            }
            public override void PostUpdateEquips(Player player)
            {
                if (SniperStateActive)
                {
                    player.aggro -= (int)(player.aggro * 0.5f);
                    player.statDefense = player.statDefense *= 0.75f; // -25% defense
                }
                else if (SniperStateRecharging)
                {
                    player.statDefense = player.statDefense *= 1.30f; // +30% defense
                }

                CooldownBarManager.Activate("SniperStateCharge", ModContent.Request<Texture2D>("ssm/SoA/Enchantments/CosmicCommanderEnchant").Value, new(21, 142, 100),
                    () => SniperStateCharge / (60f * 15), true, activeFunction: player.HasEffect<CosmicCommanderEffect>);
                    
                    Main.NewText(SniperStateCharge, Color.Orange);
            }

            public override void OnHitByEither(Player player, NPC npc, Projectile proj)
            {
                if (SniperStateActive)
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
