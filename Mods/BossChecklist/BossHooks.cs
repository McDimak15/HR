using System;
using System.Reflection;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using MonoMod.RuntimeDetour;
using Terraria.ModLoader;
using Terraria.Localization;

namespace HomewardRagnarok.Mods.BossChecklist
{
    public class CoJBCLKeyChanger : ModSystem
    {
        private ILHook ilHook;

        public override void Load()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coJ))
                return;

            var bcClass = coJ.Code.GetType("ContinentOfJourney.CoJ_BossChecklist");
            if (bcClass == null)
                return;

            var postSetup = bcClass.GetMethod("PostSetupContent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (postSetup == null)
                return;

            ilHook = new ILHook(postSetup, IL_EditBossChecklistKeys);
        }

        private void IL_EditBossChecklistKeys(ILContext il)
        {
            var c = new ILCursor(il);
            const int scanAhead = 80;

            var map = new[]
            {
                new { Key = "CoJWallOfShadow",  Expected = 18.5f, NewVal = 18.49f },
                new { Key = "CoJOverwatcher",   Expected = 20.2f, NewVal = 19.51f },
                new { Key = "CoJMaterealizer",  Expected = 22.2f, NewVal = 19.52f },
                new { Key = "CoJLifebringer",   Expected = 21.2f, NewVal = 19.53f },
                new { Key = "CoJScarabBelief",  Expected = 24f,   NewVal = 20.7f  },
                new { Key = "CoJOrdeals",       Expected = 23f,   NewVal = 21.6f  },
                new { Key = "CoJWorldsEndEverlastingFallingWhale", Expected = 25f, NewVal = 21.7f },
                new { Key = "CoJSon",           Expected = 26f,   NewVal = 23.6f  }
            };

            
            foreach (var entry in map)
            {
                c.Index = 0;
                while (c.TryGotoNext(MoveType.After, i => i.MatchLdstr(entry.Key)))
                {
                    int ldstrIndex = c.Index - 1;
                    for (int offset = 1; offset <= scanAhead && (ldstrIndex + offset) < il.Instrs.Count; offset++)
                    {
                        var instr = il.Instrs[ldstrIndex + offset];
                        if (instr.OpCode == OpCodes.Ldc_R4 && instr.Operand is float foundFloat)
                        {
                            if (Math.Abs(foundFloat - entry.Expected) < 0.6f)
                            {
                                instr.Operand = entry.NewVal;
                            }
                            break;
                        }
                        if (instr.OpCode == OpCodes.Ldc_R8 && instr.Operand is double foundDouble)
                        {
                            float foundF = (float)foundDouble;
                            if (Math.Abs(foundF - entry.Expected) < 0.6f)
                            {
                                instr.OpCode = OpCodes.Ldc_R4;
                                instr.Operand = entry.NewVal;
                            }
                            break;
                        }
                    }
                }
            }
            

            c.Index = 0;
            while (c.TryGotoNext(MoveType.After, i => i.MatchLdstr("CoJSlimeGod")))
            {
                int start = c.Index;
                for (int offset = 1; offset <= scanAhead && (start + offset) < il.Instrs.Count; offset++)
                {
                    var instr = il.Instrs[start + offset];
                    if (instr.OpCode == OpCodes.Ldstr && instr.Operand is string strVal)
                    {
                        if (strVal.Contains("Mods.ContinentOfJourney.SG") || strVal.Contains("SG"))
                        {
                            instr.Operand = Language.GetTextValue("Mods.HomewardRagnarok.ModCompat.BossChecklistBoss.Checklist.SlimeGod");
                            return;
                        }
                    }
                }
            }
        }

        public override void Unload()
        {
            ilHook?.Dispose();
            ilHook = null;
        }
    }
}
