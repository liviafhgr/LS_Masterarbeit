using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import für TextMeshPro

public class ChatInputHandler : MonoBehaviour
{
    public TMP_InputField inputField; // Verweis auf das TMP_InputField
    public Button sendButton; // Verweis auf den Send-Button
    public Color highlightColor = Color.green; // Farbe, wenn der Button hervorgehoben wird
    private Color originalColor; // Ursprüngliche Farbe des Buttons
    public string playerInput; // Variable, um die Eingabe des Spielers zu speichern

    void Start()
    {
        if (inputField != null && sendButton != null)
        {
            // Speichere die ursprüngliche Farbe des Buttons
            Image buttonImage = sendButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                originalColor = buttonImage.color;
            }

            // Listener für den Button-Klick
            sendButton.onClick.AddListener(() =>
            {
                SavePlayerInput();
                ClearInputField(); // Leere das Textfeld
                HighlightSendButton();
                ResetSendButtonColor();
            });

            // Listener für die Enter-Taste
            inputField.onSubmit.AddListener(delegate
            {
                SavePlayerInput();
                ClearInputField(); // Leere das Textfeld
                HighlightSendButton();
                ResetSendButtonColor();
            });
        }
    }

    void SavePlayerInput()
    {
        // Speichere die Eingabe des Spielers
        playerInput = inputField.text;
    }

    void ClearInputField()
    {
        // Leere das Textfeld
        inputField.text = string.Empty;
    }

    void HighlightSendButton()
    {
        // Ändere die Farbe des Buttons
        Image buttonImage = sendButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = highlightColor;
        }
    }

    void ResetSendButtonColor()
    {
        // Setze die ursprüngliche Farbe des Buttons zurück
        Image buttonImage = sendButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }
}