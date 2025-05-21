using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Crossmod.Name)]
    internal class EEWeaponsUnnerf : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<GuardianTome>())
            {
                entity.damage = (int)(entity.damage + (entity.damage * 0.8f));
            }
            if (entity.type == ModContent.ItemType<TheBiggestSting>())
            {
                entity.damage = (int)(entity.damage + (entity.damage * 0.7f));
            }
            if (entity.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>())
            {
                entity.damage = (int)(entity.damage + (entity.damage * 0.5f));
            }
            if (entity.type == ModContent.ItemType<SlimeRain>())
            {
                entity.damage = (int)(entity.damage + (entity.damage * 0.92f));
            }
        }
    }
}
