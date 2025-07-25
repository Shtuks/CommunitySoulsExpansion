using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Guntera
{
    public class GunUzi : GunCelebration
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 48;
            NPC.height = 36;
        }

        public override void Offset(NPC guntera)
        {
            NPC.Center = guntera.Center + new Vector2(36, -42).RotatedBy(guntera.rotation);
        }
    }
}