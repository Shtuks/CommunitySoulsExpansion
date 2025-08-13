using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using System;
using ThoriumMod.Utilities;
using Terraria.Audio;
using System.Collections.Generic;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class EbonEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override List<AccessoryEffect> ActiveSkillTooltips =>
           [AccessoryEffectLoader.GetEffect<EbonEffect>()];

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<EbonEffect>(Item);
            player.AddEffect<EbonEffectConversion>(Item);
        }

        public class EbonEffectConversion : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EbonEnchant>();
        }
        public class EbonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<EbonEnchant>();
            public override bool ActiveSkill => true;

            public bool cleansingMode = true;
            private int conversionCooldown;
            private const int CooldownMax = 10;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                cleansingMode = !cleansingMode;
                conversionCooldown = 0; 

                SoundEngine.PlaySound(SoundID.Item4);
            }
            public override void PostUpdateEquips(Player player)
            {
                if (cleansingMode)
                {
                    player.GetThoriumPlayer().healBonus += (int)(player.GetThoriumPlayer().healBonus * 0.1f);
                }
                else
                {
                    player.GetDamage(HealerDamage.Instance) += 0.1f;
                }
            }

            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer) return;

                if (conversionCooldown > 0 && player.HasEffect<EbonEffectConversion>()) conversionCooldown--;

                if (conversionCooldown == 0 && player.HasEffect<EbonEffectConversion>())
                {
                    ConvertBlocksBelow(player);
                    conversionCooldown = CooldownMax;
                }
            }
            private void ConvertBlocksBelow(Player player)
            {
                int rangeX = 3; 
                int rangeY = 2; 
                int playerTileX = (int)(player.Center.X / 16);
                int playerTileY = (int)((player.position.Y + player.height + 32) / 16);

                for (int i = playerTileX - rangeX / 2; i < playerTileX + rangeX / 2; i++)
                {
                    for (int j = playerTileY; j < playerTileY + rangeY; j++)
                    {
                        if (!WorldGen.InWorld(i, j)) continue;

                        Tile tile = Main.tile[i, j];
                        if (!tile.HasTile) continue;

                        if (cleansingMode)
                        {
                            ConvertToPure(i, j);
                        }
                        else
                        {
                            ConvertToEvil(i, j);
                        }
                    }
                }

                NetMessage.SendTileSquare(player.whoAmI, playerTileX, playerTileY, Math.Max(rangeX, rangeY));
            }
            private void ConvertToPure(int i, int j)
            {
                switch (Main.tile[i, j].TileType)
                {
                    case TileID.CorruptGrass:
                        WorldGen.Convert(i, j, 0, 0);
                        break;
                    case TileID.Ebonstone:
                        WorldGen.Convert(i, j, 0, 0);
                        break;
                    case TileID.CrimsonGrass:
                        WorldGen.Convert(i, j, 0, 0);
                        break;
                    case TileID.Crimstone:
                        WorldGen.Convert(i, j, 0, 0);
                        break;
                    case TileID.HallowedGrass:
                        WorldGen.Convert(i, j, 0, 0);
                        break;
                    case TileID.Pearlstone:
                        WorldGen.Convert(i, j, 0, 0);
                        break;
                }
            }
            private void ConvertToEvil(int i, int j)
            {
                int evilType = WorldGen.crimson ? 2 : 1;
                WorldGen.Convert(i, j, evilType, 0);
            }
        }
        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<EbonHood>());
            recipe.AddIngredient(ModContent.ItemType<EbonCloak>());
            recipe.AddIngredient(ModContent.ItemType<EbonLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DarkHeart>());
            recipe.AddIngredient(ModContent.ItemType<LeechBolt>());
            recipe.AddIngredient(ModContent.ItemType<ShadowWand>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
