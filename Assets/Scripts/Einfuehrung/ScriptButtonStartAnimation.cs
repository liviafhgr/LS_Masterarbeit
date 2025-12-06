using UnityEngine;

public class ScriptButtonStartAnimation : MonoBehaviour
{
    [SerializeField] private GameObject startscreen;            // UI-Container, der deaktiviert werden soll
    [SerializeField] private GameObject einfuehrungsanimation;  // Objekt, das aktiviert werden soll

    // Methode im Button-OnClick zuweisen
    public void OnStartButtonClicked()
    {
        Debug.Log("ScriptButtonStartAnimation: Button klick erkannt.");

        if (startscreen != null)
        {
            startscreen.SetActive(false);
            Debug.Log($"Startscreen deaktiviert: {startscreen.name}");
        }
        else
        {
            Debug.LogWarning("Startscreen ist nicht zugewiesen.");
        }

        if (einfuehrungsanimation != null)
        {
            einfuehrungsanimation.SetActive(true);
            Debug.Log($"Einführungsanimation aktiviert: {einfuehrungsanimation.name}");
        }
        else
        {
            Debug.LogWarning("Einführungsanimation ist nicht zugewiesen.");
        }
    }
}
