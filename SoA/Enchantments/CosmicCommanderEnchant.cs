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
using System.Security.Cryptography.X509Certificates;

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
            var CosmicCommanderPlayer = player.GetModPlayer<CosmicCommanderPlayer>();
            player.AddEffect<CosmicCommanderEffect>(Item);
            CosmicCommanderPlayer.HasCosmicCommanderEnchantThisFrame = true;
        }

        public class CosmicCommanderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<CosmicCommanderEnchant>();
            public override bool ActiveSkill => true;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                var CosmicCommanderPlayer = player.GetModPlayer<CosmicCommanderPlayer>();

                if (!player.HasBuff(ModContent.BuffType<SniperCooldownBuff>()) && !player.HasBuff(ModContent.BuffType<SniperBuff>()) && CosmicCommanderPlayer.SniperStateCharge == 15 * 60)
                {
                    CosmicCommanderPlayer.SniperStateActive = true;
                    CosmicCommanderPlayer.SniperStateRecharging = false;
                    player.AddBuff(ModContent.BuffType<SniperBuff>(), 15 * 60);
                }
            }

            private void DeactivateSniperState(Player player)
            {
                var CosmicCommanderPlayer = player.GetModPlayer<CosmicCommanderPlayer>();
                CosmicCommanderPlayer.SniperStateActive = false;
                CosmicCommanderPlayer.SniperStateRecharging = true;
                CosmicCommanderPlayer.SniperStateCharge = 1;
                player.AddBuff(ModContent.BuffType<SniperCooldownBuff>(), 15 * 60);
            }
            public override void PostUpdate(Player player)
            {
                var CosmicCommanderPlayer = player.GetModPlayer<CosmicCommanderPlayer>();
                if (CosmicCommanderPlayer.SniperStateActive)
                {
                    if (CosmicCommanderPlayer.SniperStateCharge-- <= 0)
                    {
                        DeactivateSniperState(player);
                    }
                }

                if (CosmicCommanderPlayer.SniperStateRecharging)
                {
                    if (CosmicCommanderPlayer.SniperStateCharge++ >= ((15 * 60) - 1))
                    {
                        CosmicCommanderPlayer.SniperStateRecharging = false;
                    }
                }
            }

            public override void PostUpdateEquips(Player player)
            {
                var CosmicCommanderPlayer = player.GetModPlayer<CosmicCommanderPlayer>();

                //Main.NewText(CosmicCommanderPlayer, Color.LimeGreen);

                if (CosmicCommanderPlayer.SniperStateActive)
                {
                    player.aggro -= (int)(player.aggro * 0.5f);
                    player.statDefense = player.statDefense *= 0.75f;
                }
                else if (CosmicCommanderPlayer.SniperStateRecharging)
                {
                    player.statDefense = player.statDefense *= 1.30f;
                }

                CooldownBarManager.Activate("CosmicCommanderCooldown", ModContent.Request<Texture2D>("ssm/SoA/Enchantments/CosmicCommanderEnchant").Value, new(21, 142, 100),
                    () => CosmicCommanderPlayer.SniperStateCharge / (60f * 15), true, activeFunction: player.HasEffect<CosmicCommanderEffect>);
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

    public class CosmicCommanderPlayer : ModPlayer
    {
        private bool HadCosmicCommanderEnchantLastFrame;
        public bool HasCosmicCommanderEnchantThisFrame;
        public bool SniperStateActive = false;
        public int SniperStateCharge = 0;
        public bool SniperStateRecharging = true;

        public override void ResetEffects()
        {
            HasCosmicCommanderEnchantThisFrame = false;
        }

        public override void UpdateEquips()
        {
            if (!HadCosmicCommanderEnchantLastFrame && HasCosmicCommanderEnchantThisFrame)
            {
                SniperStateActive = false;
                SniperStateRecharging = true;
                SniperStateCharge = 0;
            }

            if (HadCosmicCommanderEnchantLastFrame && !HasCosmicCommanderEnchantThisFrame)
            {
                SniperStateActive = false;
                SniperStateRecharging = false;
                SniperStateCharge = 0;
                Player.ClearBuff(ModContent.BuffType<SniperCooldownBuff>());
                Player.ClearBuff(ModContent.BuffType<SniperBuff>());
            }

            HadCosmicCommanderEnchantLastFrame = HasCosmicCommanderEnchantThisFrame;
        }

        public void OnEnterWorld()
        {
            SniperStateActive = false;
            SniperStateRecharging = true;
            SniperStateCharge = 0;
        }
    }
}