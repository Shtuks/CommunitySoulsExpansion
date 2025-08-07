using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Dreadfire;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Weapons.Dreadfire;
using ssm.Content.Buffs;
using FargowiltasSouls;
using ssm.Content.Projectiles;
using System.Collections.Generic;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class DreadfireEnchant : BaseEnchant
    {
        public override List<AccessoryEffect> ActiveSkillTooltips =>
            [AccessoryEffectLoader.GetEffect<DreadfireEffect>()];
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SacredTools;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 70000;
        }

        public override Color nameColor => new(191, 62, 6);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DreadfireEffect>(Item);
        }

        public class DreadfireEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadfireEnchant>();
            public override bool ActiveSkill => true;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (!player.HasBuff<DreadflameAuraCD>())
                {
                    player.AddBuff(ModContent.BuffType<DreadflameAura>(), 600);

                    player.AddBuff(ModContent.BuffType<DreadflameAuraCD>(), player.ForceEffect<DreadfireEffect>() ? 2600 : 2000);
                }
            }
            public static int Range(Player player, bool forceEffect) => (int)((forceEffect ? 450f : 250f));
            public override void PostUpdateEquips(Player player)
            {
                FargoSoulsPlayer modPlayer = player.FargoSouls();

                if (player.whoAmI != Main.myPlayer)
                    return;

                int visualProj = ModContent.ProjectileType<DreadfireAuraProj>();
                if (player.ownedProjectileCounts[visualProj] <= 0)
                {
                    Projectile.NewProjectile(GetSource_EffectItem(player), player.Center, Vector2.Zero, visualProj, 0, 0, Main.myPlayer);
                }

                bool forceEffect = modPlayer.ForceEffect<DreadfireEnchant>();
                int dist = Range(player, forceEffect);

                for (int i = 0; i < Main.maxNPCs; i++)
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
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<DreadfireMask>();
            recipe.AddIngredient<DreadfirePlate>();
            recipe.AddIngredient<DreadfireBoots>();
            recipe.AddIngredient<DreadflameEmblem>();
            recipe.AddIngredient<PumpkinFlare>();
            recipe.AddIngredient<PumpkinCarver>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
