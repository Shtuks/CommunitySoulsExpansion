using System;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm.Content.Items
{
    public abstract class SwarmSummon : ModItem
    {
        private int counter;

        private int npcType;

        private readonly int maxSpawn;

        private readonly string spawnMessageKey;

        private readonly string material;

        protected SwarmSummon(int npcType, string spawnMessageKey, int maxSpawn, string material)
        {
            npcType = npcType;
            spawnMessageKey = spawnMessageKey;
            maxSpawn = maxSpawn;
            material = material;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 100;
            Item.value = 10000;
            Item.rare = 1;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 5;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }
        public override bool? UseItem(Player player)
        {
            ssm.SwarmSetDefaults = true;
            ssm.SwarmActive = true;
            int num = (ssm.SwarmItemsUsed = Math.Min(player.inventory[player.selectedItem].stack, 10));
            ssm.SwarmNoHyperActive = ssm.SwarmItemsUsed < 5;

            int num2 = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), npcType);
            Main.npc[num2].GetGlobalNPC<CSENpcs>().SwarmActive = true;

            player.inventory[player.selectedItem].stack -= num - 1;
            if (Main.netMode == 2)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.ssm.MessageInfo." + spawnMessageKey), new Color(175, 75, 255));
                NetMessage.SendData(7);
            }
            else if (Main.netMode == 0)
            {
                Main.NewText(Language.GetTextValue("Mods.ssm.MessageInfo." + spawnMessageKey), 175, 75);
            }

            SoundEngine.PlaySound(in SoundID.Roar, player.position);
            ssm.SwarmSetDefaults = false;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(null, material).AddIngredient(null, "Overloader").AddTile(26)
                .Register();
        }
    }
}
