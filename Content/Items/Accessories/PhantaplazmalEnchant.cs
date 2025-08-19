using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ID;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using Terraria.Localization;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using Luminance.Core.Graphics;

namespace ssm.Content.Items.Accessories
{
    public class PhantaplazmalEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.EternityForce;
        }
        private readonly Mod FargoSoul = ModLoader.GetMod("FargowiltasSouls");

        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[Type] = true;

        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override Color nameColor => new(0, 255, 170);

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(28, 222, 152));
                shader.TrySetParameter("secondaryColor", new Color(168, 245, 228));
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CSEPlayer>().equippedPhantasmalEnchantment = true;
            ModContent.Find<ModItem>(FargoSoul.Name, "MutantEye").UpdateAccessory(player, true);

            if (player.AddEffect<PhantaplazmalEffect>(Item))
            {
                ModContent.Find<ModItem>(FargoSoul.Name, "MutantMask").UpdateArmorSet(player);
                ModContent.Find<ModItem>(FargoSoul.Name, "MutantBody").UpdateArmorSet(player);
                ModContent.Find<ModItem>(FargoSoul.Name, "MutantPants").UpdateArmorSet(player);
            }
            player.buffImmune[ModContent.Find<ModBuff>(FargoSoul.Name, "MutantPresenceBuff").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>(FargoSoul.Name, "GodEaterBuff").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<MutantMask>();
            recipe.AddIngredient<MutantBody>();
            recipe.AddIngredient<MutantPants>();
            recipe.AddIngredient<MutantEye>();
            recipe.AddIngredient<PhantasmalLeashOfCthulhu>(1);
            recipe.AddIngredient<GuardianTome>(1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public class PhantaplazmalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MutantEye>();
        }
    }
}
