using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.SpaceJunk;
using SacredTools.Content.Items.Weapons.Event;
using ssm.Core;
using FargowiltasSouls;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SpaceJunkEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(120, 135, 154);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SpaceJunkEffect>(Item))
            {
                player.GetModPlayer<SoAPlayer>().spaceJunkEnchant = player.ForceEffect<SpaceJunkEffect>() ? 2 : 1;
            }
            player.AddEffect<SpaceJunkAbilityEffect>(Item);
        }

        public class SpaceJunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpaceJunkEnchant>();
        }

        public class SpaceJunkAbilityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpaceJunkEnchant>();
            public override bool ActiveSkill => true;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                NPC nearestEnemy = null;
                float closestDistance = float.MaxValue;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.CanBeChasedBy() && npc.immune[player.whoAmI] == 0)
                    {
                        float distance = Vector2.Distance(player.Center, npc.Center);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            nearestEnemy = npc;
                        }
                    }
                }

                if (nearestEnemy != null)
                {
                    ShtunUtils.ProjectileRain(Main.LocalPlayer.GetSource_FromThis(), nearestEnemy.Center, 400, 100, 500, 800, 15, ModContent.ProjectileType<SpaceJunkProj>(), (int)Main.LocalPlayer.GetDamage<GenericDamageClass>().ApplyTo(40), 1, Main.LocalPlayer.whoAmI);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<SpaceJunkHelm>();
            recipe.AddIngredient<SpaceJunkBody>();
            recipe.AddIngredient<SpaceJunkLegs>();
            recipe.AddIngredient<OrbFlayer>();
            recipe.AddIngredient<HornetNeedle>();
            recipe.AddIngredient<GoldDoorHandle>();
            recipe.AddTile(125);
            recipe.Register();
        }
    }
}
