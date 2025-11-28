using UnityEngine;

public class openUIStart : MonoBehaviour
{
    [SerializeField] private GameObject UIStart; // Aktiviert werden soll dieses GameObject

    // Diese Methode im Button-OnClick zuweisen
    public void OnStartClicked()
    {
        if (UIStart != null)
        {
            UIStart.SetActive(true);
        }
        else
        {
            Debug.LogWarning("openUIStart: Kein UIStart-GameObject zugewiesen.");
        }
    }
}
