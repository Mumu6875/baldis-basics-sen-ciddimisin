using UnityEngine;
using UnityEngine.SceneManagement;

public static class MobileControlsBootstrap
{
    private const string SchoolScene = "School";
    private const string RigResource = "Mobile/CF2-Rig";
    private const string RigInstanceName = "SenCiddimisin-MobileControls";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Register()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != SchoolScene || !ShouldShowControls() || GameObject.Find(RigInstanceName) != null)
        {
            return;
        }

        GameObject prefab = Resources.Load<GameObject>(RigResource);
        if (prefab == null)
        {
            Debug.LogError("Mobile control prefab is missing from Resources/Mobile/CF2-Rig.");
            return;
        }

        GameObject rig = Object.Instantiate(prefab);
        rig.name = RigInstanceName;
        rig.SetActive(true);
    }

    private static bool ShouldShowControls()
    {
#if UNITY_EDITOR
        return true;
#else
        return Application.isMobilePlatform;
#endif
    }
}
