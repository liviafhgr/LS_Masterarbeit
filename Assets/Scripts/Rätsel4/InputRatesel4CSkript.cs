using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRatesel4CSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab41991;         // Prefab für richtige Antwort
    public GameObject prefab4192;          // Prefab für falsche Antwort
    public GameObject prefab420;           // Prefab für nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject sendButton;          // Zu deaktivieren
    public GameObject textArea;            // Zu deaktivieren
    public GameObject newMessageButton5;   // Zu aktivieren

    // Liste aller erlaubten Schreibweisen für "simonetta"
    private readonly HashSet<string> simonettaVarianten = new HashSet<string>()
    {
        "simonetta", "simoneta", "simonneta", "simmonetta", "simonett@", "s1monetta", "simonetta!", "sim0netta"
    };

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

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }

    IEnumerator CheckInputNextFrame()
    {
        yield return null;
        var inputScript = sendingBoxContent.GetComponent<ChatInputHandler>();
        if (inputScript != null && !string.IsNullOrWhiteSpace(inputScript.playerInput))
        {
            string eingabe = inputScript.playerInput.Trim().ToLower();
            if (simonettaVarianten.Contains(eingabe))
            {
                Instantiate(prefab41991, receiveBoxContent);
                yield return new WaitForSeconds(1f);
                if (prefab420 != null)
                    Instantiate(prefab420, receiveBoxContent);

                // Nach prefab420: sendButton und textArea deaktivieren
                if (sendButton != null)
                    sendButton.SetActive(false);
                if (textArea != null)
                    textArea.SetActive(false);

                // NewMessageButton5 aktivieren
                if (newMessageButton5 != null)
                    newMessageButton5.SetActive(true);

                // Dieses GameObject deaktivieren
                gameObject.SetActive(false);
            }
            else
            {
                Instantiate(prefab4192, receiveBoxContent);
            }
        }
    }
}
