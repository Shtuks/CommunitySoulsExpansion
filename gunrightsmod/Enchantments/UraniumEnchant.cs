using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework.Graphics;
using gunrightsmod.Content.Items;
using ssm.Core;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles.Enchantments;
using ssm.Content.Buffs;
using FargowiltasSouls;

namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]
    public class UraniumEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.TerMerica;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override Color nameColor => new(94, 48, 117);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = 126965;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<UraniumEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<UraniumHelmet>();
            recipe.AddIngredient<UraniumChestplate>();
            recipe.AddIngredient<UraniumLeggings>();
            recipe.AddIngredient<UraniumSword>();
            recipe.AddIngredient<PlasmoidWand>();
            recipe.AddIngredient<ParticleGun>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }

    public class UraniumEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
        public override int ToggleItemType => ModContent.ItemType<UraniumEnchant>();
        public static int Range(Player player, bool forceEffect) => (int)((forceEffect ? 450f : 250f));
        public override void PostUpdateEquips(Player player)
        {
            FargoSoulsPlayer modPlayer = player.FargoSouls();

            if (player.whoAmI != Main.myPlayer)
                return;

            int visualProj = ModContent.ProjectileType<UraniumAura>();
            if (player.ownedProjectileCounts[visualProj] <= 0)
            {
                Projectile.NewProjectile(GetSource_EffectItem(player), player.Center, Vector2.Zero, visualProj, 0, 0, Main.myPlayer);
            }

            bool forceEffect = modPlayer.ForceEffect<UraniumEnchant>();
            int dist = Range(player, forceEffect);

        /*    for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage)
                {
                    Vector2 npcComparePoint = FargoSoulsUtil.ClosestPointInHitbox(npc, player.Center);
                    if (player.Distance(npcComparePoint) < dist && (forceEffect || Collision.CanHitLine(player.Center, 0, 0, npcComparePoint, 0, 0)))
                    {
                        npc.AddBuff(ModContent.BuffType<DreadflameAura>(), 120);
                    }
                }
            }

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player targetPlayer = Main.player[i];
                if (targetPlayer.active &&
                    !targetPlayer.dead &&
                    targetPlayer.whoAmI != player.whoAmI &&
                    targetPlayer.team == player.team &&
                    player.team != 0)
                {
                    Vector2 targetComparePoint = targetPlayer.Center;

                    if (player.Distance(targetComparePoint) < dist &&
                        (forceEffect || Collision.CanHitLine(player.Center, 0, 0, targetComparePoint, 0, 0)))
                    {
                        targetPlayer.AddBuff(ModContent.BuffType<DreadflameAura>(), 120);
                    }
                }
            } */
        }
    }
}