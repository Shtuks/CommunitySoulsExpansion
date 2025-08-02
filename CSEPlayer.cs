using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Buffs.Masomode;
using ssm.Content.Projectiles;
using ssm.Content.NPCs.MutantEX;
using ssm.Systems;
using FargowiltasSouls;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ID;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Core;
using BombusApisBee.BeeDamageClass;
using CalamityMod.CalPlayer;
using ThoriumMod.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;
using FargowiltasSouls.Core.Systems;
using Terraria.Chat;
using ssm.Content.Items.Summons;

namespace ssm
{
    public partial class CSEPlayer : ModPlayer
    {
        public float throwerVelocity = 1f;
        public bool CyclonicFin;
        public int CyclonicFinCD;
        public bool MonstrosityPresence;
        public bool lumberjackSet;
        public bool starlightFruit;
        public bool eternalMonolith;

        public int Screenshake;
        private float blindTime;
        private float blindDuration;
        private float blindSharpness;

        //Enchants
        public bool equippedPhantasmalEnchantment;
        public bool equippedAbominableEnchantment;
        public bool equippedNekomiEnchantment;
        public bool equippedMonstrosityEnchantment;

        public float monstrosityHits;
        private bool wasGhost = false;
        private int ghostTimer = 0;
        public void DiscordWhiteTheme(float duration, float sharpness)
        {
            blindDuration = duration;
            blindTime = duration;
            blindSharpness = MathHelper.Clamp(sharpness, 1f, 10f);
        }

