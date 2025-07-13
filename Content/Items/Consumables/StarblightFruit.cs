using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Microsoft.Xna.Framework;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Consumables
{
    public class StarblightFruit : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.consumable = true;
            Item.UseSound = SoundID.Item123;
            Item.value = Item.sellPrice(0, 15);
        }

        public override bool CanUseItem(Player player)
        {
            return !player.CSE().starlightFruit;
        }

        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                player.CSE().starlightFruit = true;
            }
            return true;
        }

        public override Color? GetAlpha(Color lightColor) => Color.Red;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            if (ModCompatibility.SacredTools.Loaded)
            {
                //ModCompatibility.SacredTools.Mod.TryFind<ModItem>("ComboPotion", out ModItem soa);
                ModCompatibility.SacredTools.Mod.TryFind<ModItem>("EmberOfOmen", out ModItem soa2);
                //recipe.AddIngredient(soa, 50);
                recipe.AddIngredient(soa2, 5);
            }

            //if (ModCompatibility.AlchNPCs.Loaded)
            //{
            //    ModCompatibility.AlchNPCs.Mod.TryFind<ModItem>("ExplorerCombination", out ModItem alch1);
            //    ModCompatibility.AlchNPCs.Mod.TryFind<ModItem>("UniversalCombination", out ModItem alch);
            //    recipe.AddIngredient(alch, 50);
            //    recipe.AddIngredient(alch1, 50);
            //}

            if (ModCompatibility.Calamity.Loaded)
            {
                ModCompatibility.Calamity.Mod.TryFind<ModItem>("ShadowspecBar", out ModItem cal);
                recipe.AddIngredient(cal, 5);
            }
            else
            {
                recipe.AddIngredient<AbomEnergy>(10);
            }

            recipe.AddIngredient(ItemID.LifeFruit, 10);
            recipe.AddIngredient(ItemID.DemonHeart);
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
    public class StarblightAccessorySlot : ModAccessorySlot
    {
        public override bool IsEnabled()
        {
            if (!Player.active)
                return false;

            return Player.CSE().starlightFruit && ((ModCompatibility.Calamity.Loaded && ModCompatibility.SacredTools.Loaded) || (ModCompatibility.Thorium.Loaded && ModCompatibility.SacredTools.Loaded) || (ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded));
        }
        public override bool IsHidden() => IsEmpty && !IsEnabled();
    }

    //public class SlotSystem : ModSystem
    //{
    //    public override void Load()
    //    {
    //        IL_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += itemSlotDrawColourFixPatch;
    //    }
    //    public void itemSlotDrawColourFixPatch(ILContext il)
    //    {
    //        var ilCursor = new ILCursor(il);
    //        var backgroundTexture = 0;
    //        if (!ilCursor.TryGotoNext(MoveType.After, i => i.MatchCallvirt<AccessorySlotLoader>("GetBackgroundTexture"),
    //                i => i.MatchStloc(out backgroundTexture)))
    //        {
    //        }
    //        ilCursor.EmitLdarg3();
    //        ilCursor.EmitLdarg2();
    //        ilCursor.EmitCall<SlotSystem>("getColour");
    //        ilCursor.Emit(OpCodes.Stloc_S, (byte)8);
    //        ilCursor.EmitCall<SlotSystem>("getLoader");
    //        ilCursor.EmitLdarg3();
    //        ilCursor.EmitLdarg2();
    //        ilCursor.EmitCall<SlotSystem>("getTexture");
    //        ilCursor.EmitStloc(backgroundTexture);
    //    }

    //    public static AccessorySlotLoader getLoader()
    //    {
    //        return LoaderManager.Get<AccessorySlotLoader>();
    //    }

    //    public static Color getColour(int slot, int context)
    //    {
    //        return GetColorByLoadout(slot, context);
    //    }

    //    public static Texture2D getTexture(AccessorySlotLoader loader, int slot, int context)
    //    {
    //        ModAccessorySlot modAccessorySlot = loader.Get(slot);
    //        switch (context)
    //        {
    //            case -12:
    //                return ModContent.RequestIfExists(modAccessorySlot.DyeBackgroundTexture, out Asset<Texture2D> asset1)
    //                    ? asset1.Value
    //                    : TextureAssets.InventoryBack13.Value;
    //            case -11:
    //                return ModContent.RequestIfExists(modAccessorySlot.VanityBackgroundTexture, out Asset<Texture2D> asset2)
    //                    ? asset2.Value
    //                    : TextureAssets.InventoryBack13.Value;
    //            case -10:
    //                return ModContent.RequestIfExists(modAccessorySlot.FunctionalBackgroundTexture,
    //                    out Asset<Texture2D> asset3)
    //                    ? asset3.Value
    //                    : TextureAssets.InventoryBack13.Value;
    //            default:
    //                return TextureAssets.InventoryBack13.Value;
    //        }
    //    }
    //    public static Color GetColorByLoadout(int slot, int context)
    //    {
    //        var _lastTimeForVisualEffectsThatLoadoutWasChanged = (double)typeof(ItemSlot)
    //            .GetField("_lastTimeForVisualEffectsThatLoadoutWasChanged", BindingFlags.Static | BindingFlags.NonPublic)!
    //            .GetValue(null)!;
    //        Color color1 = Color.White;
    //        Color color2;
    //        if (TryGetSlotColor(Main.LocalPlayer.CurrentLoadoutIndex, context, out color2))
    //            color1 = color2;
    //        Color color3 = new Color(color1.ToVector4() * Main.inventoryBack.ToVector4());
    //        float num = Utils.Remap(
    //            (float)(Main.timeForVisualEffects - _lastTimeForVisualEffectsThatLoadoutWasChanged), 0.0f, 30f, 0.5f, 0.0f);
    //        Color white = Color.White;
    //        double amount = num * num * num;
    //        return Color.Lerp(color3, white, (float)amount);
    //    }

    //    public static bool TryGetSlotColor(int loadoutIndex, int context, out Color color)
    //    {
    //        var LoadoutSlotColors = (Color[,])typeof(ItemSlot)
    //            .GetField("LoadoutSlotColors", BindingFlags.Static | BindingFlags.NonPublic)!
    //            .GetValue(null)!;
    //        color = new Color();
    //        if (loadoutIndex < 0 || loadoutIndex >= 3)
    //            return false;
    //        int index = -1;
    //        switch (context)
    //        {
    //            case 8:
    //            case 10:
    //            case -10:
    //                index = 0;
    //                break;
    //            case 9:
    //            case 11:
    //            case -11:
    //                index = 1;
    //                break;
    //            case 12:
    //            case -12:
    //                index = 2;
    //                break;
    //        }

    //        if (index == -1)
    //            return false;
    //        color = LoadoutSlotColors[loadoutIndex, index];
    //        return true;
    //    }
    //}
}