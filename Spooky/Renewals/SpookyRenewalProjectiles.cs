using Terraria.ID;
using Terraria.ModLoader;
using Fargowiltas.Projectiles;
using Spooky.Content.Items.SpookyBiome.Misc;

namespace ssm.Spooky.Renewals
{
    public class SpookyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewal";

        public SpookyRenewalProj() : base("SpookyRenewal", ModContent.ProjectileType<SpookySolutionProj>(), 1, false)
        {
        }
    }

    public class SpookyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewalSupreme";
        public SpookyRenewalSupremeProj() : base("SpookyRenewalSupreme", ModContent.ProjectileType<SpookySolutionProj>(), 1, true)
        {
        }
    }
}