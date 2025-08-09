using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using gunrightsmod.Content.Items;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.UI.Elements;
using gunrightsmod.Content.Projectiles;
using FargowiltasSouls;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class PlutoniumEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = 1024041;
        }
        public override Color nameColor => new(94, 48, 117);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PlutoniumEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PlutoniumFacemask>();
            recipe.AddIngredient<PlutoniumChestplate>();
            recipe.AddIngredient<PlutoniumPants>();
            recipe.AddIngredient<Needler>();
            recipe.AddIngredient<PlutoniumAutoPistol>();
            recipe.AddIngredient<MidnightAfterburner>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class PlutoniumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlutoniumEnchant>();


            public override void PostUpdate(Player player)
            {
                CSEgunrightsmodPlayer CSEgunrightsmodPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();
                Main.NewText("Plutonium Charge: " + CSEgunrightsmodPlayer.PlutoniumCharge);
                if (CSEgunrightsmodPlayer.PlutoniumCharge == 4)
                {
                    LaunchPlutoniumParticle(player);
                    CSEgunrightsmodPlayer.PlutoniumCharge = 0;
                }
            }
            public void LaunchPlutoniumParticle(Player player)
            {
                CSEgunrightsmodPlayer CSEgunrightsmodPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();
                Projectile.NewProjectile(player.GetSource_Misc("LaunchPlutoniumParticle"), new Vector2(100f, 100f), new Vector2(10f, 0f), ModContent.ProjectileType<PlutoniumParticle>(), CSEgunrightsmodPlayer.PlutoniumMissileDamage, 0f, player.whoAmI);
            }
        }
    }
}