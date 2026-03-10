using System.Collections.Generic;
using System.Reflection.Emit;

namespace QoL.Patches.OldPatch;

[HarmonyPatch(typeof(TinkEffect), nameof(TinkEffect.TryDoTinkReactionNoDamager))]
internal static class TinkEffectPatch
{
    [HarmonyWrapSafe, HarmonyTranspiler]
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var code in instructions)
        {
            yield return code;

            if (code.opcode == OpCodes.Stloc_S && ((LocalBuilder)code.operand).LocalIndex == 11)
            {
                yield return new CodeInstruction(OpCodes.Ldloc_S, (byte)11);
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(OpCodes.Ldarg_1);
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TinkEffectPatch), nameof(OverrideFlag6)));
                yield return new CodeInstruction(OpCodes.Stloc_S, (byte)11);
            }
        }
    }

    static bool OverrideFlag6(bool orig, TinkEffect tink, GameObject obj)
    {
        if (Configs.OldVoltVessels.Value && tink.gameObject.name.StartsWith("Lightning Bola Ball") && !obj.name.StartsWith("Harpoon"))
            return false;

        return orig;
    }
}
