using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using Redemption.NPCs.Bosses.ADD;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.Providence;
using Redemption.NPCs.Bosses.PatientZero;
using Terraria.Audio;
using Terraria;
using Microsoft.Xna.Framework;
using Redemption.WorldGeneration;
using System;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.ExoMechs;
using Redemption.NPCs.Bosses.Neb;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    public class RedemptionBossRush : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.RedemptionBR;
        }
        internal static void SpawnTP()
        {
            ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Player player = enumerator.Current;
                player.Spawn(PlayerSpawnContext.RecallFromItem);
                SoundStyle style = TeleportSound with {Volume = 1.6f};
                SoundEngine.PlaySound(in style, player.Center);
            }
        }

        public Vector2 PZRoom()
        {
            ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                _ = enumerator.Current;
                Vector2 labVector = RedeGen.LabVector;
                int rectX = (int)(labVector.X + 109f) * 16;
                int rectY = (int)(labVector.Y + 170f) * 16;
                int width = 1120;
                int height = 672;

                Vector2 centerPos = new Vector2(
                    rectX + width / 2f,
                    rectY + height / 2f
                );

                return centerPos;
            }
            return new Vector2(0f, 0f);
        }
        public override void PostSetupContent()
        {
            Action<int> pz = delegate
            {
                int num8 = Player.FindClosest(new Vector2(Main.maxTilesX, Main.maxTilesY) * 16f * 0.5f, 1, 1);
                _ = Main.player[num8];
                NPC.SpawnOnPlayer(num8, ModContent.NPCType<PZ_Body_Holo>());
            };

            for (int i = Bosses.Count - 1; i >= 0; i--)
            {

                if (Bosses[i].EntityID == ModContent.NPCType<Providence>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<PZ_Body_Holo>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<PZ_Kari>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<PZ>(), TimeChangeContext.None, spawnContext: type => 
                    {
                        int num8 = Player.FindClosest(new Vector2(Main.maxTilesX, Main.maxTilesY) * 16f * 0.5f, 1, 1);
                        num8.ToPlayer().Teleport(PZRoom());
                        _ = Main.player[num8];
                        NPC.SpawnOnPlayer(num8, ModContent.NPCType<PZ_Body_Holo>());
                    }));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Polterghast>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Akka>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Ukko>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Draedon>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Nebuleus>(), TimeChangeContext.Night));
                }
            }

            BossDeathEffects.Add(ModContent.NPCType<PZ>(), delegate
            {
                SpawnTP();
            });
        }
    }
}