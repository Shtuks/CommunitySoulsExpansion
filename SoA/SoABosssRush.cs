using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using SacredTools.NPCs.Boss.Abaddon;
using SacredTools.NPCs.Boss.Araghur;
using CalamityMod.NPCs.DevourerofGods;
using SacredTools.NPCs.Boss.Lunarians;
using CalamityMod.NPCs.Providence;
using SacredTools.NPCs.Boss.Erazor;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ExoMechs;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using Terraria;
using Terraria.Audio;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    public class SoABossRush : ModSystem
    {
        internal static void SpawnTP()
        {
            ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Player player = enumerator.Current;
                player.Spawn(PlayerSpawnContext.RecallFromItem);
                SoundStyle style = TeleportSound with { Volume = 1.6f };
                SoundEngine.PlaySound(in style, player.Center);
            }
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SoABR;
        }
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<Providence>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Abaddon>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Polterghast>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurMinion>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurBody>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurTail>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<AraghurHead>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Novaniel>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Draedon>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<ErazorBoss>()));
                }
                //mweh
                //if (Bosses[i].EntityID == ModContent.NPCType<SupremeCalamitas>())
                //{
                //    Bosses.Insert(i+1, new Boss(ModContent.NPCType<Nihilus>(), TimeChangeContext.Night, type =>
                //    {
                //        int num8 = Player.FindClosest(new Vector2(Main.maxTilesX, Main.maxTilesY) * 16f * 0.5f, 1, 1);
                //        int tileType = ModContent.TileType<NihilusObeliskTile>();
                //        Point? obeliskOrigin = CSEUtils.FindNearestMultitile(Main.LocalPlayer.Center, tileType, 2000);

                //        num8.ToPlayer().Teleport(obeliskOrigin.Value.ToVector2());

                //        Point origin = new Point(obeliskOrigin.Value.X + 1, obeliskOrigin.Value.Y + 6);

                //        ArenaSystem.ActivateArena(Main.LocalPlayer.whoAmI, ArenaBoss.Nihilus, origin, new Point(-200, 0));
                //    }, -1, false, 0, [ModContent.NPCType<NihilusAbyssRock>(), ModContent.NPCType<Nihilus2Hand>(), ModContent.NPCType<NihilusAwaken>(), ModContent.NPCType<NihilusAwakenSpirit>(), 
                //        ModContent.NPCType<NihilusBarricade>(), ModContent.NPCType<NihilusChainBomb>(), ModContent.NPCType<NihilusChainBombVolatile>(), ModContent.NPCType<NihilusCrystal>(), 
                //        ModContent.NPCType<NihilusCrystalArrive>(), ModContent.NPCType<NihilusHandBeam>(), ModContent.NPCType<NihilusLantern>(), ModContent.NPCType<NihilusLantern>(), 
                //        ModContent.NPCType<NihilusLanternBig>(), ModContent.NPCType<NihilusLanternRisen>(), ModContent.NPCType<NihilusAgateGrab>()]));
                //}
            }

            BossIDsAfterDeath.Add(ModContent.NPCType<Nihilus>(), [ModContent.NPCType<Nihilus2>()]);
            BossDeathEffects.Add(ModContent.NPCType<Nihilus2>(), delegate
            {
                SpawnTP();
            });
        }
    }
}