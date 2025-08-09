using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles
{
    [AutoloadBossHead]
    public class MutantIllusion : ModNPC
    {
        public override string Texture => "ssm/Content/NPCs/MutantEX/MutantEX";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 124;
            NPC.damage = 360;
            NPC.defense = 400;
            NPC.lifeMax = 700000000;
            NPC.dontTakeDamage = true;
            NPC.HitSound = SoundID.NPCHit57;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = (int)((float)NPC.damage * 0.5f);
            NPC.lifeMax = (int)((float)NPC.lifeMax * 0.5f * balance);
        }

        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            return false;
        }

        public override void AI()
        {
            NPC mutant = FargoSoulsUtil.NPCExists(NPC.ai[0], ModContent.NPCType<MutantEX>());
            if (mutant == null || mutant.ai[0] < 18f || mutant.ai[0] > 19f || mutant.life <= 1)
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.SimpleStrikeNPC(int.MaxValue, 0, crit: false, 0f, null, damageVariation: false, 0f, noPlayerInteraction: true);
                NPC.active = false;
                for (int i = 0; i < 40; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 5);
                    Main.dust[d].velocity *= 2.5f;
                    Main.dust[d].scale += 0.5f;
                }
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 9f;
                }
                return;
            }
            NPC.target = mutant.target;
            NPC.damage = mutant.damage;
            NPC.defDamage = mutant.damage;
            NPC.frame.Y = mutant.frame.Y;
            if (NPC.HasValidTarget)
            {
                Vector2 target = Main.player[mutant.target].Center;
                Vector2 distance = target - mutant.Center;
                NPC.Center = target;
                NPC.position.X += distance.X * NPC.ai[1];
                NPC.position.Y += distance.Y * NPC.ai[2];
                NPC.direction = (NPC.spriteDirection = ((NPC.position.X < Main.player[NPC.target].position.X) ? 1 : (-1)));
            }
            else
            {
                NPC.Center = mutant.Center;
            }
            if ((NPC.ai[3] -= 1f) == 0f)
            {
                int ai0 = ((!(NPC.ai[1] < 0f)) ? ((NPC.ai[2] < 0f) ? 1 : 2) : 0);
                if (FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(mutant.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -5f, ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(mutant.damage, 1.3333334f), 0f, Main.myPlayer, ai0, NPC.whoAmI);
                }
            }
            if (Main.getGoodWorld && (NPC.localAI[0] += 1f) > 6f)
            {
                NPC.localAI[0] = 0f;
                NPC.AI();
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
        }
    }
}