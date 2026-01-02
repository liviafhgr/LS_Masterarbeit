using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipIntroSkript : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "viagg-io hub";

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OpenScene);
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(OpenScene);
    }
}
