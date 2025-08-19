using Terraria.ModLoader;
using Terraria;
using ssm.Content.Items.Armor;
using ssm.CrossMod.CraftingStations;
using FargowiltasSouls.Content.Items.Armor;
using ssm.Content.Items.Materials;
using ssm.Core;

namespace ssm.Content.Items.DevItems.TModTechPucheglazik
{
    [AutoloadEquip(EquipType.Head)]
    public class TModTechHelmet : DevItem
    {
        public override bool isUpgradeable => false;
        public override string devName => "Pucheglazik";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.DevItems && ModCompatibility.CatTech.Loaded;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(10, 0, 0, 0);
            Item.defense = 100;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1f;
            player.GetCritChance(DamageClass.Generic) += 30f;
            player.maxMinions += 10;
            player.maxTurrets += 10;
            player.manaCost -= 1;
            player.ammoCost75 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TModTechHelmet>() && legs.type == ModContent.ItemType<TrueMonstrosityPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 10f;
            player.thorns = 1f;
            player.GetArmorPenetration(DamageClass.Generic) += 1000f;
            player.GetAttackSpeed(DamageClass.Generic) += 1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            
            recipe.AddIngredient<tModLoadiumBar>(15);
            recipe.AddIngredient<MutantMask>();
            recipe.AddIngredient<StyxCrown>();
            if (ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("DemonshadeHelm"), 1);
                recipe.AddRecipeGroup("ssm:Auric");
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                recipe.AddRecipeGroup("ssm:AsthralHelms");
            }
            if (ModCompatibility.CatTech.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("DigammaHead"), 1);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("NeutroniumArmorPlating"), 10);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("DNTArmorPlating"), 10);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("Circuit_Ultimate"), 5);
            }

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
    [AutoloadEquip(EquipType.Body)]
    public class TModTechSuit : DevItem
    {
        public override bool isUpgradeable => false;
        public override string devName => "Pucheglazik";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.DevItems && ModCompatibility.CatTech.Loaded;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(10, 0, 0, 0);
            Item.defense = 100;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1f;
            player.GetCritChance(DamageClass.Generic) += 30f;
            player.statLifeMax2 += 200;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();


            recipe.AddIngredient<tModLoadiumBar>(15);
            recipe.AddIngredient<MutantBody>();
            recipe.AddIngredient<StyxChestplate>();
            if (ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("DemonshadeBreastplate"), 1);
                recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("AuricTeslaBodyArmor"), 1);
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("AsthralChest"), 1);
            }
            if (ModCompatibility.CatTech.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("DigammaBody"), 1);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("NeutroniumArmorPlating"), 15);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("DNTArmorPlating"), 15);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("Circuit_Ultimate"), 5);
            }

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class TModTechLegs : DevItem
    {
        public override bool isUpgradeable => false;
        public override string devName => "Pucheglazik";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.DevItems && ModCompatibility.CatTech.Loaded;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(10, 0, 0, 0);
            Item.defense = 100;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1f;
            player.GetCritChance(DamageClass.Generic) += 30f;
            player.moveSpeed += 0.5f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();


            recipe.AddIngredient<tModLoadiumBar>(15);
            recipe.AddIngredient<MutantPants>();
            recipe.AddIngredient<StyxLeggings>();
            if (ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("DemonshadeGreaves"), 1);
                recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("AuricTeslaCuisses"), 1);
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("AsthralLegs"), 1);
            }
            if (ModCompatibility.CatTech.Loaded)
            {
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("DigammaLegs"), 1);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("NeutroniumArmorPlating"), 10);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("DNTArmorPlating"), 10);
                recipe.AddIngredient(ModCompatibility.CatTech.Mod.Find<ModItem>("Circuit_Ultimate"), 5);
            }

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
