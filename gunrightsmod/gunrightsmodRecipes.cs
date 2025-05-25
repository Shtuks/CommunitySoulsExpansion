using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Terraria.Localization;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gunrightsmod.Content.Items;

namespace ssm.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class gunrightsmodRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TerMerica;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Kevlar Helmet", ModContent.ItemType<KevlarBeret>(), ModContent.ItemType<KevlarFedora>(), ModContent.ItemType<KevlarHelmet>(), ModContent.ItemType<KevlarMask>(), ModContent.ItemType<KevlarVisor>());
            RecipeGroup.RegisterGroup("ssm:KevlarHelms", rec);
        }
    }
}   
