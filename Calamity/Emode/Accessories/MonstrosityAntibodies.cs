using FargowiltasSouls.Content.Items;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using FargowiltasSouls;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.DamageOverTime;

namespace ssm.Calamity.Emode.Accessories
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class MonstrosityAntibodies : SoulsItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.ExperimentalContent;
        }
        public override bool Eternity => true;
         
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Irradiated>()] = true;
            player.buffImmune[ModContent.BuffType<SulphuricPoisoning>()] = true;
            player.buffImmune[ModContent.BuffType<FishAlert>()] = true;
            player.buffImmune[ModContent.BuffType<RiptideDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<CrushDepth>()] = true;
            player.buffImmune[ModContent.BuffType<Eutrophication>()] = true;

            DamageClass damageClass = player.ProcessDamageTypeFromHeldItem();
            player.GetDamage(damageClass) += 0.35f;
        }
    }
}
