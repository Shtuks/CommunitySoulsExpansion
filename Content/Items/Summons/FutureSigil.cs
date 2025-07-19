using Fargowiltas.Items.Summons;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.NPCs.Ceiling;

namespace ssm.Content.Items.Summons
{
    public class FutureSigil : BaseSummon
    {
        public override string Texture => "Terraria/Images/Item_3601";
        public override int NPCType => ModContent.NPCType<CeilingOfMoonLord>();

        public override string NPCName => "CeilingOfMoonLord";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient(ItemID.LunarBar, 5)
               .AddIngredient(ItemID.FragmentNebula, 50)
               .AddIngredient(ItemID.FragmentSolar, 50)
               .AddIngredient(ItemID.FragmentStardust, 50)
               .AddIngredient(ItemID.FragmentVortex, 50)
               .AddTile(TileID.DemonAltar)
               .Register();
        }
    }
}
