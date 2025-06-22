using Terraria.ModLoader;
using Fargowiltas.Projectiles;
using Spooky.Content.Items.SpookyBiome.Misc;
using ssm.Core;

namespace ssm.Spooky.Renewals
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]

    public class SpookyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewal";

        public SpookyRenewalProj() : base("SpookyRenewal", ModContent.ProjectileType<SpookySolutionProj>(), 1, false)
        {
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewalSupreme";

        public SpookyRenewalSupremeProj() : base("SpookyRenewalSupreme", ModContent.ProjectileType<SpookySolutionProj>(), 1, true)
        {
        }
    }
}