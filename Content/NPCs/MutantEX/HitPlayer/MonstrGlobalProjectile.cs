using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Content.Bosses.MutantBoss;

namespace ssm.Content.NPCs.MutantEX.HitPlayer
{
    internal class MonstrGlobalProjectile : GlobalProjectile
    {
        private void ApplyHealthReduction(Player player, float val)
        {
            var modPlayer = player.GetModPlayer<MonstrHealthPlayer>();

            if (modPlayer.OriginalMaxLife == 0)
            {
                modPlayer.OriginalMaxLife = player.statLifeMax2;
            }

            int damage = (int)(modPlayer.OriginalMaxLife * val);
            if (damage < 1) damage = 1;

            modPlayer.HealthReduction += damage;

            player.statLifeMax2 = modPlayer.OriginalMaxLife - modPlayer.HealthReduction;

            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }

            if (player.statLifeMax2 <= 0)
            {
                player.dead = true;
                player.ghost = true;
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit18, player.Center);

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                modPlayer.SyncData();

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, player.whoAmI);
                }
            }
        }

        public override void AI(Projectile projectile)
        {
            if (FargoSoulsUtil.BossIsAlive(ref CSENpcs.mutantEX, ModContent.NPCType<MutantEX>()))
            {
                foreach (Player player in Main.player)
                {
                    if (!player.active || player.dead) continue;

                    if (projectile.Hitbox.Intersects(player.Hitbox) && projectile.hostile && !(player.GetModPlayer<MonstrHealthPlayer>().iFrames > 0))
                    {
                        ApplyHealthReduction(player, WorldSavingSystem.MasochistModeReal ? 0.15f : 0.1f);
                        Main.NewText("Hit!");
                        player.GetModPlayer<MonstrHealthPlayer>().iFrames += 20;
                        projectile.Kill();
                        return;
                    }
                }
            }

            //if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
            //{
            //    foreach (Player player in Main.player)
            //    {
            //        if (!player.active || player.dead) continue;

            //        if (projectile.Hitbox.Intersects(player.Hitbox) && projectile.hostile && !(player.GetModPlayer<MonstrHealthPlayer>().iFrames > 0))
            //        {
            //            ApplyHealthReduction(player, WorldSavingSystem.MasochistModeReal ? 0.1f : 0.05f);
            //            Main.NewText("Hit!");
            //            player.GetModPlayer<MonstrHealthPlayer>().iFrames += 20;
            //            projectile.Kill();
            //            return;
            //        }
            //    }
            //}
            //if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
            //{
            //    foreach (Player player in Main.player)
            //    {
            //        if (!player.active || player.dead) continue;

            //        if (projectile.Hitbox.Intersects(player.Hitbox) && projectile.hostile && !(player.GetModPlayer<MonstrHealthPlayer>().iFrames > 0))
            //        {
            //            player.Heal(-(player.statLifeMax2/10));
            //            player.GetModPlayer<MonstrHealthPlayer>().iFrames += 20;
            //        }
            //    }
            //}
            base.AI(projectile);
        }
    }
}
