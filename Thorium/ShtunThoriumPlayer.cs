using Terraria.ModLoader;
using ssm.Core;
using ssm.Content.Buffs;
using Terraria.ID;
using Terraria;
using ssm.Thorium.Enchantments;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]

    public partial class ShtunThoriumPlayer : ModPlayer
    {
        public bool ThunderTalonEternity;
        public bool DarkenedCloak;

        //to complicated behaviour for enchantment methods
        public bool cyberEnchant;
        public int orbCount = 2;
        private CyberneticOrb[] orbs;
        public override void Initialize()
        {
            orbs = new CyberneticOrb[orbCount];
        }
        public override void ResetEffects()
        {
            if (!cyberEnchant)
            {
                for (int i = 0; i < orbs.Length; i++)
                {
                    orbs[i] = null;
                }
            }
            cyberEnchant = false;
            ThunderTalonEternity = false;
            DarkenedCloak = false;
        }
        public override void PostUpdate()
        {
            if (orbCount > 0 && cyberEnchant)
            {
                for (int i = 0; i < orbCount; i++)
                {
                    if (orbs[i] == null || !orbs[i].active)
                    {
                        orbs[i] = new CyberneticOrb(Player, i, orbCount);
                    }
                    orbs[i].Update();
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (orbCount > 0 && cyberEnchant)
            {
                for (int i = 0; i < orbCount; i++)
                {
                    if (orbs[i] != null && orbs[i].active)
                    {
                        orbs[i].Draw();
                    }
                }
            }
        }
    }
}