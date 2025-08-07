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


namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]

    public class AstatineEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override Color nameColor => new(183, 62, 97);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 11;
            Item.value = 3135864;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AstatineEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<AstatineHelmet>();
            recipe.AddIngredient<AstatineBreastplate>();
            recipe.AddIngredient<AstatineGreaves>();
            recipe.AddIngredient<ATFsNightmare>();
            recipe.AddIngredient<PlasmaRifle3>();
            recipe.AddIngredient<FissileDart>(3396);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }


    public class AstatineEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
        public override int ToggleItemType => ModContent.ItemType<AstatineEnchant>();

        public override void PostUpdate(Player player)
        {
            var CSEgunrightsmodPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();

            if (CSEgunrightsmodPlayer.AstatineExplosionCharge < CSEgunrightsmodPlayer.AstatineExplosionCooldown)
                CSEgunrightsmodPlayer.AstatineExplosionCharge++;

            CooldownBarManager.Activate("AstatineExplosionCooldown", ModContent.Request<Texture2D>("ssm/gunrightsmod/Enchantments/AstatineEnchant").Value, new(183, 62, 97),
                () => CSEgunrightsmodPlayer.AstatineExplosionCharge / CSEgunrightsmodPlayer.AstatineExplosionCooldown, true, activeFunction: player.HasEffect<AstatineEffect>);
        }

        public override void OnHitByEither(Player player, NPC npc, Projectile proj)
        {
            var CSEgunrightsmodPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();

            if (CSEgunrightsmodPlayer.AstatineExplosionCharge >= CSEgunrightsmodPlayer.AstatineExplosionCooldown)
                AstatineExplosion(player);
        }

        public void AstatineExplosion(Player player)
        {
            var CSEgunrightsmodPlayer = player.GetModPlayer<CSEgunrightsmodPlayer>();
            CSEgunrightsmodPlayer.AstatineExplosionCharge = 0;
            Vector2 position = player.Center;
            Projectile.NewProjectile(player.GetSource_Misc("SpawnProjectileOnPlayer"), player.Center, Vector2.Zero, ModContent.ProjectileType<AstaBoomBig>(), 6000, 5f, player.whoAmI);
        }
    }
}