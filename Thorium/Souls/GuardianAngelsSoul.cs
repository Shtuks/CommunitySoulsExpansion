using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Items.Terrarium;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using ThoriumMod.Items.BossThePrimordials.Dream;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GuardianAngelsSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }


        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 750000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "GraveGoods").UpdateAccessory(player, hideVisual);
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            player.GetDamage<HealerDamage>() += 0.25f;
            player.GetCritChance<HealerDamage>() += 0.12f;
            player.GetAttackSpeed<HealerDamage>() += 0.15f;
            player.GetAttackSpeed((DamageClass)ThoriumDamageBase<HealerTool>.Instance) += 0.20f;
            player.GetModPlayer<ThoriumPlayer>().healBonus += 20;

            thoriumPlayer.accSupportSash = true;

            thoriumPlayer.graveGoods = true;

            thoriumPlayer.darkAura = true;

            thoriumPlayer.healBonus += 20;

            thoriumPlayer.medicalAcc = true;

            if (player.AddEffect<GuardianEffect>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "MedicalBag").UpdateAccessory(player, true);
            }

            if (ModCompatibility.CalBardHealer.Loaded)
            {
                ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "ElementalBloom").UpdateAccessory(player, false);
            }
        }

        public class GuardianEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<GuardianAngelsSoul>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "HealerEssence");
            if (ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }

            if (!ModCompatibility.CalBardHealer.Loaded)
            {
                recipe.AddIngredient<SupportSash>();
                recipe.AddIngredient<SavingGrace>();
                recipe.AddIngredient<SoulGuard>();
                recipe.AddIngredient<ArchDemonCurse>();
                recipe.AddIngredient<ArchangelHeart>();
                recipe.AddIngredient<MedicalBag>();
                recipe.AddIngredient<TeslaDefibrillator>();
                recipe.AddIngredient<MoonlightStaff>();
                recipe.AddIngredient<TerrariumHolyScythe>();
                recipe.AddIngredient<TerraScythe>();
                recipe.AddIngredient<PhoenixStaff>();
                recipe.AddIngredient<ShieldDroneBeacon>();
                recipe.AddIngredient<LifeAndDeath>();
            }
            else
            {
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "ElementalBloom"));
                recipe.AddIngredient<SupportSash>();
                recipe.AddIngredient<SavingGrace>();
                recipe.AddIngredient<MedicalBag>();
                recipe.AddIngredient<UnboundFantasy>();
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "BloomingSaintessDevotion"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "SavingGrace"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "WilloftheRagnarok"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "PhoenicianBeak"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "MilkyWay"));
            }

            recipe.AddIngredient<OceanEssence>(5);
            recipe.AddIngredient<InfernoEssence>(5);
            recipe.AddIngredient<DeathEssence>(5);
            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
