using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Enchantments.FallenPrinceEnchant;
using static ssm.SoA.Enchantments.CosmicCommanderEnchant;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;
using static ssm.SoA.Enchantments.NebulousApprenticeEnchant;
using static ssm.SoA.Enchantments.StellarPriestEnchant;
using ssm.SoA.Enchantments;
using static ssm.SoA.Enchantments.QuasarEnchant;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.UI.Elements;
using ssm.Content.Buffs;


namespace ssm.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoranForce : BaseForce
    {
        public override List<AccessoryEffect> ActiveSkillTooltips =>
            [AccessoryEffectLoader.GetEffect<CosmicCommanderEffect>()];
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        public override void SetStaticDefaults()
        {
            Enchants[Type] = new int[5]
            {
                ModContent.ItemType<CosmicCommanderEnchant>(),
                ModContent.ItemType<BlazingBruteEnchant>(),
                ModContent.ItemType<NebulousApprenticeEnchant>(),
                ModContent.ItemType<StellarPriestEnchant>(),
                ModContent.ItemType<FallenPrinceEnchant>()
            };
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CosmicCommanderEffect>(Item);
            player.AddEffect<BlazingBruteEffect>(Item);
            player.AddEffect<NebulousApprenticeEffect>(Item);
            player.AddEffect<StellarPriestEffect>(Item);
            player.AddEffect<SupernovaEffect>(Item);
            player.AddEffect<QuasarEffect>(Item);

            player.AddEffect<SoranEffect>(Item);
        }

        public class SoranEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;

            public override void PostUpdateEquips(Player player)
            {
                var CosmicCommanderPlayer = player.GetModPlayer<CosmicCommanderPlayer>();
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
            Recipe recipe = CreateRecipe();
            int[] array = Enchants[Type];
            foreach (int itemID in array)
            {
                recipe.AddIngredient(itemID);
            }

            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }

    public class SoranForcePlayer : ModPlayer
    {
        private bool HadCSoranForceLastFrame;
        public bool HasCSoranForceThisFrame;
        public bool SniperStateActive = false;
        public int SniperStateCharge = 0;
        public bool SniperStateRecharging = true;

        public override void ResetEffects()
        {
            HasCSoranForceThisFrame = false;
        }

        public override void UpdateEquips()
        {
            if (!HadCSoranForceLastFrame && HasCSoranForceThisFrame)
            {
                SniperStateActive = false;
                SniperStateRecharging = true;
                SniperStateCharge = 0;
            }

            if (HadCSoranForceLastFrame && !HasCSoranForceThisFrame)
            {
                SniperStateActive = false;
                SniperStateRecharging = false;
                SniperStateCharge = 0;
                Player.ClearBuff(ModContent.BuffType<SniperCooldownBuff>());
                Player.ClearBuff(ModContent.BuffType<SniperBuff>());
            }

            HadCSoranForceLastFrame = HasCSoranForceThisFrame;
        }
    }
}
