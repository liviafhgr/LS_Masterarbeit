using System.Collections;
using UnityEngine;

public class InputRatesel3DSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3241;          // Prefab für richtige Antwort ("4")
    public GameObject prefab3242;          // Prefab für falsche Antwort
    public GameObject hinweis4;            // Prefab für Hinweis nach 3 Fehlern
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject sendButton;          // Zu deaktivieren
    public GameObject textArea;            // Zu deaktivieren
    public GameObject newMessageButton3B;  // Zu aktivieren

    private int falscheAntwortCounter = 0; // Zähler für prefab3242

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
            if (inputScript.playerInput == "4")
            {
                Instantiate(prefab3241, receiveBoxContent);

                // SendButton und TextArea deaktivieren
                if (sendButton != null)
                    sendButton.SetActive(false);
                if (textArea != null)
                    textArea.SetActive(false);

                // Dieses GameObject deaktivieren
                gameObject.SetActive(false);

                // NewMessageButton3B aktivieren
                if (newMessageButton3B != null)
                    newMessageButton3B.SetActive(true);
            }
            else
            {
                Instantiate(prefab3242, receiveBoxContent);
                falscheAntwortCounter++;
                if (falscheAntwortCounter == 3 && hinweis4 != null)
                {
                    Instantiate(hinweis4, receiveBoxContent);
                }
            }
        }
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
