using Fargowiltas.NPCs;
using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Assets.Sounds;
using FargowiltasSouls.Content.BossBars;
using FargowiltasSouls.Content.Bosses.Champions.Life;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using Luminance.Common.Utilities;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using ssm.Content.Buffs;
using ssm.Content.Items.Consumables;
using ssm.Content.NPCs.RealMutantEX.Projectiles;
using ssm.Content.NPCs.RealMutantEX.Projectiles.Fargo;
using ssm.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX
{
    [AutoloadBossHead]
    public class RealMutantEX : ModNPC
    {
        public SlotId? TelegraphSound = null;
        Player player => Main.player[NPC.target];
        public bool playerInvulTriggered;

        public int ritualProj, spriteProj, ringProj;
        private bool droppedSummon = false;
        public ref float AttackChoice => ref NPC.ai[0];
        public Queue<float> attackHistory = new();
        public int attackCount;
        public float endTimeVariance;

        public int hyper;
        public const int HyperMax = 5;

        public Vector2 AuraCenter = Vector2.Zero;
        public bool ShouldDrawAura;
        public float AuraScale = 1f;

        string TownNPCName;

        private float HistoryAttack1;
        private int ShouldChampion;
        private bool makedSword;
        private int ShouldDoSword;
        public float[] NewAI = new float[4];
        public float[] localAI2 = new float[4];
        private Vector2 swordTarget = Vector2.Zero;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            NPCID.Sets.MustAlwaysDraw[Type] = true;
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement($"Mods.FargowiltasSouls.Bestiary.{Name}")
            ]);
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 120;
            NPC.damage = 4000;
            NPC.defense = 200;
            NPC.value = Item.buyPrice(15);
            NPC.lifeMax = 7777777;
            NPC.HitSound = SoundID.NPCHit57;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 50f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.timeLeft = NPC.activeTime * 30;
            NPC.BossBar = ModContent.GetInstance<MutantBossBar>();

            if (WorldSavingSystem.AngryMutant)
            {
                NPC.damage *= 17;
                NPC.defense *= 10;
            }

            if (ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod))
            {
                Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/rePrologue");
            }

            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = (int)Math.Round(NPC.damage * 0.5);
            NPC.lifeMax = (int)Math.Round(NPC.lifeMax * 0.5 * balance);
        }
        
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            //modifiers.FinalDamage *= 0.65f;
        }
        
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            //modifiers.FinalDamage *= 0.65f;
        }

        private void ClearNewAI()
        {
            for (int i = 0; i < 4; i++)
            {
                NewAI[i] = 0f;
            }
        }
        public override void UpdateLifeRegen(ref int damage)
        {
            //damage /= 3;
        }
        
        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            CooldownSlot = 1;
            return NPC.Distance(FargoSoulsUtil.ClosestPointInHitbox(target, NPC.Center)) < Player.defaultHeight && AttackChoice > -1;
        }

        public override bool CanHitNPC(NPC target)
        {
            if (target.type == ModContent.NPCType<Deviantt>() || target.type == ModContent.NPCType<Abominationn>() || target.type == ModContent.NPCType<Mutant>())
                return false;
            return base.CanHitNPC(target);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.localAI[0]);
            writer.Write(NPC.localAI[1]);
            writer.Write(NPC.localAI[2]);
            writer.Write(endTimeVariance);
        }

        private void GoNextAI0()
        {
            base.NPC.ai[0] += 1f;
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadSingle();
            NPC.localAI[1] = reader.ReadSingle();
            NPC.localAI[2] = reader.ReadSingle();
            endTimeVariance = reader.ReadSingle();
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (ModContent.TryFind("Fargowiltas", "Mutant", out ModNPC modNPC))
            {
                int n = NPC.FindFirstNPC(modNPC.Type);
                if (n != -1 && n != Main.maxNPCs)
                {
                    NPC.Bottom = Main.npc[n].Bottom;
                    TownNPCName = Main.npc[n].GivenName;

                    Main.npc[n].life = 0;
                    Main.npc[n].active = false;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                }
            }
            AuraCenter = NPC.Center;
        }

        public override bool PreAI()
        {
            if (!Main.dedServ)
            {
                if (!Main.LocalPlayer.ItemTimeIsZero && (Main.LocalPlayer.HeldItem.type == ItemID.RodofDiscord || Main.LocalPlayer.HeldItem.type == ItemID.RodOfHarmony))
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 600);
            }
            return base.PreAI();
        }

        public override void AI()
        {
            //CSEUtils.DisplayLocalizedText("ai[0] = " + NPC.ai[0].ToString() + "  ai[1] = " + NPC.ai[1].ToString() + "  ai[2] = " + NPC.ai[2].ToString() + "  ai[3] = " + NPC.ai[3].ToString() + "  localai[0] = " + NPC.localAI[0].ToString() + "  localai[1] = " + NPC.localAI[1].ToString() + "  localai[2] = " + NPC.localAI[2].ToString() + "  localai[3] = " + NPC.localAI[3].ToString());

            CSENpcs.RealMutantEX = NPC.whoAmI;

            NPC.dontTakeDamage = AttackChoice < 0;

            ShouldDrawAura = false;

            ManageAurasAndPreSpawn();
            ManageNeededProjectiles();

            NPC.direction = NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;

            bool drainLifeInP3 = true;

            if (TelegraphSound != null)
            {
                if (SoundEngine.TryGetActiveSound(TelegraphSound.Value, out ActiveSound s))
                {
                    s.Position = NPC.Center;
                }
            }

            switch ((int)AttackChoice)
            {
                #region phase 1
                case 888:
                    {
                        if (NPC.localAI[0] == 0f)
                        {
                            Projectile[] projectile = Main.projectile;
                            foreach (Projectile p in projectile)
                            {
                                if (p.type == ModContent.ProjectileType<MutantSphereRing>() && p.active)
                                {
                                    p.Kill();
                                }
                            }
                            StrongAttackTeleport(player.Center + (NPC.Center - player.Center).SafeNormalize(Vector2.Zero).RotatedBy(Main.rand.NextBool(2) ? 0.7854f : (-0.7854f)) * 600f);
                            NPC.localAI[0] = 1f;
                        }
                        Vector2 targetPos = Vector2.Zero;
                        if ((NewAI[1] += 1f) < 150f)
                        {
                            NPC.velocity = Vector2.Zero;
                            if (NewAI[2] == 0f)
                            {
                                double angle = ((NPC.position.X < player.position.X) ? (-Math.PI / 4.0) : (Math.PI / 4.0));
                                NewAI[2] = (float)angle * -4f / 30f;
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + -Vector2.UnitY.RotatedBy(angle) * 90f, Vector2.Zero, ModContent.ProjectileType<DeviSparklingLove>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 2f), 0f, Main.myPlayer, (float)NPC.whoAmI, 90f);
                                }
                                Vector2 offset = -Vector2.UnitY.RotatedBy(angle) * 80f;
                                if (Main.netMode != 1)
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        SpawnAxeHitbox(NPC.Center + offset * i);
                                    }
                                    for (int i = 1; i < 3; i++)
                                    {
                                        SpawnAxeHitbox(NPC.Center + offset * 5f + offset.RotatedBy((0.0 - angle) * 2.0) * i);
                                        SpawnAxeHitbox(NPC.Center + offset * 6f + offset.RotatedBy((0.0 - angle) * 2.0) * i);
                                    }
                                }
                                if (WorldSavingSystem.MasochistModeReal && Main.netMode != 1)
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Vector2 target = new Vector2(80f, 80f).RotatedBy((float)Math.PI / 2f * (float)i);
                                        Vector2 speed = 2f * target / 90f;
                                        float acceleration = (0f - speed.Length()) / 90f;
                                        int damage = ((NPC.localAI[3] > 1f) ? FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f) : FargoSoulsUtil.ScaledProjectileDamage(NPC.damage));
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<DeviEnergyHeart>(), damage, 0f, Main.myPlayer, 0f, acceleration);
                                    }
                                }
                            }
                            NPC.direction = (NPC.spriteDirection = Math.Sign(NewAI[2]));
                        }
                        else if (NewAI[1] == 150f)
                        {
                            targetPos = player.Center;
                            targetPos.X -= 360 * Math.Sign(NewAI[2]);
                            NPC.velocity = (targetPos - NPC.Center) / 30f;
                            NPC.netUpdate = true;
                            NPC.direction = (NPC.spriteDirection = Math.Sign(NewAI[2]));
                            if (!WorldSavingSystem.MasochistModeReal && Math.Sign(targetPos.X - NPC.Center.X) != Math.Sign(NewAI[2]))
                            {
                                NPC.velocity.X *= 0.5f;
                            }
                        }
                        else if (NewAI[1] < 180f)
                        {
                            NewAI[3] += NewAI[2];
                            NPC.direction = (NPC.spriteDirection = Math.Sign(NewAI[2]));
                        }
                        else
                        {
                            targetPos = player.Center + player.DirectionTo(NPC.Center) * 400f;
                            if (NPC.Distance(targetPos) > 50f)
                            {
                                Movement(targetPos, 0.2f);
                            }
                            if (NewAI[1] > 300f)
                            {
                                ClearNewAI();
                                ChooseNextAttack(44, 45, 26, 29);
                            }
                        }
                        break;
                    }
                case 132:
                    if (!(NewAI[1] < 90f) || AliveCheck(player))
                    {
                        if (NewAI[2] == 0f && NewAI[3] == 0f)
                        {
                            NewAI[2] = NPC.Center.X + (float)((player.Center.X < NPC.Center.X) ? (-1000) : 1000);
                        }
                        if (NPC.localAI[2] == 0f)
                        {
                            NPC.localAI[2] = ((!(NewAI[2] > NPC.Center.X)) ? 1 : (-1));
                        }
                        if (NewAI[1] > 90f)
                        {
                            FancyFireballs((int)NewAI[1] - 90);
                        }
                        else
                        {
                            bool k = Main.rand.NextBool(2);
                            NewAI[3] = player.Center.Y + (float)(k ? 1000 : (-1000));
                            NewAI[0] = ((!k) ? 1 : (-1));
                        }
                        Vector2 targetPos4 = new Vector2(NewAI[2], NewAI[3]);
                        Movement(targetPos4, 1.4f);
                        if ((NewAI[1] += 1f) > 150f)
                        {
                            SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                            NPC.netUpdate = true;
                            NPC.ai[0] += 1f;
                            NewAI[1] = 0f;
                            NewAI[2] = NPC.localAI[2];
                            NewAI[3] = 0f;
                            NPC.localAI[2] = 0f;
                        }
                    }
                    break;
                case 133:
                    {
                        NPC.velocity.X = NewAI[2] * 12f;
                        NPC.velocity.Y = NewAI[0] * 12f;
                        Vector2 v = NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296);
                        Vector2 d = player.Center - NPC.Center;
                        float num2 = v.X * d.X + v.Y * d.Y;
                        float Dis = NPC.Distance(player.Center);
                        if (num2 > 300f)
                        {
                            if ((player.Center - NPC.Center - NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 50f).Length() < Dis)
                            {
                                NPC.velocity += NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 3f;
                            }
                            else
                            {
                                NPC.velocity += NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-1.5707000494003296) * 3f;
                            }
                        }
                        if (num2 <= 300f)
                        {
                            if ((player.Center - NPC.Center - NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 50f).Length() < Dis)
                            {
                                NPC.velocity -= NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 3f;
                            }
                            else
                            {
                                NPC.velocity -= NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-1.5707000494003296) * 3f;
                            }
                        }
                        NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.velocity.X));
                        if ((NewAI[3] += 1f) > 5f)
                        {
                            NewAI[3] = 0f;
                            SoundEngine.PlaySound(SoundID.Item12, (Vector2?)NPC.Center);
                            float timeLeft = 2400f / Math.Abs(NPC.velocity.X) * 2f - NewAI[1] + 120f;
                            if (NewAI[1] <= 15f)
                            {
                                timeLeft = 0f;
                            }
                            else
                            {
                                if (NPC.localAI[2] != 0f)
                                {
                                    timeLeft = 0f;
                                }
                                if ((NPC.localAI[2] += 1f) > 2f)
                                {
                                    NPC.localAI[2] = 0f;
                                }
                            }
                            if (Main.netMode != 1)
                            {
                                float R = new Vector2(NewAI[2], NewAI[0]).ToRotation();
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy((double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5) + (double)R), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, timeLeft, 0f);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy((double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5) + (double)R), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, timeLeft, 0f);
                            }
                        }
                        if ((NewAI[1] += 1f) > 218.18182f)
                        {
                            NPC.netUpdate = true;
                            NPC.velocity.X = NewAI[2] * 18f;
                            NPC.ai[0] += 1f;
                            NewAI[1] = 0f;
                            NewAI[3] = 0f;
                        }
                        break;
                    }
                case 134:
                    if (!(NewAI[1] < 150f) || AliveCheck(player))
                    {
                        NPC.velocity.Y = 0f;
                        NPC.velocity *= 0.947f;
                        NewAI[3] += NPC.velocity.Length();
                        if (NewAI[1] > 150f)
                        {
                            FancyFireballs((int)NewAI[1] - 150);
                        }
                        if ((NewAI[1] += 1f) > 210f)
                        {
                            SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                            NPC.netUpdate = true;
                            NPC.ai[0] += 1f;
                            NewAI[1] = 0f;
                            NewAI[3] = 0f;
                            NewAI[2] = 0f - NewAI[2];
                            NewAI[0] = 0f - NewAI[0];
                        }
                    }
                    break;
                case 135:
                    {
                        NPC.velocity.X = NewAI[2] * 12f;
                        NPC.velocity.Y = NewAI[0] * 12f;
                        Vector2 v = NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296);
                        Vector2 d = player.Center - NPC.Center;
                        float num3 = v.X * d.X + v.Y * d.Y;
                        float Dis = NPC.Distance(player.Center);
                        if (num3 > 300f)
                        {
                            if ((player.Center - NPC.Center - NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 2f).Length() < Dis)
                            {
                                NPC.velocity += NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 3f;
                            }
                            else
                            {
                                NPC.velocity += NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-1.5707000494003296) * 3f;
                            }
                        }
                        else if ((player.Center - NPC.Center - NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 2f).Length() < Dis)
                        {
                            NPC.velocity -= NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707000494003296) * 3f;
                        }
                        else
                        {
                            NPC.velocity -= NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-1.5707000494003296) * 3f;
                        }
                        NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.velocity.X));
                        if ((NewAI[3] += 1f) > 5f)
                        {
                            NewAI[3] = 0f;
                            SoundEngine.PlaySound(SoundID.Item12, (Vector2?)NPC.Center);
                            float timeLeft = 2400f / Math.Abs(NPC.velocity.X) * 2f - NewAI[1] + 120f;
                            if (NewAI[1] <= 15f)
                            {
                                timeLeft = 0f;
                            }
                            else
                            {
                                if (NPC.localAI[2] != 0f)
                                {
                                    timeLeft = 0f;
                                }
                                if ((NPC.localAI[2] += 1f) > 2f)
                                {
                                    NPC.localAI[2] = 0f;
                                }
                            }
                            if (Main.netMode != 1)
                            {
                                float R = new Vector2(NewAI[2], NewAI[0]).ToRotation();
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy((double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5) + (double)R), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, timeLeft, 0f);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy((double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5) + (double)R), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, timeLeft, 0f);
                            }
                        }
                        if (!((NewAI[1] += 1f) > 218.18182f))
                        {
                            break;
                        }
                        NPC.netUpdate = true;
                        NPC.velocity.X = NewAI[2] * -18f;
                        NewAI[0] = 0f;
                        ChooseNextAttack(11, 13, 19, 33, 24, 41, 44, 45);
                        Projectile[] projectile = Main.projectile;
                        foreach (Projectile proj in projectile)
                        {
                            if (proj.type == ModContent.ProjectileType<AbomDeathrayMark>())
                            {
                                ((AbomDeathrayMark)proj.ModProjectile).DontS = true;
                            }
                            if (proj.type == ModContent.ProjectileType<AbomDeathray>())
                            {
                                ((AbomDeathray)proj.ModProjectile).DontSpawn = true;
                            }
                            if (proj.type == ModContent.ProjectileType<AbomScytheSplit>())
                            {
                                proj.Kill();
                            }
                        }
                        ShouldDoSword = 0;
                        NewAI[1] = 0f;
                        NewAI[2] = 0f;
                        NewAI[3] = 0f;
                        break;
                    }
                case 1919:
                    if (NewAI[1] == 1f)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                    }
                    if (NewAI[2] <= 114f)
                    {
                        Vector2 targetPos = player.Center;
                        targetPos.X += 600 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                        NPC.position += player.velocity / 3f;
                        Movement(targetPos, 1.2f);
                    }
                    if (NewAI[2] == 114f)
                    {
                        Projectile[] projectile = Main.projectile;
                        foreach (Projectile p in projectile)
                        {
                            if (p.type == ModContent.ProjectileType<MutantSphereRing>() && p.active)
                            {
                                p.Kill();
                            }
                        }
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, -20f);
                        }
                    }
                    if ((NewAI[2] += 1f) <= 315f)
                    {
                        Vector2 targetPos = player.Center;
                        targetPos.X += 600 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                        NPC.position += player.velocity / 3f;
                        Movement(targetPos, 1.2f);
                        if ((NPC.localAI[0] -= 1f) < 0f && NewAI[2] > 114f)
                        {
                            NPC.localAI[0] = 90f;
                            if (Main.netMode != 1)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    for (int i = -11; i <= 11; i++)
                                    {
                                        Vector2 target = player.Center;
                                        target.X += 180f * (float)i;
                                        target.Y += (400f + 27.272728f * (float)Math.Abs(i)) * (float)j;
                                        Vector2 speed = (target - NPC.Center) / 20f;
                                        int individualTiming = 60 + Math.Abs(i * 2);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed / 2f, ModContent.ProjectileType<CosmosSphere>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 20f, (float)individualTiming);
                                    }
                                }
                            }
                        }
                        NPC.rotation = NPC.DirectionTo(player.Center).ToRotation();
                        if (NPC.direction < 0)
                        {
                            NPC.rotation += (float)Math.PI;
                        }
                        NewAI[3] = ((NPC.Center.X < player.Center.X) ? 1 : (-1));
                        if (NewAI[2] == 315f)
                        {
                            NPC.velocity = 42f * NPC.DirectionTo(player.Center);
                            NPC.netUpdate = true;
                            if (Main.netMode != 1)
                            {
                                int modifier = Math.Sign(NPC.Center.Y - player.Center.Y);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 3000f * NPC.DirectionFrom(player.Center) * modifier, NPC.DirectionTo(player.Center) * modifier, ModContent.ProjectileType<CosmosDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                    else
                    {
                        NPC.direction = (NPC.spriteDirection = Math.Sign(NewAI[3]));
                    }
                    if ((NewAI[1] += 1f) > 400f || (NewAI[2] > 315f && ((NewAI[3] > 0f) ? (NPC.Center.X > player.Center.X + 800f) : (NPC.Center.X < player.Center.X - 800f))))
                    {
                        NPC.velocity.X = 0f;
                        NPC.TargetClosest();
                        GoNextAI0();
                        NewAI[1] = 0f;
                        NewAI[2] = 0f;
                        NewAI[3] = 0f;
                        NPC.localAI[0] = 0f;
                        NPC.netUpdate = true;
                    }
                    break;
                case 1920:
                    if ((NewAI[1] += 1f) < 110f)
                    {
                        Vector2 targetPos = player.Center;
                        targetPos.X += 300 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                        if (NPC.Distance(targetPos) > 50f)
                        {
                            Movement(targetPos, 0.8f);
                        }
                        if (NewAI[1] == 1f)
                        {
                            SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                        }
                        if ((NewAI[2] += 1f) <= 6f)
                        {
                            NPC.rotation = NPC.DirectionTo(player.Center).ToRotation();
                            if (NPC.direction < 0)
                            {
                                NPC.rotation += (float)Math.PI;
                            }
                            NewAI[3] = ((NPC.Center.X < player.Center.X) ? 1 : (-1));
                            if (NewAI[2] != 6f)
                            {
                                break;
                            }
                            NPC.netUpdate = true;
                            if (NewAI[1] > 50f)
                            {
                                if (Main.netMode != 1)
                                {
                                    Vector2 offset = Vector2.UnitX;
                                    if (NPC.direction < 0)
                                    {
                                        offset.X *= -1f;
                                    }
                                    offset = offset.RotatedBy(NPC.DirectionTo(player.Center).ToRotation());
                                    int modifier = Math.Sign(NPC.Center.Y - player.Center.Y);
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + offset + 3000f * NPC.DirectionFrom(player.Center) * modifier, NPC.DirectionTo(player.Center) * modifier, ModContent.ProjectileType<CosmosDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                                }
                            }
                            else
                            {
                                NewAI[2] = 0f;
                            }
                        }
                        else
                        {
                            NPC.direction = (NPC.spriteDirection = Math.Sign(NewAI[3]));
                            if (NewAI[2] > 12f)
                            {
                                NewAI[2] = 0f;
                                NewAI[3] = 0f;
                                NPC.netUpdate = true;
                            }
                        }
                        break;
                    }
                    if (NewAI[1] <= 155f)
                    {
                        Vector2 targetPos = player.Center;
                        targetPos.X += 350 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                        targetPos.Y += 700f;
                        NPC.position += player.velocity / 3f;
                        Movement(targetPos, 2.4f);
                        NPC.rotation = NPC.DirectionTo(player.Center).ToRotation();
                        if (NPC.direction < 0)
                        {
                            NPC.rotation += (float)Math.PI;
                        }
                        if (NewAI[1] == 155f)
                        {
                            NPC.velocity = 42f * NPC.DirectionTo(player.Center);
                            NPC.netUpdate = true;
                            NewAI[3] = Math.Abs(player.Center.Y - NPC.Center.Y) / 42f;
                            NewAI[3] *= 2f;
                            NPC.localAI[0] = player.Center.X;
                            NPC.localAI[1] = player.Center.Y;
                            NPC.localAI[0] += ((NPC.Center.X < player.Center.X) ? (-50) : 50);
                            if (Main.netMode != 1)
                            {
                                int modifier = Math.Sign(NPC.Center.Y - player.Center.Y);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 3000f * NPC.DirectionFrom(player.Center) * modifier, NPC.DirectionTo(player.Center) * modifier, ModContent.ProjectileType<CosmosDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                        break;
                    }
                    NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.velocity.X));
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.direction < 0)
                    {
                        NPC.rotation += (float)Math.PI;
                    }
                    if (Math.Abs(NPC.Center.Y - NPC.localAI[1]) < 300f)
                    {
                        Vector2 vector = NPC.Center - NPC.velocity / 2f;
                        Vector2 target = new Vector2(NPC.localAI[0], NPC.localAI[1]);
                        Vector2 vel = Vector2.Normalize(vector - target);
                        if (Main.netMode != 1)
                        {
                            int modifier = ((Math.Sign(player.Center.X - target.X) == NPC.direction) ? 1 : (-1));
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, (float)modifier * 0.5f * vel, ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, (float)modifier * 0.5f * NPC.DirectionFrom(target), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                    else if ((NewAI[2] += 1f) > 1f)
                    {
                        NewAI[2] = 0f;
                        if (Main.netMode != 1)
                        {
                            Vector2 target = new Vector2(NPC.localAI[0], NPC.localAI[1]);
                            Math.Sign(player.Center.X - target.X);
                            _ = NPC.direction;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 0.5f * NPC.DirectionFrom(target), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (NewAI[1] > 155f + NewAI[3])
                    {
                        NPC.velocity.Y = 0f;
                        NPC.TargetClosest();
                        NPC.ai[0] = 1922f;
                        NewAI[1] = ((!WorldSavingSystem.EternityMode || NPC.localAI[2] == 0f) ? (-120) : 0);
                        NewAI[2] = 0f;
                        NewAI[3] = 0f;
                        NPC.localAI[0] = 0f;
                        NPC.localAI[1] = 0f;
                        NPC.netUpdate = true;
                    }
                    break;
                case 1921:
                    GoNextAI0();
                    break;
                case 1922:
                    {
                        ManagedScreenFilter filter = ShaderManager.GetFilter("FargowiltasSouls.Invert");
                        //Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 500f;
                        if (NewAI[1] < 130f || (NPC.Distance(player.Center) > 200f && NPC.Distance(player.Center) < 600f))
                        {
                            NPC.velocity *= 0.97f;
                        }
                        //else if (NPC.Distance(targetPos) > 50f)
                        //{
                        //    Movement(targetPos, 0.8f);
                        //    NPC.position += player.velocity / 4f;
                        //}
                        if (NewAI[1] >= 10)
                        {
                            if (filter != null)
                            {
                                if (Main.netMode != NetmodeID.Server && !ShaderManager.GetFilter("FargowiltasSouls.Invert").IsActive)
                                {
                                    filter.SetFocusPosition(NPC.Center);
                                    filter.Activate();
                                }
                            }
                        }
                        if (NewAI[1] == 10f)
                        {
                            NPC.localAI[0] = Main.rand.NextFloat((float)Math.PI * 2f);
                            if (!Main.dedServ)
                            {
                                SoundEngine.PlaySound(FargosSoundRegistry.ZaWarudo, (Vector2?)player.Center);
                            }
                        }
                        else if (NewAI[1] < 210f)
                        {
                            int duration = 60 + Math.Max(2, 210 - (int)NewAI[1]);
                            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead)
                            {
                                Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), duration);
                            }
                            for (int i = 0; i < 200; i++)
                            {
                                if (Main.npc[i].active)
                                {
                                    Main.npc[i].AddBuff(ModContent.BuffType<TimeFrozenBuff>(), duration, quiet: true);
                                }
                            }
                            for (int i = 0; i < 1000; i++)
                            {
                                if (Main.projectile[i].active && !Main.projectile[i].GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune)
                                {
                                    Main.projectile[i].GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFrozen = duration;
                                }
                            }
                            if (NewAI[1] < 130f && (NewAI[2] += 1f) > 12f)
                            {
                                NewAI[2] = 0f;
                                bool altAttack = WorldSavingSystem.EternityMode && NPC.localAI[2] != 0f;
                                int baseDistance = 300;
                                float offset = (altAttack ? 250f : 150f);
                                float speed = (altAttack ? 4f : 2.5f);
                                int damage = FargoSoulsUtil.ScaledProjectileDamage(NPC.damage);
                                if (NewAI[1] < 85f || !altAttack)
                                {
                                    if (altAttack && NewAI[3] % 2f == 0f)
                                    {
                                        float radius = (float)baseDistance + NewAI[3] * offset;
                                        int circumference = (int)((float)Math.PI * 2f * radius);
                                        NPC.localAI[0] = MathHelper.WrapAngle(NPC.localAI[0] + (float)Math.PI + Main.rand.NextFloat((float)Math.PI / 2f));
                                        for (int i = 0; i < circumference; i += 120)
                                        {
                                            float angle = (float)i / radius;
                                            if (!((double)angle > Math.PI * 2.0 - (double)MathHelper.WrapAngle(MathHelper.ToRadians(60f))))
                                            {
                                                float spawnOffset = radius;
                                                Vector2 spawnPos = player.Center + spawnOffset * Vector2.UnitX.RotatedBy(angle + NPC.localAI[0]);
                                                Vector2 vel = speed * player.DirectionFrom(spawnPos);
                                                float ai0 = player.Distance(spawnPos) / speed + 30f;
                                                if (Main.netMode != 1)
                                                {
                                                    Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.ProjectileType<CosmosInvaderTime>(), damage, 0f, Main.myPlayer, ai0, vel.ToRotation());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int max = (altAttack ? 12 : (8 + (int)NewAI[3] * ((NPC.localAI[2] == 0f) ? 2 : 4)));
                                        float rotationOffset = Main.rand.NextFloat((float)Math.PI * 2f);
                                        for (int i = 0; i < max; i++)
                                        {
                                            float ai0 = baseDistance;
                                            float distance = ai0 + NewAI[3] * offset;
                                            Vector2 spawnPos = player.Center + distance * Vector2.UnitX.RotatedBy(Math.PI * 2.0 / (double)max * (double)i + (double)rotationOffset);
                                            Vector2 vel = speed * player.DirectionFrom(spawnPos);
                                            ai0 = distance / speed + 30f;
                                            if (Main.netMode != 1)
                                            {
                                                Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.ProjectileType<CosmosInvaderTime>(), damage, 0f, Main.myPlayer, ai0, vel.ToRotation());
                                            }
                                        }
                                    }
                                }
                                NewAI[3] += 1f;
                            }
                        }
                        if (NewAI[1] == 300f)
                        {
                            filter.Deactivate();
                        }
                        if (!((NewAI[1] += 1f) > 480f))
                        {
                            break;
                        }
                        Projectile[] projectile = Main.projectile;
                        foreach (Projectile proj in projectile)
                        {
                            if (proj.active && proj.type == ModContent.ProjectileType<CosmosInvaderTime>())
                            {
                                proj.Kill();
                            }
                        }
                        NPC.TargetClosest();
                        ClearNewAI();
                        NPC.localAI[0] = 0f;
                        NPC.localAI[1] = 0f;
                        NPC.localAI[2] = 0f;
                        NPC.localAI[3] = 0f;
                        ChooseNextAttack(13, 21, 24, 44, 45);
                        break;
                    }
                case 1919810:
                    if (Phase2Check())
                    {
                        return;
                    }
                    if (NewAI[1] < 110f)
                    {
                        FancyFireballs2((int)NewAI[1]);
                    }
                    if ((NewAI[1] += 1f) == 110f)
                    {
                        Projectile[] projectile = Main.projectile;
                        foreach (Projectile p in projectile)
                        {
                            if (p.type == ModContent.ProjectileType<MutantEyeHoming>() || p.type == ModContent.ProjectileType<MutantSphereRing>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                            {
                                p.active = false;
                            }
                        }
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    Vector2 spawnPos = player.Center + Main.rand.NextFloat(500f, 700f) * Vector2.UnitX.RotatedBy(Main.rand.NextDouble() * 2.0 * Math.PI);
                                    Vector2 vel = NPC.velocity.RotatedBy(Main.rand.NextDouble() * Math.PI * 2.0);
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.ProjectileType<ShadowClone>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.target, (float)(60 + 30 * i));
                                }
                            }
                        }
                    }
                    if (NewAI[1] == 455f)
                    {
                        ClearNewAI();
                        P1NextAttackOrMasoOptions(HistoryAttack1);
                    }
                    Movement(player.Center + (NPC.Center - player.Center).SafeNormalize(Vector2.Zero) * 666f, 1f);
                    break;
                case 1919811:
                    {
                        if (Phase2Check())
                        {
                            return;
                        }
                        if (NPC.localAI[0] < 100f)
                        {
                            FancyFireballs3((int)NPC.localAI[0]);
                        }
                        if (NPC.localAI[0] == 100f)
                        {
                            Projectile[] projectile = Main.projectile;
                            foreach (Projectile p in projectile)
                            {
                                if (p.type == ModContent.ProjectileType<MutantEyeHoming>() || p.type == ModContent.ProjectileType<MutantSphereRing>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                                {
                                    p.active = false;
                                }
                            }
                        }
                        Vector2 vel = player.Center - NPC.Center;
                        NPC.rotation = vel.ToRotation();
                        if (vel.X > 0f)
                        {
                            vel.X -= 550f;
                            NPC.direction = (NPC.spriteDirection = 1);
                        }
                        else
                        {
                            vel.X += 550f;
                            NPC.direction = (NPC.spriteDirection = -1);
                        }
                        vel.Y -= 250f;
                        vel.Normalize();
                        vel *= 16f;
                        if (NPC.velocity.X < vel.X)
                        {
                            NPC.velocity.X += 0.25f;
                            if (NPC.velocity.X < 0f && vel.X > 0f)
                            {
                                NPC.velocity.X += 0.25f;
                            }
                        }
                        else if (NPC.velocity.X > vel.X)
                        {
                            NPC.velocity.X -= 0.25f;
                            if (NPC.velocity.X > 0f && vel.X < 0f)
                            {
                                NPC.velocity.X -= 0.25f;
                            }
                        }
                        if (NPC.velocity.Y < vel.Y)
                        {
                            NPC.velocity.Y += 0.25f;
                            if (NPC.velocity.Y < 0f && vel.Y > 0f)
                            {
                                NPC.velocity.Y += 0.25f;
                            }
                        }
                        else if (NPC.velocity.Y > vel.Y)
                        {
                            NPC.velocity.Y -= 0.25f;
                            if (NPC.velocity.Y > 0f && vel.Y < 0f)
                            {
                                NPC.velocity.Y -= 0.25f;
                            }
                        }
                        if ((NPC.localAI[0] += 1f) > 100f)
                        {
                            NPC.localAI[0] = 47f;
                            if (Main.netMode != 1 && NewAI[1] < 90f)
                            {
                                SoundEngine.PlaySound(SoundID.Item34, (Vector2?)NPC.Center);
                                Vector2 spawn = new Vector2(40f, 50f);
                                if (NPC.direction < 0)
                                {
                                    spawn.X *= -1f;
                                    spawn = spawn.RotatedBy(Math.PI);
                                }
                                spawn = spawn.RotatedBy(NPC.rotation);
                                spawn += NPC.Center;
                                Vector2 projVel = NPC.DirectionTo(player.Center).RotatedBy((Main.rand.NextDouble() - 0.5) * Math.PI / 10.0);
                                projVel.Normalize();
                                projVel *= Main.rand.NextFloat(8f, 12f);
                                int type = 467;
                                if (Main.rand.NextBool())
                                {
                                    type = ModContent.ProjectileType<WillFireball>();
                                    projVel *= 2.5f;
                                }
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, projVel, type, NPC.defDamage / 4, 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                        if ((NPC.localAI[1] -= 1f) < -75f)
                        {
                            NPC.localAI[1] = -50f;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, Math.Max(600f, player.Center.Y - 2000f)), Vector2.UnitY, ModContent.ProjectileType<WillDeathraySmall>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, player.Center.X, (float)NPC.whoAmI);
                            }
                        }
                        if ((NewAI[1] += 1f) == 1f)
                        {
                            SoundEngine.PlaySound(SoundID.ForceRoarPitched, (Vector2?)NPC.Center);
                        }
                        else if (NewAI[1] > 200f)
                        {
                            NewAI[1] = 0f;
                            NPC.localAI[0] = 0f;
                            NPC.netUpdate = true;
                            ClearNewAI();
                            P1NextAttackOrMasoOptions(HistoryAttack1);
                        }
                        break;
                    }
                case 1919812:
                    if (Phase2Check())
                    {
                        return;
                    }
                    if (NewAI[1] == 70f)
                    {
                        Projectile[] projectile = Main.projectile;
                        foreach (Projectile p in projectile)
                        {
                            if (p.type == ModContent.ProjectileType<MutantSphereRing>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                            {
                                p.active = false;
                            }
                        }
                        SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                        NPC.localAI[0] = player.Center.X;
                        NPC.localAI[1] = player.Center.Y;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, (player.Center - NPC.Center) / 120f, ModContent.ProjectileType<TimberSquirrel>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NewAI[3] + 10f, (float)NPC.whoAmI);
                        }
                    }
                    if (NewAI[1] < 100f)
                    {
                        FancyFireballs5((int)NewAI[1]);
                    }
                    if (NewAI[1] < 100f)
                    {
                        Vector2 targetPos = player.Center;
                        targetPos.X += ((NPC.Center.X < player.Center.X) ? (-200) : 200);
                        targetPos.Y -= 200f;
                        if (NPC.Distance(targetPos) > 50f)
                        {
                            Movement(targetPos, 0.4f);
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.9f;
                    }
                    if ((NewAI[1] += 1f) < 160f)
                    {
                        if (NewAI[3] != 0f)
                        {
                            break;
                        }
                        if (NewAI[1] == 90f)
                        {
                            NPC.velocity = Vector2.Zero;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY, ModContent.ProjectileType<GlowLine>(), 0, 0f, Main.myPlayer, 19f, 0f);
                            }
                        }
                        if (!(NewAI[1] > 90f) || NewAI[1] % 3f != 0f)
                        {
                            break;
                        }
                        float current = NewAI[1] - 90f;
                        current /= 3f;
                        float offset = 192f * current;
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Vector2 spawnPos = new Vector2(NPC.Center.X + offset * (float)i, player.Center.Y + 1500f);
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, -Vector2.UnitY, ModContent.ProjectileType<GlowLine>(), 0, 0f, Main.myPlayer, 19f, 0f);
                            }
                        }
                    }
                    else if (NewAI[1] < 310f)
                    {
                        if (NewAI[3] != 0f)
                        {
                            break;
                        }
                        if (NewAI[1] % 3f == 0f)
                        {
                            SoundEngine.PlaySound(SoundID.Item157, (Vector2?)NPC.Center);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 spawnPos = new Vector2(NPC.localAI[0], NPC.localAI[1]);
                            spawnPos.X += (Main.rand.NextBool(2) ? Main.rand.Next(-1600, -600) : Main.rand.Next(600, 1600));
                            spawnPos.Y -= Main.rand.NextFloat(600f, 800f);
                            Vector2 speed = Main.rand.NextFloat(7.5f, 12.5f) * Vector2.UnitY;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, speed, ModContent.ProjectileType<TimberLaser>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 0f);
                            }
                        }
                    }
                    else if (!Main.projectile.Any((Projectile p) => p.active && p.type == ModContent.ProjectileType<TimberSquirrel>() && (float)NPC.whoAmI == p.ai[1]))
                    {
                        NPC.TargetClosest();
                        P1NextAttackOrMasoOptions(HistoryAttack1);
                        NewAI[1] = 0f;
                        NewAI[2] = 0f;
                        NewAI[3] = 0f;
                        ClearNewAI();
                        NPC.localAI[0] = (NPC.localAI[1] = 0f);
                        NPC.netUpdate = true;
                    }
                    break;
                case 1919813:
                    if (Phase2Check())
                    {
                        return;
                    }
                    if (NewAI[3] == 100f)
                    {
                        Projectile[] projectile = Main.projectile;
                        foreach (Projectile p in projectile)
                        {
                            if (p.type == ModContent.ProjectileType<MutantEyeHoming>() || p.type == ModContent.ProjectileType<MutantSphereRing>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                            {
                                p.active = false;
                            }
                        }
                    }
                    if (NewAI[3] < 100f)
                    {
                        FancyFireballs4((int)NewAI[3]);
                    }
                    else
                    {
                        NPC.velocity *= 0.9f;
                    }
                    NewAI[3] += 1f;
                    if ((NewAI[2] += 1f) > 100f)
                    {
                        NewAI[2] = 60f;
                        SoundEngine.PlaySound(SoundID.Item92, (Vector2?)NPC.Center);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                float num4 = Main.rand.NextFloat(4f, 8f);
                                Vector2 velocity = num4 * Vector2.UnitX.RotatedBy(Main.rand.NextDouble() * 2.0 * Math.PI);
                                float ai1 = num4 / Main.rand.NextFloat(60f, 120f);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<SpiritSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, ai1);
                            }
                            for (int i = 0; i < 12; i++)
                            {
                                Vector2 vel = NPC.DirectionTo(player.Center).RotatedBy(Math.PI / 6.0 * (double)i);
                                float ai0 = 1.04f;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<SpiritHand>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, 0f);
                            }
                        }
                    }
                    if (NewAI[3] > 470f)
                    {
                        ClearNewAI();
                        P1NextAttackOrMasoOptions(HistoryAttack1);
                    }
                    break;

                case 0: SpearTossDirectP1AndChecks(); break;

                case 1: OkuuSpheresP1(); break;

                case 2: PrepareTrueEyeDiveP1(); break;
                case 3: TrueEyeDive(); break;

                case 4: PrepareSpearDashDirectP1(); break;
                case 5: SpearDashDirectP1(); break;
                case 6: WhileDashingP1(); break;

                case 7: ApproachForNextAttackP1(); break;
                case 8: VoidRaysP1(); break;

                case 9: BoundaryBulletHellAndSwordP1(); break;

                case 114518:
                    NPC.direction = (NPC.spriteDirection = Math.Sign(NewAI[2] - NPC.Center.X));
                    if (NewAI[1] == 0f && Main.netMode != 1)
                    {
                        float horizontalModifier = Math.Sign(NPC.ai[2] - NPC.Center.X);
                        float verticalModifier = Math.Sign(NPC.ai[3] - NPC.Center.Y);
                        float ai0 = horizontalModifier * (float)Math.PI / 60f * verticalModifier;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX * (0f - horizontalModifier), ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, ai0, (float)NPC.whoAmI);
                        if (WorldSavingSystem.MasochistModeReal)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitX * (0f - horizontalModifier), ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, ai0, (float)NPC.whoAmI);
                        }
                    }
                    if ((NewAI[1] += 1f) > 60f)
                    {
                        ChooseNextAttack(11, 13, 19, 33, 24, 41);
                        NPC.velocity.X = 0f;
                        NPC.velocity.Y = 24 * Math.Sign(NewAI[3] - NPC.Center.Y);
                        ClearNewAI();
                    }
                    break;
                case 114514:
                    NPC.velocity *= 0.9f;
                    if (NewAI[1] < 60f)
                    {
                        FancyFireballs((int)NewAI[1]);
                    }
                    if (NewAI[1] == 0f && NewAI[2] != 2f && Main.netMode != 1)
                    {
                        float ai1 = ((NewAI[2] != 1f) ? 1 : (-1));
                        ai1 *= MathHelper.ToRadians(270f) / 120f * -1f * 60f;
                        int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3f, ai1);
                        if (p != 1000)
                        {
                            Main.projectile[p].localAI[1] = NPC.whoAmI;
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(27, -1, -1, null, p);
                            }
                        }
                        int p2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3f, ai1 + 3.1416f);
                        if (p2 != 1000)
                        {
                            Main.projectile[p2].localAI[1] = NPC.whoAmI;
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(27, -1, -1, null, p2);
                            }
                        }
                    }
                    if ((NewAI[1] += 1f) > 90f)
                    {
                        ClearNewAI();
                        NPC.netUpdate = true;
                        NPC.ai[0] += 1f;
                        NewAI[1] = 0f;
                        NPC.velocity = NPC.DirectionTo(player.Center) * 3f;
                    }
                    else if (NewAI[1] == 60f && Main.netMode != 1)
                    {
                        NPC.netUpdate = true;
                        NPC.velocity = Vector2.Zero;
                        SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                        float ai0 = ((NewAI[2] != 1f) ? 1 : (-1));
                        ai0 *= MathHelper.ToRadians(270f) / 120f;
                        Vector2 vel = NPC.DirectionTo(player.Center).RotatedBy((0f - ai0) * 60f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, ai0, (float)NPC.whoAmI);
                        if (WorldSavingSystem.MasochistModeReal)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -vel, ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, ai0, (float)NPC.whoAmI);
                        }
                    }
                    break;
                case 114515:
                    NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.velocity.X));
                    if ((NewAI[1] += 1f) > 120f)
                    {
                        NPC.netUpdate = true;
                        NPC.ai[0] = 114516f;
                        ClearNewAI();
                    }
                    break;
                case 114516:
                    if (AliveCheck(player))
                    {
                        Vector2 targetPos2 = player.Center + player.DirectionTo(NPC.Center) * 500f;
                        if (NPC.Distance(targetPos2) > 50f)
                        {
                            Movement(targetPos2, 0.7f);
                        }
                        if ((NewAI[1] += 1f) > 60f)
                        {
                            NPC.netUpdate = true;
                            ClearNewAI();
                            NPC.localAI[0] = 0f;
                            NPC.localAI[1] = 0f;
                            NPC.localAI[2] = 0f;
                            NPC.localAI[3] = 0f;
                            ChooseNextAttack(20, 21, 24, 25, 29, 44, 45);
                        }
                    }
                    break;
                case 114517:
                    {
                        if (NewAI[1] < 90f && !AliveCheck(player))
                        {
                            break;
                        }
                        if (NewAI[2] == 0f && NewAI[3] == 0f)
                        {
                            NPC.netUpdate = true;
                            NewAI[2] = player.Center.X;
                            NewAI[3] = player.Center.Y;
                            if (FargoSoulsUtil.ProjectileExists(ritualProj, ModContent.ProjectileType<AbomRitual>()) != null)
                            {
                                NewAI[2] = Main.projectile[ritualProj].Center.X;
                                NewAI[3] = Main.projectile[ritualProj].Center.Y;
                            }
                            Vector2 offset = default(Vector2);
                            offset.X = Math.Sign(player.Center.X - NewAI[2]);
                            offset.Y = Math.Sign(player.Center.Y - NewAI[3]);
                            NPC.localAI[2] = offset.ToRotation();
                        }
                        Vector2 actualTargetPositionOffset = (float)Math.Sqrt(2880000.0) * NPC.localAI[2].ToRotationVector2();
                        actualTargetPositionOffset.Y -= 450 * Math.Sign(actualTargetPositionOffset.Y);
                        Vector2 targetPos = new Vector2(NewAI[2], NewAI[3]) + actualTargetPositionOffset;
                        Movement(targetPos, 1f);
                        if (NewAI[1] == 0f && Main.netMode != 1)
                        {
                            float num = Math.Sign(NPC.ai[2] - targetPos.X);
                            float verticalModifier = Math.Sign(NPC.ai[3] - targetPos.Y);
                            float startRotation = ((num > 0f) ? (MathHelper.ToRadians(0.1f) * (0f - verticalModifier)) : ((float)Math.PI - MathHelper.ToRadians(0.1f) * (0f - verticalModifier)));
                            float ai1 = ((num > 0f) ? ((float)Math.PI) : 0f);
                            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, startRotation.ToRotationVector2(), ModContent.ProjectileType<GlowLine>(), 1, 0f, 0, 4f, ai1).localAI[1] = NPC.whoAmI;
                            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, startRotation.ToRotationVector2(), ModContent.ProjectileType<GlowLine>(), 1, 0f, 0, 4f, ai1 + 3.1416f).localAI[1] = NPC.whoAmI;
                        }
                        if (NewAI[1] > 90f)
                        {
                            FancyFireballs((int)NewAI[1] - 90);
                        }
                        if ((NewAI[1] += 1f) > 150f)
                        {
                            NPC.netUpdate = true;
                            NPC.velocity = Vector2.Zero;
                            NPC.ai[0] += 1f;
                            NewAI[1] = 0f;
                        }
                        break;
                    }

                #endregion

                #region phase 2

                case 10: Phase2Transition(); break;

                case 11: ApproachForNextAttackP2(); break;
                case 12: VoidRaysP2(); break;

                case 13: PrepareSpearDashPredictiveP2(); break;
                case 14: SpearDashPredictiveP2(); break;
                case 15: WhileDashingP2(); break;

                case 16: goto case 11; 
                case 17: BoundaryBulletHellP2(); break;

                case 18: goto case 49;

                case 19: PillarDunk(); break;

                case 20: EOCStarSickles(); break;

                case 21: PrepareSpearDashDirectP2(); break;
                case 22: SpearDashDirectP2(); break;
                case 23:
                    if (NPC.ai[1] % 3 == 0)
                        NPC.ai[1]++;
                    goto case 15;

                case 24: SpawnDestroyersForPredictiveThrow(); break;
                case 25: SpearTossPredictiveP2(); break;

                case 26: PrepareMechRayFan(); break;
                case 27: MechRayFan(); break;

                case 28: AttackChoice++; break;

                case 29: PrepareFishron1(); break;
                case 30: SpawnFishrons(); break;

                case 31: PrepareTrueEyeDiveP2(); break;
                case 32: goto case 3;

                case 33: PrepareNuke(); break;
                case 34: Nuke(); break;

                case 35: PrepareSlimeRain(); break;
                case 36: SlimeRain(); break;

                case 37: PrepareFishron2(); break;
                case 38: goto case 30; 

                case 39: PrepareOkuuSpheresP2(); break;
                case 40: OkuuSpheresP2(); break;

                case 41: SpearTossDirectP2(); break;

                case 42: PrepareTwinRangsAndCrystals(); break;
                case 43: TwinRangsAndCrystals(); break;

                case 44: EmpressSwordWave(); break;

                case 45: PrepareMutantSword(); break;
                case 46: MutantSword(); break;

                case 47: goto case 35;
                case 48: QueenSlimeRain(); break;

                case 49: SANSGOLEM(); break;

                case 415411:
                    {
                        if (Phase2Check())
                        {
                            return;
                        }
                        if (NPC.localAI[3] == 100f)
                        {
                            Projectile[] projectile = Main.projectile;
                            foreach (Projectile p in projectile)
                            {
                                if (p.type == ModContent.ProjectileType<MutantEyeHoming>() || p.type == ModContent.ProjectileType<MutantSphereRing>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                                {
                                    p.active = false;
                                }
                            }
                        }
                        if (NPC.localAI[3] < 100f)
                        {
                            FancyFireballs6((int)NPC.localAI[3]);
                        }
                        NPC.velocity *= 0.9f;
                        if (NPC.localAI[3] < 100f)
                        {
                            NPC.localAI[3] += 1f;
                        }
                        float length = MathHelper.Lerp(2500f, 1000f, NPC.localAI[3] / 100f);
                        for (int i = 0; i < 50; i++)
                        {
                            Dust.NewDustDirect(NPC.Center + Main.rand.NextVector2CircularEdge(length, length), 0, 0, 252, 0f, 0f, 0, default(Color), 1.5f).noGravity = true;
                        }
                        if (NPC.Distance(player.Center) > length)
                        {
                            player.velocity *= 0f;
                            player.Center += (NPC.Center - player.Center).SafeNormalize(Vector2.Zero) * 5f;
                        }
                        if (NewAI[3] == 0f)
                        {
                            NewAI[3] = ((!(NPC.Center.X < player.Center.X)) ? 1 : (-1));
                        }
                        if ((NewAI[2] += 1f) > (float)((NPC.localAI[2] == 1f) ? 40 : 60))
                        {
                            NewAI[2] = 0f;
                            SoundEngine.PlaySound(SoundID.Item92, (Vector2?)NPC.Center);
                            if (NPC.localAI[0] > 0f)
                            {
                                NPC.localAI[0] = -1f;
                            }
                            else
                            {
                                NPC.localAI[0] = 1f;
                            }
                            if (Main.netMode != 1)
                            {
                                Vector2 projTarget = NPC.Center;
                                projTarget.X += 1200f * NewAI[3];
                                projTarget.Y += 1200f * (0f - NPC.localAI[0]);
                                int max = ((NPC.localAI[2] == 1f) ? 30 : 20);
                                int increment = ((NPC.localAI[2] == 1f) ? 180 : 250);
                                projTarget.Y += Main.rand.NextFloat(increment);
                                for (int i = 0; i < max; i++)
                                {
                                    projTarget.Y += (float)increment * NPC.localAI[0];
                                    Vector2 speed = (projTarget - NPC.Center) / 40f;
                                    float ai0 = (float)((NPC.localAI[2] == 1f) ? 8 : 6) * (0f - NewAI[3]);
                                    float ai1 = 6f * (0f - NPC.localAI[0]);
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<ChampionBeetle>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, ai1);
                                }
                            }
                        }
                        if ((NewAI[1] += 1f) > 620f)
                        {
                            NPC.localAI[0] = 0f;
                            NPC.localAI[3] = 0f;
                            NPC.TargetClosest();
                            P1NextAttackOrMasoOptions(HistoryAttack1);
                            NewAI[1] = 0f;
                            NewAI[2] = 0f;
                            NewAI[3] = 0f;
                            NPC.netUpdate = true;
                        }
                        break;
                    }
                //case 50: PrepareDeathrayRain(); break;
                //case 51: DeathrayRain(); break;
                //case 52: PrepareDeathrayRain2(); break;
                //case 53: DeathrayRain2(); break;

                //case 50: //wof

                //gap in the numbers here so the ai loops right
                //when adding a new attack, remember to make ChooseNextAttack() point to the right case!

                case 70: P2NextAttackPause(); break;

                #endregion

                #region phase 3

                case -1: drainLifeInP3 = Phase3Transition(); break;

                case -2: VoidRaysP3(); break;

                case -3: OkuuSpheresP3(); break;

                case -4: BoundaryBulletHellP3(); break;

                case -5: AbomSwordP3(); break;
                case -6: AbomSwordDashP3(); break;
                case -7: AbomSwordWaitP3(); break;

                case -8: FinalSpark(); break;

                case -9: DyingDramaticPause(); break;
                case -10: DyingAnimationAndHandling(); break;

                #endregion

                default: AttackChoice = 11; goto case 11; 
            }

            if (AttackChoice == 1)
            {
                AuraScale = MathHelper.Lerp(AuraScale, 0.7f, 0.02f);
            }
            else if (AttackChoice == 5 || AttackChoice == 6)
            {
                AuraScale = MathHelper.Lerp(AuraScale, 1.25f, 0.1f);
            }
            else
            {
                AuraScale = MathHelper.Lerp(AuraScale, 1f, 0.1f);
            }

            if (AttackChoice != 5 && AttackChoice != 6 && AttackChoice != 1922) 
            {
                AuraCenter = Vector2.Lerp(AuraCenter, NPC.Center, 0.3f);
            }


            if (AttackChoice < 0 || AttackChoice > 10 || AttackChoice == 10 && NPC.ai[1] > 150)
            {
                Main.dayTime = false;
                Main.time = 16200;

                Main.raining = false;
                Main.rainTime = 0;
                Main.maxRaining = 0;

                Main.bloodMoon = false;
            }

            if (AttackChoice < 0 && NPC.life > 1 && drainLifeInP3) 
            {
                int time = 480 + 240 + 420 + 480 + 1020 - 60;
                time = Main.getGoodWorld ? 5000 : 4350;
                int drain = NPC.lifeMax / time;
                NPC.life -= drain;
                if (NPC.life < 1)
                    NPC.life = 1;
            }

            if (player.immune || player.hurtCooldowns[0] != 0 || player.hurtCooldowns[1] != 0)
                playerInvulTriggered = true;

            if (WorldSavingSystem.DownedAbom && !WorldSavingSystem.DownedMutant && FargoSoulsUtil.HostCheck && NPC.HasPlayerTarget && !droppedSummon)
            {
                Item.NewItem(NPC.GetSource_Loot(), player.Hitbox, ModContent.ItemType<MutantsCurse>());
                droppedSummon = true;
            }

            //if (Main.getGoodWorld && ++hyper > HyperMax + 1)
            //{
            //    hyper = 0;
            //    NPC.AI();
            //}
            void FancyFireballs(int repeats)
            {
                float modifier = 0f;
                for (int i = 0; i < repeats; i++)
                {
                    modifier = MathHelper.Lerp(modifier, 1f, 0.08f);
                }
                float distance = 1400f * (1f - modifier);
                float rotation = (float)Math.PI * 2f * modifier;
                for (int i = 0; i < 4; i++)
                {
                    int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + (float)Math.PI / 2f * (float)i), 0, 0, 70, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 6f - 4f * modifier;
                }
            }
            void FancyFireballs2(int repeats)
            {
                float modifier = 0f;
                for (int i = 0; i < repeats; i++)
                {
                    modifier = MathHelper.Lerp(modifier, 1f, 0.08f);
                }
                float distance = 1400f * (1f - modifier);
                float rotation = (float)Math.PI * 2f * modifier;
                for (int i = 0; i < 9; i++)
                {
                    int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + (float)Math.PI * 2f / 9f * (float)i), 0, 0, 173, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 7f - 4f * modifier;
                }
            }
            void FancyFireballs3(int repeats)
            {
                float modifier = 0f;
                for (int i = 0; i < repeats; i++)
                {
                    modifier = MathHelper.Lerp(modifier, 1f, 0.08f);
                }
                float distance = 1400f * (1f - modifier);
                float rotation = (float)Math.PI * 2f * modifier;
                for (int i = 0; i < 7; i++)
                {
                    int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + 0.8975979f * (float)i), 0, 0, 292, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 6f - 4f * modifier;
                }
            }
            void FancyFireballs4(int repeats)
            {
                this.Movement(this.player.Center - NPC.DirectionTo(this.player.Center).SafeNormalize(Vector2.Zero) * 560f, 1f);
                float modifier = 0f;
                for (int i = 0; i < repeats; i++)
                {
                    modifier = MathHelper.Lerp(modifier, 1f, 0.08f);
                }
                float distance = 1600f * (1f - modifier);
                float rotation = (float)Math.PI * 2f * modifier;
                for (int i = 0; i < 10; i++)
                {
                    int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + (float)Math.PI / 5f * (float)i), 0, 0, 63, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 7f - 4f * modifier;
                }
            }
            void FancyFireballs5(int repeats)
            {
                float modifier = 0f;
                for (int i = 0; i < repeats; i++)
                {
                    modifier = MathHelper.Lerp(modifier, 1f, 0.08f);
                }
                float distance = 1400f * (1f - modifier);
                float rotation = (float)Math.PI * 2f * modifier;
                for (int i = 0; i < 6; i++)
                {
                    int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + (float)Math.PI / 3f * (float)i), 0, 0, 227, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 6f - 4f * modifier;
                }
            }
            void FancyFireballs6(int repeats)
            {
                float modifier = 0f;
                for (int i = 0; i < repeats; i++)
                {
                    modifier = MathHelper.Lerp(modifier, 1f, 0.08f);
                }
                float distance = 1400f * (1f - modifier);
                float rotation = (float)Math.PI * 2f * modifier;
                for (int i = 0; i < 4; i++)
                {
                    int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + (float)Math.PI / 2f * (float)i), 0, 0, 252, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 6f - 4f * modifier;
                }
            }
            void SpawnAxeHitbox(Vector2 spawnPos)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<DeviAxe>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 2f), 0f, Main.myPlayer, (float)NPC.whoAmI, NPC.Distance(spawnPos));
            }

            if (!AliveCheck(player))
            {
                NPC.active = false;
            }
        }

        #region helper functions

        bool spawned;
        void MovementY(float targetY, float speedModifier)
        {
            if (NPC.Center.Y < targetY)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.Y) > 24)
                NPC.velocity.Y = 24 * Math.Sign(NPC.velocity.Y);
        }
        
        void ManageAurasAndPreSpawn()
        {
            if (!spawned)
            {
                spawned = true;

                int prevLifeMax = NPC.lifeMax;
                if (WorldSavingSystem.AngryMutant)
                {
                    NPC.lifeMax *= 100;
                    if (NPC.lifeMax < prevLifeMax)
                        NPC.lifeMax = int.MaxValue;
                }
                NPC.life = NPC.lifeMax;

                if (player.FargoSouls().TerrariaSoul)
                    EdgyBossText(GFBQuote(1));
            }

            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2);

            if (NPC.localAI[3] == 0)
            {
                NPC.TargetClosest();
                if (NPC.timeLeft < 30)
                    NPC.timeLeft = 30;
                if (NPC.Distance(Main.player[NPC.target].Center) < 1500)
                {
                    NPC.localAI[3] = 1;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                    EdgyBossText(GFBQuote(2));
                }
            }
            else if (NPC.localAI[3] == 1)
            {
                ShouldDrawAura = true;
                ArenaAura(AuraCenter, 2000f * AuraScale, true, -1, default, ModContent.BuffType<ChtuxlagorInferno>(), ModContent.BuffType<MutantFangBuff>());
            }
            else
            {
                if (Main.LocalPlayer.active && NPC.Distance(Main.LocalPlayer.Center) < 3000f)
                {
                    if (Main.expertMode)
                    {
                        Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2);
                        if (Main.getGoodWorld)
                            Main.LocalPlayer.AddBuff(ModContent.BuffType<GoldenStasisCDBuff>(), 2);
                    }

                    if (AttackChoice < 0 && AttackChoice > -6)
                    {
                        Main.LocalPlayer.AddBuff(ModContent.BuffType<GoldenStasisCDBuff>(), 2);
                        Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeStopCDBuff>(), 2);
                        Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantDesperationBuff>(), 2);
                    }
                }
            }
        }

        void ManageNeededProjectiles()
        {
            if (FargoSoulsUtil.HostCheck)
            {
                if (WorldSavingSystem.EternityMode && AttackChoice != -7 && FargoSoulsUtil.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null)
                    ritualProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, 0f, NPC.whoAmI);

                if (FargoSoulsUtil.ProjectileExists(ringProj, ModContent.ProjectileType<MutantRitual5>()) == null)
                    ringProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual5>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);

                if (FargoSoulsUtil.ProjectileExists(spriteProj, ModContent.ProjectileType<MutantBoss>()) == null)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        int number = 0;
                        for (int index = 999; index >= 0; --index)
                        {
                            if (!Main.projectile[index].active)
                            {
                                number = index;
                                break;
                            }
                        }
                        if (number >= 0)
                        {
                            Projectile projectile = Main.projectile[number];
                            projectile.SetDefaults(ModContent.ProjectileType<MutantBoss>());
                            projectile.Center = NPC.Center;
                            projectile.owner = Main.myPlayer;
                            projectile.velocity.X = 0;
                            projectile.velocity.Y = 0;
                            projectile.damage = 0;
                            projectile.knockBack = 0f;
                            projectile.identity = number;
                            projectile.gfxOffY = 0f;
                            projectile.stepSpeed = 1f;
                            projectile.ai[1] = NPC.whoAmI;

                            spriteProj = number;
                        }
                    }
                    else 
                    {
                        spriteProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantBoss>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }
                }
            }
        }

        private void ChooseNextAttack(params int[] args)
        {
            this.ShouldDoSword++;
            float buffer = ((base.NPC.ai[0] > 100f) ? this.HistoryAttack1 : base.NPC.ai[0]);
            base.NPC.ai[0] = 70f;
            base.NPC.ai[1] = 0f;
            base.NPC.ai[2] = buffer - 1f;
            base.NPC.ai[3] = 0f;
            base.NPC.localAI[0] = 0f;
            base.NPC.localAI[1] = 0f;
            base.NPC.localAI[2] = 0f;
            base.NPC.netUpdate = true;
            if (this.ShouldDoSword == 4)
            {
                this.HistoryAttack1 = buffer;
                Projectile[] projectile = Main.projectile;
                foreach (Projectile p in projectile)
                {
                    if (p.type == ModContent.ProjectileType<MutantDeathray2>() || p.type == ModContent.ProjectileType<MutantDeathraySmall>() || p.type == ModContent.ProjectileType<MutantDeathrayAim>() || p.type == ModContent.ProjectileType<MutantMark1>() || p.type == ModContent.ProjectileType<MutantDeathray3>() || p.type == ModContent.ProjectileType<MutantFragment>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                    {
                        p.active = false;
                    }
                }
                base.NPC.ai[0] = 114514f;
                return;
            }
            if (this.ShouldDoSword == 8)
            {
                this.HistoryAttack1 = buffer;
                Projectile[] projectile = Main.projectile;
                foreach (Projectile p in projectile)
                {
                    if (p.type == ModContent.ProjectileType<MutantDeathray2>() || p.type == ModContent.ProjectileType<MutantDeathraySmall>() || p.type == ModContent.ProjectileType<MutantDeathrayAim>() || p.type == ModContent.ProjectileType<MutantDeathray3>() || p.type == ModContent.ProjectileType<MutantMark1>() || p.type == ModContent.ProjectileType<MutantFragment>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                    {
                        p.active = false;
                    }
                }
                this.ClearNewAI();
                base.NPC.ai[0] = 888f;
                return;
            }
            if (this.ShouldDoSword == 11)
            {
                this.HistoryAttack1 = buffer;
                this.ClearNewAI();
                base.NPC.ai[0] = 1919f;
                return;
            }
            if (this.ShouldDoSword == 14)
            {
                this.HistoryAttack1 = buffer;
                Projectile[] projectile = Main.projectile;
                foreach (Projectile p in projectile)
                {
                    if (p.type == ModContent.ProjectileType<MutantDeathray2>() || p.type == ModContent.ProjectileType<MutantDeathraySmall>() || p.type == ModContent.ProjectileType<MutantDeathrayAim>() || p.type == ModContent.ProjectileType<MutantMark1>() || p.type == ModContent.ProjectileType<MutantDeathray3>() || p.type == ModContent.ProjectileType<MutantFragment>() || p.type == ModContent.ProjectileType<MutantTrueEyeDeathray>() || p.type == ModContent.ProjectileType<MutantTrueEyeL>() || p.type == ModContent.ProjectileType<MutantTrueEyeR>() || p.type == ModContent.ProjectileType<MutantTrueEyeS>())
                    {
                        p.active = false;
                    }
                }
                base.NPC.ai[0] = 132f;
                return;
            }
            if (WorldSavingSystem.EternityMode)
            {
                bool useRandomizer = true;
                if (Main.netMode != 1)
                {
                    Queue<float> recentAttacks = new Queue<float>(this.attackHistory);
                    if (useRandomizer)
                    {
                        base.NPC.ai[2] = Main.rand.Next(args);
                    }
                    while (recentAttacks.Count > 0)
                    {
                        bool foundAttackToUse = false;
                        for (int i = 0; i < 5; i++)
                        {
                            if (!recentAttacks.Contains(base.NPC.ai[2]))
                            {
                                foundAttackToUse = true;
                                break;
                            }
                            base.NPC.ai[2] = Main.rand.Next(args);
                        }
                        if (foundAttackToUse)
                        {
                            break;
                        }
                        recentAttacks.Dequeue();
                    }
                }
            }
            if (Main.netMode != 1)
            {
                int maxMemory = (WorldSavingSystem.MasochistModeReal ? 10 : 16);
                if ((double)this.attackCount++ > (double)maxMemory * 1.25)
                {
                    this.attackCount = 0;
                    maxMemory /= 4;
                }
                this.attackHistory.Enqueue(base.NPC.ai[2]);
                while (this.attackHistory.Count > maxMemory)
                {
                    this.attackHistory.Dequeue();
                }
            }
            this.endTimeVariance = (WorldSavingSystem.MasochistModeReal ? Main.rand.NextFloat() : 0f);
        }

        private void P1NextAttackOrMasoOptions(float sourceAI)
        {
            ShouldChampion++;
            if (ShouldChampion == 3)
            {
                HistoryAttack1 = NPC.ai[0];
                NPC.ai[0] = 1919810f;
                return;
            }
            if (ShouldChampion == 6)
            {
                HistoryAttack1 = NPC.ai[0];
                for (int i = 0; i < 4; i++)
                {
                    NPC.localAI[i] = 0f;
                }
                NPC.ai[0] = 1919811f;
                return;
            }
            if (ShouldChampion == 9)
            {
                HistoryAttack1 = NPC.ai[0];
                for (int i = 0; i < 4; i++)
                {
                    NPC.localAI[i] = 0f;
                }
                NPC.ai[0] = 1919812f;
                return;
            }
            if (ShouldChampion == 12)
            {
                HistoryAttack1 = NPC.ai[0];
                for (int i = 0; i < 4; i++)
                {
                    NPC.localAI[i] = 0f;
                }
                NPC.ai[0] = 1919813f;
                return;
            }
            if (ShouldChampion == 15)
            {
                HistoryAttack1 = NPC.ai[0];
                for (int i = 0; i < 4; i++)
                {
                    NPC.localAI[i] = 0f;
                }
                NPC.ai[0] = 415411f;
                ShouldChampion = 0;
                return;
            }
            if (WorldSavingSystem.MasochistModeReal && Main.rand.NextBool(3))
            {
                int[] options = new int[7] { 0, 1, 2, 4, 7, 9, 9 };
                NPC.ai[0] = Main.rand.Next(options);
                if (NPC.ai[0] == sourceAI)
                {
                    NPC.ai[0] = ((sourceAI != 9f) ? 9 : 0);
                }
                bool badCombo = false;
                if (NPC.ai[0] == 9f && (sourceAI == 1f || sourceAI == 2f || sourceAI == 7f))
                {
                    badCombo = true;
                }
                if ((NPC.ai[0] == 0f || NPC.ai[0] == 7f) && sourceAI == 2f)
                {
                    badCombo = true;
                }
                if (badCombo)
                {
                    NPC.ai[0] = 4f;
                }
                else if (NPC.ai[0] == 9f && Main.rand.NextBool())
                {
                    NPC.localAI[2] = 1f;
                }
                else
                {
                    NPC.localAI[2] = 0f;
                }
            }
            else if (NPC.ai[0] == 9f && NPC.localAI[2] == 0f)
            {
                NPC.localAI[2] = 1f;
            }
            else
            {
                NPC.ai[0] += 1f;
                NPC.localAI[2] = 0f;
            }
            if (NPC.ai[0] >= 10f)
            {
                NPC.ai[0] = ((!Main.rand.NextBool(2)) ? 2 : 0);
            }
            NPC.ai[1] = 0f;
            NPC.ai[2] = 0f;
            NPC.ai[3] = 0f;
            NPC.localAI[0] = 0f;
            NPC.localAI[1] = 0f;
            NPC.netUpdate = true;
        }

        void SpawnSphereRing(int max, float speed, int damage, float rotationModifier, float offset = 0)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            float rotation = 2f * (float)Math.PI / max;
            int type = ModContent.ProjectileType<MutantSphereRing>();
            for (int i = 0; i < max; i++)
            {
                Vector2 vel = speed * Vector2.UnitY.RotatedBy(rotation * i + offset);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * NPC.spriteDirection, speed);
            }
            SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
        }

        bool AliveCheck(Player p, bool forceDespawn = false)
        {
            if (WorldSavingSystem.SwarmActive || forceDespawn || (!p.active || p.dead || Vector2.Distance(NPC.Center, p.Center) > 3000f) && NPC.localAI[3] > 0)
            {
                NPC.TargetClosest();
                p = Main.player[NPC.target];
                if (WorldSavingSystem.SwarmActive || forceDespawn || !p.active || p.dead || Vector2.Distance(NPC.Center, p.Center) > 3000f)
                {
                    if (NPC.timeLeft > 30)
                        NPC.timeLeft = 30;
                    NPC.velocity.Y -= 1f;
                    if (NPC.timeLeft == 1)
                    {
                        EdgyBossText(GFBQuote(36));
                        if (NPC.position.Y < 0)
                            NPC.position.Y = 0;
                        if (FargoSoulsUtil.HostCheck && ModContent.TryFind("Fargowiltas", "Mutant", out ModNPC modNPC) && !NPC.AnyNPCs(modNPC.Type))
                        {
                            FargoSoulsUtil.ClearHostileProjectiles(2, NPC.whoAmI);
                            int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, modNPC.Type);
                            if (n != Main.maxNPCs)
                            {
                                Main.npc[n].homeless = true;
                                if (TownNPCName != default)
                                    Main.npc[n].GivenName = TownNPCName;
                                if (Main.netMode == NetmodeID.Server)
                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                            }
                        }
                    }
                    return false;
                }
            }

            if (NPC.timeLeft < 3600)
                NPC.timeLeft = 3600;

            if (player.Center.Y / 16f > Main.worldSurface)
            {
                NPC.velocity.X *= 0.95f;
                NPC.velocity.Y -= 1f;
                if (NPC.velocity.Y < -32f)
                    NPC.velocity.Y = -32f;
                return false;
            }

            return true;
        }

        bool Phase2Check()
        {
            if (Main.expertMode && NPC.life < NPC.lifeMax / 2)
            {
                if (FargoSoulsUtil.HostCheck)
                {
                    AttackChoice = 10;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                    FargoSoulsUtil.ClearHostileProjectiles(1, NPC.whoAmI);
                    EdgyBossText(GFBQuote(3));
                }
                return true;
            }
            return false;
        }

        private void StrongAttackTeleport(Vector2 teleportTarget = default(Vector2))
        {
            if ((teleportTarget == default(Vector2)) ? (NPC.Distance(this.player.Center) < 450f) : (NPC.Distance(teleportTarget) < 80f))
            {
                return;
            }
            TeleportDust();
            if (Main.netMode != 1)
            {
                if (teleportTarget != default(Vector2))
                {
                    NPC.Center = teleportTarget;
                }
                else if (this.player.velocity == Vector2.Zero)
                {
                    NPC.Center = this.player.Center + 450f * Vector2.UnitX.RotatedByRandom(Math.PI * 2.0);
                }
                else
                {
                    NPC.Center = this.player.Center + 450f * Vector2.Normalize(this.player.velocity);
                }
                NPC.velocity /= 2f;
                NPC.netUpdate = true;
            }
            TeleportDust();
            SoundEngine.PlaySound(SoundID.Item84, (Vector2?)NPC.Center);
        }

        private void TeleportDust()
        {
            for (int index1 = 0; index1 < 25; index1++)
            {
                int index2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 272, 0f, 0f, 100, default(Color), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 7f;
                Main.dust[index2].noLight = true;
                int index3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 272, 0f, 0f, 100);
                Main.dust[index3].velocity *= 4f;
                Main.dust[index3].noGravity = true;
                Main.dust[index3].noLight = true;
            }
        }
        void Movement(Vector2 target, float speed, bool fastX = true, bool obeySpeedCap = true)
        {
            float turnaroundModifier = 2f;
            float maxSpeed = 48;
            speed *= 2f;

            if (Math.Abs(NPC.Center.X - target.X) > 10)
            {
                if (NPC.Center.X < target.X)
                {
                    NPC.velocity.X += speed;
                    if (NPC.velocity.X < 0)
                        NPC.velocity.X += speed * (fastX ? 2 : 1) * turnaroundModifier;
                }
                else
                {
                    NPC.velocity.X -= speed;
                    if (NPC.velocity.X > 0)
                        NPC.velocity.X -= speed * (fastX ? 2 : 1) * turnaroundModifier;
                }
            }
            if (NPC.Center.Y < target.Y)
            {
                NPC.velocity.Y += speed;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speed * 2 * turnaroundModifier;
            }
            else
            {
                NPC.velocity.Y -= speed;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speed * 2 * turnaroundModifier;
            }

            if (obeySpeedCap)
            {
                if (Math.Abs(NPC.velocity.X) > maxSpeed)
                    NPC.velocity.X = maxSpeed * Math.Sign(NPC.velocity.X);
                if (Math.Abs(NPC.velocity.Y) > maxSpeed)
                    NPC.velocity.Y = maxSpeed * Math.Sign(NPC.velocity.Y);
            }
        }

        void DramaticTransition(bool fightIsOver, bool normalAnimation = true)
        {
            NPC.velocity = Vector2.Zero;

            if (fightIsOver)
            {
                Main.player[NPC.target].ClearBuff(ModContent.BuffType<MutantFangBuff>());
                Main.player[NPC.target].ClearBuff(ModContent.BuffType<AbomRebirthBuff>());
            }

            SoundEngine.PlaySound(SoundID.Item27 with { Volume = 1.5f }, NPC.Center);

            if (normalAnimation)
            {
                if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantBomb>(), 0, 0f, Main.myPlayer);
            }

            const int max = 40;
            float totalAmountToHeal = fightIsOver
                ? Main.player[NPC.target].statLifeMax2 / 4f
                : NPC.lifeMax - NPC.life + NPC.lifeMax * 0.1f;
            for (int i = 0; i < max; i++)
            {
                int heal = (int)(Main.rand.NextFloat(0.9f, 1.1f) * totalAmountToHeal / max);
                Vector2 vel = normalAnimation
                    ? Main.rand.NextFloat(2f, 18f) * -Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) 
                    : 0.1f * -Vector2.UnitY.RotatedBy(MathHelper.TwoPi / max * i); 
                float ai0 = fightIsOver ? -Main.player[NPC.target].whoAmI - 1 : NPC.whoAmI;
                float ai1 = vel.Length() / Main.rand.Next(fightIsOver ? 90 : 150, 180); 
                if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantHeal>(), heal, 0f, Main.myPlayer, ai0, ai1);
            }
        }

        void EModeSpecialEffects()
        {
            if (Main.GameModeInfo.IsJourneyMode && CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().Enabled)
                CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().SetPowerInfo(false);

            if (!SkyManager.Instance["ssm:RealMutantEX"].IsActive())
                SkyManager.Instance.Activate("ssm:RealMutantEX");

            if (ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod))
            {
                if (musicMod.Version >= Version.Parse("0.1.1"))
                    Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Storia");
            }
        }

        void FancyFireballs(int repeats)
        {
            float modifier = 0;
            for (int i = 0; i < repeats; i++)
                modifier = MathHelper.Lerp(modifier, 1f, 0.08f);

            float distance = 1600 * (1f - modifier);
            float rotation = MathHelper.TwoPi * modifier;
            const int max = 6;
            for (int i = 0; i < max; i++)
            {
                int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + MathHelper.TwoPi / max * i), 0, 0, DustID.Vortex, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, newColor: Color.White);
                Main.dust[d].noGravity = true;
                Main.dust[d].scale = 6f - 4f * modifier;
            }
        }

        private void EdgyBossText(string text)
        {
            //if (Main.zenithWorld)
            //{
                Color color = Color.Cyan;
                FargoSoulsUtil.PrintText(text, color);
                CombatText.NewText(NPC.Hitbox, color, text, true);
            //}
        }
        const int ObnoxiousQuoteCount = 71;
        const string GFBLocPath = $"Mods.FargowiltasSouls.NPCs.MutantBoss.GFBText.";
        private string RandomObnoxiousQuote() => Language.GetTextValue($"{GFBLocPath}Random{Main.rand.Next(ObnoxiousQuoteCount)}");
        private string GFBQuote(int num) => Language.GetTextValue($"{GFBLocPath}Quote{num}");

        #endregion

        #region p1

        void SpearTossDirectP1AndChecks()
        {
            if (!AliveCheck(player))
                return;
            if (Phase2Check())
                return;
            NPC.localAI[2] = 0;
            Vector2 targetPos = player.Center;
            targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 50)
            {
                Movement(targetPos, NPC.localAI[3] > 0 ? 0.5f : 2f, true, NPC.localAI[3] > 0);
            }

            if (NPC.ai[3] == 0)
            {
                NPC.ai[3] = Main.rand.Next(2, 8);
                NPC.netUpdate = true;
            }

            if (NPC.localAI[3] > 0) 
                NPC.ai[1]++;

            if (NPC.ai[1] < 145) 
            {
                NPC.localAI[0] = NPC.SafeDirectionTo(player.Center + player.velocity * 30f).ToRotation();
            }

            if (NPC.ai[1] > 150) 
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 60;
                if (++NPC.ai[2] > NPC.ai[3])
                {
                    P1NextAttackOrMasoOptions(AttackChoice);
                    NPC.velocity = NPC.SafeDirectionTo(player.Center) * 2f;
                }
                else if (FargoSoulsUtil.HostCheck)
                {
                    Vector2 vel = NPC.localAI[0].ToRotationVector2() * 25f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.target);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer);
                }
                NPC.localAI[0] = 0;
            }
            else if (NPC.ai[1] == 61 && NPC.ai[2] < NPC.ai[3] && FargoSoulsUtil.HostCheck)
            {
                if (NPC.ai[2] == 0) 
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
                    if (FargoSoulsUtil.HostCheck) 
                    {
                        int appearance = Main.rand.Next(2);
                        for (int j = 0; j < 8; j++)
                        {
                            Vector2 vel = NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.ToRadians(120)) * 10f;
                            float ai1 = 0.8f + 0.4f * j / 5f;
                            int current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerHead>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.target, ai1, appearance);
                            Main.projectile[current].timeLeft = 90 * ((int)NPC.ai[3] + 1) + 30 + j * 6;
                            int max = Main.rand.Next(8, 19);
                            for (int i = 0; i < max; i++)
                                current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerBody>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, Main.projectile[current].identity, 0f, appearance);
                            int previous = current;
                            current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerTail>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, Main.projectile[current].identity, 0f, appearance);
                            Main.projectile[previous].localAI[1] = Main.projectile[current].identity;
                            Main.projectile[previous].netUpdate = true;
                        }
                    }
                }



                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.SafeDirectionTo(player.Center + player.velocity * 30f), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0f, Main.myPlayer, 85f, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.whoAmI, 3);

                //Projectile.NewProjectile(npc.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.whoAmI);
            }
        }

        void OkuuSpheresP1()
        {
            if (Phase2Check())
                return;

            NPC.velocity = Vector2.Zero;
            if (--NPC.ai[1] < 0)
            {
                NPC.netUpdate = true;
                float modifier = 3;
                NPC.ai[1] = 90 / modifier;
                if (++NPC.ai[2] > 4 * modifier)
                {
                    if (NPC.ai[2] > 6 * modifier)
                    {
                        P1NextAttackOrMasoOptions(AttackChoice);
                    }

                }
                else
                {
                    EdgyBossText(RandomObnoxiousQuote());

                    int max = 10;
                    float speed = 11;
                    int sign = NPC.ai[2] % 2 == 0 ? 1 : -1;
                    SpawnSphereRing(max, speed, (int)(0.8 * FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage)), 1f * sign);
                    SpawnSphereRing(max, speed, (int)(0.8 * FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage)), -0.5f * sign);
                }

            }
        }

        void PrepareTrueEyeDiveP1()
        {
            if (!AliveCheck(player))
                return;
            if (Phase2Check())
                return;
            Vector2 targetPos = player.Center;
            targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
            targetPos.Y -= 400;
            Movement(targetPos, 0.6f);
            if (NPC.Distance(targetPos) < 50 || ++NPC.ai[1] > 180) //dive here
            {
                NPC.velocity.X = 35f * (NPC.position.X < player.position.X ? 1 : -1);
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y *= -1;
                NPC.velocity.Y *= 0.3f;
                AttackChoice++;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                EdgyBossText(RandomObnoxiousQuote());
            }
        }

        void TrueEyeDive()
        {
            if (NPC.ai[3] == 0)
                NPC.ai[3] = Math.Sign(NPC.Center.X - player.Center.X);

            if (NPC.ai[2] > 3)
            {
                Vector2 targetPos = player.Center;
                targetPos.X += NPC.Center.X < player.Center.X ? -500 : 500;
                if (NPC.Distance(targetPos) > 50)
                    Movement(targetPos, 0.3f);
            }
            else
            {
                NPC.velocity *= 0.99f;
            }

            if (--NPC.ai[1] < 0)
            {
                NPC.ai[1] = 15;
                int maxEyeThreshold = 7;
                int endlag = 2;
                if (++NPC.ai[2] > maxEyeThreshold + endlag)
                {
                    if (AttackChoice == 3)
                        P1NextAttackOrMasoOptions(2);
                    else
                        ChooseNextAttack(13, 19, 21, 24, 33, 33, 33, 39, 41, 44);
                }
                else if (NPC.ai[2] <= maxEyeThreshold)
                {
                    if (FargoSoulsUtil.HostCheck)
                    {
                        int type;
                        float ratio = NPC.ai[2] / maxEyeThreshold * 3;
                        if (ratio <= 1f)
                            type = ModContent.ProjectileType<MutantTrueEyeL>();
                        else if (ratio <= 2f)
                            type = ModContent.ProjectileType<MutantTrueEyeS>();
                        else
                            type = ModContent.ProjectileType<MutantTrueEyeR>();

                        int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, type, FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.8f), 0f, Main.myPlayer, NPC.target);
                        if (p != Main.maxProjectiles) 
                        {
                            Main.projectile[p].localAI[1] = NPC.ai[3]; 
                            Main.projectile[p].netUpdate = true;
                        }
                    }
                    SoundEngine.PlaySound(SoundID.Item92, NPC.Center);
                    for (int i = 0; i < 30; i++)
                    {
                        int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceTorch, 0f, 0f, 0, default, 3f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                }
            }
        }

        void PrepareSpearDashDirectP1()
        {
            if (Phase2Check())
                return;
            if (NPC.ai[3] == 0)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[3] = 1;
                if (FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearSpin>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.whoAmI, 240); // 250);
                    TelegraphSound = SoundEngine.PlaySound(FargosSoundRegistry.MutantUnpredictive with { Volume = 2f }, NPC.Center);
                }


                EdgyBossText(GFBQuote(4));
            }

            if (++NPC.ai[1] > 240)
            {
                if (!AliveCheck(player))
                    return;
                AttackChoice++;
                NPC.ai[3] = 0;
                NPC.netUpdate = true;
            }

            Vector2 targetPos = player.Center;
            if (NPC.Top.Y < player.Bottom.Y)
                targetPos.X += 600f * Math.Sign(NPC.Center.X - player.Center.X);
            targetPos.Y += 400f;
            Movement(targetPos, 0.7f, false);
        }

        void SpearDashDirectP1()
        {
            if (Phase2Check())
                return;
            NPC.velocity *= 0.9f;

            if (NPC.ai[3] == 0)
                NPC.ai[3] = Main.rand.Next(5, 20);

            if (++NPC.ai[1] > NPC.ai[3])
            {
                NPC.netUpdate = true;
                AttackChoice++;
                NPC.ai[1] = 0;
                if (++NPC.ai[2] > 5)
                {
                    P1NextAttackOrMasoOptions(4);
                }
                else
                {
                    float speed = 50f;
                    NPC.velocity = speed * NPC.SafeDirectionTo(player.Center + player.velocity);
                    if (FargoSoulsUtil.HostCheck)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearDash>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.whoAmI);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer);
                    }

                    EdgyBossText(GFBQuote(5));
                }
            }
        }

        void WhileDashingP1()
        {
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
            if (++NPC.ai[1] > 30)
            {
                if (!AliveCheck(player))
                    return;
                NPC.netUpdate = true;
                AttackChoice--;
                NPC.ai[1] = 0;
            }
        }

        void ApproachForNextAttackP1()
        {
            if (!AliveCheck(player))
                return;
            if (Phase2Check())
                return;
            Vector2 targetPos = player.Center + player.SafeDirectionTo(NPC.Center) * 250;
            if (NPC.Distance(targetPos) > 50 && ++NPC.ai[2] < 180)
            {
                Movement(targetPos, 0.5f);
            }
            else
            {
                NPC.netUpdate = true;
                AttackChoice++;
                NPC.ai[1] = 0;
                NPC.ai[2] = player.SafeDirectionTo(NPC.Center).ToRotation();
                NPC.ai[3] = (float)Math.PI / 10f;
                if (player.Center.X < NPC.Center.X)
                    NPC.ai[3] *= -1;
            }
        }

        void VoidRaysP1()
        {
            if (Phase2Check())
                return;
            NPC.velocity = Vector2.Zero;
            if (--NPC.ai[1] < 0)
            {
                if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(2, 0).RotatedBy(NPC.ai[2]), ModContent.ProjectileType<MutantMark1>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer);
                NPC.ai[1] = 3;
                NPC.ai[2] += NPC.ai[3];
                if (NPC.localAI[0]++ == 20 || NPC.localAI[0] == 40)
                {
                    NPC.netUpdate = true;
                    NPC.ai[2] -= NPC.ai[3] / 4;

                    EdgyBossText(GFBQuote(6));
                }
                else if (NPC.localAI[0] >= 65)
                {
                    P1NextAttackOrMasoOptions(7);
                }
            }
        }

        const int MUTANT_SWORD_SPACING = 80;
        const int MUTANT_SWORD_MAX = 12;

        void BoundaryBulletHellAndSwordP1()
        {
            switch ((int)NPC.localAI[2])
            {
                case 0: 
                    if (NPC.ai[3] == 0)
                    {
                        if (AliveCheck(player))
                        {
                            NPC.ai[3] = 1;
                            NPC.localAI[0] = Math.Sign(NPC.Center.X - player.Center.X);
                        }
                        else
                        {
                            break;
                        }

                        EdgyBossText(GFBQuote(7));
                    }

                    if (Phase2Check())
                        return;

                    NPC.velocity = Vector2.Zero;

                    if (++NPC.ai[1] > 2) 
                    {
                        SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                        NPC.ai[1] = 0;
                        NPC.ai[2] += (float)Math.PI / 8 / 480 * (NPC.ai[3] - 300) * NPC.localAI[0];

                        if (FargoSoulsUtil.HostCheck)
                        {
                            int max = 5;
                            for (int i = 0; i < max; i++)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -7f).RotatedBy(NPC.ai[2] + MathHelper.TwoPi / max * i),
                                    ModContent.ProjectileType<MutantEye>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer);
                            }
                        }

                    }

                    if (++NPC.ai[3] > 360)
                    {
                        P1NextAttackOrMasoOptions(AttackChoice);
                    }
                    break;

                case 1:
                    PrepareMutantSword();
                    break;

                case 2:
                    MutantSword();
                    break;

                default:
                    break;
            }
        }

        private void PrepareMutantSword()
        {
            if (NPC.ai[0] == 9f && Main.LocalPlayer.active && NPC.Distance(Main.LocalPlayer.Center) < 3000f && Main.expertMode)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<PurgedBuff>(), 2);
            }
            int sign = ((NPC.ai[0] == 9f || NPC.localAI[2] % 2f != 1f) ? 1 : (-1));
            if (NPC.ai[2] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                Vector2 targetPos = player.Center;
                targetPos.X += 420 * Math.Sign(NPC.Center.X - player.Center.X);
                targetPos.Y -= 210 * sign;
                Movement(targetPos, 2f);
                if ((!((NPC.localAI[0] += 1f) > 30f) && !WorldSavingSystem.MasochistModeReal) || !(NPC.Distance(targetPos) < 64f))
                {
                    return;
                }
                NPC.velocity = Vector2.Zero;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Roar, (Vector2?)NPC.Center);
                NPC.localAI[1] = Math.Sign(player.Center.X - NPC.Center.X);
                float startAngle = (float)Math.PI / 4f * (0f - NPC.localAI[1]);
                NPC.ai[2] = startAngle * -4f / 20f * (float)sign;
                if (sign < 0)
                {
                    startAngle += (float)Math.PI / 2f * (0f - NPC.localAI[1]);
                }
                if (Main.netMode != 1)
                {
                    Vector2 offset = Vector2.UnitY.RotatedBy(startAngle) * -80f;
                    for (int i = 0; i < 12; i++)
                    {
                        MakeSword(offset * i, 80 * i);
                    }
                    for (int i = -1; i <= 1; i += 2)
                    {
                        MakeSword(offset.RotatedBy(MathHelper.ToRadians(26.5f * (float)i)), 180f);
                        MakeSword(offset.RotatedBy(MathHelper.ToRadians(40 * i)), 240f);
                    }
                }
                return;
            }
            if (NPC.Distance(swordTarget) > 75f && makedSword)
            {
                Movement(swordTarget, 2.25f);
            }
            else
            {
                NPC.velocity *= 0f;
            }
            FancyFireballs((int)(NPC.ai[1] / 60f * 60f));
            if ((NPC.ai[1] += 1f) > 60f)
            {
                if (NPC.ai[0] != 9f)
                {
                    NPC.ai[0] += 1f;
                }
                makedSword = false;
                NPC.localAI[2] += 1f;
                Vector2 targetPos = player.Center;
                targetPos.X -= 300f * NPC.ai[2];
                NPC.velocity = (targetPos - NPC.Center) / 20f;
                NPC.ai[1] = 0f;
                NPC.netUpdate = true;
            }
            NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.localAI[1]));
            void MakeSword(Vector2 pos, float spacing, float rotation = 0f)
            {
                Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center + pos, Vector2.Zero, ModContent.ProjectileType<MutantSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, (float)NPC.whoAmI, spacing);
            }
        }

        private void MutantSword()
        {
            if (NPC.ai[0] == 9f && Main.LocalPlayer.active && NPC.Distance(Main.LocalPlayer.Center) < 3000f && Main.expertMode)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<PurgedBuff>(), 2);
            }
            NPC.ai[3] += NPC.ai[2];
            NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.localAI[1]));
            if (NPC.ai[1] == 15f)
            {
                if (!Main.dedServ && Main.LocalPlayer.active)
                {
                    Main.LocalPlayer.GetModPlayer<CSEPlayer>().Screenshake = 30;
                }
                if ((WorldSavingSystem.EternityMode && NPC.ai[0] != 9f) || WorldSavingSystem.MasochistModeReal)
                {
                    if (!Main.dedServ)
                    {
                        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Thunder");
                        soundStyle.Pitch = -0.5f;
                        SoundEngine.PlaySound(soundStyle, (Vector2?)NPC.Center);
                    }
                    Vector2 offset = Math.Sign(NPC.localAI[1]) * Utils.RotatedBy(radians: (float)Math.PI / 4f * (float)Math.Sign(NPC.ai[2]), spinningpoint: Vector2.UnitX);
                    Vector2 spawnPos = NPC.Center + 480f * offset;
                    Vector2 baseDirection = player.DirectionFrom(spawnPos);
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 angle = baseDirection.RotatedBy((float)Math.PI / 4f * (float)i);
                        float ai1 = ((i <= 2 || i == 6) ? 48 : 24);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), spawnPos + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Vector2.Zero, ModContent.ProjectileType<MoonLordMoonBlast>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, MathHelper.WrapAngle(angle.ToRotation()), ai1).extraUpdates = 1;
                        }
                    }
                }
            }
            if (!((NPC.ai[1] += 1f) > 18f))
            {
                return;
            }
            if (NPC.ai[0] == 9f)
            {
                P1NextAttackOrMasoOptions(NPC.ai[0]);
            }
            else if (WorldSavingSystem.MasochistModeReal && NPC.localAI[2] < 5f * endTimeVariance + 2f)
            {
                if (Main.rand.NextBool(2))
                {
                    makedSword = true;
                    float leng = MathHelper.Clamp((player.Center - NPC.Center).Length(), 460f, 600f);
                    for (int i = 0; i < 77; i++)
                    {
                        Dust.NewDust(NPC.Center + Main.rand.NextVector2Circular(450f, 450f), 0, 0, 259, 0f, 0f, 0, default(Color), 1.2f);
                    }
                    swordTarget = player.Center + (player.Center - NPC.Center).SafeNormalize(Vector2.Zero) * 2.48f * leng;
                    NPC.Center = player.Center + (player.Center - NPC.Center).SafeNormalize(Vector2.Zero) * leng;
                    for (int i = 0; i < 77; i++)
                    {
                        Dust.NewDust(NPC.Center + Main.rand.NextVector2Circular(450f, 450f), 0, 0, 173, 0f, 0f, 0, default(Color), 1.2f);
                    }
                }
                else
                {
                    makedSword = false;
                }
                NPC.ai[0] -= 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                NPC.localAI[1] = 0f;
                NPC.netUpdate = true;
            }
            else
            {
                ChooseNextAttack(13, 21, 24, 29, 31, 33, 37, 41, 42, 44);
            }
        }


        #endregion

        #region p2

        void Phase2Transition()
        {
            NPC.velocity *= 0.9f;
            NPC.dontTakeDamage = true;

            if (NPC.buffType[0] != 0)
                NPC.DelBuff(0);

            EModeSpecialEffects();

            if (NPC.ai[2] == 0)
            {
                if (NPC.ai[1] < 60 && !Main.dedServ && Main.LocalPlayer.active)
                    FargoSoulsUtil.ScreenshakeRumble(6);
            }
            else
            {
                NPC.velocity = Vector2.Zero;
            }

            if (NPC.ai[1] < 240)
            {
                //make you stop attacking
                if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && NPC.Distance(Main.LocalPlayer.Center) < 3000)
                {
                    Main.LocalPlayer.controlUseItem = false;
                    Main.LocalPlayer.controlUseTile = false;
                    Main.LocalPlayer.FargoSouls().NoUsingItems = 2;
                }
            }

            if (NPC.ai[1] == 0)
            {
                FargoSoulsUtil.ClearAllProjectiles(2, NPC.whoAmI);

                if (WorldSavingSystem.EternityMode)
                {
                    DramaticTransition(false, NPC.ai[2] == 0);

                    if (FargoSoulsUtil.HostCheck)
                    {
                        ritualProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, 0f, NPC.whoAmI);

                        //maso rings
                        //Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual2>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        //Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual3>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        //Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual4>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }
                }
            }
            else if (NPC.ai[1] == 150)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                if (FargoSoulsUtil.HostCheck)
                {
                    //Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.GlowRingHollow>(), 0, 0f, Main.myPlayer, 5);
                    //Projectile.NewProjectile(npc.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.GlowRing>(), 0, 0f, Main.myPlayer, NPC.whoAmI, -22);
                }

                for (int i = 0; i < 50; i++)
                {
                    int d = Dust.NewDust(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height, DustID.Vortex, 0f, 0f, 0, default, 2.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 9f;
                }
                if (player.FargoSouls().TerrariaSoul)
                    EdgyBossText(GFBQuote(1));
            }
            else if (NPC.ai[1] > 150)
            {
                NPC.localAI[3] = 3;
            }

            if (++NPC.ai[1] > 270)
            {
                NPC.life = NPC.lifeMax;
                AttackChoice = Main.rand.Next(new int[] { 11, 13, 16, 19, 20, 21, 24, 26, 29, 35, 37, 39, 42, 47, 49}); 
                NPC.ai[2] = 0;
                NPC.netUpdate = true;

                attackHistory.Enqueue(AttackChoice);
            }
        }

        private void ApproachForNextAttackP2()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            Vector2 targetPos = player.Center + player.DirectionTo(NPC.Center) * 300f;
            if (NPC.Distance(targetPos) > 50f && (NPC.ai[2] += 1f) < 180f)
            {
                Movement(targetPos, 0.8f);
                return;
            }
            NPC.netUpdate = true;
            NPC.ai[0] += 1f;
            NPC.ai[1] = 0f;
            NPC.ai[2] = player.DirectionTo(NPC.Center).ToRotation();
            NPC.ai[3] = (float)Math.PI / 10f;
            NPC.localAI[0] = 0f;
            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            if (player.Center.X < NPC.Center.X)
            {
                NPC.ai[3] *= -1f;
            }
        }

        private void VoidRaysP2()
        {
            NPC.velocity = Vector2.Zero;
            if (!((NPC.ai[1] -= 1f) < 0f))
            {
                return;
            }
            if (Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(2f, 0f).RotatedBy(NPC.ai[2]), ModContent.ProjectileType<MutantMark1>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
            }
            NPC.ai[1] = 3f;
            NPC.ai[2] += NPC.ai[3];
            if (NPC.localAI[0]++ == 20f || NPC.localAI[0] == 40f)
            {
                NPC.netUpdate = true;
                NPC.ai[2] -= NPC.ai[3] / (float)3;
                if ((NPC.localAI[0] == 21f && endTimeVariance > 0.75f) || (NPC.localAI[0] == 41f && endTimeVariance < 0.25f))
                {
                    NPC.localAI[0] = 60f;
                }
            }
            else if (NPC.localAI[0] >= 60f)
            {
                ChooseNextAttack(13, 19, 21, 24, 31, 39, 41, 42, 47, 49);
            }
        }

        private void PrepareSpearDashPredictiveP2()
        {
            if (NPC.ai[3] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.ai[3] = 1f;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearSpin>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 180f);
                }
            }
            if ((NPC.ai[1] += 1f) > 180f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.netUpdate = true;
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                NPC.ai[3] = 0f;
            }
            Vector2 targetPos = player.Center;
            targetPos.Y += 400f * (float)Math.Sign(NPC.Center.Y - player.Center.Y);
            Movement(targetPos, 0.7f, fastX: false);
            if (NPC.Distance(player.Center) < 200f)
            {
                Movement(NPC.Center + NPC.DirectionFrom(player.Center), 1.4f);
            }
        }

        private void SpearDashPredictiveP2()
        {
            if (NPC.localAI[1] == 0f)
            {
                NPC.localAI[1] = Main.rand.Next(3, 9);
            }
            if (NPC.ai[1] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                if (NPC.ai[2] == NPC.localAI[1] - 1f)
                {
                    if (NPC.Distance(player.Center) > 450f)
                    {
                        Movement(player.Center, 0.6f);
                        return;
                    }
                    NPC.velocity *= 0.75f;
                }
                if (NPC.ai[2] < NPC.localAI[1])
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(player.Center + player.velocity * 30f), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0f, Main.myPlayer, 55f, (float)NPC.whoAmI);
                    }
                    if (NPC.ai[2] == NPC.localAI[1] - 1f)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 4f);
                        }
                    }
                }
            }
            NPC.velocity *= 0.9f;
            if (NPC.ai[1] < 55f)
            {
                NPC.localAI[0] = NPC.DirectionTo(player.Center + player.velocity * 30f).ToRotation();
            }
            int endTime = 60;
            if (NPC.ai[2] == NPC.localAI[1] - 1f)
            {
                endTime = 80;
            }
            if (NPC.ai[2] == 0f || NPC.ai[2] >= NPC.localAI[1])
            {
                endTime = 0;
            }
            if (!((NPC.ai[1] += 1f) > (float)endTime))
            {
                return;
            }
            NPC.netUpdate = true;
            NPC.ai[0] += 1f;
            NPC.ai[1] = 0f;
            NPC.ai[3] = 0f;
            if ((NPC.ai[2] += 1f) > NPC.localAI[1])
            {
                ChooseNextAttack(16, 19, 20, 26, 29, 31, 33, 39, 42, 44, 45);
            }
            else
            {
                NPC.velocity = NPC.localAI[0].ToRotationVector2() * 45f;
                float spearAi = 0f;
                if (NPC.ai[2] == NPC.localAI[1])
                {
                    spearAi = -2f;
                }
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearDash>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, spearAi);
                }
            }
            NPC.localAI[0] = 0f;
        }

        private void WhileDashingP2()
        {
            NPC.direction = (NPC.spriteDirection = Math.Sign(NPC.velocity.X));
            if ((NPC.ai[1] += 1f) > 30f && AliveCheck(player))
            {
                NPC.netUpdate = true;
                NPC.ai[0] -= 1f;
                NPC.ai[1] = 0f;
                if (NPC.ai[0] == 14f && NPC.ai[2] == NPC.localAI[1] - 1f && NPC.Distance(player.Center) > 450f)
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * 16f;
                }
            }
        }

        private void BoundaryBulletHellP2()
        {
            NPC.velocity = Vector2.Zero;
            if (NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = Math.Sign(NPC.Center.X - player.Center.X);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, (float)NPC.whoAmI, -2f);
                }
            }
            if (NPC.ai[3] > 60f && (NPC.ai[1] += 1f) > 2f)
            {
                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                NPC.ai[1] = 0f;
                NPC.ai[2] += 0.0008181231f * NPC.ai[3] * NPC.localAI[0];
                if (NPC.ai[2] > (float)Math.PI)
                {
                    NPC.ai[2] -= (float)Math.PI * 2f;
                }
                if (Main.netMode != 1)
                {
                    int max = 6;
                    for (int i = 0; i < max; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -6f).RotatedBy((double)NPC.ai[2] + Math.PI * 2.0 / (double)max * (double)i), ModContent.ProjectileType<MutantEye>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            int endTime = 420 + (int)(480f * (endTimeVariance - 0.33f));
            if ((NPC.ai[3] += 1f) > (float)endTime)
            {
                int[] obj = new int[10] { 11, 13, 19, 20, 21, 24, 0, 33, 41, 44 };
                obj[6] = 31;
                ChooseNextAttack(obj);
            }
        }

        private void PillarDunk()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            int pillarAttackDelay = 60;
            if (NPC.ai[2] == 0f && NPC.ai[3] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != 1)
                {
                    Clone(-1f, 1f, pillarAttackDelay * 4);
                    Clone(1f, -1f, pillarAttackDelay * 2);
                    Clone(1f, 1f, pillarAttackDelay * 3);
                    Clone(1f, 1f, pillarAttackDelay * 6);
                }
                NPC.netUpdate = true;
                NPC.ai[2] = NPC.Center.X;
                NPC.ai[3] = NPC.Center.Y;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>() && Main.projectile[i].ai[1] == (float)NPC.whoAmI)
                    {
                        NPC.ai[2] = Main.projectile[i].Center.X;
                        NPC.ai[3] = Main.projectile[i].Center.Y;
                        break;
                    }
                }
                Vector2 offset = 1000f * Vector2.UnitX.RotatedBy(MathHelper.ToRadians(45f));
                if (Main.rand.NextBool())
                {
                    if (player.Center.X > NPC.ai[2])
                    {
                        offset.X *= -1f;
                    }
                    if (Main.rand.NextBool())
                    {
                        offset.Y *= -1f;
                    }
                }
                else
                {
                    if (Main.rand.NextBool())
                    {
                        offset.X *= -1f;
                    }
                    if (player.Center.Y > NPC.ai[3])
                    {
                        offset.Y *= -1f;
                    }
                }
                NPC.localAI[1] = NPC.ai[2];
                NPC.localAI[2] = NPC.ai[3];
                NPC.ai[2] = offset.Length();
                NPC.ai[3] = offset.ToRotation();
            }
            Vector2 targetPos = player.Center;
            targetPos.X += ((NPC.Center.X < player.Center.X) ? (-700) : 700);
            targetPos.Y += ((NPC.ai[1] < 240f) ? 400 : 150);
            if (NPC.Distance(targetPos) > 50f)
            {
                Movement(targetPos, 1f);
            }
            int endTime = 240 + pillarAttackDelay * 4 + 60;
            endTime += pillarAttackDelay * 2;
            NPC.localAI[0] = (float)endTime - NPC.ai[1];
            NPC.localAI[0] += 60f + 60f * (1f - NPC.ai[1] / (float)endTime);
            if (NPC.ai[1] == 95f || NPC.ai[1] == 135f || NPC.ai[1] == (float)(endTime - 30))
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 dir = player.Center - NPC.Center;
                    float ai1New = (Main.rand.NextBool() ? 1 : (-1));
                    Vector2 vel = Vector2.Normalize(dir.RotatedByRandom(Math.PI / 4.0)) * 38f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<HostileLightning>(), 30, 0f, Main.myPlayer, dir.ToRotation(), ai1New);
                }
            }
            if ((NPC.ai[1] += 1f) > (float)endTime)
            {
                ChooseNextAttack(11, 13, 20, 21, 26, 33, 41, 44, 47, 49);
            }
            else if (NPC.ai[1] == (float)pillarAttackDelay)
            {
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -5f, ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, 3f, (float)NPC.whoAmI);
                }
            }
            else if (NPC.ai[1] == (float)(pillarAttackDelay * 5) && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -5f, ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, 1f, (float)NPC.whoAmI);
            }
            void Clone(float ai1, float ai2, float ai3)
            {
                FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromAI(), NPC.Center, ModContent.NPCType<MutantIllusion>(), NPC.whoAmI, NPC.whoAmI, ai1, ai2, ai3);
            }
        }

        private void EOCStarSickles()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            if (NPC.ai[1] == 0f)
            {
                float ai1 = 30f;
                NPC.ai[1] = 30f;
                if (Main.netMode != 1)
                {
                    int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY, ModContent.ProjectileType<MutantEyeOfCthulhu>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.target, ai1);
                    if (p != 1000)
                    {
                        Main.projectile[p].timeLeft -= 30;
                    }
                }
            }
            if (NPC.ai[1] < 120f)
            {
                NPC.ai[2] = player.Center.X;
                NPC.ai[3] = player.Center.Y;
            }
            if (NPC.ai[1] == 120f || NPC.ai[1] == 156f)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                Vector2 offset = NPC.Center - player.Center;
                Vector2 spawnPos = player.Center;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        LaserSpread(new Vector2(spawnPos.X + offset.X, spawnPos.Y + offset.Y));
                        LaserSpread(new Vector2(spawnPos.X + offset.X, spawnPos.Y - offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X + offset.X, spawnPos.Y - offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X + offset.X, spawnPos.Y + offset.Y));
                        break;
                    case 1:
                        LaserSpread(new Vector2(spawnPos.X + offset.X, spawnPos.Y - offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X + offset.X, spawnPos.Y - offset.Y));
                        LaserSpread(new Vector2(spawnPos.X - offset.X, spawnPos.Y + offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X - offset.X, spawnPos.Y + offset.Y));
                        break;
                    case 2:
                        LaserSpread(new Vector2(spawnPos.X - offset.X, spawnPos.Y + offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X - offset.X, spawnPos.Y + offset.Y));
                        LaserSpread(new Vector2(spawnPos.X - offset.X, spawnPos.Y - offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X - offset.X, spawnPos.Y - offset.Y));
                        break;
                    case 3:
                        LaserSpread(new Vector2(spawnPos.X - offset.X, spawnPos.Y - offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X - offset.X, spawnPos.Y - offset.Y));
                        LaserSpread(new Vector2(spawnPos.X + offset.X, spawnPos.Y + offset.Y));
                        TelegraphConfusion(new Vector2(spawnPos.X + offset.X, spawnPos.Y + offset.Y));
                        break;
                }
            }
            Vector2 targetPos = new Vector2(NPC.ai[2], NPC.ai[3]);
            targetPos += NPC.DirectionFrom(targetPos).RotatedBy(MathHelper.ToRadians(-5f)) * 450f;
            if (NPC.Distance(targetPos) > 50f)
            {
                Movement(targetPos, 0.25f);
            }
            if ((NPC.ai[1] += 1f) > 450f)
            {
                ChooseNextAttack(11, 13, 16, 21, 26, 29, 31, 33, 35, 37, 41, 44, 45, 47, 49);
            }
            void LaserSpread(Vector2 spawn)
            {
                int max = 3;
                int degree = 1;
                int laserDamage = FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, new Vector2(0f, -4f), ModContent.ProjectileType<BrainofConfusion>(), 0, 0f, Main.myPlayer, 0f, 0f);
                for (int i = -max; i <= max; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, 0.2f * player.DirectionFrom(spawn).RotatedBy(MathHelper.ToRadians(degree) * (float)i), ModContent.ProjectileType<DestroyerLaser>(), laserDamage, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            void TelegraphConfusion(Vector2 spawn)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0f, Main.myPlayer, 8f, 180f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0f, Main.myPlayer, 8f, 200f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0f, Main.myPlayer, 8f, 220f);
            }
        }
        
        private void PrepareSpearDashDirectP2()
        {
            if (NPC.ai[3] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.ai[3] = 1f;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearSpin>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 180f);
                }
            }
            if ((NPC.ai[1] += 1f) > 180f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.netUpdate = true;
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                NPC.ai[3] = 0f;
            }
            Vector2 targetPos = player.Center;
            targetPos.Y += 450f * (float)Math.Sign(NPC.Center.Y - player.Center.Y);
            Movement(targetPos, 0.7f, fastX: false);
            if (NPC.Distance(player.Center) < 200f)
            {
                Movement(NPC.Center + NPC.DirectionFrom(player.Center), 1.4f);
            }
        }

        private void SpearDashDirectP2()
        {
            NPC.velocity *= 0.9f;
            if (NPC.localAI[1] == 0f)
            {
                NPC.localAI[1] = Main.rand.Next(3, 9);
            }
            if (!((NPC.ai[1] += 1f) > (float)5))
            {
                return;
            }
            NPC.netUpdate = true;
            NPC.ai[0] += 1f;
            NPC.ai[1] = 0f;
            if ((NPC.ai[2] += 1f) > NPC.localAI[1])
            {
                ChooseNextAttack(11, 13, 16, 19, 20, 31, 33, 35, 39, 42, 44);
            }
            else
            {
                NPC.velocity = NPC.DirectionTo(player.Center) * (60f);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearDash>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 0f);
                }
            }
        }
       
        private void SpawnDestroyersForPredictiveThrow()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 500f;
            if (Math.Abs(targetPos.X - player.Center.X) < 150f)
            {
                targetPos.X = player.Center.X + (float)(150 * Math.Sign(targetPos.X - player.Center.X));
                Movement(targetPos, 0.3f);
            }
            if (NPC.Distance(targetPos) > 50f)
            {
                Movement(targetPos, 0.9f);
            }
            if (NPC.localAI[1] == 0f)
            {
                NPC.localAI[1] = Main.rand.Next(3, 9);
            }
            if (!((NPC.ai[1] += 1f) > 60f))
            {
                return;
            }
            NPC.netUpdate = true;
            NPC.ai[1] = 30f;
            int cap = 7;
            NPC.ai[1] += 15f;

            if ((NPC.ai[2] += 1f) > (float)cap)
            {
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                return;
            }
            SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
            if (Main.netMode != 1)
            {
                Vector2 vel = NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.ToRadians(120f)) * 10f;
                float ai1 = 0.8f + 0.4f * NPC.ai[2] / 5f;
                ai1 += 0.4f;
                int current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerHead>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.target, ai1);
                Main.projectile[current].timeLeft = 30 * (cap - (int)NPC.ai[2]) + 60 * (int)NPC.localAI[1] + 30 + (int)NPC.ai[2] * 6;
                int max = Main.rand.Next(8, 19);
                for (int i = 0; i < max; i++)
                {
                    current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerBody>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)Main.projectile[current].identity, 0f);
                }
                int previous = current;
                current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerTail>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)Main.projectile[current].identity, 0f);
                Main.projectile[previous].localAI[1] = Main.projectile[current].identity;
                Main.projectile[previous].netUpdate = true;
            }
        }

        private void SpearTossPredictiveP2()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            Vector2 targetPos = player.Center;
            targetPos.X += 500 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
            if (NPC.Distance(targetPos) > 25f)
            {
                Movement(targetPos, 0.8f);
            }
            if ((NPC.ai[1] += 1f) > 60f)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 0f;
                bool shouldAttack = true;
                if ((NPC.ai[2] += 1f) > NPC.localAI[1])
                {
                    shouldAttack = false;
                    ChooseNextAttack(11, 19, 20, 29, 31, 33, 35, 37, 39, 42, 44, 45);
                }
                if (Main.netMode != 1)
                {
                    Vector2 vel = NPC.DirectionTo(player.Center + player.velocity * 30f) * 30f;
                    for (int i = -1; i <= 1; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel).RotatedBy((float)i * 0.12f), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel).RotatedBy((float)i * 0.12f), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel.RotatedBy((float)i * 0.12f), ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.target, 1f);
                    }
                }
            }
            else if (Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(player.Center + player.velocity * 30f), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0f, Main.myPlayer, 60f, (float)NPC.whoAmI);
                for (int i = -1; i <= 1; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 2f);
                }
            }
        }
        
        private void PrepareMechRayFan()
        {
            if (NPC.ai[1] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.ai[1] = 31f;
            }
            if (NPC.ai[1] == 30f)
            {
                SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, (float)NPC.whoAmI, 125f);
                }
            }
            Vector2 targetPos;
            if (NPC.ai[1] < 30f)
            {
                targetPos = player.Center + NPC.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(15f)) * 500f;
                if (NPC.Distance(targetPos) > 50f)
                {
                    Movement(targetPos, 0.3f);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(NPC.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 3f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 12f;
                }
                targetPos = player.Center;
                targetPos.X += 600 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                Movement(targetPos, 1.2f, fastX: false);
            }
            if ((NPC.ai[1] += 1f) > 150f || NPC.Distance(targetPos) < 64f)
            {
                NPC.netUpdate = true;
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }

        private void MechRayFan()
        {
            NPC.velocity = Vector2.Zero;
            if (NPC.ai[2] == 0f)
            {
                NPC.ai[2] = ((!Main.rand.NextBool()) ? 1 : (-1));
            }
            if (NPC.ai[3] == 0f && Main.netMode != 1)
            {
                int max = 7;
                for (int i = 0; i <= max; i++)
                {
                    Vector2 dir = Vector2.UnitX.RotatedBy(NPC.ai[2] * (float)i * (float)Math.PI / (float)max) * 6f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + dir, Vector2.Zero, ModContent.ProjectileType<MutantGlowything>(), 0, 0f, Main.myPlayer, dir.ToRotation(), (float)NPC.whoAmI);
                }
            }
            int endTime = 390;
            int timeBeforeAttackEnds;
            if (NPC.ai[3] > (float)(45) && NPC.ai[3] < 240f && (NPC.ai[1] += 1f) > 10f)
            {
                NPC.ai[1] = 0f;
                if (Main.netMode != 1)
                {
                    float rotation = MathHelper.ToRadians(245f) * NPC.ai[2] / 80f;
                    timeBeforeAttackEnds = endTime - (int)NPC.ai[3];
                    SpawnRay(NPC.Center, 8f * NPC.ai[2], rotation);
                    SpawnRay(NPC.Center, -8f * NPC.ai[2] + 180f, 0f - rotation);
                    Vector2 spawnPos = NPC.Center + NPC.ai[2] * -1200f * Vector2.UnitY;
                    SpawnRay(spawnPos, 8f * NPC.ai[2] + 180f, rotation);
                    SpawnRay(spawnPos, -8f * NPC.ai[2], 0f - rotation);
                }
            }
            if (NPC.ai[3] > 210f && Main.netMode != 1)
            {
                float spawnOffset = (float)((!Main.rand.NextBool()) ? 1 : (-1)) * Main.rand.NextFloat(1400f, 1800f);
                float maxVariance = MathHelper.ToRadians(16f);
                Vector2 aimPoint = NPC.Center - Vector2.UnitY * NPC.ai[2] * 600f;
                Vector2 spawnPos = aimPoint + spawnOffset * Vector2.UnitX.RotatedByRandom(maxVariance).RotatedBy(MathHelper.ToRadians(0f));
                Vector2 vel = 32f * Vector2.Normalize(aimPoint - spawnPos);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.ProjectileType<MutantGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, 0f, 0f);
            }
            if (NPC.ai[3] < 180f && (NPC.localAI[0] += 1f) > 1f)
            {
                NPC.localAI[0] = 0f;
                SpawnPrime(15f, 0f);
            }
            if ((NPC.ai[3] += 1f) > (float)endTime)
            {
                ChooseNextAttack(11, 13, 16, 19, 21, 24, 29, 31, 33, 35, 37, 39, 41, 42, 45, 47, 49);
                NPC.netUpdate = true;
            }
            void SpawnPrime(float varianceInDegrees, float rotationInDegrees)
            {
                SoundEngine.PlaySound(SoundID.Item21, NPC.Center);
                if (Main.netMode != 1)
                {
                    float spawnOffset = (float)((!Main.rand.NextBool()) ? 1 : (-1)) * Main.rand.NextFloat(1400f, 1800f);
                    float maxVariance = MathHelper.ToRadians(varianceInDegrees);
                    Vector2 aimPoint = NPC.Center - Vector2.UnitY * NPC.ai[2] * 600f;
                    Vector2 spawnPos = aimPoint + spawnOffset * Vector2.UnitY.RotatedByRandom(maxVariance).RotatedBy(MathHelper.ToRadians(rotationInDegrees));
                    Vector2 vel = 32f * Vector2.Normalize(aimPoint - spawnPos);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.ProjectileType<MutantGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, 0f, 0f);
                }
            }
            void SpawnRay(Vector2 pos, float angleInDegrees, float turnRotation)
            {
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), pos, MathHelper.ToRadians(angleInDegrees).ToRotationVector2(), ModContent.ProjectileType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantDeathray3>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, turnRotation, (float)NPC.whoAmI);
                if (p != 1000 && Main.projectile[p].timeLeft > timeBeforeAttackEnds)
                {
                    Main.projectile[p].timeLeft = timeBeforeAttackEnds;
                }
            }
        }
        
        private void PrepareFishron1()
        {
            if (AliveCheck(player))
            {
                Vector2 targetPos = new Vector2(player.Center.X, player.Center.Y + (float)(600 * Math.Sign(NPC.Center.Y - player.Center.Y)));
                Movement(targetPos, 1.4f, fastX: false);
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[2] = Math.Sign(NPC.Center.X - player.Center.X);
                }
                if ((NPC.ai[1] += 1f) > 60f || NPC.Distance(targetPos) < 64f)
                {
                    NPC.velocity.X = 30f * NPC.ai[2];
                    NPC.velocity.Y = 0f;
                    NPC.ai[0] += 1f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
            }
        }

        private void SpawnFishrons()
        {
            NPC.velocity *= 0.97f;
            if (NPC.ai[1] == 0f)
            {
                NPC.ai[2] = (Main.rand.NextBool() ? 1 : 0);
            }
            int maxFishronSets = 3;
            if (NPC.ai[1] % 3f == 0f && NPC.ai[1] <= (float)(3 * maxFishronSets))
            {
                if (Main.netMode != 1)
                {
                    for (int j = -1; j <= 1; j += 2)
                    {
                        int max = (int)NPC.ai[1] / 3;
                        for (int i = -max; i <= max; i++)
                        {
                            if (Math.Abs(i) == max)
                            {
                                float spread = 0.5711987f / (float)(maxFishronSets + 1);
                                Vector2 offset = ((NPC.ai[2] == 0f) ? (Vector2.UnitY.RotatedBy(spread * (float)i) * -450f * j) : (Vector2.UnitX.RotatedBy(spread * (float)i) * 475f * j));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantFishron>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                            }
                        }
                        for (int i = -max; i <= max; i++)
                        {
                            if (Math.Abs(i) == max)
                            {
                                float spread = (float)Math.PI / 36f / (float)(maxFishronSets + 1);
                                Vector2 offset = ((NPC.ai[2] == 0f) ? (Vector2.UnitX.RotatedBy(spread * (float)i) * -450f * j) : (Vector2.UnitY.RotatedBy(spread * (float)i) * 615f * j));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantFishron>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                            }
                        }
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 135, 0f, 0f, 0, default(Color), 3f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 12f;
                }
            }
            if ((NPC.ai[1] += 1f) > (float)(60))
            {
                int[] obj = new int[14]
                {
                13, 19, 20, 21, 0, 31, 31, 31, 33, 35,
                39, 41, 42, 44
                };
                obj[4] = 44;
                ChooseNextAttack(obj);
            }
        }

        private void PrepareTrueEyeDiveP2()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            Vector2 targetPos = player.Center;
            targetPos.X += 400 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
            targetPos.Y += 400f;
            Movement(targetPos, 1.2f);
            if ((NPC.ai[1] += 1f) > 60f)
            {
                NPC.velocity.X = 30f * (float)((NPC.position.X < player.position.X) ? 1 : (-1));
                if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y *= -1f;
                }
                NPC.velocity.Y *= 0.3f;
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                NPC.netUpdate = true;
            }
        }

        private void PrepareNuke()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            Vector2 targetPos = player.Center;
            targetPos.X += 400 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
            targetPos.Y -= 400f;
            Movement(targetPos, 1.2f, fastX: false);
            if ((NPC.ai[1] += 1f) > 60f)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != 1)
                {
                    float gravity = 0.2f;
                    float time = 120f;
                    Vector2 distance = player.Center - NPC.Center;
                    distance.X /= time;
                    distance.Y = distance.Y / time - 0.5f * gravity * time;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, distance, ModContent.ProjectileType<MutantNuke>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, gravity, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantFishronRitual>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, (float)NPC.whoAmI, 0f);
                }
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                if (Math.Sign(player.Center.X - NPC.Center.X) == Math.Sign(NPC.velocity.X))
                {
                    NPC.velocity.X *= -1f;
                }
                if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y *= -1f;
                }
                NPC.velocity.Normalize();
                NPC.velocity *= 3f;
                NPC.netUpdate = true;
            }
        }

        private void Nuke()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            Vector2 target = ((NPC.Bottom.Y < player.Top.Y) ? (player.Center + 300f * Vector2.UnitX * Math.Sign(NPC.Center.X - player.Center.X)) : (NPC.Center + 30f * NPC.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(60f) * (float)Math.Sign(player.Center.X - NPC.Center.X))));
            Movement(target, 0.1f);
            if (NPC.velocity.Length() > 2f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 2f;
            }
            if (NPC.ai[1] > (float)(120))
            {
                if (!Main.dedServ && Main.LocalPlayer.active)
                {
                    Main.LocalPlayer.GetModPlayer<CSEPlayer>().Screenshake = 2;
                }
                if (Main.netMode != 1)
                {
                    Vector2 safeZone = NPC.Center;
                    safeZone.Y -= 100f;
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 spawnPos = NPC.Center + Main.rand.NextVector2Circular(1200f, 1200f);
                        if (Vector2.Distance(safeZone, spawnPos) < 350f)
                        {
                            Vector2 directionOut = spawnPos - safeZone;
                            directionOut.Normalize();
                            spawnPos = safeZone + directionOut * Main.rand.NextFloat(350f, 1200f);
                        }
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<MutantBomb>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 1.3333334f), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if ((NPC.ai[1] += 1f) > 360f + 360f * endTimeVariance)
            {
                int[] obj = new int[12]
                {
                11, 13, 16, 19, 24, 0, 31, 35, 37, 39,
                41, 42
                };
                obj[5] = 26;
                ChooseNextAttack(obj);
            }
            if (!(NPC.ai[1] > 45f))
            {
                return;
            }
            for (int i = 0; i < 20; i++)
            {
                Vector2 offset = default(Vector2);
                offset.Y -= 100f;
                double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
                offset.X += (float)(Math.Sin(angle) * 150.0);
                offset.Y += (float)(Math.Cos(angle) * 150.0);
                Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset - new Vector2(4f, 4f), 0, 0, 60, 0f, 0f, 100, Color.White, 1.5f)];
                dust.velocity = NPC.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(offset) * 5f;
                }
                dust.noGravity = true;
            }
        }

        private void PrepareSlimeRain()
        {
            if (AliveCheck(player))
            {
                Vector2 targetPos = player.Center;
                targetPos.X += 700 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                targetPos.Y += 200f;
                Movement(targetPos, 2f);
                if ((NPC.ai[2] += 1f) > 30f || NPC.Distance(targetPos) < 64f)
                {
                    NPC.ai[0] += 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
            }
        }

        private void SlimeRain()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSlimeRain>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 0f);
                }
            }
            if (NPC.ai[1] == 0f)
            {
                bool num = NPC.localAI[0] == 0f;
                NPC.localAI[0] = Main.rand.Next(5, 9) * 120;
                if (num)
                {
                    if (player.Center.X < NPC.Center.X && NPC.localAI[0] > 1200f)
                    {
                        NPC.localAI[0] -= 1200f;
                    }
                    else if (player.Center.X > NPC.Center.X && NPC.localAI[0] < 1200f)
                    {
                        NPC.localAI[0] += 1200f;
                    }
                }
                else if (player.Center.X < NPC.Center.X && NPC.localAI[0] < 1200f)
                {
                    NPC.localAI[0] += 1200f;
                }
                else if (player.Center.X > NPC.Center.X && NPC.localAI[0] > 1200f)
                {
                    NPC.localAI[0] -= 1200f;
                }
                NPC.localAI[0] += 60f;
                Vector2 basePos = NPC.Center;
                basePos.X -= 1200f;
                for (int i = -360; i <= 2760; i += 120)
                {
                    if (Main.netMode != 1 && i + 60 != (int)NPC.localAI[0])
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), basePos.X + (float)i + 60f, basePos.Y, 0f, 0f, ModContent.ProjectileType<MutantReticle>(), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                NPC.ai[1] += 20f;
                NPC.ai[2] += 20f;
            }
            if (NPC.ai[1] > 120f && NPC.ai[1] % 5f == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item34, NPC.Center);
                if (Main.netMode != 1)
                {
                    Vector2 basePos = NPC.Center;
                    basePos.X -= 1200f;
                    float yOffset = -1300f;
                    for (int i = -360; i <= 2760; i += 75)
                    {
                        float xOffset = i + Main.rand.Next(75);
                        if (!(Math.Abs(xOffset - NPC.localAI[0]) < 110f))
                        {
                            Vector2 spawnPos = basePos;
                            spawnPos.X += xOffset;
                            Vector2 velocity = Vector2.UnitY * Main.rand.NextFloat(15f, 20f);
                            Slime(spawnPos, yOffset, velocity);
                        }
                    }
                    Slime(basePos + Vector2.UnitX * (NPC.localAI[0] + 110f), yOffset, Vector2.UnitY * 20f);
                    Slime(basePos + Vector2.UnitX * (NPC.localAI[0] - 110f), yOffset, Vector2.UnitY * 20f);
                }
            }
            if ((NPC.ai[1] += 1f) > 180f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.ai[1] = 0f;
            }
            if (NPC.ai[1] == 120f && NPC.ai[2] < 480f && Main.rand.NextBool(3))
            {
                NPC.ai[2] = 480f;
            }
            NPC.velocity = Vector2.Zero;
            if (NPC.ai[2] == 480f)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
            int endTime = 540;
            endTime += 240 + (int)(120f * endTimeVariance) - 50;
            if ((NPC.ai[2] += 1f) > (float)endTime)
            {
                int[] obj = new int[12]
                {
                11, 16, 19, 20, 0, 31, 33, 37, 39, 41,
                42, 45
                };
                obj[4] = 26;
                ChooseNextAttack(obj);
            }
            if (NPC.ai[2] > 510f)
            {
                if (NPC.ai[2] % 3f == 1f && NPC.ai[2] < (float)(endTime - 80))
                {
                    Vector2 range = player.Center + new Vector2(((float)Main.rand.Next(2) - 0.5f) * 2200f, 0f);
                    Vector2 vel = (player.Center - range).SafeNormalize(Vector2.Zero) * (15f + ((float)Main.rand.Next(2) - 0.5f) * 4f);
                    Projectile projectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), range + new Vector2(0f, ((float)Main.rand.Next(2) - 0.5f) * 12f), vel, ModContent.ProjectileType<MutantBigSting22>(), 50, 0f, 0, 256f, 0f);
                    projectile.hostile = true;
                    projectile.friendly = false;
                }
                if (NPC.ai[1] > 170f)
                {
                    NPC.ai[1] -= 30f;
                }
                if (NPC.localAI[1] == 0f)
                {
                    float safespotX = NPC.Center.X - 1200f + NPC.localAI[0];
                    NPC.localAI[1] = Math.Sign(NPC.Center.X - safespotX);
                }
                NPC.localAI[0] += 4.1666665f * NPC.localAI[1];
            }
            void Slime(Vector2 pos, float off, Vector2 vel)
            {
                int flip = ((!(NPC.ai[2] < 360f) || !Main.rand.NextBool()) ? 1 : (-1));
                Vector2 spawnPos = pos + off * Vector2.UnitY * flip;
                float ai0 = ((FargoSoulsUtil.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null) ? 0f : NPC.Distance(Main.projectile[ritualProj].Center));
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel * flip, ModContent.ProjectileType<MutantSlimeBall>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, 0f);
            }
        }
       
        private void QueenSlimeRain()
        {
            if (NPC.ai[3] == 0)
            {
                NPC.ai[3] = 1;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSlimeRain>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.whoAmI);
            }

            if (NPC.ai[1] == 0) 
            {
                NPC.localAI[0] = Main.rand.Next(6, 9) * 120;
                if (player.Center.X > NPC.Center.X)
                    NPC.localAI[0] += 600;
                NPC.localAI[0] += 60;

                Vector2 basePos = NPC.Center;
                basePos.X -= 1200;
                for (int i = -360; i <= 2760; i += 120) 
                {
                    if (FargoSoulsUtil.HostCheck)
                    {
                        if (i + 60 == (int)NPC.localAI[0])
                            continue;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), basePos.X + i + 60, basePos.Y, 0f, 0f, ModContent.ProjectileType<MutantReticle>(), 0, 0f, Main.myPlayer, ai2: 1);
                    }
                }
            }

            const int masoMovingRainAttackTime = 60;

            if (NPC.ai[1] > masoMovingRainAttackTime && NPC.ai[1] % 3 == 0) 
            {
                SoundEngine.PlaySound(SoundID.Item34, player.Center);
                if (FargoSoulsUtil.HostCheck)
                {
                    int frame = Main.rand.Next(3);

                    void Slime(Vector2 pos, float off, Vector2 vel)
                    {
                        const int flip = 1;
                        Vector2 spawnPos = pos + off * Vector2.UnitY * flip;
                        float ai0 = FargoSoulsUtil.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null ? 0f : NPC.Distance(Main.projectile[ritualProj].Center);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel * flip, ModContent.ProjectileType<MutantSlimeSpike>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, ai0, ai2: frame);
                    }

                    Vector2 basePos = NPC.Center;
                    basePos.X -= 1200;
                    float yOffset = -1300;

                    const int safeRange = 110;
                    const int spacing = safeRange;
                    for (int i = 0; i < 2400; i += spacing)
                    {
                        float rightOffset = NPC.localAI[0] + safeRange + i;
                        if (basePos.X + rightOffset < NPC.Center.X + 1200)
                            Slime(basePos + Vector2.UnitX * rightOffset, yOffset, Vector2.UnitY * 20f);
                        float leftOffset = NPC.localAI[0] - safeRange - i;
                        if (basePos.X + leftOffset > NPC.Center.X - 1200)
                            Slime(basePos + Vector2.UnitX * leftOffset, yOffset, Vector2.UnitY * 20f);
                    }
                }
            }

            NPC.velocity = Vector2.Zero;

            const int timeToMove = 360;
            if (NPC.ai[1] == masoMovingRainAttackTime)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                EdgyBossText(GFBQuote(21));
            }

            if (NPC.ai[1] > masoMovingRainAttackTime && --NPC.ai[2] < 0)
            {
                float safespotMoveSpeed = 7f;

                if (--NPC.localAI[2] < 0) 
                {
                    float safespotX = NPC.Center.X - 1200f + NPC.localAI[0];
                    NPC.localAI[1] = Math.Sign(NPC.Center.X - safespotX);

                    float farSideArenaBorder = NPC.Center.X + 1200f * NPC.localAI[1];
                    float distanceToBorder = Math.Abs(farSideArenaBorder - safespotX);
                    float minRequiredDistance = Math.Abs(NPC.Center.X - safespotX) + 100;

                    float distanceToTravel = MathHelper.Lerp(minRequiredDistance, distanceToBorder, Main.rand.NextFloat(0.6f));

                    NPC.localAI[2] = distanceToTravel / safespotMoveSpeed;
                    NPC.ai[2] = 15; 
                }

                NPC.localAI[0] += safespotMoveSpeed * NPC.localAI[1];
            }

            int endTime = masoMovingRainAttackTime + timeToMove + (int)(300 * endTimeVariance);
            if (++NPC.ai[1] > endTime)
            {
                ChooseNextAttack(11, 16, 19, 20, 26, 31, 33, 37, 39, 41, 42, 45);
            }
        }

        private void PrepareFishron2()
        {
            if (AliveCheck(player))
            {
                Vector2 targetPos = player.Center;
                targetPos.X += 400 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                targetPos.Y -= 400f;
                Movement(targetPos, 0.9f);
                if ((NPC.ai[1] += 1f) > 60f || NPC.Distance(targetPos) < 32f)
                {
                    NPC.velocity.X = 35f * (float)((NPC.position.X < player.position.X) ? 1 : (-1));
                    NPC.velocity.Y = 10f;
                    NPC.ai[0] += 1f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
            }
        }

        private void PrepareOkuuSpheresP2()
        {
            if (AliveCheck(player))
            {
                Vector2 targetPos = player.Center + player.DirectionTo(NPC.Center) * 450f;
                if ((NPC.ai[1] += 1f) < 180f && NPC.Distance(targetPos) > 50f)
                {
                    Movement(targetPos, 0.8f);
                    return;
                }
                NPC.netUpdate = true;
                NPC.ai[0] += 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
            }
        }

        private void OkuuSpheresP2()
        {
            NPC.velocity = Vector2.Zero;
            int endTime = 420 + (int)(360f * (endTimeVariance - 0.33f));
            if ((NPC.ai[1] += 1f) > 10f && NPC.ai[3] > 60f && NPC.ai[3] < (float)(endTime - 60))
            {
                NPC.ai[1] = 0f;
                float rotation = MathHelper.ToRadians(60f) * (NPC.ai[3] - 45f) / 240f * NPC.ai[2];
                int max = 10;
                float speed = 11f;
                SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), -1f, rotation);
                SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 1f, rotation);
            }
            if (NPC.ai[2] == 0f)
            {
                NPC.ai[2] = ((!Main.rand.NextBool()) ? 1 : (-1));
                NPC.ai[3] = Main.rand.NextFloat((float)Math.PI * 2f);
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, (float)NPC.whoAmI, -2f);
                }
            }
            if ((NPC.ai[3] += 1f) > (float)endTime)
            {
                int[] obj = new int[9] { 13, 19, 20, 0, 0, 41, 44, 47, 49};
                obj[3] = 13;
                obj[4] = 44;
                ChooseNextAttack(obj);
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
        }

        private void SpawnSpearTossDirectP2Attack()
        {
            if (FargoSoulsUtil.HostCheck)
            {
                Vector2 vel = NPC.SafeDirectionTo(player.Center) * 30f;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.8f), 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.8f), 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.target);
            }

            EdgyBossText(RandomObnoxiousQuote());
        }

        private void SpearTossDirectP2()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            if (NPC.ai[1] == 0f)
            {
                NPC.localAI[0] = MathHelper.WrapAngle((NPC.Center - player.Center).ToRotation());
                NPC.localAI[1] = Main.rand.Next(3, 9);
                NPC.localAI[1] += 3f;
                NPC.localAI[2] = ((!Main.rand.NextBool()) ? 1 : (-1));
                NPC.netUpdate = true;
            }
            Vector2 targetPos = player.Center + 500f * Vector2.UnitX.RotatedBy((float)Math.PI / 150f * NPC.ai[3] * NPC.localAI[2] + NPC.localAI[0]);
            if (NPC.Distance(targetPos) > 25f)
            {
                Movement(targetPos, 0.6f);
            }
            NPC.ai[3] += 1f;
            if ((NPC.ai[1] += 1f) > 180f)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 150f;
                bool shouldAttack = true;
                if ((NPC.ai[2] += 1f) > NPC.localAI[1])
                {
                    int[] obj = new int[11]
                    {
                    11, 16, 19, 20, 0, 31, 33, 35, 42, 44,
                    45
                    };
                    obj[4] = 44;
                    ChooseNextAttack(obj);
                    shouldAttack = false;
                }
                Attack();
            }
            else if (NPC.ai[1] == 160f)
            {
                Attack();
            }
            else if (NPC.ai[1] == 165f)
            {
                Attack();
            }
            else if (NPC.ai[1] == 170f)
            {
                Attack();
            }
            else if (NPC.ai[1] == 175f)
            {
                Attack();
            }
            else if (NPC.ai[1] == 151f)
            {
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, 1f);
                }
            }
            else if (NPC.ai[1] == 1f && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.whoAmI, -1f);
            }
            void Attack()
            {
                if (Main.netMode != 1)
                {
                    Vector2 vel = NPC.DirectionTo(player.Center) * 30f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, (float)NPC.target, 0f);
                }
            }
        }

        private void PrepareTwinRangsAndCrystals()
        {
            if (AliveCheck(player))
            {
                Vector2 targetPos = player.Center;
                targetPos.X += 500 * ((!(NPC.Center.X < targetPos.X)) ? 1 : (-1));
                if (NPC.Distance(targetPos) > 50f)
                {
                    Movement(targetPos, 0.8f);
                }
                if ((NPC.ai[1] += 1f) > 45f)
                {
                    NPC.netUpdate = true;
                    NPC.ai[0] += 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                }
            }
        }

        private void TwinRangsAndCrystals()
        {
            //IL_03e5: Unknown result type (might be due to invalid IL or missing references)
            base.NPC.velocity = Vector2.Zero;
            if (base.NPC.ai[3] == 0f)
            {
                base.NPC.localAI[0] = base.NPC.DirectionFrom(this.player.Center).ToRotation();
                if (!WorldSavingSystem.MasochistModeReal && Main.netMode != 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center + Vector2.UnitX.RotatedBy(Math.PI / 2.0 * (double)i) * 525f, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(base.NPC.damage), 0f, Main.myPlayer, 1f, 0f);
                        Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center + Vector2.UnitX.RotatedBy(Math.PI / 2.0 * (double)i + Math.PI / 4.0) * 350f, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(base.NPC.damage), 0f, Main.myPlayer, 2f, 0f);
                    }
                }
            }
            int ringDelay = (WorldSavingSystem.MasochistModeReal ? 12 : 15);
            int ringMax = (WorldSavingSystem.MasochistModeReal ? 5 : 4);
            if (base.NPC.ai[3] % (float)ringDelay == 0f && base.NPC.ai[3] < (float)(ringDelay * ringMax) && Main.netMode != 1)
            {
                float rotationOffset = (float)Math.PI * 2f / (float)ringMax * base.NPC.ai[3] / (float)ringDelay + base.NPC.localAI[0];
                int baseDelay = 60;
                float flyDelay = 120f + base.NPC.ai[3] / (float)ringDelay * (float)(WorldSavingSystem.MasochistModeReal ? 40 : 50);
                int p = Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, 300f / (float)baseDelay * Vector2.UnitX.RotatedBy(rotationOffset), ModContent.ProjectileType<MutantMark2>(), FargoSoulsUtil.ScaledProjectileDamage(base.NPC.damage), 0f, Main.myPlayer, (float)baseDelay, (float)baseDelay + flyDelay);
                if (p != 1000)
                {
                    float rotation = (float)Math.PI * 2f / 5f;
                    for (int i = 0; i < 5; i++)
                    {
                        float myRot = rotation * (float)i + rotationOffset;
                        Vector2 spawnPos = base.NPC.Center + new Vector2(125f, 0f).RotatedBy(myRot);
                        Projectile.NewProjectile(base.NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<MutantCrystalLeaf>(), FargoSoulsUtil.ScaledProjectileDamage(base.NPC.damage), 0f, Main.myPlayer, (float)Main.projectile[p].identity, myRot);
                    }
                }
            }
            if (base.NPC.ai[3] > 45f && (base.NPC.ai[1] -= 1f) < 0f)
            {
                base.NPC.netUpdate = true;
                base.NPC.ai[1] = 20f;
                base.NPC.ai[2] = ((!(base.NPC.ai[2] > 0f)) ? 1 : (-1));
                SoundEngine.PlaySound(SoundID.Item92, (Vector2?)base.NPC.Center);
                if (Main.netMode != 1 && base.NPC.ai[3] < 330f)
                {
                    float retiSpeed = 10.995575f;
                    float spazSpeed = 12.217305f;
                    float retiAcc = retiSpeed * retiSpeed / 525f * base.NPC.ai[2];
                    float spazAcc = spazSpeed * spazSpeed / 350f * (0f - base.NPC.ai[2]);
                    float rotationOffset = (WorldSavingSystem.MasochistModeReal ? ((float)Math.PI / 4f) : 0f);
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.UnitX.RotatedBy(Math.PI / 2.0 * (double)i + (double)rotationOffset) * retiSpeed, ModContent.ProjectileType<MutantRetirang>(), FargoSoulsUtil.ScaledProjectileDamage(base.NPC.damage), 0f, Main.myPlayer, retiAcc, 300f);
                        Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.UnitX.RotatedBy(Math.PI / 2.0 * (double)i + Math.PI / 4.0 + (double)rotationOffset) * spazSpeed, ModContent.ProjectileType<MutantSpazmarang>(), FargoSoulsUtil.ScaledProjectileDamage(base.NPC.damage), 0f, Main.myPlayer, spazAcc, 180f);
                    }
                }
            }
            if (base.NPC.ai[3] > 350f && base.NPC.ai[3] < 450f)
            {
                Vector2 v = base.NPC.DirectionTo(this.player.Center);
                for (int i = -1; i <= 1; i += 2)
                {
                    Projectile.NewProjectile((IEntitySource)null, base.NPC.Center, 21f * v.RotatedBy((float)i * (0.734f - (base.NPC.ai[3] - 360f) / 150f) + Main.rand.NextFloat(-0.05f, 0.05f)), 259, 66, 0f, 0, 0f, 0f);
                }
            }
            if ((base.NPC.ai[3] += 1f) > 450f)
            {
                this.ChooseNextAttack(11, 13, 16, 21, 24, 26, 29, 31, 33, 35, 39, 41, 44, 45);
            }
        }

        private void EmpressSwordWave()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            NPC.velocity = Vector2.Zero;
            int attackThreshold = 48;
            int timesToAttack = (3 + (int)(endTimeVariance * 5f));
            int startup = 90;
            if (NPC.ai[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                NPC.ai[3] = Main.rand.NextFloat((float)Math.PI * 2f);
            }
            if (NPC.ai[1] >= (float)startup && NPC.ai[1] < (float)(startup + attackThreshold * timesToAttack) && (NPC.ai[2] -= 1f) < 0f)
            {
                NPC.ai[2] = attackThreshold - 15;
                float gap = 220f;
                SoundEngine.PlaySound(SoundID.Item163, NPC.Center);
                float randomrot = Main.rand.NextFloat(6.283f);
                Vector2 RandomOffset2 = Main.rand.NextVector2Circular(75f, 75f);
                Vector2 RandomOffset = Main.rand.NextVector2Circular(120f, 120f);
                for (int i = 0; i < 3; i++)
                {
                    float rot = randomrot + (float)i * ((float)Math.PI * 2f) / 3f;
                    for (int j = -12; j <= 12; j++)
                    {
                        Vector2 targetpos = NPC.Center + RandomOffset + rot.ToRotationVector2() * 1200f + (rot + 1.5707f).ToRotationVector2() * gap * j;
                        Sword(targetpos + RandomOffset2, rot + 3.1416f, Main.rand.NextFloat(0f, 1f), -RandomOffset2 / 60f, shouldUpdate: true);
                    }
                }
                NPC.netUpdate = true;
            }
            int swordSwarmTime = startup + attackThreshold * timesToAttack + 40;
            if (NPC.ai[1] == (float)swordSwarmTime)
            {
                MegaSwordSwarm(player.Center);
                NPC.localAI[0] = player.Center.X;
                NPC.localAI[1] = player.Center.Y;
            }
            if (NPC.ai[1] == (float)(swordSwarmTime + 30))
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    MegaSwordSwarm(new Vector2(NPC.localAI[0], NPC.localAI[1]) + 600 * i * NPC.ai[3].ToRotationVector2());
                }
            }
            if ((NPC.ai[1] += 1f) > (float)(swordSwarmTime + (60)))
            {
                int[] obj = new int[14]
                {
                11, 13, 16, 21, 0, 29, 31, 35, 37, 39,
                41, 45, 47, 49
                };
                obj[4] = 26;
                ChooseNextAttack(obj);
            }
            void MegaSwordSwarm(Vector2 target)
            {
                SoundEngine.PlaySound(SoundID.Item164, NPC.Center);
                float safeAngle = NPC.ai[3];
                float safeRange = MathHelper.ToRadians(10f);
                int max = 60;
                for (int i = 0; i < max; i++)
                {
                    float rotationOffset = Main.rand.NextFloat(safeRange, (float)Math.PI - safeRange);
                    Vector2 offset = Main.rand.NextFloat(600f, 2400f) * (safeAngle + rotationOffset).ToRotationVector2();
                    if (Main.rand.NextBool())
                    {
                        offset *= -1f;
                    }
                    Vector2 spawnPos = target + offset;
                    Vector2 vel = (target - spawnPos) / 60f;
                    Sword(spawnPos, vel.ToRotation(), (float)i / (float)max, -vel * 0.75f, shouldUpdate: false);
                }
            }
            void Sword(Vector2 pos, float ai0, float ai1, Vector2 vel, bool shouldUpdate)
            {
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), pos - vel * 60f, vel, 919, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, ai1).extraUpdates = (shouldUpdate ? 1 : 0);
                }
            }
        }
        
        private void SANSGOLEM()
        {
            Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 300;
            Movement(targetPos, 0.3f);

            int attackDelay = 50;

            if (NPC.ai[1] > 0 && NPC.ai[1] % attackDelay == 0)
            {
                EdgyBossText(GFBQuote(35));

                float oldOffset = NPC.ai[2];
                while (NPC.ai[2] == oldOffset)
                    NPC.ai[2] = Main.rand.Next(-1, 2); 

                Vector2 centerPoint = FargoSoulsUtil.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null ? player.Center : Main.projectile[ritualProj].Center;
                float maxVariance = 150; 
                float maxOffsetWithinStep = maxVariance / 3 * .75f; 
                centerPoint.Y += maxVariance * NPC.ai[2]; 
                centerPoint.Y += Main.rand.NextFloat(-maxOffsetWithinStep, maxOffsetWithinStep);

                for (int i = -1; i <= 1; i += 2)
                {
                    float xSpeedWhenAttacking = Main.rand.NextFloat(8f, 20f);

                    for (int j = -1; j <= 1; j += 2) 
                    {
                        float gapRadiusHeight = 120;
                        Vector2 sansTargetPos = centerPoint;
                        const int timeToReachMiddle = 60;
                        sansTargetPos.X += xSpeedWhenAttacking * timeToReachMiddle * i;
                        sansTargetPos.Y += gapRadiusHeight * j;

                        int travelTime = 50;
                        Vector2 vel = (sansTargetPos - NPC.Center) / travelTime;

                        if (FargoSoulsUtil.HostCheck)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel,
                                ModContent.ProjectileType<MutantSansHead>(),
                                FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer,
                                travelTime, xSpeedWhenAttacking * -i, j);
                        }
                    }
                }
            }

            const int attacksToDo = 6;
            int endTime = attackDelay * (attacksToDo + 1) - 5 + attackDelay * (int)Math.Round(4 * endTimeVariance);
            if (++NPC.ai[1] > endTime)
            {
                ChooseNextAttack(13, 19, 20, 21, 24, 31, 33, 35, 41, 44);
            }
        }

        private void P2NextAttackPause()
        {
            if (AliveCheck(player))
            {
                EModeSpecialEffects();
                Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 400f;
                Movement(targetPos, 0.3f);
                if (NPC.Distance(targetPos) > 200f)
                {
                    Movement(targetPos, 0.3f);
                }
                if ((NPC.ai[1] += 1f) > 60f || (NPC.Distance(targetPos) < 200f && NPC.ai[1] > (float)((NPC.localAI[3] >= 3f) ? 15 : 30)))
                {
                    NPC.velocity *= (WorldSavingSystem.MasochistModeReal ? 0.25f : 0.5f);
                    NPC.ai[0] = NPC.ai[2];
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.netUpdate = true;
                }
            }
        }
        #endregion

        #region p3

        private bool Phase3Transition()
        {
            bool retval = true;
            NPC.localAI[3] = 3f;
            EModeSpecialEffects();
            if (NPC.buffType[0] != 0)
            {
                NPC.DelBuff(0);
            }
            if (NPC.ai[1] == 0f)
            {
                NPC.life = NPC.lifeMax;
                DramaticTransition(fightIsOver: true);
            }
            if (NPC.ai[1] < 60f && !Main.dedServ && Main.LocalPlayer.active)
            {
                Main.LocalPlayer.GetModPlayer<CSEPlayer>().Screenshake = 2;
            }
            if (NPC.ai[1] == 360f)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
            if ((NPC.ai[1] += 1f) > 480f)
            {
                retval = false;
                if (!AliveCheck(player))
                {
                    return retval;
                }
                Vector2 targetPos = player.Center;
                targetPos.Y -= 300f;
                Movement(targetPos, 1f, fastX: true, obeySpeedCap: false);
                if (NPC.Distance(targetPos) < 50f || NPC.ai[1] > 720f)
                {
                    NPC.netUpdate = true;
                    NPC.velocity = Vector2.Zero;
                    NPC.localAI[0] = 0f;
                    NPC.ai[0] -= 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = NPC.DirectionFrom(player.Center).ToRotation();
                    NPC.ai[3] = (float)Math.PI / 20f;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                    if (player.Center.X < NPC.Center.X)
                    {
                        NPC.ai[3] *= -1f;
                    }
                }
            }
            else
            {
                NPC.velocity *= 0.9f;
                if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && NPC.Distance(Main.LocalPlayer.Center) < 3000f)
                {
                    Main.LocalPlayer.controlUseItem = false;
                    Main.LocalPlayer.controlUseTile = false;
                    Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().NoUsingItems = 1;
                }
                if ((NPC.localAI[0] -= 1f) < 0f)
                {
                    NPC.localAI[0] = Main.rand.Next(15);
                    if (Main.netMode != 1)
                    {
                        Vector2 spawnPos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                        int type = ModContent.ProjectileType<PhantasmalBlast>();
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
            return retval;
        }

        private void VoidRaysP3()
        {
            if ((NPC.ai[1] -= 1f) < 0f)
            {
                if (Main.netMode != 1)
                {
                    float speed = ((NPC.localAI[0] <= 40f) ? 4f : 2f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed * Vector2.UnitX.RotatedBy(NPC.ai[2]), ModContent.ProjectileType<MutantMark1>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                }
                NPC.ai[1] = 1f;
                NPC.ai[2] += NPC.ai[3];
                if (NPC.localAI[0] < 30f)
                {
                    EModeSpecialEffects();
                }
                if (NPC.localAI[0]++ == 40f || NPC.localAI[0] == 80f || NPC.localAI[0] == 120f)
                {
                    NPC.netUpdate = true;
                    NPC.ai[2] -= NPC.ai[3] / (float)(3);
                }
                else if (NPC.localAI[0] >= (float)(160))
                {
                    NPC.netUpdate = true;
                    NPC.ai[0] -= 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.localAI[0] = 0f;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
            NPC.velocity = Vector2.Zero;
        }

        private void OkuuSpheresP3()
        {
            if (NPC.ai[2] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.ai[2] = ((!Main.rand.NextBool()) ? 1 : (-1));
                NPC.ai[3] = Main.rand.NextFloat((float)Math.PI * 2f);
            }
            int endTime = 480;
            endTime += 360;

            if ((NPC.ai[1] += 1f) > 10f && NPC.ai[3] > 60f && NPC.ai[3] < (float)(endTime - 120))
            {
                NPC.ai[1] = 0f;
                float rotation = MathHelper.ToRadians(45f) * (NPC.ai[3] - 60f) / 240f * NPC.ai[2];
                int max = 11;
                float speed = 11f;
                SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), -0.75f, rotation);
                SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0.75f, rotation);
            }
            if (NPC.ai[3] < 30f)
            {
                EModeSpecialEffects();
            }
            if ((NPC.ai[3] += 1f) > (float)endTime)
            {
                NPC.netUpdate = true;
                NPC.ai[0] -= 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
            NPC.velocity = Vector2.Zero;
        }

        private void BoundaryBulletHellP3()
        {
            if (NPC.localAI[0] == 0f)
            {
                if (!AliveCheck(player))
                {
                    return;
                }
                NPC.localAI[0] = Math.Sign(NPC.Center.X - player.Center.X);
            }
            if ((NPC.ai[1] += 1f) > 3f)
            {
                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                NPC.ai[1] = 0f;
                NPC.ai[2] += 0.0014959965f * NPC.ai[3] * NPC.localAI[0] * (2f);
                if (NPC.ai[2] > (float)Math.PI)
                {
                    NPC.ai[2] -= (float)Math.PI * 2f;
                }
                if (Main.netMode != 1)
                {
                    int max = 10;
                    for (int i = 0; i < max; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -6f).RotatedBy(NPC.ai[2] + (float)Math.PI * 2f / (float)max * (float)i), ModContent.ProjectileType<MutantEye>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if (NPC.ai[3] < 30f)
            {
                EModeSpecialEffects();
            }
            int endTime = 360;
            endTime += 360;
            endTime += 120;
            if ((NPC.ai[3] += 1f) > (float)endTime)
            {
                NPC.ai[0] -= 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                NPC.localAI[0] = 0f;
                NPC.netUpdate = true;
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
            NPC.velocity = Vector2.Zero;
        }

        private void AbomSwordP3()
        {
            if (!AliveCheck(player))
                return;

            if (NPC.ai[1] < 60)
                FancyFireballs((int)NPC.ai[1]);

            if (NPC.ai[1] == 0 && NPC.ai[2] != 2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                float ai1 = NPC.ai[2] == 1 ? -1 : 1;
                ai1 *= MathHelper.ToRadians(270) / 120 * -1 * 60;
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3, ai1);
                if (p != Main.maxProjectiles)
                {
                    Main.projectile[p].localAI[1] = NPC.whoAmI;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncProjectile, number: p);
                }
            }

            else if (NPC.ai[1] == 60 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.netUpdate = true;
                NPC.velocity = Vector2.Zero;

                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                float ai0 = NPC.ai[2] == 1 ? -1 : 1;
                ai0 *= MathHelper.ToRadians(270) / 120;
                Vector2 vel = NPC.DirectionTo(player.Center).RotatedBy(-ai0 * 60);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, ai0, NPC.whoAmI);

                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -vel, ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, ai0, NPC.whoAmI);
            }
            if (++NPC.ai[1] > 60)
            {
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;

                NPC.velocity.X = 0f;
                NPC.velocity.Y = 1.5f;
            }
        }
        
        private void AbomSwordDashP3()
        {
            NPC.position += NPC.velocity;
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2] - NPC.Center.X);
            if (++NPC.ai[1] > 90)
            {
                NPC.netUpdate = true;
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
            }
        }
        
        private void AbomSwordWaitP3()
        {
            if (!AliveCheck(player))

                NPC.localAI[2] = 0;
            Vector2 targetPos = player.Center;
            targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 50)
                if (++NPC.ai[1] > 60)
                {
                    NPC.netUpdate = true;
                    NPC.ai[0]--;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.localAI[0] = 0;
                }
        }

        void FinalSpark()
        {
            void SpinLaser(bool useMasoSpeed)
            {
                float newRotation = NPC.SafeDirectionTo(Main.player[NPC.target].Center).ToRotation();
                float difference = MathHelper.WrapAngle(newRotation - NPC.ai[3]);
                float rotationDirection = 2f * (float)Math.PI * 1f / 6f / 60f;
                rotationDirection *= useMasoSpeed ? 1.1f : 1f;
                float change = Math.Min(rotationDirection, Math.Abs(difference)) * Math.Sign(difference);
                if (useMasoSpeed)
                {
                    change *= 1.1f;
                    float angleLerp = NPC.ai[3].AngleLerp(newRotation, 0.015f) - NPC.ai[3];
                    if (Math.Abs(MathHelper.WrapAngle(angleLerp)) > Math.Abs(MathHelper.WrapAngle(change)))
                        change = angleLerp;
                }
                NPC.ai[3] += change;

                EdgyBossText(GFBQuote(31));
            }

            if (!AliveCheck(player))
                return;

            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = Main.rand.Next(30);
                if (FargoSoulsUtil.HostCheck)
                {
                    Vector2 spawnPos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                    int type = ModContent.ProjectileType<MutantBombSmall>();
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer);
                }
            }

            bool harderRings = NPC.ai[2] >= 420 - 90;
            int ringTime = harderRings ? 100 : 120;
            if (++NPC.ai[1] > ringTime)
            {
                NPC.ai[1] = 0;

                EModeSpecialEffects();

                if (FargoSoulsUtil.HostCheck)
                {
                    int max = 11;
                    int damage = FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage);
                    SpawnSphereRing(max, 6f, damage, 0.5f);
                    SpawnSphereRing(max, 6f, damage, -.5f);
                }
            }

            else if (NPC.ai[2] == 420 - 90)
            {
                if (NPC.localAI[1] == 0)
                {
                    NPC.localAI[1] = 1;
                    NPC.ai[2] -= 600 + 180;

                    NPC.ai[3] -= MathHelper.ToRadians(20);

                    if (FargoSoulsUtil.HostCheck)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]),
                            ModContent.ProjectileType<MutantGiantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.5f), 0f, Main.myPlayer, 0, NPC.whoAmI);

                        Projectile.NewProjectile(
                            NPC.GetSource_FromThis(),
                            NPC.Center,
                            Vector2.UnitX.RotatedBy(NPC.ai[3] + MathHelper.Pi),
                            ModContent.ProjectileType<MutantGiantDeathray2>(),
                            FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.5f),
                            0f,
                            Main.myPlayer,
                            0,
                            NPC.whoAmI
                        );
                    }

                    NPC.netUpdate = true;
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                    if (FargoSoulsUtil.HostCheck)
                    {
                        const int max = 8;
                        for (int i = 0; i < max; i++)
                        {
                            float offset = i - 0.5f;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, (NPC.ai[3] + MathHelper.TwoPi / max * offset).ToRotationVector2(), ModContent.ProjectileType<GlowLine>(), 0, 0f, Main.myPlayer, 13f, NPC.whoAmI);
                        }
                    }
                }
            }

            if (NPC.ai[2] < 420)
            {
                if (NPC.localAI[1] == 0 || NPC.ai[2] > 420 - 90)
                    NPC.ai[3] = NPC.DirectionFrom(player.Center).ToRotation();
            }
            else
            {
                if (!Main.dedServ)
                {
                    ManagedScreenFilter filter = ShaderManager.GetFilter("FargowiltasSouls.FinalSpark");
                    filter.Activate();
                    if (Main.WaveQuality == 0)
                        Main.WaveQuality = 1;
                }

                if (NPC.ai[1] % 3 == 0 && FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 24f * Vector2.UnitX.RotatedBy(NPC.ai[3]), ModContent.ProjectileType<MutantEyeWavy>(), 0, 0f, Main.myPlayer,
                      Main.rand.NextFloat(0.5f, 1.25f) * (Main.rand.NextBool() ? -1 : 1), Main.rand.Next(10, 60));
                }
            }

            int endTime = 1020;
            endTime += 180;
            if (++NPC.ai[2] > endTime && NPC.life <= 1)
            {
                NPC.netUpdate = true;
                AttackChoice--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                FargoSoulsUtil.ClearAllProjectiles(2, NPC.whoAmI);
            }
            else if (NPC.ai[2] == 420)
            {
                NPC.netUpdate = true;

                NPC.ai[3] += MathHelper.ToRadians(20);

                if (FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]),
                        ModContent.ProjectileType<MutantGiantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.5f), 0f, Main.myPlayer, 0, NPC.whoAmI);

                    Projectile.NewProjectile(
                        NPC.GetSource_FromThis(),
                        NPC.Center,
                        Vector2.UnitX.RotatedBy(NPC.ai[3] + MathHelper.Pi),
                        ModContent.ProjectileType<MutantGiantDeathray2>(),
                        FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage, 0.5f),
                        0f,
                        Main.myPlayer,
                        0,
                        NPC.whoAmI
                    );
                }
            }
            else if (NPC.ai[2] < 300 && NPC.localAI[1] != 0)
            {
                float num1 = 0.99f;
                if (NPC.ai[2] >= 60)
                    num1 = 0.79f;
                if (NPC.ai[2] >= 120)
                    num1 = 0.58f;
                if (NPC.ai[2] >= 180)
                    num1 = 0.43f;
                if (NPC.ai[2] >= 240)
                    num1 = 0.33f;
                for (int i = 0; i < 9; ++i)
                {
                    if (Main.rand.NextFloat() >= num1)
                    {
                        float f = Main.rand.NextFloat() * 6.283185f;
                        float num2 = Main.rand.NextFloat();
                        Dust dust = Dust.NewDustPerfect(NPC.Center + f.ToRotationVector2() * (110 + 600 * num2), 229, (f - 3.141593f).ToRotationVector2() * (14 + 8 * num2), 0, default, 1f);
                        dust.scale = 0.9f;
                        dust.fadeIn = 1.15f + num2 * 0.3f;
                        dust.noGravity = true;
                    }
                }
            }

            SpinLaser(NPC.ai[2] >= 420);

            if (AliveCheck(player))
                NPC.localAI[2] = 0;
            else
                NPC.localAI[2]++;

            NPC.velocity = Vector2.Zero;
        }
        private void DyingDramaticPause()
        {
            if (!AliveCheck(player))
            {
                return;
            }
            NPC.ai[3] -= (float)Math.PI / 360f;
            NPC.velocity = Vector2.Zero;
            if ((NPC.ai[1] += 1f) > 120f)
            {
                NPC.netUpdate = true;
                NPC.ai[0] -= 1f;
                NPC.ai[1] = 0f;
                NPC.ai[3] = -(float)Math.PI / 2f;
                NPC.netUpdate = true;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -1f, ModContent.ProjectileType<MutantGiantDeathray2>(), 0, 0f, Main.myPlayer, 1f, (float)NPC.whoAmI);
                }
            }
            if ((NPC.localAI[0] -= 1f) < 0f)
            {
                NPC.localAI[0] = Main.rand.Next(15);
                if (Main.netMode != 1)
                {
                    Vector2 spawnPos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                    int type = ModContent.ProjectileType<PhantasmalBlast>();
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
        }

        private void DyingAnimationAndHandling()
        {
            NPC.velocity = Vector2.Zero;
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 161, 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 12f;
            }
            if ((NPC.localAI[0] -= 1f) < 0f)
            {
                NPC.localAI[0] = Main.rand.Next(5);
                if (Main.netMode != 1)
                {
                    Vector2 spawnPos = NPC.Center + Main.rand.NextVector2Circular(240f, 240f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            if ((NPC.ai[1] += 1f) % 3f == 0f && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 24f * Vector2.UnitX.RotatedBy(NPC.ai[3]), ModContent.ProjectileType<MutantEyeWavy>(), 0, 0f, Main.myPlayer, Main.rand.NextFloat(0.75f, 1.5f) * (float)((!Main.rand.NextBool()) ? 1 : (-1)), (float)Main.rand.Next(10, 90));
            }
            if (++NPC.alpha <= 255)
            {
                return;
            }
            NPC.alpha = 255;
            NPC.life = 0;
            NPC.dontTakeDamage = false;
            //NPC.checkDead();
            if (Main.netMode == 1 || !ModContent.TryFind<ModNPC>("Fargowiltas", "Mutant", out var modNPC) || NPC.AnyNPCs(modNPC.Type))
            {
                return;
            }
            int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, modNPC.Type);
            if (n != 200)
            {
                Main.npc[n].homeless = true;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(23, -1, -1, null, n);
                }
            }
            EdgyBossText(GFBQuote(33));
        }

        #endregion
        
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.FargoSouls().MaxLifeReduction += 100;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
            target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 3; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Vortex, 0f, 0f, 0, default, 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 3f;
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (WorldSavingSystem.AngryMutant)
                modifiers.FinalDamage *= 0.07f;
        }

        public override bool CheckDead()
        {
            if (AttackChoice == -10)
                return true;

            NPC.life = 1;
            NPC.active = true;
            if (FargoSoulsUtil.HostCheck && AttackChoice > -1)
            {
                AttackChoice = AttackChoice >= 10 ? -1 : 10;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                NPC.localAI[2] = 0;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                FargoSoulsUtil.ClearAllProjectiles(2, NPC.whoAmI, AttackChoice < 0);
                EdgyBossText(GFBQuote(34));
            }
            return false;
        }

        public override void OnKill()
        {
            //Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<PhantasmalEnergy>(), 30);
            Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<EternalEnergy>(), 9999);
            Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<MutantMask>(), 1);
            Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<MutantBody>(), 1);
            Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<MutantPants>(), 1);
            Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.Penetrator>(), 1);
            Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<EternitySoul>(), 1);
            NPC.SetEventFlagCleared(ref WorldSaveSystem.downedRealMutantEX, -1);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sadism>(), 1, 20, 40));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<UltimateHealingPotion>();
        }

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter > 4)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                    NPC.frame.Y = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Npc[NPC.type].Value;
            Vector2 position = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);
            Rectangle rectangle = NPC.frame;
            Vector2 origin2 = rectangle.Size() / 2f;

            SpriteEffects effects = NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.EntitySpriteDraw(texture2D13, position, new Rectangle?(rectangle), NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, effects, 0);

            Vector2 auraPosition = AuraCenter;
            //if (ShouldDrawAura)
            //    DrawAura(spriteBatch, auraPosition, AuraScale);

            return false;
        }

        public void DrawAura(SpriteBatch spriteBatch, Vector2 position, float auraScale)
        {
            Color outerColor = Color.CadetBlue;
            outerColor.A = 0;

            Color darkColor = outerColor;
            Color mediumColor = Color.Lerp(outerColor, Color.White, 0.75f);
            Color lightColor2 = Color.Lerp(outerColor, Color.White, 0.5f);

            Vector2 auraPos = position;
            float radius = 2000f * auraScale;
            var target = Main.LocalPlayer;
            var blackTile = TextureAssets.MagicPixel;
            var diagonalNoise = FargosTextureRegistry.WavyNoise;
            if (!blackTile.IsLoaded || !diagonalNoise.IsLoaded)
                return;
            var maxOpacity = NPC.Opacity;

            ManagedShader borderShader = ShaderManager.GetShader("FargowiltasSouls.MutantP1Aura");
            borderShader.TrySetParameter("colorMult", 7.35f);
            borderShader.TrySetParameter("time", Main.GlobalTimeWrappedHourly);
            borderShader.TrySetParameter("radius", radius);
            borderShader.TrySetParameter("anchorPoint", auraPos);
            borderShader.TrySetParameter("screenPosition", Main.screenPosition);
            borderShader.TrySetParameter("screenSize", Main.ScreenSize.ToVector2());
            borderShader.TrySetParameter("playerPosition", target.Center);
            borderShader.TrySetParameter("maxOpacity", maxOpacity);
            borderShader.TrySetParameter("darkColor", darkColor.ToVector4());
            borderShader.TrySetParameter("midColor", mediumColor.ToVector4());
            borderShader.TrySetParameter("lightColor", lightColor2.ToVector4());

            spriteBatch.GraphicsDevice.Textures[1] = diagonalNoise.Value;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, borderShader.WrappedEffect, Main.GameViewMatrix.TransformationMatrix);
            Rectangle rekt = new(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
            spriteBatch.Draw(blackTile.Value, rekt, null, default, 0f, blackTile.Value.Size() * 0.5f, 0, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

        }
        
        public static void ArenaAura(Vector2 center, float distance, bool reverse = false, int dustid = -1, Color color = default, params int[] buffs)
        {
            Player p = Main.LocalPlayer;


            if (buffs.Length == 0 || buffs[0] < 0)
                return;

            float range = center.Distance(p.Center);
            if (p.active && !p.dead && !p.ghost && (reverse ? range > distance && range < Math.Max(3000f, distance * 2) : range < distance))
            {
                foreach (int buff in buffs)
                {
                    FargoSoulsUtil.AddDebuffFixedDuration(p, buff, 2);
                }
            }
        }
    }
}