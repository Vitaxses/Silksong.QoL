namespace QoL.FSMEdits;

public static class FastMenu
{
    internal static void ShopUI(PlayMakerFSM fsm)
    {
        if (!Configs.FastUI.Value || Configs.SlowerOptions.Value)
            return;

        FsmFloat fadeTime = fsm.FindFloatVariable("Fade Time")!;
        if (fadeTime != null) fadeTime.RawValue = 0.1f;

        if (fsm.FsmName == "ui_list_item" && (fsm.name == "No" || fsm.name == "Yes"))
        {
            fsm.GetState("Chosen")!.GetLastActionOfType<Wait>()!.time = 0f;
        }

        else if (fsm.FsmName == "shop_control")
        {
            fsm.GetState("Down")!.DisableActionsOfType<Wait>();
            fsm.GetState("Open")!.DisableActionsOfType<Wait>();
        }

        else if (fsm.FsmName == "Confirm Control" && fsm.name == "UI List")
        {
            fsm.GetState("Particles")!.GetFirstActionOfType<Wait>()!.time = 0.1f;
        }

        else if (fsm.FsmName == "ui_list")
        {
            fsm.GetState("Selection Made")!.GetFirstActionOfType<Wait>()!.time = 0.15f;
            foreach (FsmState state in fsm.FsmStates)
            {
                if (!state.Name.Contains("Left") && !state.Name.Contains("Right") && !state.Name.Contains("Up") && !state.Name.Contains("Down"))
                {
                    continue;
                }

                var wait = state.GetFirstActionOfType<Wait>();
                if (wait == null)
                {
                    continue;
                }

                wait.time = 0.8f;
            }
        }

        else if (fsm.FsmName == "Shift_pos")
        {
            fsm.GetState("Tween")!.GetFirstActionOfType<iTweenMoveTo>()!.time.Value *= 0.13f;
        }
    }

    internal static void QuestUIPrompt(PlayMakerFSM fsm)
    {
        if (!Configs.FastUI.Value)
            return;

        if (fsm.FsmName != "Control" || (fsm.name != "Wish Granted Prompt New(Clone)" && fsm.name != "Wish Promised Prompt(Clone)"))
        {
            return;
        }

        var animator = fsm.gameObject.transform.GetChild(1).GetComponent<Animator>();

        if (animator != null)
            animator.speed = 2f;

        fsm.GetState("Idle")!.GetFirstActionOfType<Wait>()!.time = 1f;
        fsm.GetState("Fade Down")!.DisableActionsOfType<Wait>();
        fsm.GetState("Explainer Up")!.DisableActionsOfType<Wait>();
    }

}
