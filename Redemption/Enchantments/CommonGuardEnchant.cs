using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Armor.PreHM.CommonGuard;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using System;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls;
using FargowiltasSouls.Content.UI.Elements;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class CommonGuardEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(139, 145, 156);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CommonGuardEffect>(Item);
        }

        public class CommonGuardEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
            public override bool ActiveSkill => true;

            public int abilityCD;

            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                    CooldownBarManager.Activate("CommonGuardCD", ModContent.Request<Texture2D>("ssm/Redemption/Enchantments/CommonGuardEnchant").Value, new Color(139, 145, 156),
                        () => (float)abilityCD / 1200, true, activeFunction: () => abilityCD > 0);
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (abilityCD < 0)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];

                        if (npc.active && npc.getRect().Intersects(new Rectangle(0, 0, Main.screenWidth, Main.screenHeight)))
                        {
                            npc.RedemptionGuard().GuardPoints = player.ForceEffect<CommonGuardEffect>() ? 0 : npc.RedemptionGuard().GuardPoints / 2;
                            abilityCD = 1200;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:CommonGuardHelms");
            recipe.AddIngredient(ModContent.ItemType<CommonGuardPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<CommonGuardGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Wardbreaker>());
            recipe.AddIngredient(ModContent.ItemType<KeepersCirclet>());
            recipe.AddIngredient(ModContent.ItemType<TrappedSoulBauble>());

            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
