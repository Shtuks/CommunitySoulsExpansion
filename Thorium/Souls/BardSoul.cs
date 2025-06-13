using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BardSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            player.GetDamage<BardDamage>() += 0.22f;
            player.GetCritChance<BardDamage>() += 0.10f;
            player.GetAttackSpeed<BardDamage>() += 0.15f;
            player.GetModPlayer<ThoriumPlayer>().bardBuffDuration += 3000;
            player.GetModPlayer<ThoriumPlayer>().inspirationRegenBonus += 1;
            player.GetModPlayer<ThoriumPlayer>().bardResourceDropBoost += 1;
            player.GetModPlayer<ThoriumPlayer>().bardResource += 20;
            player.GetModPlayer<ThoriumPlayer>().bardHomingSpeedBonus += 10;
            player.GetModPlayer<ThoriumPlayer>().bardHomingRangeBonus += 10;
            player.GetModPlayer<ThoriumPlayer>().bardBounceBonus = 10;

            if (!ModCompatibility.CalBardHealer.Loaded)
            {
                player.GetModPlayer<ThoriumPlayer>().accWindHoming = true;
                player.GetModPlayer<ThoriumPlayer>().accBrassMute2 = true;
                player.GetModPlayer<ThoriumPlayer>().accPercussionTuner2 = true;
            }
            else
            {
                ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "OmniSpeaker").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "TreeWhispererAmulet").UpdateAccessory(player, false);
            }

        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "BardEssence");
            if (ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }

            if (!ModCompatibility.CalBardHealer.Loaded)
            {
                recipe.AddIngredient<HauntingBassDrum>();
                recipe.AddIngredient<HellBell>();
                recipe.AddIngredient<JingleBells>();
                recipe.AddIngredient<CallofCthulhu>();
                recipe.AddIngredient<TerrariumSurroundSound>();
                recipe.AddIngredient<TerrariumAutoharp>();
                recipe.AddIngredient<DigitalTuner>();
                recipe.AddIngredient<EpicMouthpiece>();
                recipe.AddIngredient<GuitarPickClaw>();
                recipe.AddIngredient<StraightMute>();
                recipe.AddIngredient<BandKit>();
                recipe.AddIngredient<Fishbone>();
                recipe.AddIngredient<SonicAmplifier>();
            }
            else
            {
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "OmniSpeaker"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "TreeWhispererAmulet"));
                recipe.AddIngredient<TheSet>();
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "TreeWhisperHarp"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "FeralKeytar"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "UnbreakableCombetUkulele"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "FaceMelter"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "SongoftheCosmos"));
                recipe.AddIngredient(ModContent.Find<ModItem>(ModCompatibility.CalBardHealer.Name, "YharimsJam"));
            }

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
