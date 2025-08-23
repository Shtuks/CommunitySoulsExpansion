using Microsoft.Xna.Framework;
using ssm.Core;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX
{
    [JITWhenModsEnabled(ModCompatibility.Infernum.Name)]
    [ExtendsFromMod(ModCompatibility.Infernum.Name)]
    public class SomeExperimenting : ModSystem
    {
        public override void PostSetupContent()
        {
            AddCard(ModContent.NPCType<RealMutantEX>(), (float horz, float anim) => Color.Lerp(Color.Teal, Color.Cyan, anim), "RealMutantEX", SoundID.DD2_BetsyFireballShot, new("FargowiltasSouls/Assets/Sounds/DifficultyEmode"));
        }
        public void AddCard(int type, Func<float, float, Color> color, string title, SoundStyle tickSound, SoundStyle endSound, int time = 300, float size = 1f)
        {
            AddCard(() => NPC.AnyNPCs(type), color, title, tickSound, endSound, time, size);
        }

        public void AddCard(Func<bool> condition, Func<float, float, Color> color, string title, SoundStyle tickSound, SoundStyle endSound, int time = 300, float size = 1f)
        {
            object instance = ModCompatibility.Infernum.Mod.Call("InitializeIntroScreen", Mod.GetLocalization("Infernum." + title), time, true, condition, color);
            ModCompatibility.Infernum.Mod.Call("IntroScreenSetupLetterDisplayCompletionRatio", instance, (Func<int, float>)((int animationTimer) => MathHelper.Clamp((float)animationTimer / (float)time * 1.36f, 0f, 1f)));
            Action onCompletionDelegate = delegate
            {
            };
            ModCompatibility.Infernum.Mod.Call("IntroScreenSetupCompletionEffects", instance, onCompletionDelegate);
            Func<SoundStyle> chooseLetterSoundDelegate = () => tickSound;
            ModCompatibility.Infernum.Mod.Call("IntroScreenSetupLetterAdditionSound", instance, chooseLetterSoundDelegate);
            Func<SoundStyle> chooseMainSoundDelegate = () => endSound;
            Func<int, int, float, float, bool> why = (int _, int _2, float _3, float _4) => true;
            ModCompatibility.Infernum.Mod.Call("IntroScreenSetupMainSound", instance, why, chooseMainSoundDelegate);
            ModCompatibility.Infernum.Mod.Call("IntroScreenSetupTextScale", instance, size);
            ModCompatibility.Infernum.Mod.Call("RegisterIntroScreen", instance);
        }
    }
}
