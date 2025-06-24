using Terraria.ID;
using Fargowiltas.Projectiles;
using Terraria.ModLoader;
using Spooky.Content.Items.SpookyBiome.Misc;
using Spooky.Content.Items.Cemetery.Misc;
using ssm.Core;
using System;
using Terraria;
using Spooky.Content.Generation;
using Microsoft.Xna.Framework;

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

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SwampyRenewal";
        public SwampyRenewalProj() : base("SwampyRenewal", ModContent.ProjectileType<CemeterySolutionProj>(), 4, false)
        {
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SwampyRenewalSupreme";
        public SwampyRenewalSupremeProj() : base("SwampyRenewalSupreme", ModContent.ProjectileType<CemeterySolutionProj>(), 4, true)
        {
        }
    }
}