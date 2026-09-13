using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ModBrandingBootstrap
{
    public const string TitleText = "Sen Ciddimisin Reis?";
    private const string MainMenuScene = "MainMenu";
    private const string BrandingObject = "SenCiddimisin-Title";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Register()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != MainMenuScene || GameObject.Find(BrandingObject) != null)
        {
            return;
        }

        GameObject oldTitle = GameObject.Find("TitleImage");
        if (oldTitle != null)
        {
            oldTitle.SetActive(false);
        }

        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            return;
        }

        GameObject titleObject = new GameObject(BrandingObject, typeof(RectTransform), typeof(TextMeshProUGUI));
        titleObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = titleObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0f, -24f);
        rect.sizeDelta = new Vector2(900f, 120f);

        TextMeshProUGUI title = titleObject.GetComponent<TextMeshProUGUI>();
        title.text = TitleText;
        title.alignment = TextAlignmentOptions.Center;
        title.fontSize = 64f;
        title.fontStyle = FontStyles.Bold;
        title.color = new Color(1f, 0.92f, 0.1f, 1f);
        title.enableAutoSizing = true;
        title.fontSizeMin = 34f;
        title.fontSizeMax = 64f;
        title.raycastTarget = false;
    }
}
