using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections.Generic;

namespace HomewardRagnarok.Compat
{
    [JITWhenModsEnabled("InfernumMode")]
    [ExtendsFromMod("InfernumMode")]
    public class CoJBossCardsSystem : ModSystem
    {
        internal static Mod CoJ;
        internal static Mod Infernum;

        public override void Load()
        {
            CoJ = ModLoader.GetMod("ContinentOfJourney");
            Infernum = ModLoader.GetMod("InfernumMode");
        }

        public override void PostSetupContent()
        {
            if (CoJ == null || Infernum == null)
                return;

            AddCoJBossIntros();
        }

        private void AddCoJBossIntros()
        {
            var bosses = new List<(string InternalName, Color Color, SoundStyle Tick, SoundStyle End)>
            {
                ("MarquisMoonsquid", Color.DeepSkyBlue, SoundID.MenuTick, SoundID.Item14),
                ("PriestessRod", Color.White, SoundID.MenuTick, SoundID.Item14),
                ("Diver", Color.Purple, SoundID.MenuTick, SoundID.Item14),
                ("TheMotherbrain", Color.Red, SoundID.MenuTick, SoundID.Item14),
                ("WallofShadow", Color.Black, SoundID.MenuTick, SoundID.Item14),
                ("SlimeGod", Color.Orange, SoundID.MenuTick, SoundID.Item14),
                ("TheOverwatcher", Color.Gold, SoundID.MenuTick, SoundID.Item14),
                ("TheLifebringerHead", Color.LightGreen, SoundID.MenuTick, SoundID.Item14),
                ("TheMaterealizer", Color.Cyan, SoundID.MenuTick, SoundID.Item14),
                ("ScarabBelief", Color.Yellow, SoundID.MenuTick, SoundID.Item14),
                ("WorldsEndEverlastingFallingWhale", Color.DeepSkyBlue, SoundID.MenuTick, SoundID.Item14),
                ("TheSon", Color.White, SoundID.MenuTick, SoundID.NPCHit13)
            };

            foreach (var boss in bosses)
            {
                int npcType = CoJ.Find<ModNPC>(boss.InternalName)?.Type ?? 0;
                if (npcType == 0)
                    continue;

                Func<bool> condition = () => NPC.AnyNPCs(npcType);
                Func<float, float, Color> colorFunc = (progress, anim) => Color.Lerp(Color.White, boss.Color, anim);

                var text = Mod.GetLocalization("Mods.HomewardRagnarok.CoJIntro." + boss.InternalName);

                object introScreen = Infernum.Call(
                    "InitializeIntroScreen",
                    text,
                    400,       // display time
                    true,    
                    condition,
                    colorFunc
                );

                Infernum.Call("SetupLetterAdditionSound", introScreen, (Func<SoundStyle>)(() => SoundID.MenuTick));
                Infernum.Call("IntroScreenSetupLetterAdditionSound", introScreen, new Func<SoundStyle>(() => boss.Tick));
                Infernum.Call("IntroScreenSetupMainSound", introScreen, new Func<int, int, float, float, bool>((t, _, __, ___) => t == 0), new Func<SoundStyle>(() => boss.End));

                Infernum.Call("IntroScreenSetupLetterDisplayCompletionRatio", introScreen, new Func<int, float>(t => t / 180f));
                Infernum.Call("IntroScreenSetupCompletionEffects", introScreen, new Action(() => { }));
                Infernum.Call("IntroScreenSetupTextScale", introScreen, 1f);

                Infernum.Call("RegisterIntroScreen", introScreen);
            }
        }
    }
}
