using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Fargowiltas.Items.Tiles;
using ssm.Content.Items.Materials;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Calamity.Addons;
using ssm.Thorium.Items;
using ssm.Core;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Systems;

namespace ssm.Content.Items.DevItems
{
    internal class Catlight : DevItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.DevItems;
        }
        public override string devName => "StarlightCat";
        public override bool isUpgradeable => true;
        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.channel = true;
            Item.width = 40;
            Item.height = 40;
            Item.rare = 11;
            Item.DamageType = CatlightDamage.Instance;
            Item.damage = 1;
            Item.damage = 740;
            Item.crit = 10;
            Item.shoot = ModContent.ProjectileType<CatlightDeathray>();
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int GetPlayerProgression(Player player)
            {
                if (WorldSavingSystem.DownedMutant) return 12;
                if (WorldSavingSystem.DownedAbom) return 11;
                if (NPC.downedMoonlord) return 10;
                if (NPC.downedAncientCultist) return 9;
                if (NPC.downedGolemBoss) return 8;
                if (NPC.downedPlantBoss) return 7;
                if (NPC.downedMechBoss3) return 6;
                if (NPC.downedMechBoss2) return 5;
                if (NPC.downedMechBoss1) return 4;
                if (Main.hardMode) return 3;
                if (NPC.downedBoss3) return 2;
                if (NPC.downedBoss2) return 1;

                return 0;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()) || NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()))
            {
                damage *= 0.6f;
            }
            else
            {
                if(GetPlayerProgression(player) == 11)
                {
                    damage *= 0.1f;
                }
            }
            if (GetPlayerProgression(player) == 0)
            {
                damage.Flat = 15;
                Item.damage = 15;
            }
            if (GetPlayerProgression(player) == 1)
            {
                damage.Flat = 20;
                Item.damage = 20;
            }
            if (GetPlayerProgression(player) == 2)
            {
                damage.Flat = 30;
                Item.damage = 30;
            }
            if (GetPlayerProgression(player) == 3)
            {
                damage.Flat = 45;
                Item.damage = 45;
            }
            if (GetPlayerProgression(player) == 4)
            {
                damage.Flat = 50;
                Item.damage = 50;
            }
            if (GetPlayerProgression(player) == 5)
            {
                damage.Flat = 55;
                Item.damage = 55;
            }
            if (GetPlayerProgression(player) == 6)
            {
                damage.Flat = 60;
                Item.damage = 60;
            }
            if (GetPlayerProgression(player) == 7)
            {
                damage.Flat = 75;
                Item.damage = 75;
            }
            if (GetPlayerProgression(player) == 8)
            {
                damage.Flat = 90;
                Item.damage = 90;
            }
            if (GetPlayerProgression(player) == 9)
            {
                damage.Flat = 120;
                Item.damage = 120;
            }
            if (GetPlayerProgression(player) == 10)
            {
                damage.Flat = 200;
                Item.damage = 200;
            }
            if (GetPlayerProgression(player) == 11)
            {
                damage.Flat = 740;
                Item.damage = 740;
            }
            if (GetPlayerProgression(player) == 12)
            {
                damage.Flat = 7400;
                Item.damage = 7400;
            }

            damage.Flat *= 0.75f;
            Item.damage = (int)(Item.damage * 0.75f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int typeProj = ModContent.ProjectileType<CatlightDeathrayJRR>();

            if (NPC.downedMoonlord)
            {
                typeProj = ModContent.ProjectileType<CatlightDeathray>();
            }
            else if (Main.hardMode && !NPC.downedMoonlord)
            {
                typeProj = ModContent.ProjectileType<CatlightDeathrayJR>();
            }

            Projectile.NewProjectile(source, position, velocity, typeProj, damage, knockback, player.whoAmI);

            SoundEngine.PlaySound(new("ssm/Assets/Sounds/CatlightExplosion"), player.Center);

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            //if (CSEConfig.Instance.SecretBosses)
            //{
            //    recipe.AddIngredient<EternalScale>(1);
            //}

            //if (ModCompatibility.Entropy.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.Entropy.Mod.Find<ModItem>("WyrmTooth"), 1);
            //}
            //if (ModCompatibility.CatTech.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("NeutroniumBar"), 1);
            //}
            //if (ModCompatibility.WrathoftheGods.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.WrathoftheGods.Mod.Find<ModItem>("MetallicChunk"), 1);
            //    recipe.AddIngredient<NDMaterialPlaceholder>(1);
            //}
            //if (ModCompatibility.Calamity.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("ShadowspecBar"), 1);
            //    recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("MiracleMatter"), 1);
            //}
            //if (ModCompatibility.SacredTools.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("EmberOfOmen"), 1);
            //}

            //if (ModCompatibility.Homeward.Loaded && !ModCompatibility.Calamity.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("FinalBar"), 1);
            //}
            //if (ModCompatibility.Thorium.Loaded && !ModCompatibility.Calamity.Loaded)
            //{
            //    recipe.AddIngredient<DreamEssence>(1);
            //}
            //if (ModCompatibility.Redemption.Loaded && !ModCompatibility.Calamity.Loaded)
            //{
            //    recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("LifeFragment"), 1);
            //}
            //if (!ModCompatibility.Calamity.Loaded)
            //{
            //    recipe.AddIngredient<AbomEnergy>(1);
            //}

            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.Catfish);
            recipe.AddIngredient(ItemID.FallenStar, 3996);

            recipe.Register();
        }
    }
}
