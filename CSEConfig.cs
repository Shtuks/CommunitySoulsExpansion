using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ssm
{
    [BackgroundColor(32, 50, 32, 216)]
    public class CSEConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static CSEConfig Instance;

        [Header("General")]

        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool WorldEnterMessage { get; set; }

        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool MutantWontShutUp { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool DebugMode { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool ExperimentalContent { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool AlternativeSiblings { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool DevItems { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SecretBosses { get; set; }

        [Header("CrossMods")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Boots { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Shields { get; set; }

        [Header("EnchantsLoad")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Thorium { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EternityForce { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SacredTools { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Polarities { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Homeward { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Redemption { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Spooky { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Beekeeper { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool TerMerica { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SpiritMod { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Consolaria { get; set; }

        [Header("EmodeAccesLoad")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeThorium { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeRedemption { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeSoA { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeHWJ { get; set; }

        [Header("EmodeWeaponsLoad")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeThoriumW { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeRedemptionW { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeSoAW { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool EmodeHWJW { get; set; }

        [Header("BossRush")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool ThoriumBR { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool RedemptionBR { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SoABR { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool HWJBR { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool BossRushPostMutant { get; set; }
    }
}
