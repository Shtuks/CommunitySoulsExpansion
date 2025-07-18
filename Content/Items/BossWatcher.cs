using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ssm.Content.Items
{
    internal class BossWatcher : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player) => true;

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                var modPlayer = player.GetModPlayer<BossSpectatorPlayer>();
                modPlayer.ToggleBossLock();
                return true;
            }
            return base.UseItem(player);
        }
    }

    public class BossSpectatorPlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        private NPC lockedBoss = null;
        private bool isLocked = false;
        public void ToggleBossLock()
        {
            if (!isLocked)
            {
                lockedBoss = FindNearestBoss();
                if (lockedBoss != null)
                {
                    isLocked = true;
                    Main.NewText("Locked onto " + lockedBoss.FullName, 255, 100, 100);
                }
                else
                {
                    Main.NewText("No boss found!", 255, 50, 50);
                }
            }
            else
            {
                ReleaseLock();
                Main.NewText("Boss lock released", 100, 255, 100);
            }
        }

        private NPC FindNearestBoss()
        {
            NPC nearest = null;
            float minDistance = float.MaxValue;

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss && npc != null)
                {
                    float distance = Player.Distance(npc.Center);
                    if (distance < minDistance && distance < 3000f)
                    {
                        minDistance = distance;
                        nearest = npc;
                    }
                }
            }
            return nearest;
        }

        private void ReleaseLock()
        {
            lockedBoss = null;
            isLocked = false;
        }

        public override void PreUpdate()
        {
            if (isLocked)
            {
                if (lockedBoss == null || !lockedBoss.active || lockedBoss.life <= 0)
                {
                    ReleaseLock();
                    Main.NewText("Boss target lost", 255, 150, 50);
                    return;
                }

                Player.Center = lockedBoss.Center;
                Player.velocity = lockedBoss.velocity;
                Player.gfxOffY = lockedBoss.gfxOffY;
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (isLocked)
            {
                modifiers.FinalDamage *= 0;
            }
        }
    }
}