        private float CalculateIntensity()
        {
            float progress = blindTime / blindDuration;

            return 1f - MathHelper.Clamp((1f - progress) * blindSharpness, 0f, 1f);
        }
        public override void UpdateEquips()
        {
            if (Player.name.ToLower().Contains("starlightcatt"))
            {
                Player.manaRegenBuff = true;
                Player.archery = true;
                Player.ammoPotion = true;
                Player.lavaImmune = true;
                Player.fireWalk = true;
                Player.buffImmune[24] = true;
                Player.buffImmune[29] = true;
                Player.buffImmune[39] = true;
                Player.buffImmune[44] = true;
                Player.buffImmune[46] = true;
                Player.buffImmune[47] = true;
                Player.buffImmune[69] = true;
                Player.buffImmune[110] = true;
                Player.buffImmune[112] = true;
                Player.buffImmune[113] = true;
                Player.buffImmune[114] = true;
                Player.buffImmune[115] = true;
                Player.buffImmune[117] = true;
                Player.buffImmune[150] = true;
                Player.buffImmune[348] = true;
                Player.buffImmune[1] = true;
                Player.buffImmune[2] = true;
                Player.buffImmune[5] = true;
                Player.buffImmune[6] = true;
                Player.buffImmune[7] = true;
                Player.buffImmune[14] = true;
                Player.maxMinions+=2;
                Player.statDefense += 10;
                Player.lifeRegen += 5;
                Player.lifeForce = true;
                Player.moveSpeed += 0.25f;
                Player.endurance += 0.1f;
                Player.statManaMax2 += 20;
                Player.manaCost -= 0.2f;
                Player.ammoBox = true;
                Player.pickSpeed -= 0.2f;
                Player.moveSpeed += 0.2f;
                Player.manaRegenBonus += 2;
                Main.SceneMetrics.HasHeartLantern = true;
                Main.SceneMetrics.HasCampfire = true;
                ++Player.maxTurrets;
                Player.GetDamage(DamageClass.Generic) += 0.1f;
                Player.GetCritChance(DamageClass.Generic) += 0.1f;
                Player.GetArmorPenetration(DamageClass.Generic) += 15;
                Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 20;
                if (Player.thorns < 1.0)
                {
                    Player.thorns = 0.3333333f;
                }
            }
            //Player.statLifeMax2 = (int)(Player.statLifeMax2 * (monstrosityHits / 10));
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (attempt.playerFishingConditions.BaitItemType == ModContent.ItemType<TruffleWormEX>())
            {
                itemDrop = 0;
                bool spawned = false;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].bobber
                        && Main.projectile[i].owner == Player.whoAmI && Player.whoAmI == Main.myPlayer)
                    {
                        Main.projectile[i].ai[0] = 2f;
                        Main.projectile[i].netUpdate = true;

                        if (!spawned && Main.projectile[i].wet && WorldSavingSystem.EternityMode && !NPC.AnyNPCs(NPCID.DukeFishron)) 
                        {
                            spawned = true;
                            if (Main.netMode == NetmodeID.SinglePlayer) 
                            {
                                EModeGlobalNPC.spawnFishronEX = true;
                                NPC.NewNPC(Main.projectile[i].GetSource_FromThis(),(int)Main.projectile[i].Center.X, (int)Main.projectile[i].Center.Y + 100,NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, Player.whoAmI);
                                Main.NewText("Duke Fishron EX has awoken!", 50, 100, 255);
                            }
                            else if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                var netMessage = Mod.GetPacket();
                                netMessage.Write((byte)ssm.PacketID.SpawnFishronEX);
                                netMessage.Write((byte)Player.whoAmI);
                                netMessage.Write((int)Main.projectile[i].Center.X);
                                netMessage.Write((int)Main.projectile[i].Center.Y + 100);
                                netMessage.Send();
                            }
                            else if (Main.netMode == NetmodeID.Server)
                            {
                                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("???????"), Color.White);
                            }
                            //CSEUtils.DisplayLocalizedText("Music: True Masochism by ENNWAY", Color.White);
                        }
                    }
                }
            }
        }
        public override void PostUpdateEquips()
        {
            if (Player.FargoSouls().MutantSetBonusItem != null)
            {
                Player.CSE().throwerVelocity += 0.3f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(Player, 1000, 0, 1, 2f, 100, 50, 2, 1000); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(Player, 30); }
                if (ModCompatibility.Calamity.Loaded) { ThrowerCal(Player, 5); }
            }

            if (Player.FargoSouls().StyxSet)
            {
                Player.CSE().throwerVelocity += 0.2f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(Player, 10, 0, 0.5f, 1f, 30, 15, 2, 600); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(Player, 20); }
                if (ModCompatibility.Calamity.Loaded) { ThrowerCal(Player, 2); }
            }

            if (Player.FargoSouls().GaiaSet)
            {
                Player.CSE().throwerVelocity += 0.1f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(Player, 1, 200, 0.3f, 0.5f, 20, 10, 2, 300); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(Player, 20); }
                if (ModCompatibility.Calamity.Loaded) { ThrowerCal(Player, 1.1f); }
            }

            if (Player.FargoSouls().NekomiSet)
            {
                Player.CSE().throwerVelocity += 0.05f;
                if (ModCompatibility.Thorium.Loaded) { BardAndHealer(Player, 0.5f, 100, 0.1f, 0.25f, 10, 5, 1, 180); }
                if (ModCompatibility.BeekeeperClass.Loaded) { Beekeeper(Player, 10); }
                if (ModCompatibility.Calamity.Loaded) { ThrowerCal(Player, 0.7f); }
            }
        }
        public override void SaveData(TagCompound tag)
        {
            var PlayerData = new List<string>();
            if (starlightFruit) PlayerData.Add("starlightFruit");

            tag.Add($"{Mod.Name}.{Player.name}.Data", PlayerData);
        }

        public override void LoadData(TagCompound tag)
        {
            var PlayerData = tag.GetList<string>($"{Mod.Name}.{Player.name}.Data");
            starlightFruit = PlayerData.Contains("starlightFruit");
        }
        public override void OnEnterWorld()
        {
            // debug only //
            //if (BossRushEvent.Bosses == null || BossRushEvent.Bosses.Count == 0)
            //{
            //    Main.NewText("empty");
            //    return;
            //}
            //string message = "bosses ids: ";
            //foreach (BossRushEvent.Boss boss in BossRushEvent.Bosses)
            //{
            //    message += $"{boss.EntityID}, ";

            //    if (message.Length > 500)
            //    {
            //        Main.NewText(message.TrimEnd(',', ' '));
            //        message = "more: ";
            //    }
            //}
            //if (message.Length > 0)
            //{
            //    Main.NewText(message.TrimEnd(',', ' '));
            //}
            // debug only //


            if (!ModLoader.TryGetMod("ThrowerUnification", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoThrower"), Color.LimeGreen);
            }
            if (!ModLoader.TryGetMod("ThoriumRework", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoRework"), Color.LimeGreen);
            }
            if (ModCompatibility.Calamity.Loaded && ModCompatibility.Thorium.Loaded && !ModLoader.TryGetMod("WHummusMultiModBalancing", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoBalancing"), Color.LimeGreen);
            }
            if (ModLoader.TryGetMod("InfernalEclipseAPI", out Mod _) || ModLoader.TryGetMod("WHummusMultiModBalancing", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.IHateRogue"), Color.LimeGreen);
            }

            //if (!ModLoader.TryGetMod("SoABardHealer", out Mod _) && ModLoader.TryGetMod("SoA", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            //{
            //    Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoSoABardHealer1"), Color.Purple);
            //    Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoSoABardHealer2"), Color.Purple);
            //}
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (CyclonicFin)
            {
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 900);
                target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);

                if (hit.Crit && CyclonicFinCD <= 0 && proj.type != ModContent.ProjectileType<RazorbladeTyphoonFriendly>())
                {
                    CyclonicFinCD = 420;

                    float screenX = Main.screenPosition.X;
                    if (Player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    Projectile.NewProjectile(proj.GetSource_FromThis(), spawn, vel, ModContent.ProjectileType<SpectralFishron>(), 200, 10f, proj.owner, target.whoAmI, 300);
                }
            }
        }

        public override void PostUpdate()
        {
            if (Main.masterMode &&
                Player.CountItem(ItemID.Spike, 3859) >= 3859 &&
                Main.worldName.StartsWith("Q", System.StringComparison.OrdinalIgnoreCase))
            {
                Player.AddBuff(BuffID.ChaosState, 100);
            }

            if (blindTime > 0) blindTime--;

            //No "free dps"
            if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()) && Player.HeldItem != null && !Player.HeldItem.IsAir && Player.HeldItem.DamageType != DamageClass.Summon && Player.HeldItem.DamageType != DamageClass.SummonMeleeSpeed)
            {
                Player.maxMinions = 0;
            }

            //auto revive for ech, mutant and monstrosity
            //if (Player.ghost && !wasGhost && !(Player.difficulty == 2))
            //{
            //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("You will be revived in 5 seconds!"), new Color(255, 255, 255));
            //    ghostTimer = 300;
            //    wasGhost = true;
            //}

            //else if (!Player.ghost)
            //{
            //    wasGhost = false;
            //    ghostTimer = 0;
            //}

            //if (ghostTimer > 0)
            //{
            //    ghostTimer--;

            //    if (ghostTimer <= 0)
            //    {
            //        Player.ghost = false;
            //        Player.dead = false;
            //    }
            //}
        }


        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        public void BardAndHealer(Player Player, float bonus1, int bonus2, float bonus3, float bonus4, int bonus5, int bonus6, int bonus7, short bonus8)
        {
            Player.GetThoriumPlayer().bardBuffDuration += bonus8;
            Player.GetThoriumPlayer().techPointsMax += bonus7;
            Player.GetThoriumPlayer().throwerExhaustionRegenBonus += bonus1;
            Player.GetThoriumPlayer().throwerExhaustionMax += bonus2;
            Player.GetThoriumPlayer().bardResourceDropBoost += bonus3;
            Player.GetThoriumPlayer().inspirationRegenBonus += bonus4;
            Player.GetThoriumPlayer().bardResourceMax2 += bonus5;
            Player.GetThoriumPlayer().healBonus += bonus6;
        }


        [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
        public void Beekeeper(Player Player, int bonus)
        {
            Player.GetModPlayer<BeeDamagePlayer>().BeeResourceMax2 += bonus;
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        public void ThrowerCal(Player Player, float bonus)
        {
            Player.GetModPlayer<CalamityPlayer>().wearingRogueArmor = true;
            Player.GetModPlayer<CalamityPlayer>().rogueStealthMax += bonus;
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return !lumberjackSet;
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return !lumberjackSet;
        }
        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);

            if (blindTime <= 0) return;

            float intensity = CalculateIntensity();
            if (intensity <= 0) return;
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            Rectangle screen = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            Color color = Color.White * intensity;

            Main.spriteBatch.Draw(pixel, screen, color);

            Main.spriteBatch.End();
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (equippedMonstrosityEnchantment)
            {
                modifiers.SetMaxDamage(1000);
            }
        }

        public override void UpdateDead()
        {
            blindTime = 0;
            monstrosityHits = 0;
        }
        public override void ResetEffects()
        {
            if (Screenshake > 0)
                Screenshake--;

            equippedPhantasmalEnchantment = false;
            equippedAbominableEnchantment = false;
            equippedNekomiEnchantment = false;
            throwerVelocity = 1f;
            CyclonicFin = false;
            lumberjackSet = false;
            eternalMonolith = false;
        }
    }
}
