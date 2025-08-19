using System;
using System.Collections.Generic;
using System.Text;
using CalamityMod.Items.LoreItems;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Lore
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeMonstrosity" })]
    public class MonstrosityLore : LoreItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
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

        //public override void AddRecipes()
        //{
        //    this.CreateRecipe(1).AddIngredient<MonstrosityTrophy>(1).AddTile(101);
        //}
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeMutant" })]
    public class MutantLore : LoreItem
    {
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Teal);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient<MutantTrophy>(1).AddTile(101);
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeDukeEX" })]
    public class DukeEXLore : LoreItem
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.CadetBlue);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        //public override void AddRecipes()
        //{
        //    this.CreateRecipe(1).AddIngredient<DukeEXTrophy>(1).AddTile(101);
        //}
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeCeiling" })]
    public class CeilingLore : LoreItem
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Teal);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        //public override void AddRecipes()
        //{
        //    this.CreateRecipe(1).AddIngredient<DukeEXTrophy>(1).AddTile(101);
        //}
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeGuntera" })]
    public class GunteraLore : LoreItem
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Lime);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        //public override void AddRecipes()
        //{
        //    this.CreateRecipe(1).AddIngredient<DukeEXTrophy>(1).AddTile(101);
        //}
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeEch" })]
    public class EchLore : LoreItem
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
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

        //public override void AddRecipes()
        //{
        //    this.CreateRecipe(1).AddIngredient<EchTrophy>(1).AddTile(101);
        //}
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeDevi" })]
    public class DeviLore : LoreItem
    {
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Pink);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient<DeviTrophy>(1).AddTile(101);
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeAbom" })]
    public class AbomLore : LoreItem
    {
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Orange);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient<MutantTrophy>(1).AddTile(101);
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeAmalgam" })]
    public class AmalgamLore : LoreItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Green);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe(1).AddIngredient<AmalgamTrophy>(1).AddTile(101);
        //}
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [LegacyName(new string[] { "KnowledgeDivergentt" })]
    public class DivergenttLore : LoreItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.AlternativeSiblings;
        }
        public override void SetStaticDefaults() => base.SetStaticDefaults();

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.rare = 11;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Color.Yellow);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe(1).AddIngredient<DivergettTrophy>(1).AddTile(101);
        //}
    }
}
