using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Guntera
{
    public class GunSniper : GunCelebration
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 62;
            NPC.height = 24;
        }

        public override void Offset(NPC guntera)
        {
            NPC.Center = guntera.Center + new Vector2(54 * NPC.spriteDirection, -22).RotatedBy(guntera.rotation);
        }
    }
}