using FargowiltasSouls.Content.Items;
using ssm.Core;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Buffs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework;

namespace ssm.Thorium.Emode.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class JellyfishCoil : SoulsItem
    {
        public override bool Eternity => true;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = 2;
            Item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Bubbled>()] = true;
            player.AddEffect<JellyfishCoilEffect>(Item);
        }

        public class JellyfishCoilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EnergizedMobileDeviceHeader>();
            public override int ToggleItemType => ModContent.ItemType<JellyfishCoil>();

            public bool bubbleActive = false;
            public float bubbleDamageAbsorbed = 0;
            public int bubbleTimer = 0;
            public int bubbleCooldown = 0;
            public const int BUBBLE_DURATION = 3600; 
            public const int BUBBLE_COOLDOWN = 1200;

            public override void PreUpdate(Player player)
            {
                if (bubbleCooldown > 0)
                {
                    bubbleCooldown--;
                }

                if (bubbleActive)
                {
                    bubbleTimer++;

                    if (bubbleTimer >= BUBBLE_DURATION)
                    {
                        BreakBubble(player);
                    }

                    if (Main.rand.NextBool(15))
                    {
                        Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.BubbleBurst_Blue, 0, 0, 100, default, 1.2f);
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
                else if (bubbleCooldown <= 0)
                {
                    ActivateBubble();
                }
            }

            private void ActivateBubble()
            {
                bubbleActive = true;
                bubbleDamageAbsorbed = 0;
                bubbleTimer = 0;
            }

            private void BreakBubble(Player player)
            {
                bubbleActive = false;
                bubbleCooldown = BUBBLE_COOLDOWN;
                bubbleTimer = 0;
                bubbleDamageAbsorbed = 0;

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.BubbleBurst_Blue, 0, 0, 100, default, 1.5f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
            }
            public override void ModifyHurt(Player player, ref Player.HurtModifiers modifiers)
            {
                if (bubbleActive)
                {
                    modifiers.FinalDamage.Flat = ProcessBubbleDamage(modifiers.FinalDamage.Flat, player);
                }
            }

            private float ProcessBubbleDamage(float damage, Player player)
            {
                //int maxDamage = player.HasEffect<EnergizedMobileDeviceEffect>() ? 150 : 100;

                bubbleDamageAbsorbed += damage;

                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Cyan, 1f);
                    dust.noGravity = true;
                }

                if (bubbleDamageAbsorbed >= 100)
                {
                    BreakBubble(player);
                    return damage - 100; 
                }

                return 0;
            }
        }
    }
}
