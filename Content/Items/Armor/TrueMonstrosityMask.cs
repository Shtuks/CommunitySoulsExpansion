using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ssm.CrossMod.CraftingStations;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Consumables;

namespace ssm.Content.Items.Armor
{
    //[ExtendsFromMod(ModCompatibility.Calamity.Name)]
    //[JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class TrueMonstrosityMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(10, 0, 0, 0);
            Item.defense = 150;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1f;
            player.GetArmorPenetration(DamageClass.Generic) += 700f;
            player.GetCritChance(DamageClass.Generic) += 2f;
            player.maxMinions += 20;
            player.maxTurrets += 20;
            player.manaCost -= 1;
            player.ammoCost75 = true;
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
        public static string GetSetBonusString()
        {
            return Language.GetTextValue($"Mods.ssm.SetBonus.Monstrosity");
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TrueMonstrositySuit>() && legs.type == ModContent.ItemType<TrueMonstrosityPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 10f;
            player.GetDamage(DamageClass.Generic) += 1f;
            player.thorns = 1f;
            player.GetArmorPenetration(DamageClass.Generic) += 700f;
            player.GetAttackSpeed(DamageClass.Generic) += 2f;
            player.longInvince = true;
            player.endurance += 5f;
            player.lavaImmune = true;
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;
            player.ignoreWater = true;
            player.pStone = true;
            player.findTreasure = true;
            player.noKnockback = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<EternalEnergy>(15);
            recipe.AddIngredient<Sadism>(15);
            recipe.AddIngredient<MonstrosityMask>();

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
