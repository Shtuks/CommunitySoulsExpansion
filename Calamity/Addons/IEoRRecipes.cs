using FargowiltasSouls.Core.Globals;
using FargowiltasSouls;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Bosses.MutantBoss;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.IEoR.Name)]
    [JITWhenModsEnabled(ModCompatibility.IEoR.Name)]
    public class IEoRRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (ModCompatibility.SacredTools.Loaded)
                {
                    if ((recipe.HasResult(ModCompatibility.IEoR.Mod.Find<ModItem>("Swordofthe14thGlitch")) ||
                        recipe.HasResult(ModCompatibility.IEoR.Mod.Find<ModItem>("NovaBomb")) ||
                        recipe.HasResult(ModLoader.GetMod("InfernumMode").Find<ModItem>("Kevin"))
                        ) && !recipe.HasIngredient(ModContent.Find<ModItem>("EmberofOmen")))
                    {
                        recipe.AddIngredient(ModContent.Find<ModItem>("EmberofOmen"), 3);
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.IEoR.Name)]
    [JITWhenModsEnabled(ModCompatibility.IEoR.Name)]
    public class IeORItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void ModifyWeaponDamage(Item entity, Player player, ref StatModifier damage)
        {
            if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>())){
                if (entity.type == ModCompatibility.IEoR.Mod.Find<ModItem>("Swordofthe14thGlitch").Type)
                {
                    damage *= 0.1f;
                }
                if (entity.type == ModCompatibility.IEoR.Mod.Find<ModItem>("NovaBomb").Type)
                {
                    damage *= 0.01f;
                }
                if (entity.type == ModLoader.GetMod("InfernumMode").Find<ModItem>("Kevin").Type)
                {
                    damage *= 0.1f;
                }
            }
        }
    }
}
