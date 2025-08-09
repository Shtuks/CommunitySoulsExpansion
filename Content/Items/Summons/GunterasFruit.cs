using Fargowiltas.Items.Summons;
using Fargowiltas.Items.Summons.Mutant;
using ssm.Content.NPCs.Guntera;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Summons
{
    public class GunterasFruit : BaseSummon
    {
        public override int NPCType => ModContent.NPCType<Guntera>();

        public override string NPCName => "Guntera";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient<PlanterasFruit>(5)
               .AddIngredient(ItemID.Celeb2)
               .AddIngredient(ItemID.Shotgun)
               .AddIngredient(ItemID.SniperRifle)
               .AddIngredient(ItemID.QuadBarrelShotgun)
               .AddIngredient(ItemID.VenusMagnum)
               .AddIngredient(ItemID.Uzi)
               .AddTile(TileID.DemonAltar)
               .Register();
        }
    }
}
