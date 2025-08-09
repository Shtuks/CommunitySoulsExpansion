using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ssm.CrossMod.CraftingStations;
using Fargowiltas.Items.Vanity;
using ssm.Content.Items.Accessories;
using ssm.Content.Items.Consumables;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TrueLumberjackMask : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return FargoSoulsUtil.AprilFools;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(100, 0, 0, 0);
            Item.defense = int.MaxValue/1000;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Main.DiscoColor);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += int.MaxValue / 1000;
            player.GetArmorPenetration(DamageClass.Generic) += int.MaxValue / 1000;
            player.GetCritChance(DamageClass.Generic) += int.MaxValue / 1000;
            player.maxMinions += int.MaxValue / 1000;
            player.maxTurrets += int.MaxValue / 1000;
            player.manaCost -= int.MaxValue / 1000;
            player.ammoCost75 = true;
        }

        public static string GetSetBonusString()
        {
            return Language.GetTextValue($"Mods.ssm.SetBonus.TrueLumberjack");
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TrueLumberjackBody>() && legs.type == ModContent.ItemType<TrueLumberjackPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.CSE().lumberjackSet = true;
            player.AddEffect<MayoRing>(Item);
            player.GetCritChance(DamageClass.Generic) += int.MaxValue / 10;
            player.GetDamage(DamageClass.Generic) += int.MaxValue / 10;
            player.thorns = int.MaxValue / 100;
            player.GetArmorPenetration(DamageClass.Generic) += int.MaxValue / 10;
            player.GetAttackSpeed(DamageClass.Generic) += int.MaxValue / 1000;
            player.endurance += int.MaxValue / 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LumberjackMask>();

            if (CSEConfig.Instance.AlternativeSiblings) { recipe.AddIngredient<Sadism>(100); recipe.AddIngredient<StargateSoul>(4); }
            recipe.AddIngredient<EternalEnergy>(100);
            recipe.AddIngredient<Eridanium>(100);
            recipe.AddIngredient<AbomEnergy>(100);
            recipe.AddIngredient<DeviatingEnergy>(100);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }

        public class MayoRing : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<LumberjackArmorHeader>();
            public override int ToggleItemType => ModContent.ItemType<TrueLumberjackMask>();
            public override void PostUpdateEquips(Player player)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SquirrelRing>()] < 1)
                    FargoSoulsUtil.NewSummonProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<SquirrelRing>(), 8000000, 0f, player.whoAmI);
            }

        }
    }
}
