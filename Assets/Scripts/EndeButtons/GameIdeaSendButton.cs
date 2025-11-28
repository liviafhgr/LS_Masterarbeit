using UnityEngine;

public class OpenFHGRWebsiteButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://viaggio.fhgr.ch/de/");
    }
}
