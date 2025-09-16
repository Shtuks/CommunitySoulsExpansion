using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Buffs.Bard;
using ssm.CrossMod.CraftingStations;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core;
using Terraria.DataStructures;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 61));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 52;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 5000000;
            Item.defense = 30;
            Item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ClearBuff(ModContent.BuffType<MetronomeDebuff>());

            //MUSPELHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MuspelheimForce").UpdateAccessory(player, hideVisual);
            //JOTUNHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "JotunheimForce").UpdateAccessory(player, hideVisual);
            //ALFHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AlfheimForce").UpdateAccessory(player, hideVisual);
            //NIFLHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "NiflheimForce").UpdateAccessory(player, hideVisual);
            //SVARTALFHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SvartalfheimForce").UpdateAccessory(player, hideVisual);
            //MIDGARD
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MidgardForce").UpdateAccessory(player, hideVisual);
            //VANAHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "VanaheimForce").UpdateAccessory(player, hideVisual);
            //HELHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "HelheimForce").UpdateAccessory(player, hideVisual);
            //ASGARD
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AsgardForce").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MotDE").UpdateAccessory(player, hideVisual);

            player.AddEffect<ThoriumEffect>(Item);
        }

        public class ThoriumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            if (!ModCompatibility.Calamity.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }
            recipe.AddIngredient(null, "MuspelheimForce");
            recipe.AddIngredient(null, "JotunheimForce");
            recipe.AddIngredient(null, "AlfheimForce");
            recipe.AddIngredient(null, "NiflheimForce");
            recipe.AddIngredient(null, "SvartalfheimForce");
            recipe.AddIngredient(null, "MidgardForce");
            recipe.AddIngredient(null, "VanaheimForce");
            recipe.AddIngredient(null, "HelheimForce");
            recipe.AddIngredient(null, "AsgardForce");
            recipe.AddIngredient(null, "MotDE");

            recipe.AddTile<DreamersForgeTile>();

            recipe.Register();
        }
    }
}