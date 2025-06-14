using Microsoft.Xna.Framework.Graphics;
using SacredTools.Common.GlobalItems;
using SacredTools.Content.Projectiles.Misc;
using SacredTools.Projectiles;
using SacredTools.Utilities;
using SacredTools;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.Systems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Projectiles;

namespace ssm.Content.NPCs
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    internal class MutantAuraOfSupression : ModNPC
    {
        Player player => Main.player[NPC.target];
        public override bool CheckActive()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            });
        }
        public override void SetDefaults()
        {
            //NPC.boss = true;
            NPC.scale = 1.3f;
            NPC.width = 182;
            NPC.height = 210;
            NPC.damage = 0;
            if (!WorldSavingSystem.MasochistModeReal)
            {
                NPC.lifeMax = ModCompatibility.Calamity.Loaded ? 20000000 : 15000000;
            }
            else
            {
                NPC.lifeMax = ModCompatibility.Calamity.Loaded ? 30000000 : 20000000;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCHit1;
            NPC.value = Item.buyPrice();
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.GetGlobalProjectile<ModGlobalProjectile>().relicProj || SoASets.Projectile.RelicProjectileType[projectile.type] != 0 || projectile.type == ModContent.ProjectileType<StyxGazerArmor>() || projectile.type == ModContent.ProjectileType<StyxArmorScythe>() || projectile.type == ModContent.ProjectileType<StyxArmorScythe2>())
            {
                return null;
            }
            return false;
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return item.ModItem is IRelicItem;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)NPC.ai[0]];
            NPC.frame.Y = host.frame.Y;
            NPC.position.X = host.Center.X - (float)(NPC.width / 2);
            NPC.position.Y = host.Center.Y - (float)(NPC.height / 2);
            NPC.gfxOffY = host.gfxOffY;
            NPC.direction = NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;
            NPC.spriteDirection = host.spriteDirection;
            NPC.timeLeft = host.timeLeft;
            NPC.target = host.target;
            if (host.life <= 0 || !host.active)
            {
                NPC.active = false;
                NPC.life = 0;
                NPC.checkDead();
                NPC.HitEffect();
            }
        }
        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveVisualEffectProjectile>(), 0, 0f, Main.myPlayer);
            }
            NPC host = Main.npc[(int)NPC.ai[0]];
            host.HideStrikeDamage = true;
            host.SimpleStrikeNPC(host.lifeMax / 2, 1);
            SoundEngine.PlaySound(in NihilusSounds.NihilusShockwave, NPC.Center);
            host.HideStrikeDamage = false;
            return true;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            Utils.SelectRandom<int>(Main.rand, 6, 259, 158);
            if (NPC.life > 0)
            {
                for (int num40 = 0; (double)num40 < (double)hit.Damage / (double)NPC.lifeMax * 20.0; num40++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f);
                    if (Main.rand.NextBool(4))
                    {
                        Dust obj = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 6)];
                        obj.noGravity = true;
                        obj.scale = 1.5f;
                        obj.fadeIn = 1f;
                        obj.velocity *= 3f;
                    }
                }
            }
        }
    }
}
