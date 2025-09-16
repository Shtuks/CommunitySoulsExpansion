using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Core;

namespace ssm.SpiritMod
{
    [ExtendsFromMod(ModCompatibility.Spirit.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spirit.Name)]
    public class SpiritSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SpiritMod;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationRectangularV(6, 4, 5));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 38;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 5000000;
            Item.defense = 20;
            Item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AdventurerForce").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AtlantisForce").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "HurricaneForce").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FrostburnForce").UpdateAccessory(player, hideVisual);

            player.AddEffect<SpiritEffect>(Item);
        }

        public class SpiritEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (!ModCompatibility.Calamity.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }
            recipe.AddIngredient(null, "AdventurerForce");
            recipe.AddIngredient(null, "AtlantisForce");
            recipe.AddIngredient(null, "HurricaneForce");
            recipe.AddIngredient(null, "FrostburnForce");

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}