using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using gunrightsmod.Content.Items;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.UI.Elements;
using gunrightsmod.Content.Projectiles;
using FargowiltasSouls.Common.Utilities;
using ssm.gunrightsmod.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using System;
using Terraria.GameContent.Bestiary;
using Terraria.UI;
using FargowiltasSouls;
using static ssm.gunrightsmod.Enchantments.PlasticEnchant.PlasticEffect;


namespace ssm.gunrightsmod
{
    public class CSEgunrightsmodProjectiles : GlobalProjectile
    {
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo hurtInfo)
        {
            Player attacker = Main.player[projectile.owner];
            TryApplyMicroplastics(attacker, target);
        }
    }
}