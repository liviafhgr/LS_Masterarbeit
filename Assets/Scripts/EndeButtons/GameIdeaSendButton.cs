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
        Application.OpenURL("https://www.fhgr.ch/fh-graubuenden/angewandte-zukunftstechnologien/institut-fuer-multimedia-production-imp/viagg-io-entdeckungsreise-in-die-sprache-und-kultur-der-italienischsprachigen-schweiz/");
    }
}
