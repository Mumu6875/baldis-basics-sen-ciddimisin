using UnityEngine;
using UnityEngine.SceneManagement;

public static class MobileControlsBootstrap
{
    private const string SchoolScene = "School";
    private const string RigResource = "Mobile/CF2-Rig";
    private const string RigInstanceName = "SenCiddimisin-MobileControls";
    private const string CanvasName = "CF2-Canvas";
    private const string PanelName = "CF2-Panel";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Register()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != SchoolScene || !ShouldShowControls())
        {
            return;
        }

        GameObject rig = GameObject.Find(RigInstanceName);
        if (rig == null)
        {
            GameObject prefab = Resources.Load<GameObject>(RigResource);
            if (prefab == null)
            {
                Debug.LogError("Mobile control prefab is missing from Resources/Mobile/CF2-Rig.");
                return;
            }

            rig = Object.Instantiate(prefab);
            rig.name = RigInstanceName;
        }

        PrepareRig(rig);
    }

    private static void PrepareRig(GameObject rig)
    {
        rig.SetActive(true);

        Transform[] transforms = rig.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < transforms.Length; i++)
        {
            Transform current = transforms[i];

            if (current.name == CanvasName)
            {
                current.localScale = Vector3.one;
            }
            else if (current.name == PanelName)
            {
                current.gameObject.SetActive(true);
            }
        }
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
