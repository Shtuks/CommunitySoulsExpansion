using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Accessories;
using FargowiltasSouls;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.NPCs;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class AsthraltiteEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(94, 48, 117);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AsthraltiteEffect>(Item);

            ModContent.Find<ModItem>(this.soa.Name, "AsthralRing").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(this.soa.Name, "CasterArcanum").UpdateAccessory(player, false);
        }

        public class AsthraltiteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if(Main.rand.NextBool() && hitInfo.Crit)
                {
                    hitInfo.Damage *= 2;
                }
                target.GetGlobalNPC<ModGlobalNPC>().InflictDraconicBlaze(target, 300, hitInfo.Damage / 2);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddRecipeGroup("ssm:AsthralHelms");
            recipe.AddIngredient<AsthralChest>();
            recipe.AddIngredient<AsthralLegs>();
            recipe.AddIngredient<AsthralRing>();
            recipe.AddIngredient<CasterArcanum>();
            recipe.AddIngredient<AsthralBlade>();
            //recipe.AddIngredient<MementoMori>(); now in class souls
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
