using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Bismuth;
using SacredTools.Items.Weapons.Venomite;
using SacredTools.Items.Weapons.Herbs;
using SacredTools.Items.Weapons;
using FargowiltasSouls;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class BismuthEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(184, 66, 66);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BismuthEffect>(Item);
        }

        public class BismuthEffect : AccessoryEffect
        {
            public int bismuthCrystalStage = 0;
            public int bismuthFormationTimer = 0;
            public const int FormationTime = 300;
            public const int MaxStages = 3;
            public const int MaxDamageAbsorption = 90;
            public int currentDamageAbsorbed = 0;
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BismuthEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                bismuthFormationTimer++;

                if (bismuthFormationTimer >= FormationTime / MaxStages * (bismuthCrystalStage + 1) && bismuthCrystalStage < MaxStages)
                {
                    bismuthCrystalStage++;
                    currentDamageAbsorbed = 0;
                }
            }

            public override void ModifyHurt(Player player, ref Player.HurtModifiers modifiers)
            {
                if (bismuthCrystalStage > 0 && currentDamageAbsorbed < MaxDamageAbsorption)
                {
                    float damageReduction = 0f;

                    if (ShtunUtils.AnyBossAlive())
                    {
                        damageReduction = player.ForceEffect<BismuthEffect>() ? 0.1f : 0.05f;
                    }
                    else
                    {
                        damageReduction = player.ForceEffect<BismuthEffect>() ? 0.2f : 0.1f;
                    }

                    float damageToAbsorb = modifiers.FinalDamage.Flat * damageReduction;

                    if (currentDamageAbsorbed + damageToAbsorb > MaxDamageAbsorption)
                    {
                        damageToAbsorb = MaxDamageAbsorption - currentDamageAbsorbed;
                    }

                    if (damageToAbsorb > 0)
                    {
                        modifiers.FinalDamage -= damageToAbsorb;
                        currentDamageAbsorbed += (int)damageToAbsorb;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<BismuthChest>();
            recipe.AddIngredient<BismuthLegs>();
            recipe.AddIngredient<BismuthHelm>();
            recipe.AddIngredient<VenomiteStaff>();
            recipe.AddIngredient<DeathsGarden>();
            recipe.AddIngredient<GospelOfDismay>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
