using UnityEngine;

public class buttonstartwebchat : MonoBehaviour
{
    [SerializeField] private GameObject UI;       // Wird aktiviert (SetActive(true))
    [SerializeField] private GameObject UIStart;  // Wird deaktiviert (SetActive(false))

    void Awake()
    {
        if (UIStart == null) UIStart = gameObject;
    }

    void Start()
    {
        // Anfangszustand: Start-UI aktiv, Haupt-UI deaktiv
        if (UI != null) UI.SetActive(false);
        if (UIStart != null) UIStart.SetActive(true);
    }

    // Methode im Button-OnClick zuweisen
    public void OnStartWebChatClicked()
    {
        // Aktivieren / Deaktivieren statt nur sichtbar/unsichtbar
        if (UI != null) UI.SetActive(true);
        if (UIStart != null) UIStart.SetActive(false);
    }
}
