using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import für TextMeshPro

public class InputMessageHandler : MonoBehaviour
{
    [SerializeField] private GameObject sendContentPrefab; // Das Prefab, das geklont werden soll
    [SerializeField] private Transform receiveContent; // Das Ziel-GameObject, dem das Prefab als Child hinzugefügt wird
    [SerializeField] private ChatInputHandler chatInputHandler; // Referenz auf das ChatInputHandler-Skript

    private string lastPlayerInput = ""; // Speichert den letzten Player Input, um doppelte Klonvorgänge zu vermeiden

    void Update()
    {
        // Überprüfe, ob der playerInput sich geändert hat und nicht leer ist
        if (!string.IsNullOrEmpty(chatInputHandler.playerInput) && chatInputHandler.playerInput != lastPlayerInput)
        {
            lastPlayerInput = chatInputHandler.playerInput; // Aktualisiere den letzten Player Input
            SaveAndDisplayInput(); // Klone das Prefab mit dem neuen Text
        }
    }

    // Diese Methode wird aufgerufen, um den Input zu speichern und anzuzeigen
    public void SaveAndDisplayInput()
    {
        Debug.Log("SaveAndDisplayInput wurde aufgerufen.");

        // Hole den Text aus der playerInput-Variable des ChatInputHandler-Skripts
        string playerInput = "    " + chatInputHandler.playerInput; // Füge 4 Leerzeichen am Anfang hinzu
        Debug.Log($"Player Input: {playerInput}");

        // Überprüfe, ob das Prefab und das Ziel korrekt zugewiesen sind
        if (sendContentPrefab == null)
        {
            Debug.LogError("SendContentPrefab ist nicht zugewiesen!");
            return;
        }

        if (receiveContent == null)
        {
            Debug.LogError("ReceiveContent ist nicht zugewiesen!");
            return;
        }

        // Klone das SendContentPrefab
        GameObject clonedPrefab = Instantiate(sendContentPrefab, receiveContent);
        Debug.Log("SendContentPrefab wurde erfolgreich geklont.");

        // Setze den Text des geklonten Prefabs
        TextMeshProUGUI prefabText = clonedPrefab.GetComponentInChildren<TextMeshProUGUI>();
        if (prefabText != null)
        {
            prefabText.text = playerInput;
            Debug.Log($"Text im geklonten Prefab gesetzt: {playerInput}");

            // Setze die Breite des Elternobjekts "Eingabenachricht" basierend auf der Textlänge
            RectTransform parentRectTransform = prefabText.transform.parent.GetComponent<RectTransform>();
            if (parentRectTransform != null)
            {
                // Hole die bevorzugte Breite des Textes
                float preferredWidth = prefabText.preferredWidth;

                // Füge 10 Pixel (5 mm auf jeder Seite) zur Breite hinzu
                float adjustedWidth = preferredWidth + 10;

                // Setze die Breite des Elternobjekts (z. B. "Eingabenachricht")
                parentRectTransform.sizeDelta = new Vector2(adjustedWidth, parentRectTransform.sizeDelta.y);

                Debug.Log($"Breite von 'Eingabenachricht' angepasst: {adjustedWidth}");
            }
            else
            {
                Debug.LogError("Kein RectTransform für das Elternobjekt gefunden!");
            }
        }
        else
        {
            Debug.LogError("Kein TextMeshPro-Element im Prefab gefunden!");
        }
    }
}
