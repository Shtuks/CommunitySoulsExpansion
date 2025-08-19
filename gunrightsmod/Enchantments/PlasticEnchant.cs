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
using System;
using FargowiltasSouls.Content.UI.Elements;
using gunrightsmod.Content.Projectiles;
using FargowiltasSouls;

namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Gunrightsmod.Name)]

    public class PlasticEnchant : BaseEnchant
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
            Item.rare = ItemRarityID.Pink;
            Item.value = 36471;
        }
        public override Color nameColor => new(94, 48, 117);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PlasticEffect>(Item);
        }

        public class PlasticEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<IdeocracyForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlasticEnchant>();

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                base.OnHitNPCEither(player, target, hitInfo, damageClass, baseDamage, projectile, item);

                int maxMicroplasticStack = player.ForceEffect<PlasticEffect>() ? 5 : 3;

                if (target.GetGlobalNPC<CSEgunrightsmodNPC>().NPCMicroplasticStack < maxMicroplasticStack)
                {
                    if (Main.rand.NextBool(4)) // Returns true 1 out of X times because I can't remember how NextBool works :sunglas:
                    {
                        target.GetGlobalNPC<CSEgunrightsmodNPC>().NPCMicroplasticStack++;
                        target.GetGlobalNPC<CSEgunrightsmodNPC>().NPCMicroplasticTimer += 300;
                    }
                }

                if (player.ForceEffect<PlasticEffect>())
                {
                    target.GetGlobalNPC<CSEgunrightsmodNPC>().NPCForceMicroplastic = true;
                }
            }

            public static void TryApplyMicroplastics(Player attacker, Player target)
            {
                if (!attacker.active || !target.active || !target.hostile || !attacker.hostile)
                    return;
                int maxMicroplasticStack = attacker.ForceEffect<PlasticEffect>() ? 5 : 3;
                var player = target.GetModPlayer<CSEgunrightsmodPlayer>();
                if (attacker.ForceEffect<PlasticEffect>())
                {
                    player.MicroplasticForce = true;
                }
                if (player.MicroplasticStack < maxMicroplasticStack)
                {
                    if (Main.rand.NextBool(4))
                    {
                        player.MicroplasticStack++;
                        player.MicroplasticTimer += 300;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PlasticHeadgear>();
            recipe.AddIngredient<PlasticChestplate>();
            recipe.AddIngredient<PlasticPants>();
            recipe.AddIngredient<PlasticSpork>();
            recipe.AddIngredient<BrickPick>();
            recipe.AddIngredient<NerfGun>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}