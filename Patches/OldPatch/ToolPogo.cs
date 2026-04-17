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
        //Plugin.Logger.LogDebug("TryDoTinkReactionNoDamager: orig: " + orig + " | Tink: " + tink.name + " | " + obj.name);

        if (Configs.OldDelversDrillSnareSetter.Value && (obj.name == "Screw Attack Damager" || obj.name == "Snare Loop Damager"))
        {
            if (tink.name.StartsWith("Lightning Bola Ball"))
                return false;

            if (tink.name == "Pollen Shot(Clone)")
                return false;

            if (tink.name == "Sprite" && tink.transform.parent != null && tink.transform.parent.name == "Hero Conch Projectile(Clone)")
                return false;

            if (tink.name == "Throwing Bell(Clone)")
                return false;
        }

        return orig;
    }
}
