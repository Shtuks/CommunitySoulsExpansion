using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Accessories.PreHM;
using Redemption.Globals.Player;
using Redemption.BaseExtension;
using Redemption.Items.Armor.HM.Xenomite;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using ssm.Content.Projectiles.Enchantments;
using Terraria.Audio;
using static ssm.Redemption.Enchantments.XeniumEnchant;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XenomiteEnchant : BaseEnchant
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

        public override Color nameColor => new Color(88, 126, 121);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<XenomiteEffect>(Item);
        }

        public class XenomiteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XenomiteEnchant>();
            public override bool ActiveSkill => true;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                Vector2 groundPosition = FindGroundPosition(player.Center);

                for (int i = 0; i < 4; i++)
                {
                    Vector2 position = groundPosition + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-20, 20));
                    Vector2 velocity = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.2f, 0.2f));

                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        position,
                        velocity,
                        ModContent.ProjectileType<ToxicCloudsProj>(),
                        25,
                        0f,
                        player.whoAmI);

                    SoundEngine.PlaySound(SoundID.Item85 with { Pitch = -0.5f }, groundPosition);
                }
            }
            private Vector2 FindGroundPosition(Vector2 startPos)
            {
                int tileX = (int)(startPos.X / 16);
                int tileY = (int)(startPos.Y / 16);

                while (tileY < Main.maxTilesY && !WorldGen.SolidTile(tileX, tileY))
                {
                    tileY++;
                }

                return new Vector2(startPos.X, tileY * 16 - 40);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<XenomiteHelmet>();
            recipe.AddIngredient<XenomitePlate>();
            recipe.AddIngredient<XenomiteLeggings>();
            recipe.AddIngredient<Chernobyl>();
            recipe.AddIngredient<DAN>();
            recipe.AddIngredient<NecklaceOfPerception>();
            recipe.AddTile(125);

            recipe.Register();
        }
    }
}
