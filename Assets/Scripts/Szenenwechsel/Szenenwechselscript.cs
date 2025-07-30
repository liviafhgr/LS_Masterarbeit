using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForPrefabInstanceAndLoadScene : MonoBehaviour
{
    [Tooltip("Ziehe hier das Prefab hinein, auf dessen Instanzierung gewartet werden soll.")]
    public GameObject prefabReference;

    public float delay = 6f;

    private bool isWaiting = false;

    void Update()
    {
        if (!isWaiting && prefabReference != null)
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name.StartsWith(prefabReference.name))
                {
                    isWaiting = true;
                    Invoke(nameof(LoadScene), delay);
                    break;
                }
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("abschlussanimation");
    }
}
