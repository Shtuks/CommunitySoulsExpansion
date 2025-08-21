using ContinentOfJourney.Items.Armor;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name)]
    public class HWJItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item entity)
        {
            //defense bloat removal
            if (
                //equilibrium
                entity.type == ModContent.ItemType<EquilibriumBreastplate>()
                || entity.type == ModContent.ItemType<EquilibriumLeggings>()
                || entity.type == ModContent.ItemType<EquilibriumMask>()
                //biologic
                || entity.type == ModContent.ItemType<BiologicalBreastplate>()
                || entity.type == ModContent.ItemType<BiologicalHelmet>()
                || entity.type == ModContent.ItemType<BiologicalLeggings>()
                //chrono
                || entity.type == ModContent.ItemType<PerpetualHelmet>()
                || entity.type == ModContent.ItemType<PerpetualLeggings>()
                || entity.type == ModContent.ItemType<PerpetualPlate>()
                //chrono
                || entity.type == ModContent.ItemType<HeliologyHelmet>()
                || entity.type == ModContent.ItemType<HeliologyLeggings>()
                || entity.type == ModContent.ItemType<HeliologyPlate>()
                //sun
                || entity.type == ModContent.ItemType<SunlightBreastplate>()
                || entity.type == ModContent.ItemType<SunlightHelmet>()
                || entity.type == ModContent.ItemType<SunlightLegging>()
                )
            {
                entity.defense = entity.defense / 2;
            }
            if (
                //aurora
                entity.type == ModContent.ItemType<AuroraBoots>()
                || entity.type == ModContent.ItemType<AuroraHeadwear>()
                || entity.type == ModContent.ItemType<AuroraRobe>()
                //watchman
                || entity.type == ModContent.ItemType<WatchmanDress>()
                || entity.type == ModContent.ItemType<WatchmanHat>()
                || entity.type == ModContent.ItemType<WatchmanShirt>()
                //forest
                || entity.type == ModContent.ItemType<ForestBreastplate>()
                || entity.type == ModContent.ItemType<ForestHelmet>()
                || entity.type == ModContent.ItemType<ForestLeggings>()
                //reflector
                || entity.type == ModContent.ItemType<ReflectorBreastplate>()
                || entity.type == ModContent.ItemType<ReflectorHelmet>()
                || entity.type == ModContent.ItemType<ReflectorLeggings>()
                )
            {
                entity.defense = entity.defense / 3 * 2;
            }
        }
    }
}
