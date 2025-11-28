using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForPrefabInstanceAndLoadScene : MonoBehaviour
{
    [Tooltip("Prefab, dessen Instanzierung überwacht wird.")]
    public GameObject prefabReference;

    [Tooltip("Send-Button, der nach Instanzierung deaktiviert wird.")]
    public GameObject sendButton;

    [Tooltip("Verzögerung bis zum Szenenwechsel (Sekunden).")]
    public float delay = 6f;

    private bool isWaiting = false;

    void Update()
    {
        if (isWaiting || prefabReference == null) return;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith(prefabReference.name))
            {
                if (sendButton != null) sendButton.SetActive(false);
                isWaiting = true;
                Invoke(nameof(LoadScene), delay);
                break;
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("abschlussanimation");
    }
}
