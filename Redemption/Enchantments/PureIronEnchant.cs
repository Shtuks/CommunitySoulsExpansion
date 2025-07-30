using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Weapons.PreHM.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.PreHM.PureIron;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Buffs.Debuffs;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class PureIronEnchant : BaseEnchant
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

        public override Color nameColor => new Color(89, 89, 105);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PureIronEffect>(Item);
        }

        public class PureIronEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PureIronEnchant>();
            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if (player.HasEffect<PureIronEffect>())
                {
                    if (Main.rand.NextBool())
                    {
                        target.AddBuff(ModContent.BuffType<PureChillDebuff>(), 1200);
                    }
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<PureChillDebuff>(), 1200);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<PureIronHelmet>();
            recipe.AddIngredient<PureIronChestplate>();
            recipe.AddIngredient<PureIronGreaves>();
            recipe.AddIngredient<AntlerStaff>();
            recipe.AddIngredient<PureIronSword>();
            recipe.AddIngredient<CrystallizedKnowledge>();
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
