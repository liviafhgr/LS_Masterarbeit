using System.Collections;
using UnityEngine;

public class InputRaetsel3Skript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3121;          // Prefab für richtige Antwort
    public GameObject prefab3122;          // Prefab für falsche Antwort
    public GameObject hinweis1;            // Prefab für Hinweis nach 3 Fehlern
    public GameObject prefab313;              // NEU: Prefab für die nächste Nachricht
    public GameObject prefab314;              // NEU: Prefab für die nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject inputRaetsel3BObjekt;   // NEU: GameObject, das aktiviert werden soll

    private int falscheAntwortCounter = 0; // Zähler für prefab3122

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
            if (inputScript.playerInput == "6")
            {
                Instantiate(prefab3121, receiveBoxContent);
                StartCoroutine(RichtigeAntwortAbfolge());
            }
            else
            {
                Instantiate(prefab3122, receiveBoxContent);
                falscheAntwortCounter++;
                if (falscheAntwortCounter == 3 && hinweis1 != null)
                {
                    Instantiate(hinweis1, receiveBoxContent);
                }
            }
        }
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }

    IEnumerator RichtigeAntwortAbfolge()
    {
        yield return new WaitForSeconds(2f);
        if (prefab313 != null)
            Instantiate(prefab313, receiveBoxContent);

        yield return new WaitForSeconds(1f);
        if (prefab314 != null)
            Instantiate(prefab314, receiveBoxContent);

        // Dieses GameObject deaktivieren
        gameObject.SetActive(false);

        // Das andere GameObject aktivieren
        if (inputRaetsel3BObjekt != null)
            inputRaetsel3BObjekt.SetActive(true);
    }
}
