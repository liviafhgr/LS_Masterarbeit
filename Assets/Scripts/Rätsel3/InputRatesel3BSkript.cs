using System.Collections;
using UnityEngine;

public class InputRatesel3BSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3161;          // Prefab für richtige Antwort ("3")
    public GameObject prefab3162;          // Prefab für falsche Antwort
    public GameObject hinweis2;            // Prefab für Hinweis nach 3 Fehlern
    public GameObject prefab317;           // NEU: Prefab für die nächste Nachricht
    public GameObject prefab318;           // NEU: Prefab für die nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject inputRaetsel3cObjekt; // NEU: GameObject, das aktiviert werden soll

    private int falscheAntwortCounter = 0; // Zähler für prefab3162

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
            if (inputScript.playerInput == "3")
            {
                Instantiate(prefab3161, receiveBoxContent);
                StartCoroutine(RichtigeAntwortAbfolge());
            }
            else
            {
                Instantiate(prefab3162, receiveBoxContent);
                falscheAntwortCounter++;
                if (falscheAntwortCounter == 3 && hinweis2 != null)
                {
                    Instantiate(hinweis2, receiveBoxContent);
                }
            }
        }
    }

    IEnumerator RichtigeAntwortAbfolge()
    {
        yield return new WaitForSeconds(2f);
        if (prefab317 != null)
            Instantiate(prefab317, receiveBoxContent);

        yield return new WaitForSeconds(1f);
        if (prefab318 != null)
            Instantiate(prefab318, receiveBoxContent);

        // Dieses GameObject deaktivieren
        gameObject.SetActive(false);

        // Das andere GameObject aktivieren
        if (inputRaetsel3cObjekt != null)
            inputRaetsel3cObjekt.SetActive(true);
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
