using FargowiltasSouls.Content.Items;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria.ModLoader;
using ssm.Content.SoulToggles;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ssm.SoA.Emode.Accessories
{
    //public class FrozenWolfClaw : SoulsItem
    //{
    //    public override bool Eternity => true;

    //    public override void SetStaticDefaults()
    //    {
    //        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    //    }
    //    public override void SetDefaults()
    //    {
    //        Item.width = 20;
    //        Item.height = 20;
    //        Item.accessory = true;
    //        Item.rare = 1;
    //        Item.value = 400000;
    //    }

    //    public override void UpdateAccessory(Player player, bool hideVisual)
    //    {
    //        player.AddEffect<FrozenWolfClawEffect>(Item);
    //    }

    //    public class FrozenWolfClawEffect : AccessoryEffect
    //    {
    //        public override Header ToggleHeader => Header.GetHeader<FirstCreationHeader>();
    //        public override int ToggleItemType => ModContent.ItemType<FrozenWolfClaw>();
    //        public override bool ExtraAttackEffect => true;
    //        public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
    //        {
    //            if (hitInfo.Crit)
    //            {
    //                target.AddBuff(BuffID.Frostburn, 420);

    //                if (Main.rand.Next(4) == 0)
    //                {
    //                    Projectile.NewProjectile(
    //                        player.GetSource_OnHit(target),
    //                        target.Center,
    //                        Vector2.Zero,
    //                        ModContent.ProjectileType<FrostClawSlash>(),
    //                        35,
    //                        0,
    //                        player.whoAmI
    //                    );
    //                }
    //            }
    //        }
    //    }
    //}
}
