using UnityEngine.SceneManagement;

namespace QoL.Patches.OldPatch;

// SceneLoad OldPatch
internal static class OnSceneLoadPatch
{
    internal static void OnSceneLoad(Scene from, Scene to)
    {
        if (Configs.CloaklessClawline.Value && to.name == "Under_17")
        {
            Plugin.Logger.LogDebug("Modifying Cloakless Clawline Wall Grab");
            GameObject obj = GameObject.Find("terrain collider (15)");
            obj.transform.position = new Vector3(12.22f, 7.64f, 0f);
            UObject.DestroyImmediate(obj.GetComponent<NonSlider>());
        } 
        
        else if (Configs.OldVoltVessels.Value && to.name == "Aqueduct_04")
        {
            BoxCollider2D rangeCollider = GameObject.Find("drop_planks").transform.GetChild(3).GetComponent<BoxCollider2D>();
            rangeCollider.size = new(rangeCollider.size.x * 2, 50f);
        }
    }
}
