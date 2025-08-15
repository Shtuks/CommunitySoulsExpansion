using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Patreon.DemonKing;
using FargowiltasSouls.Content.Patreon.DevAesthetic;
using FargowiltasSouls.Content.Patreon.Duck;
using FargowiltasSouls.Content.Patreon.GreatestKraken;
using FargowiltasSouls.Content.Patreon.Sasha;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;

namespace ssm
{
    public class CSESets : ModSystem
    {
        public static bool GetValue(bool[] set, int index) => set != null && set[index];
        public class Items
        {
            public static bool[] ChampionTierFargoWeapon;
            public static bool[] AbomTierFargoWeapon;
            public static bool[] MutantTierFargoWeapon;
        }
        public class NPCs
        {
            public static int[] SwarmHealth;
        }
        public override void PostSetupContent()
        {
            SetFactory itemFactory = ItemID.Sets.Factory;
            NPCs.SwarmHealth = NPCID.Sets.Factory.CreateIntSet(0);

            Items.ChampionTierFargoWeapon = itemFactory.CreateBoolSet(false,
                ItemType<EaterLauncher>(),
                ItemType<FleshCannon>(),
                ItemType<HellZone>(),
                ItemType<MechanicalLeashOfCthulhu>(),
                ItemType<SlimeSlingingSlasher>(),
                ItemType<TheBigSting>(),
                ItemType<BigBrainBuster>(),
                ItemType<ScientificRailgun>(),
                ItemType<VortexMagnetRitual>(),
                ItemType<MissDrakovisFishingPole>(),
                ItemType<DeviousAestheticus>()
            );
            Items.AbomTierFargoWeapon = itemFactory.CreateBoolSet(false,
                ItemType<DragonBreath2>(),
                ItemType<DestroyerGun2>(),
                ItemType<GolemTome2>(),
                ItemType<GeminiGlaives>(),
                ItemType<Blender>(),
                ItemType<RefractorBlaster2>(),
                ItemType<NukeFishron>(),
                ItemType<StaffOfUnleashedOcean>(),
                ItemType<TheDestroyer>(),
                ItemType<UmbraRegalia>()
            );
            Items.MutantTierFargoWeapon = itemFactory.CreateBoolSet(false,
                ItemType<PhantasmalLeashOfCthulhu>(),
                ItemType<SlimeRain>(),
                ItemType<GuardianTome>(),
                ItemType<TheBiggestSting>()
            );
        }
    }
}
