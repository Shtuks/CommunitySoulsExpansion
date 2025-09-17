using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using Microsoft.Xna.Framework;
using ssm.Content.NPCs.RealMutantEX;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles
{
    public class MutantYap : ModProjectile
    {
        public override string Texture => FargoSoulsUtil.EmptyTexture;

        public string[] enterP3 =
        [
            "NO MATTER HOW MANY TIMES YOU FIGHT THIS BATTLE, YOU WILL NEVER WIN!", "I AM A CRUEL GOD OF ETERNITY.", " I WIELD THE BRANCHES OF YGGDRASIL AND RAGNORAK", "THE SWIRLING BRIMSTONE OF CALAMITY", "THE CEASELESS SHADOWS OF SACRED TOOLS", "THE BLUE-TINTED MAGICKS", "THE HALLOWED SPIRITS OF ARCANE LANDS", "THE SOARING POWER OF ASCENDED AVALON", "THE ALL-POWERFUL BLESSING OF THE ULTRASEER'S NOVA", "THE AWAKENED ESSENCE OF LOST ANCIENTS", "THE ALL-SEARING LIGHT AND SHADE OF THE RADIANT UNIVERSAL SPLIT", "THE WORLD AND CITY SHAPING TOOLS OF LUI", "AND POWER OF WORLD EDITING MACHINES OF CAT!", "RAGNAROK WILL END YOUR CHILDISH RAMPAGE!", "YOU WILL BE BURNED AS THE SUNKEN SEA WAS!", "THE TRUE FURY OF THE TYRANT WILL ANNIHILATE YOUR TINY POWER!", "YOU ARE NOTHING COMPARED TO THE ERODED SPIRITS!", "I BRING FORTH THE END UPON THE FOOLISH, THE UNWORTHY!", "YOU WANT TO DEFEAT ME?", "MAYBE IN TWO ETERNITIES!", "DIE, FOOLISH TERRARIAN!", "THEY SAID THERE WAS 3 END BRINGERS...",
            "BUT I AM THE FOURTH, A BREAKER OF REALITY!", "HELL DOESN’T ACCEPT SCUM LIKE YOU, SO SUFFER FOREVER IN MY ENDLESS ONSLAUGHT!", "MY INFINITE POWER!!", "THE POTENTIAL OF ETERNITIES STRETCHED TO THE ABSOLUTE MAXIMUM APOTHEOSES!", "YOUR UNHOLY SOUL SHALL BE CONSUMED BY DEPTHS LOWER THAN THE DEEPEST REACHES OF HELL!", "I CONTROL THE POWER THAT HAS REACHED FROM THE FAR ENDS OF THE UNIVERSE!", "UNITING DIMENSIONS, MANIPULATING TERRARIA, SLAYING MASOCHIST, AND JUDGING HEAVENS!!!", "FOR CENTURIES I HAVE TRAINED FOR ONE GOAL ONLY:", "PURGE THE WORLD OF THE UNWORTHY, SLAY THE WEAK, AND BRING FORTH TRUE POWER.", "IN THE HIGHEST REACHES OF HEAVEN, MY BROTHER RULES OVER THE SKY!",
            "SOON, ALL OF TERRARIA WILL BE PURGED OF THE UNWORTHY AND A NEW AGE WILL START!", "A NEW AGE OF AWESOME!", "A GOLDEN AGE WHERE ONLY ABSOLUTE BEINGS EXIST!", "DEATH, INFERNO, TIDE; I AM THE OMEGA AND THE ALPHA, THE BEGINNING, AND THE END!", "ALMIGHTY POWER; REVELATIONS.", "ABSOLUTE BEING, ABSOLUTE PURITY.", "WITHIN THE FOOLISH BANTERINGS OF THE MORTAL WORLD I HAVE ACHIEVED POWER!", "POWER THAT WAS ONCE BANISHED TO THE EDGE OF THE GALAXY!", "I BRING FORTH CALAMITIES, CATASTROPHES, AND CATACLYSM;", "ELDRITCH POWERS DERIVED FROM THE ABSOLUTE WORD OF FATE.",
            "FEEL MY UBIQUITOUS WRATH DRIVE YOU INTO THE GROUND!", "JUST AS A WORLD SHAPER DRIVES HIS WORLD INTO REALITY!", "THE SHARPSHOOTER’S EYE PALES IN COMPARISON OF MY PERCEPTION OF REALITY!", "THE BERSERKER'S RAGE IS NAUGHT BUT A BUNNY'S BEFORE MINE!", "THE OLYMPIANS ARE MERE LESSER GODS COMPARED TO MY IMMEASURABLE MIGHT!", "THE ARCH WIZARD'S A POSER, A HACK, A PARLOUR TRICK TOTING JOKER!", "A MASTERY OF FLIGHT AND THE IRON WILL OF A COLOSSUS ARE BOTH ELEMENTARY CONCEPTS!", "A CONJURER IS BUT A PEDDLING MAGICIAN!", "A TRAWLER IS BUT A SLIVER COMPARED TO MY LIFE MASTERY!", "SUPERSONIC SPEED, LIGHTSPEED TIME!",
            "GLORIOUS LIGHT SHALL ELIMINATE YOU, YOU FOOLISH BUFFOON!", "WHAT ARE YOUR TRUE INTENTIONS?", "WHY DO YOU REALLY EVEN NEED THIS POWER?", "WHAT IS THE POINT IN ALL OF THIS!?", "TO THINK YOU WERE SATISFIED WITH THE PROSPERITY OF THIS LAND!", "SAFETY AMONGST THE TOWN, PROTECTION OF THE EVIL THREATS, BUT NO!", "YOU WANTED MORE!", "YOU JUST WANTED TO SPITE ME, DIDN’T YOU!?", "ENOUGH OF THIS!", "I CAN’T KEEP GOING MUCH LONGER!",
            "YOU CANNOT KILL ME, THIS IS JUST THE ACT OF AN INSIGNIFICANT LUNATIC!", "I WILL SOON RETURN FOR ANOTHER BATTLE!", "THIS IS ONLY THE BEGINNING!", "DO YOU HONESTLY THINK YOU CAN SURVIVE ANY LONGER?", "POWER IS IN THE EYE OF THE BEHOLDER!", "YOU ARE NOT DESERVING OF TRUE DIVINITY!", "I SHOULD KNOW FROM THE COUNTLESS YOU HAVE SLAUGHTERED!", "YOUR MOTIVATION IS UNFOUNDED!", "ALL YOU SEEK IS THE DESTRUCTION OF ANYONE WHO POSES A THREAT TO YOU!", "THAT’S WHY YOU LIMP ON THIS MOIST ROCK!",
            "SEARCHING FOR STICKS AND PEBBLES TO ELIMINATE THE FEARS YOU CANNOT TRULY OVERCOME!", "IT REALLY SURPRISES ME THAT IT TOOK YOU AWHILE TO REACH THIS POINT!", "ESPECIALLY GIVEN HOW WELL YOU LEECH OFF YOUR OPPONENTS AFTER BARELY SCRAPING BY!", "THAT’S HOW YOU WIN ALL YOUR BATTLES, AM I RIGHT!?", "IT’S HONESTLY IMPRESSIVE THAT YOU’VE MADE IT THIS FAR.", "I HOPE YOU WEREN’T USING GODMODE, PATHETIC COWARD!", "YOU MUST BE SO NERVOUS THAT YOU’RE SO CLOSE!", "I HOPE YOU CHOKE, AND I HOPE YOU CHOKE ON THE ASH AND BLOOD OF YOUR SINS TOO!", "GLORIOUS LIGHT SHALL ELIMINATE YOU, YOU FOOLISH BUFFOON!",
            "THIS IS IT!", "NOW LET'S GET TO THE GOOD PART!!!!!"
        ];
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.npc[(int)Projectile.ai[0]].target.ToPlayer().FargoSouls().TerrariaSoul)
            {
                EdgyBossText(Main.npc[(int)Projectile.ai[0]], GFBQuote(1));
            }
            if (Main.npc[(int)Projectile.ai[0]].localAI[3] == 0)
            {
                EdgyBossText(Main.npc[(int)Projectile.ai[0]], GFBQuote(2));
            }
        }
        public override void AI()
        {
            int ai0 = (int)Projectile.ai[0];

            if (ai0 <= -1 || ai0 >= 200 || !Main.npc[ai0].active || (Main.npc[ai0].type != ModContent.NPCType<MutantBoss>() && Main.npc[ai0].type != ModContent.NPCType<RealMutantEX>()))
            {
                Projectile.Kill();
                return;
            }

            NPC npc = Main.npc[ai0];
            Projectile.Center = npc.Center;
            Projectile.timeLeft = 10;

            if (npc.ai[0] < 0f && (Projectile.localAI[1] += 1f) > 25f)
            {
                Projectile.localAI[1] = 0f;
                if (Projectile.ai[1] < enterP3.Length)
                    EdgyBossText(npc, enterP3[(int)Projectile.ai[1]++]);
            }

            //if (npc.ai[0] == 52)
            //{
            //    EdgyBossText(npc, RandomObnoxiousQuote());
            //}

            //if (npc.ai[0] == 4)
            //{
            //    EdgyBossText(npc, GFBQuote(4));
            //}

            //if (npc.ai[0] == 10)
            //{
            //    EdgyBossText(npc, GFBQuote(3));
            //}

            //if (npc.ai[0] == 1)
            //{
            //    EdgyBossText(npc, GFBQuote(3));
            //}

            //if (npc.ai[0] == 2)
            //{
            //    EdgyBossText(npc, RandomObnoxiousQuote());
            //}

            //if (npc.ai[0] == 8)
            //{
            //    EdgyBossText(npc, GFBQuote(6));
            //}

            //if (npc.ai[0] == 5)
            //{
            //    EdgyBossText(npc, GFBQuote(5));
            //}

            //if (npc.ai[0] == 9)
            //{
            //    EdgyBossText(npc, GFBQuote(7));
            //}

            //if (npc.ai[0] == 45)
            //{
            //    EdgyBossText(npc, GFBQuote(8));
            //}

            //if (npc.target.ToPlayer().FargoSouls().TerrariaSoul && npc.ai[0] == 10)
            //{
            //    EdgyBossText(npc, GFBQuote(1));
            //}

            //if (npc.ai[0] == 45)
            //{
            //    EdgyBossText(npc, GFBQuote(8));
            //}
        }
        private void EdgyBossText(NPC npc, string text)
        {
            if (npc.HasValidTarget && npc.Distance(Main.player[npc.target].Center) < 5000f)
            {
                CSEUtils.DisplayLocalizedText(text, Color.Cyan);
            }
        }
        private string RandomObnoxiousQuote() => Language.GetTextValue($"Mods.FargowiltasSouls.NPCs.MutantBoss.GFBText.Random{Main.rand.Next(71)}");
        private string GFBQuote(int num) => Language.GetTextValue($"Mods.FargowiltasSouls.NPCs.MutantBoss.GFBText.Quote{num}");
    }
}