using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using System;
using Redemption.Items.Armor.PreHM.DragonLead;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.SoulToggles;
using Redemption.Buffs.NPCBuffs;
using static ssm.Redemption.Enchantments.CommonGuardEnchant;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class DragonLeadEnchant : BaseEnchant
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

        public override Color nameColor => new Color(116, 100, 127);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DragonLeadEffect>(Item);
        }

        public class DragonLeadEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DragonLeadEnchant>();

            public int cd;
            public override void PostUpdateEquips(Player player)
            {
                if (cd > 0) { cd--; }
            }
            public override void OnHitByEither(Player player, NPC npc, Projectile proj)
            {
                cd += 600;
            }
            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if(cd > 0)
                {
                    target.AddBuff(ModContent.BuffType<DragonblazeDebuff>(), 1200);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DragonLeadSkull>());
            recipe.AddIngredient(ModContent.ItemType<DragonLeadRibplate>());
            recipe.AddIngredient(ModContent.ItemType<DragonLeadGreaves>());
            recipe.AddIngredient(ModContent.ItemType<HeartInsignia>());
            recipe.AddIngredient<DragonCleaver>();
            recipe.AddIngredient<DragonSlayersBow>();

            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
