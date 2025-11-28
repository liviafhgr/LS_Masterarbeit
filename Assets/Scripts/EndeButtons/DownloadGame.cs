using UnityEngine;

public class OpenUrlButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenViaggioUrl()
    {
        Application.OpenURL("https://play.unity.com/en/games/e2b52e47-f5bd-4bec-bb31-8b8622533b8a/festival-escape-rabadan-version-3");
    }
}
