﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Items.Weapons.Marstech;
using SacredTools.Content.Items.Armor.Marstech;
using SacredTools.Items.Claymarine;
using ssm.Core;
using static ssm.SoA.Enchantments.SpaceJunkEnchant;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class MarstechEnchant : BaseEnchant
    {
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
            Item.rare = 8;
            Item.value = 250000;
        }

        public override Color nameColor => new(61, 155, 189);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MarstechEffect>(Item);
            player.AddEffect<SpaceJunkEffect>(Item);
        }

        public class MarstechEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarstechEnchant>();
            public override bool ActiveSkill => true;

            private int cd;
            public override void PostUpdateEquips(Player player)
            {
                if (cd > 0)
                {
                    cd--;
                }
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if(!(cd > 0))
                {
                    Projectile.NewProjectile(
                        player.GetSource_GiftOrReward(),
                        player.Center,
                        new Vector2(0, -5f),
                        ModContent.ProjectileType<MartianProbe>(),
                        50,
                        5f,
                        player.whoAmI
                    );
                    cd += 15 * 60;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PhaseSlasher>();
            recipe.AddIngredient<PlasmaDischarge>();
            recipe.AddIngredient<MarstechHelm>();
            recipe.AddIngredient<MarstechPlate>();
            recipe.AddIngredient<MarstechLegs>();
            recipe.AddIngredient<SpaceJunkEnchant>();
            recipe.AddTile(125);
            recipe.Register();
        }
    }
}
