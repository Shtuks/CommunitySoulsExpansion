using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Content.Items.Armor.Lunar.Nebula;
using SacredTools.Content.Items.Accessories;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Weapons.Asthraltite;
using ssm.Core;
using SacredTools.Content.Projectiles.Armors.Nuba;
using FargowiltasSouls;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class NebulousApprenticeEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(206, 7, 221);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NebulousApprenticeEffect>(Item);
            ModContent.Find<ModItem>(this.soa.Name, "NubasBlessing").UpdateAccessory(player, false);
        }

        public class NebulousApprenticeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NebulousApprenticeEnchant>();

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if (!target.immortal && Main.rand.NextBool(10))
                {
                    float num2 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float num3 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    num2 *= 10f;
                    num3 *= 10f;
                    int[] array0 = new int[3]
                    {
                        ModContent.ProjectileType<NubaFlameDamage>(),
                        ModContent.ProjectileType<NubaFlameDefense>(),
                        ModContent.ProjectileType<NubaFlameHealth>(),
                    };
                    int[] array = new int[5]
                    {
                        ModContent.ProjectileType<NubaFlameDamage>(),
                        ModContent.ProjectileType<NubaFlameDefense>(),
                        ModContent.ProjectileType<NubaFlameHealth>(),
                        ModContent.ProjectileType<NubaFlameMana>(),
                        ModContent.ProjectileType<NubaFlameSpeed>()
                    };
                    Projectile.NewProjectile(target.GetSource_OnHurt(player), target.Center.X, target.Center.Y, num2, num3, player.ForceEffect<NebulousApprenticeEffect>() ? array[Main.rand.Next(5)] : array0[Main.rand.Next(3)], 0, 0f, projectile.owner);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<NubaHood>();
            recipe.AddIngredient<NubaChest>();
            recipe.AddIngredient<NubaRobe>();
            recipe.AddIngredient<NubasBlessing>();
            recipe.AddIngredient<LunaticBurstStaff>();
            recipe.AddIngredient<AsthralStaff>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
