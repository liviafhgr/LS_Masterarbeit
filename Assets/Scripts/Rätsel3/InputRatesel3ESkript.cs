using System.Collections;
using UnityEngine;

public class InputRatesel3ESkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3272;          // Prefab für richtige Antwort ("6324")
    public GameObject prefab3271;          // Prefab für falsche Antwort
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject newMessageButton4;   // Button mit NewMessageButton4Skript
    public GameObject sendButton;          // Zu deaktivieren
    public GameObject textArea;            // Zu deaktivieren

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CheckInputNextFrame());
        }
    }

    IEnumerator CheckInputNextFrame()
    {
        yield return null; // Einen Frame warten, damit playerInput gesetzt ist
        var inputScript = sendingBoxContent.GetComponent<ChatInputHandler>();
        if (inputScript != null && !string.IsNullOrEmpty(inputScript.playerInput))
        {
            if (inputScript.playerInput == "6324")
            {
                Instantiate(prefab3272, receiveBoxContent);

                // Button aktivieren
                if (newMessageButton4 != null)
                    newMessageButton4.SetActive(true);

                // SendButton und TextArea deaktivieren
                if (sendButton != null)
                    sendButton.SetActive(false);
                if (textArea != null)
                    textArea.SetActive(false);

                // Dieses Skript deaktivieren
                gameObject.SetActive(false);
            }
            else
            {
                Instantiate(prefab3271, receiveBoxContent);
            }
        }
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
