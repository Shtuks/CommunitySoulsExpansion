using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.BardItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CyberPunkEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
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
            player.AddEffect<CyberPunkEffect>(Item);
        }

        public class CyberPunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyberPunkEnchant>();
            public override bool MutantsPresenceAffects => true;

            public override void PostUpdate(Player player)
            {
                player.GetModPlayer<ShtunThoriumPlayer>().cyberEnchant = true;
            }
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CyberPunkHeadset>());
            recipe.AddIngredient(ModContent.ItemType<CyberPunkSuit>());
            recipe.AddIngredient(ModContent.ItemType<CyberPunkLeggings>());
            recipe.AddIngredient(ModContent.ItemType<AutoTuner>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerDamage>());
            recipe.AddIngredient(ModContent.ItemType<DissTrack>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }

    public class CyberneticOrb
    {
        public Player owner;
        public int index;
        public int totalOrbs;
        public bool active = true;
        public float rotation;
        public Vector2 position;
        public Vector2 offset;
        public int health = 100;
        public int maxHealth = 100;
        private int regenTimer = 0;
        private int respawnTimer = 0;

        public CyberneticOrb(Player owner, int index, int totalOrbs)
        {
            this.owner = owner;
            this.index = index;
            this.totalOrbs = totalOrbs;
            rotation = MathHelper.TwoPi * index / totalOrbs;
            offset = new Vector2(48, 0).RotatedBy(rotation);
            position = owner.Center + offset;
            active = true;
        }

        public void Update()
        {
            if (!active)
            {
                respawnTimer++;
                if (respawnTimer >= 3600)
                {
                    active = true;
                    health = maxHealth;
                    respawnTimer = 0;
                }
                return;
            }

            rotation += 0.02f;
            offset = new Vector2(48, 0).RotatedBy(rotation + MathHelper.TwoPi * index / totalOrbs);
            position = owner.Center + offset;

            regenTimer++;
            if (regenTimer >= 600)
            {
                health = Math.Min(health + 5, maxHealth);
                regenTimer = 0;
            }

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.hostile && proj.damage > 0)
                {
                    Rectangle orbHitbox = new Rectangle((int)position.X - 16, (int)position.Y - 16, 32, 32);
                    if (proj.Hitbox.Intersects(orbHitbox))
                    {
                        health -= proj.damage;
                        proj.active = false;

                        if (health <= 0)
                        {
                            active = false;
                            respawnTimer = 0;
                            break;
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            if (!active) return;

            Texture2D texture = ModContent.Request<Texture2D>("YourModName/Items/Accessories/CyberneticOrb").Value;
            Main.spriteBatch.Draw(texture, position - Main.screenPosition, null, Color.White, 0f, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);

            if (health < maxHealth)
            {
                float healthRatio = (float)health / maxHealth;
                Vector2 barPosition = position - new Vector2(16, -20);
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, barPosition, null, Color.Black, 0f, Vector2.Zero, new Vector2(32, 6), SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, barPosition + new Vector2(2, 2), null, Color.Red, 0f, Vector2.Zero, new Vector2(28 * healthRatio, 2), SpriteEffects.None, 0f);
            }
        }
    }
}
