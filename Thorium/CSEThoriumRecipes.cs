using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using ssm.Thorium.Souls;
using ThoriumMod.Items.Titan;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using Terraria.ID;
using ThoriumMod.Items.Misc;
using FargowiltasSouls.Content.Items.Summons;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using FargowiltasSouls.Content.Items.Armor;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ssm.Thorium.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using SacredTools.Content.Items.Materials;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.Darksteel;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Lodestone;
using ThoriumMod.Items.Illumite;
using ThoriumMod.Items.Valadium;
using ThoriumMod.Items.Dragon;
using ThoriumMod.Items.Flesh;
using ssm.Thorium.Items;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    class CSEThoriumRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //jester mask
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Jester Mask", ModContent.ItemType<JestersMask>(), ModContent.ItemType<JestersMask2>());
            RecipeGroup.RegisterGroup("ssm:AnyJesterMask", group);
            //jester shirt
            group = new RecipeGroup(() => Lang.misc[37] + " Jester Shirt", ModContent.ItemType<JestersShirt>(), ModContent.ItemType<JestersShirt2>());
            RecipeGroup.RegisterGroup("ssm:AnyJesterShirt", group);
            //jester legging
            group = new RecipeGroup(() => Lang.misc[37] + " Jester Leggings", ModContent.ItemType<JestersLeggings>(), ModContent.ItemType<JestersLeggings2>());
            RecipeGroup.RegisterGroup("ssm:AnyJesterLeggings", group);
            //evil wood tambourine
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood Tambourine", ModContent.ItemType<EbonWoodTambourine>(), ModContent.ItemType<ShadeWoodTambourine>());
            RecipeGroup.RegisterGroup("ssm:AnyTambourine", group);
            //fan letter
            group = new RecipeGroup(() => Lang.misc[37] + " Fan Letter", ModContent.ItemType<FanLetter>(), ModContent.ItemType<FanLetter2>());
            RecipeGroup.RegisterGroup("ssm:AnyLetter", group);
            //bugle horn
            group = new RecipeGroup(() => Lang.misc[37] + " Bugle Horn", ModContent.ItemType<GoldBugleHorn>(), ModContent.ItemType<PlatinumBugleHorn>());
            RecipeGroup.RegisterGroup("ssm:AnyBugleHorn", group);
            //titan 
            group = new RecipeGroup(() => Lang.misc[37] + " Titan Headgear", ModContent.ItemType<TitanHelmet>(), ModContent.ItemType<TitanMask>(), ModContent.ItemType<TitanHeadgear>());
            RecipeGroup.RegisterGroup("ssm:AnyTitanHelmet", group);
            //any gem
            group = new RecipeGroup(() => Lang.misc[37] + " Gem", ModContent.ItemType<Opal>(), ModContent.ItemType<Aquamarine>());
            RecipeGroup.RegisterGroup("ssm:AnyThoriumGem", group);
            // rhapsodist
            group = new RecipeGroup(() => Lang.misc[37] + " Rhapsodist Helmet", ModContent.ItemType<SoloistHat>(), ModContent.ItemType<InspiratorsHelmet>());
            RecipeGroup.RegisterGroup("ssm:AnyRhapsodistHelmet", group);
            // assassin
            group = new RecipeGroup(() => Lang.misc[37] + " Assassin Helmet", ModContent.ItemType<MasterMarksmansScouter>(), ModContent.ItemType<MasterArbalestHood>());
            RecipeGroup.RegisterGroup("ssm:AnyAssassinHelmet", group);

        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<CoffinSummon>())
                .AddIngredient(ItemID.ClayBlock, 15)
                .AddIngredient(ItemID.FossilOre, 8)
                .AddRecipeGroup("ssm:AnyThoriumGem", 4)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
        public override void PostAddRecipes()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
                if (CSEConfig.Instance.Thorium && recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<ThoriumSoul>())
                {
                    recipe.AddIngredient<ThoriumSoul>();
                }
                if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<BardSoul>())
                {
                    recipe.AddIngredient<GuardianAngelsSoul>();
                    recipe.AddIngredient<BardSoul>();
                }
                if (!ModCompatibility.Calamity.Loaded && recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<OlympiansSoul>())
                {
                    recipe.AddIngredient<OlympiansSoul>();
                }
                if (recipe.HasResult<TerrariumDefender>() && !recipe.HasIngredient<CorruptedWarShield>())
                {
                    recipe.AddIngredient<CorruptedWarShield>();
                }
                if (recipe.HasResult<HungeringBlossom>() && !recipe.HasIngredient(ItemID.NaturesGift))
                {
                    recipe.RemoveIngredient(ItemID.ManaFlower);
                    recipe.AddIngredient(ItemID.NaturesGift);
                }

                if (recipe.HasResult<CosmoForce>() && !recipe.HasIngredient<OceanEssence>())
                {
                    recipe.AddIngredient<DreamEssence>(2);
                }
                if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasIngredient<TerrariumWings>())
                {
                    recipe.AddIngredient<TerrariumWings>();
                    recipe.AddIngredient<DragonWings>();
                    recipe.AddIngredient<FleshWings>();
                }
                //if (recipe.HasResult<WorldShaperSoul>() && !recipe.HasIngredient<GeodeEnchant>())
                //{
                //    recipe.AddIngredient<GeodeEnchant>();
                //}

                if (recipe.HasResult<StyxCrown>() && recipe.HasIngredient(549))
                {
                    recipe.RemoveIngredient(549);
                    recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 10);
                }
                if (recipe.HasResult<StyxLeggings>() && recipe.HasIngredient(547))
                {
                    recipe.RemoveIngredient(547);
                    recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 10);
                }
                if (recipe.HasResult<StyxChestplate>() && recipe.HasIngredient(548))
                {
                    recipe.RemoveIngredient(548);
                    recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 10);
                }

                if (recipe.HasResult<GaiaHelmet>() && !recipe.HasIngredient<DarkMatter>())
                {
                    recipe.AddIngredient(ModContent.ItemType<HolyKnightsAlloy>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<BloomWeave>(), 6);
                }
                if (recipe.HasResult<GaiaGreaves>() && !recipe.HasIngredient<DarkMatter>())
                {
                    recipe.AddIngredient(ModContent.ItemType<HolyKnightsAlloy>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<BloomWeave>(), 6);
                }
                if (recipe.HasResult<GaiaPlate>() && !recipe.HasIngredient<DarkMatter>())
                {
                    recipe.AddIngredient(ModContent.ItemType<HolyKnightsAlloy>(), 9);
                    recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 9);
                    recipe.AddIngredient(ModContent.ItemType<BloomWeave>(), 9);
                }

                if ((recipe.HasResult<ColossusSoul>() ||
                    recipe.HasResult<FlightMasterySoul>() ||
                    recipe.HasResult<ArchWizardsSoul>() ||
                    recipe.HasResult<BerserkerSoul>() ||
                    recipe.HasResult<SnipersSoul>() ||
                    recipe.HasResult<ConjuristsSoul>()) && !recipe.HasIngredient<OceanEssence>())
                {
                    recipe.AddIngredient<DreamEssence>(5);
                }

                if (recipe.HasResult(ItemID.DrillContainmentUnit) && !recipe.HasIngredient<TerrariumCore>())
                {
                    recipe.RemoveIngredient(ItemID.MeteoriteBar);
                    recipe.RemoveIngredient(ItemID.HellstoneBar);
                    recipe.RemoveIngredient(ItemID.ShroomiteBar);
                    recipe.RemoveIngredient(ItemID.SpectreBar);
                    recipe.RemoveIngredient(ItemID.ChlorophyteBar);
                    recipe.AddIngredient<TerrariumCore>(40);
                    recipe.AddIngredient<TitanicBar>(40);
                    recipe.AddIngredient<SandstoneIngot>(40);
                    recipe.AddIngredient<aDarksteelAlloy>(40);
                    recipe.AddIngredient<AquaiteBar>(40);
                    recipe.AddIngredient<LodeStoneIngot>(40);
                    recipe.AddIngredient<IllumiteIngot>(40);
                    recipe.AddIngredient<ValadiumIngot>(40);
                }
            }
		}
	}
}
