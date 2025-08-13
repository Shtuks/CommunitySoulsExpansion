using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.BardItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using System;
using static ssm.Thorium.Souls.ThoriumSoul;
using static ssm.Thorium.Forces.MuspelheimForce;
using FargowiltasSouls;
using Luminance.Common.Utilities;
using static ssm.Thorium.Enchantments.CyberPunkEnchant;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using System.Collections.Generic;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CyberPunkEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CyberPunkEffect>(Item);
        }

        public class CyberPunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyberPunkEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override bool MinionEffect => true;
            public override void PostUpdateEquips(Player player)
            {
                int count = 0;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<CyberneticOrb>() && Main.npc[i].ai[0] == player.whoAmI)
                        count++;
                }
                int multiplier = 2;
                if (player.HasEffect<MuspelheimEffect>())
                    multiplier = 3;
                if (player.HasEffect<ThoriumEffect>())
                    multiplier = 4;

                if (count < multiplier)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        int n = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<CyberneticOrb>(), 0, player.whoAmI, 0f, multiplier);
                    }
                    else if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        var netMessage = ssm.Instance.GetPacket();
                        netMessage.Write((byte)ssm.PacketID.RequestCyberOrbs);
                        netMessage.Write((byte)player.whoAmI);
                        netMessage.Write((byte)multiplier);
                        netMessage.Send();
                    }
                }
            }
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CyberPunkHeadset>());
            recipe.AddIngredient(ModContent.ItemType<CyberPunkSuit>());
            recipe.AddIngredient(ModContent.ItemType<CyberPunkLeggings>());
            recipe.AddIngredient(ModContent.ItemType<AutoTuner>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerDamage>());
            recipe.AddIngredient(ModContent.ItemType<DissTrack>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CyberneticOrb : ModNPC
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetStaticDefaults()
        {
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
            this.ExcludeFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.lifeMax = 30;
            NPC.friendly = true;
            NPC.dontCountMe = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.8f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
        }

        public override void AI()
        {
            const float Radius = 64f; 
            const float BaseAngularSpeed = 0.05f; 

            if (NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = 1f;
                NPC.lifeMax *= (int)NPC.ai[2];
                NPC.defDamage *= (int)NPC.ai[2];
                NPC.defDefense *= (int)NPC.ai[2];
                NPC.life = NPC.lifeMax;
            }

            NPC.damage = NPC.defDamage;
            NPC.defense = NPC.defDefense;

            Player player = Main.player[(int)NPC.ai[0]];
            if (player.whoAmI != Main.myPlayer || !player.HasEffect<CyberPunkEffect>() || !player.active || player.dead)
            {
                int n = NPC.whoAmI;
                NPC.SimpleStrikeNPC(NPC.lifeMax * 2, 0);
                if (FargoSoulsUtil.HostCheck)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n, 9999f);
                return;
            }

            List<NPC> minions = new List<NPC>();
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.active && n.type == NPC.type && n.ai[0] == NPC.ai[0])
                {
                    minions.Add(n);
                }
            }

            minions.Sort((a, b) => a.whoAmI.CompareTo(b.whoAmI));

            int minionIndex = minions.IndexOf(NPC);
            int minionCount = minions.Count;

            Vector2 toPlayer = NPC.Center - player.Center;
            float distance = toPlayer.Length();
            if (distance > 1000f)
            {
                minionIndex = minions.IndexOf(NPC);
                minionCount = minions.Count;
                PlaceMinionAtOrbitPosition(player, minionIndex, minionCount);
                NPC.velocity = Vector2.Zero;
                return;
            }

            float angleOffset = (MathHelper.TwoPi / minionCount) * minionIndex;
            float globalRotation = (float)Main.time * BaseAngularSpeed;
            float targetAngle = globalRotation + angleOffset;

            Vector2 targetPosition = player.Center + new Vector2(
                (float)Math.Cos(targetAngle) * Radius,
                (float)Math.Sin(targetAngle) * Radius
            );

            Vector2 direction = targetPosition - NPC.Center;
            if (direction.Length() > 10f)
            {
                direction.Normalize();
                NPC.velocity = (NPC.velocity * 15f + direction * 16f) / 16f;
            }
            else
            {
                NPC.velocity *= 0.95f;
            }

            const float RepelStrength = 0.1f;
            foreach (NPC other in minions)
            {
                if (other.whoAmI == NPC.whoAmI) continue;

                Vector2 repel = NPC.DirectionFrom(other.Center);
                float distanceToOther = Vector2.Distance(NPC.Center, other.Center);

                if (distanceToOther < NPC.width)
                {
                    float repelFactor = MathHelper.Clamp(1f - distanceToOther / NPC.width, 0f, 1f);
                    NPC.velocity += repel * RepelStrength * repelFactor;
                }
            }
        }

        private void PlaceMinionAtOrbitPosition(Player player, int index, int count)
        {
            if (count == 0) return;

            float angle = (MathHelper.TwoPi / count) * index;
            Vector2 targetPosition = player.Center + new Vector2(
                (float)Math.Cos(angle) * 64f,
                (float)Math.Sin(angle) * 64f
            );

            NPC.Center = targetPosition;
        }

        //public override void FindFrame(int frameHeight)
        //{
        //    if (NPC.ai[2] <= 1)
        //        NPC.frame.Y = 0;
        //    else if (NPC.ai[2] <= 2)
        //        NPC.frame.Y = frameHeight;
        //    else
        //        NPC.frame.Y = frameHeight * 2;
        //}
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 2;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.RottenEgg:
                    return false;

                case ProjectileID.AshBallFalling:
                case ProjectileID.CrimsandBallFalling:
                case ProjectileID.DirtBall:
                case ProjectileID.EbonsandBallFalling:
                case ProjectileID.MudBall:
                case ProjectileID.PearlSandBallFalling:
                case ProjectileID.SandBallFalling:
                case ProjectileID.SiltBall:
                case ProjectileID.SlushBall:
                    if (projectile.velocity.X == 0)
                        return false;
                    break;

                default:
                    break;
            }

            return null;
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (FargoSoulsUtil.CanDeleteProjectile(projectile))
            {
                projectile.timeLeft = 0;
                projectile.FargoSouls().canHurt = false;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int heart = NPC.ai[2] > 1 ? 1 : 0;

            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<CyberHitbox>(), NPC.damage, 6f, (int)NPC.ai[0], heart);

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric);
                    Main.dust[d].velocity *= 2.5f;
                    Main.dust[d].scale += 0.5f;
                }
            }
        }

        public override bool CheckActive() => false;

        public override bool PreKill() => false;
    }

    public class CyberHitbox : ModProjectile
    {
        public override string Texture => FargoSoulsUtil.EmptyTexture;

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.hide = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
        }

        public override bool? CanDamage()
        {
            Projectile.maxPenetrate = 1;
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = Main.player[Projectile.owner].Center.X > target.Center.X ? -1 : 1;
            target.AddBuff(BuffID.Confused, 60 * 10);
            target.AddBuff(BuffID.Electrified, 60 * 10);
        }
    }
}