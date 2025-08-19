using CalamityHunt;
using CalamityHunt.Common.Utilities;
using CalamityHunt.Content.NPCs.Bosses.GoozmaBoss;
using FargowiltasSouls.Assets.ExtraTextures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Core;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ssm.Content.Items.Consumables
{
    [ExtendsFromMod(ModCompatibility.Goozma.Name)]
    [JITWhenModsEnabled(ModCompatibility.Goozma.Name)]
    public class EternalAuricSoul : ModItem
    {
        public int WorldExistTime;
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Main.LocalPlayer.CSE().eternalMonolith = true;

            WorldExistTime++;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color value = new GradientColor(SlimeUtils.GoozColors, 0.5f, 0.5f).ValueAt(Main.GlobalTimeWrappedHourly * 30f - 10f);
            Color secColor = Color.Teal;
            return Color.Lerp(value, secColor, 0.3f + MathF.Sin(Main.GlobalTimeWrappedHourly * 0.1f) * 0.1f);
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            Texture2D glowTexture = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/BloomCircleSmall").Value;

            Color glowColor = GetAlpha(Color.White) ?? Color.White;
            spriteBatch.Draw(texture, position, frame, glowColor, 0, frame.Size() * 0.5f, scale + 0.2f, 0, 0);
            spriteBatch.Draw(texture, position, frame, new Color(200, 200, 200, 0), 0, frame.Size() * 0.5f, scale + 0.2f, 0, 0);
            spriteBatch.Draw(glowTexture, position, glowTexture.Frame(), (glowColor with { A = 0 }) * 0.7f, 0, glowTexture.Size() * 0.5f, scale * 0.3f, 0, 0);

            return false;
        }

        public override bool OnPickup(Player player)
        {
            for (int i = 0; i < 150; i++)
            {
                Color glowColor = Color.Lerp(GetAlpha(Color.White).Value, Color.Teal, Main.rand.NextFloat(0.5f) + 0.3f);
                glowColor.A = 0;
                Dust.NewDustPerfect(Item.Center, 264, Main.rand.NextVector2Circular(10f, 10f), 0, glowColor, Main.rand.NextFloat(2f)).noGravity = true;
            }
            player.GetModPlayer<CSEAuricSoulPlayer>().eternalSoul = true;
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            bool includeLensFlare = true;
            Texture2D texture = TextureAssets.Item[Type].Value;
            Texture2D sparkTexture = AssetDirectory.Textures.Sparkle.Value;
            Texture2D glowTexture = AssetDirectory.Textures.Glow[1].Value;
            Color glowColor = GetAlpha(Color.Teal).Value;
            glowColor.A = 0;
            Color darkColor = Color.Lerp(glowColor, Color.Teal, 0.7f);
            darkColor.A = 0;
            float soulScale = scale;
            scale = 1f + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f % ((float)Math.PI * 2f)) * 0.01f;

            if (includeLensFlare)
            {
                float lensScale = scale + MathF.Sin(Main.GlobalTimeWrappedHourly * 24f) * 0.2f;
                float lensScaleSlow = scale + MathF.Sin(Main.GlobalTimeWrappedHourly * 24f + 3f) * 0.15f;
                float num = Main.GlobalTimeWrappedHourly * 0.1f;
                float lensRotation = num % ((float)Math.PI * 2f);
                float lensRotationSlow = (num + MathF.Sin(num * 2f) * 0.3f) % ((float)Math.PI * 2f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), darkColor, lensRotation - (float)Math.PI / 4f, sparkTexture.Size() * 0.5f, new Vector2(0.3f, 1f + lensScale * 5f), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), darkColor, lensRotation + (float)Math.PI / 4f, sparkTexture.Size() * 0.5f, new Vector2(0.3f, 1f + lensScale * 5f), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), glowColor, lensRotation - (float)Math.PI / 4f, sparkTexture.Size() * 0.5f, new Vector2(1f, 1f + lensScale), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), glowColor, lensRotation + (float)Math.PI / 4f, sparkTexture.Size() * 0.5f, new Vector2(1f, 1f + lensScale), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), darkColor, lensRotationSlow, sparkTexture.Size() * 0.5f, new Vector2(0.3f, 2f + lensScaleSlow * 5f), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), darkColor, lensRotationSlow + (float)Math.PI / 2f, sparkTexture.Size() * 0.5f, new Vector2(0.3f, 2f + lensScaleSlow * 5f), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), glowColor, lensRotationSlow, sparkTexture.Size() * 0.5f, new Vector2(1f, 2f + lensScaleSlow), SpriteEffects.None, 0f);
                spriteBatch.Draw(sparkTexture, Item.Center - Main.screenPosition, sparkTexture.Frame(), glowColor, lensRotationSlow + (float)Math.PI / 2f, sparkTexture.Size() * 0.5f, new Vector2(1f, 2f + lensScaleSlow), SpriteEffects.None, 0f);
                spriteBatch.Draw(glowTexture, Item.Center - Main.screenPosition, glowTexture.Frame(), glowColor * 0.05f, 0f, glowTexture.Size() * 0.5f, lensScale * 1.5f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(glowTexture, Item.Center - Main.screenPosition, glowTexture.Frame(), darkColor * 0.1f, 0f, glowTexture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            Rectangle frame = Main.itemAnimations[Type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            spriteBatch.Draw(texture, Item.Center - Main.screenPosition, frame, this.GetAlpha(Color.White).Value, 0f, frame.Size() * 0.5f, soulScale + 0.2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, Item.Center - Main.screenPosition, frame, new Color(255, 255, 255, 0), 0f, frame.Size() * 0.5f, soulScale + 0.2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(glowTexture, Item.Center - Main.screenPosition, glowTexture.Frame(), darkColor * 0.7f, 0f, glowTexture.Size() * 0.5f, scale * 0.2f, SpriteEffects.None, 0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[Type] = true;
            ItemID.Sets.IgnoresEncumberingStone[Type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            ItemID.Sets.ItemIconPulse[Type] = true;
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
        }
    }
    [ExtendsFromMod(ModCompatibility.Goozma.Name)]
    [JITWhenModsEnabled(ModCompatibility.Goozma.Name)]
    public class CSEAuricSoulPlayer : ModPlayer
    {
        public bool eternalSoul;
        //public override void PostUpdate()
        //{
        //    if (eternalSoul)
        //    {
        //        Player.GetDamage(DamageClass.Generic) += 0.03f;
        //        Player.GetCritChance(DamageClass.Generic) += 0.03f;
        //        Player.GetKnockback(DamageClass.Summon) += 0.03f;
        //        Player.moveSpeed += 0.03f;
        //        Player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
        //        Player.endurance += 0.01f;
        //        Player.statDefense += 3;
        //       // Player.statLifeMax2 += 10;
        //       //Player.statManaMax2 += 20;
        //    }
        //}

        //public override void SaveData(TagCompound tag)
        //{
        //    if (eternalSoul)
        //    {
        //        tag["eternalSoul"] = true;
        //    }

        //    SaveData(tag);
        //}

        //public override void LoadData(TagCompound tag)
        //{
        //    if (tag.ContainsKey("eternalSoul"))
        //    {
        //        eternalSoul = tag.GetBool("eternalSoul");
        //    }

        //    LoadData(tag);
        //}

        //public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        //{
        //    ModPacket packet = Mod.GetPacket();
        //    packet.Write(eternalSoul);
        //    packet.Send(toWho, fromWho);
        //}

        //public void ReceivePlayerSync(BinaryReader reader)
        //{
        //    eternalSoul = reader.ReadBoolean();
        //}

        //public override void CopyClientState(ModPlayer targetCopy)
        //{
        //    CSEAuricSoulPlayer auricSoulPlayer = (CSEAuricSoulPlayer)targetCopy;
        //    auricSoulPlayer.eternalSoul = eternalSoul;
        //}

        //public override void SendClientChanges(ModPlayer clientPlayer)
        //{
        //    CSEAuricSoulPlayer auricSoulPlayer = (CSEAuricSoulPlayer)clientPlayer;
        //    if (eternalSoul != auricSoulPlayer.eternalSoul)
        //    {
        //        SyncPlayer(-1, Main.myPlayer, newPlayer: false);
        //    }
        //}
    }
}
