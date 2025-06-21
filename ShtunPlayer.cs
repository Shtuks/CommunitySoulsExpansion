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
using FargowiltasSouls.Content.Buffs.Boss;
using ssm.Content.Buffs;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ID;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Core;
using FargowiltasSouls.Core.Systems;
using CalamityMod.Events;

namespace ssm
{
    public partial class ShtunPlayer : ModPlayer
    {
        public bool MutantSoul;
        public bool DevianttSoul;
        public int Screenshake;
        public float throwerVelocity = 1f;
        public bool CyclonicFin;
        public int CyclonicFinCD;
        public bool MonstrocityPresence;
        public bool lumberjackSet;
        public bool starlightFruit;

        //Enchants
        public bool equippedPhantasmalEnchantment;
        public bool equippedAbominableEnchantment;
        public bool equippedNekomiEnchantment;
        public bool equippedMonstrosityEnchantment;

        public override void PostUpdateBuffs()
        {
            //if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()) && ModCompatibility.Calamity.Loaded && !WorldSavingSystem.MasochistModeReal)
            //{
            //    ModLoader.GetMod("CalamityMod").TryFind("Enraged", out ModBuff enrage);
            //    ModLoader.GetMod("CalamityMod").TryFind("RageMode", out ModBuff rage);
            //    ModLoader.GetMod("CalamityMod").TryFind("AdrenalineMode", out ModBuff adrenaline);
            //    Main.LocalPlayer.buffImmune[enrage.Type] = true;
            //    Main.LocalPlayer.buffImmune[rage.Type] = true;
            //    Main.LocalPlayer.buffImmune[adrenaline.Type] = true;
            //}

            if (starlightFruit)
            {
                Player.accWatch = 3;
                Player.accDepthMeter = 1;
                Player.accCompass = 1;
                Player.accFishFinder = true;
                Player.accDreamCatcher = true;
                Player.accOreFinder = true;
                Player.accStopwatch = true;
                Player.accCritterGuide = true;
                Player.accJarOfSouls = true;
                Player.accThirdEye = true;
                Player.accCalendar = true;
                Player.accWeatherRadio = true;

                Player.findTreasure = true;
                Player.nightVision = true;
                Player.detectCreature = true;
                Player.pickSpeed -= 0.25f;
                Player.dangerSense = true;
                Player.gills = true;
                Player.waterWalk = true;
                Player.ignoreWater = true;
                Player.accFlipper = true;
                Player.buffImmune[4] = true;
                Player.buffImmune[15] = true;
                Player.buffImmune[109] = true;
                Player.buffImmune[9] = true;
                Player.buffImmune[11] = true;
                Player.buffImmune[12] = true;
                Player.buffImmune[17] = true;
                Player.buffImmune[104] = true;
                Player.buffImmune[111] = true;
                Player.ammoBox = true;
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

                Player.pickSpeed -= 0.2f;
                Player.moveSpeed += 0.2f;
                Player.GetArmorPenetration(DamageClass.Generic) += 10;
                Player.moveSpeed += 0.25f;
                Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 20;
                Player.lifeRegen += 5;
                Player.endurance += 0.1f;
                Player.statDefense += 10;
                Player.GetCritChance(DamageClass.Generic) += 0.1f;
                Player.GetDamage(DamageClass.Generic) += 0.1f;
                Player.maxMinions += 2;  
            }

            if (FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && Main.player[Main.myPlayer].Shtun().lumberjackSet && WorldSaveSystem.enragedMutantEX)
            {
                Main.LocalPlayer.statDefense*=0;
                Main.LocalPlayer.endurance*=0;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            var playerData = new List<string>();
            if (starlightFruit) playerData.Add("starlightFruit");

            tag.Add($"{Mod.Name}.{Player.name}.Data", playerData);
        }

        public override void LoadData(TagCompound tag)
        {
            var playerData = tag.GetList<string>($"{Mod.Name}.{Player.name}.Data");
            starlightFruit = playerData.Contains("starlightFruit");
        }
        public override void OnEnterWorld()
        {
            if (!ModLoader.TryGetMod("ThoriumRework", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoRework"), Color.LimeGreen);
            }
            if (ModCompatibility.Calamity.Loaded && ModCompatibility.Thorium.Loaded && !ModLoader.TryGetMod("WHummusMultiModBalancing", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoBalancing"), Color.LimeGreen);
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
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (Player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    Projectile.NewProjectile(proj.GetSource_FromThis(), spawn, vel, ModContent.ProjectileType<SpectralFishron>(), 300, 10f, proj.owner, target.whoAmI, 300);
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

            //No "free dps"
            if(FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()) && Player.HeldItem != null && !Player.HeldItem.IsAir && (Player.HeldItem.DamageType != DamageClass.Summon || Player.HeldItem.DamageType != DamageClass.SummonMeleeSpeed))
            {
                Player.maxMinions = 0;
            }
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
        public override void ResetEffects()
        {
            if (Screenshake > 0)
                Screenshake--;

            equippedPhantasmalEnchantment = false;
            equippedAbominableEnchantment = false;
            equippedNekomiEnchantment = false;
            DevianttSoul = false;
            MutantSoul = false;
            throwerVelocity = 1f;
            CyclonicFin = false;
            lumberjackSet = false;
        }
    }
}
