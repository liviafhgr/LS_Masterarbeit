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

        string playerInput = "    " + chatInputHandler.playerInput;
        Debug.Log($"Player Input: {playerInput}");

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

        // Instantiate und lokale Transform beibehalten
        GameObject clonedPrefab = Instantiate(sendContentPrefab, receiveContent, false);
        Debug.Log("SendContentPrefab wurde erfolgreich geklont.");

        // Text setzen
        TextMeshProUGUI prefabText = clonedPrefab.GetComponentInChildren<TextMeshProUGUI>();
        if (prefabText == null)
        {
            Debug.LogError("Kein TextMeshPro-Element im Prefab gefunden!");
            return;
        }

        prefabText.text = playerInput;
        Debug.Log($"Text im geklonten Prefab gesetzt: {playerInput}");

        // Eltern-RectTransform (z.B. die Nachrichten-Blase) anpassen
        RectTransform parentRect = prefabText.transform.parent as RectTransform;
        if (parentRect == null)
        {
            Debug.LogError("Kein RectTransform für das Elternobjekt gefunden!");
            return;
        }

        // berechnete Breite holen (TMP muss erst updaten)
        Canvas.ForceUpdateCanvases();
        float preferredWidth = prefabText.preferredWidth;
        float adjustedWidth = preferredWidth + 10f;

        // Falls LayoutElement vorhanden: preferredWidth setzen (arbeitet besser mit LayoutGroup)
        var layoutElement = parentRect.GetComponent<LayoutElement>();
        if (layoutElement != null)
        {
            layoutElement.preferredWidth = adjustedWidth;
        }
        else
        {
            parentRect.sizeDelta = new Vector2(adjustedWidth, parentRect.sizeDelta.y);
        }

        // Erzwinge rechtsbündige Anchors/Pivot (falls Prefab nicht korrekt eingestellt ist)
        RectTransform clonedRect = clonedPrefab.GetComponent<RectTransform>();
        if (clonedRect != null)
        {
            clonedRect.anchorMin = new Vector2(1f, clonedRect.anchorMin.y);
            clonedRect.anchorMax = new Vector2(1f, clonedRect.anchorMax.y);
            clonedRect.pivot = new Vector2(1f, clonedRect.pivot.y);
            clonedRect.anchoredPosition = new Vector2(-10f, clonedRect.anchoredPosition.y); // rechter Abstand
        }

        // Layout sofort komplett neu berechnen, damit Position korrekt ist
        var contentRect = receiveContent as RectTransform;
        if (contentRect != null)
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        }

        Debug.Log($"Breite von 'Eingabenachricht' angepasst: {adjustedWidth}");
    }
}
