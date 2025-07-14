using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Core;
using System.Reflection;
using System;
using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Inheritance.Name)]
    [JITWhenModsEnabled(ModCompatibility.Inheritance.Name)]
    public class InheritanceMutantEnrage : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public static bool CheckCalStatInflationBACK()
        {
            Assembly calamityInheritance = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(asm => asm.GetName().Name.Equals("CalamityInheritance", StringComparison.OrdinalIgnoreCase));

            if (calamityInheritance == null)
                return false; 

            Type configType = calamityInheritance.GetType("CalamityInheritance.CIServerConfig");
            if (configType == null)
                return false;

            PropertyInfo instanceProp = configType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            if (instanceProp == null)
                return false; 

            object configInstance = instanceProp.GetValue(null);
            if (configInstance == null)
                return false; 

            PropertyInfo inflationProp = configType.GetProperty("CalStatInflationBACK");
            if (inflationProp == null)
                return false; 

            return (bool)inflationProp.GetValue(configInstance);
        }
        public override void SetDefaults(NPC npc)
        {
            if (CheckCalStatInflationBACK())
            {
                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.damage = 2000;
                    npc.lifeMax = 177000000;
                }
                if (npc.type == ModContent.NPCType<AbomBoss>())
                {
                    npc.damage = 500;
                    npc.lifeMax = 7700000;
                }
            }
        }
    }
}