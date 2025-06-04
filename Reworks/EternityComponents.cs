using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Content.Items.Accessories;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class EternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<UniverseSoul>())
            {
                player.maxMinions += 2;
            }

            //SoU
            if (ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    ModContent.Find<ModItem>(this.Mod.Name, "StalkerSoul").UpdateAccessory(player, hideVisual);
                }
            }
            if (ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "BardSoul").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(this.Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.BeekeeperClass.Loaded && (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "BeekeeperSoul").UpdateAccessory(player, hideVisual);
            }

            //SoE
            if (ModCompatibility.SacredTools.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "SoASoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Calamity.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "CalamitySoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Thorium.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "ThoriumSoul").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Polarities.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "WildernessForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(this.Mod.Name, "SpacetimeForce").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Spooky.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "TerrorForce").UpdateAccessory(player, hideVisual);
                ModContent.Find<ModItem>(this.Mod.Name, "HorrorForce").UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Redemption.Loaded && (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>()))
            {
                ModContent.Find<ModItem>(this.Mod.Name, "AdvancementForce").UpdateAccessory(player, hideVisual);
            }
        }
    }
}