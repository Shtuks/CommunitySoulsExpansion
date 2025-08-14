using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Tracker;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using System.Collections.Generic;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AssassinEnchant : BaseEnchant
    {
        public override List<AccessoryEffect> ActiveSkillTooltips =>
           [AccessoryEffectLoader.GetEffect<AssassinEffect>()];
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AssassinEffect>(Item);
        }

        public class AssassinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<AssassinEnchant>();
            public override bool ActiveSkill => true;

            public int cd;

            public override void PostUpdateEquips(Player player)
            {
                if(cd > 0)
                {
                    cd--;
                }
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (cd < 1)
                {
                    Vector2 mousePosition = Main.MouseWorld;
                    NPC targetNpc = null;
                    float searchRadius = 80f;
                    float minDistance = float.MaxValue;

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.chaseable && !npc.friendly && npc.life > 0 && !npc.dontTakeDamage)
                        {
                            float distance = Vector2.Distance(npc.Center, mousePosition);
                            if (distance <= searchRadius && distance < minDistance)
                            {
                                minDistance = distance;
                                targetNpc = npc;
                            }
                        }
                    }

                    if (targetNpc != null)
                    {
                        Vector2 directionFromPlayer = targetNpc.Center - player.Center;
                        directionFromPlayer.Normalize();
                        Vector2 teleportPosition = targetNpc.Center;

                        player.Teleport(teleportPosition, TeleportationStyleID.TeleportationPotion);
                        player.immuneTime += 20;

                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, teleportPosition.X, teleportPosition.Y, TeleportationStyleID.TeleportationPotion);

                        player.GetModPlayer<CSEThoriumPlayer>().tripleDamageNextHit = true;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:AnyAssassinHelmet");
            recipe.AddIngredient(ModContent.ItemType<AssassinsWalkers>());
            recipe.AddIngredient(ModContent.ItemType<AssassinsGuard>());
            recipe.AddIngredient(ModContent.ItemType<DartPouch>());
            recipe.AddIngredient(ModContent.ItemType<TheBlackBow>());
            recipe.AddIngredient(ModContent.ItemType<WyrmDecimator>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
