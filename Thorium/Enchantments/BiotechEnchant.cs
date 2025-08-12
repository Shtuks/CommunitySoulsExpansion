using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls;
using ssm.Content.Buffs;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BiotechEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BiotechEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BioTechGarment>());
            recipe.AddIngredient(ModContent.ItemType<BioTechHood>());
            recipe.AddIngredient(ModContent.ItemType<BioTechLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LifeEssenceApparatus>());
            recipe.AddIngredient(ModContent.ItemType<NullZoneStaff>());
            recipe.AddIngredient(ModContent.ItemType<BarrierGenerator>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class BiotechEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BiotechEnchant>();
            public override bool MutantsPresenceAffects => true;

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile proj, Item item)
            {
                if (proj.DamageType == HealerDamage.Instance &&
                Main.rand.NextFloat() < 0.20f)
                {
                    target.AddBuff(ModContent.BuffType<CutOpen>(), 300); 
                }
                else if (player.ForceEffect<BiotechEffect>())
                {
                    target.AddBuff(ModContent.BuffType<CutOpen>(), 300);
                }
            }
        }
    }
}
