using UnityEngine.SceneManagement;

namespace QoL.Patches;

// SceneLoad (Weakness Scene, Camera pan issue, OldPatch & Fast Shakra)
internal static class OnSceneLoadPatch
{
    internal static void OnSceneLoad(Scene scene, LoadSceneMode lsm)
    {
        if (HeroController.UnsafeInstance == null)
            return;

        if (Configs.FasterNPC.Value && scene.name == "Bone_04")
            PlayerData.instance.metMapper = true;

        SkipWeakness(scene.name);
        OldPatch();
    }

    private static void OldPatch()
    {
        if (!Configs.CloaklessClawline.Value && !Configs.OldVoltVessels.Value)
            return;

        StartCoroutine(() =>
        {
            string sceneName = GameManager.instance.sceneName;
            
            if (Configs.CloaklessClawline.Value && sceneName == "Under_17")
            {
                GameObject obj = GameObject.Find("terrain collider (15)");
                obj.transform.position = new Vector3(12.22f, 7.64f, 0f);
                UObject.Destroy(obj.GetComponent<NonSlider>());
            } else if (Configs.OldVoltVessels.Value && sceneName == "Aqueduct_04")
            {
                BoxCollider2D rangeCollider = GameObject.Find("drop_planks").transform.GetChild(3).GetComponent<BoxCollider2D>();
                rangeCollider.size = new(rangeCollider.size.x, 40f);
            }
        }, 0.5f);
    }

    private static void SkipWeakness(string sceneName)
    {
        if (!Configs.SkipWeakness.Value)
            return;

        if (sceneName == "Bonetown" && !PlayerData.instance.churchKeeperIntro)
        {
            PlayerData.instance.churchKeeperIntro = true;

            StartCoroutine(() =>
            {
                GameObject.Find("Churchkeeper Intro Scene")
                    .LocateMyFSM("Control")
                    .SetState("Set End");
            }, 0.3f);
        }


        StartCoroutine(() =>
        {
            GameObject weaknessScene = GameObject.Find("Weakness Scene");

            if (sceneName == "Cog_09_Destroyed")
                weaknessScene = GameObject.Find("Weakness Cog Drop Scene");

            if (weaknessScene != null)
                weaknessScene.SetActive(false);
        }, 0.3f);
    }

    private static IEnumerator Delay(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }

    private static void StartCoroutine(Action action, float seconds)
    {
        if (HeroController.UnsafeInstance == null)
            return;
        
        HeroController.instance.StartCoroutine(Delay(seconds, action));
    }
}
