using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Buffs;
using ssm.Content.Buffs;
using ssm.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAPlayer : ModPlayer
    {
        public int rivalStreak;
        public int EerieEnchantCooldown;
        public int SpaceJunkCooldown;
        public override void PreUpdateMovement()
        {
            if (Player.HasBuff(ModContent.BuffType<SniperBuff>()))
            {
                Player.controlLeft = false;
                Player.controlRight = false;
                Player.controlJump = false;
                Player.controlDown = false;
                Player.controlUp = false;
                Player.velocity = Vector2.Zero;
                Player.gravity = 0f;
                Player.fallStart = (int)(Player.position.Y / 16f);
            }
        }

        public override void UpdateEquips()
        {
            if (Player.GetModPlayer<ModdedPlayer>().DragonSetEffect)
            {
                Player.GetDamage(DamageClass.Generic) += 0.05f;
            }
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (Player.HasBuff(ModContent.BuffType<SniperBuff>()))
            {
                damage += 1.25f;
            }
            else if (Player.HasBuff(ModContent.BuffType<SniperCooldownBuff>()))
            {
                damage *= 0.75f;
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAPlayerTest : ModPlayer
    {
        public bool flariumWings;

        public override void ResetEffects()
        {
            flariumWings = false;
        }
    }
}
