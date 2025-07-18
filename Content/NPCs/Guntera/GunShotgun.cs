using Microsoft.Xna.Framework;
using Terraria;

namespace ssm.Content.NPCs.Guntera
{
    public class GunShotgun : GunCelebration
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 46;
            NPC.height = 26;
        }

        public override void Offset(NPC guntera)
        {
            NPC.Center = guntera.Center + new Vector2(-60, -10).RotatedBy(guntera.rotation);
        }
    }
}