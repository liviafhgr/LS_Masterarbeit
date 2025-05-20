using System.Collections;
using UnityEngine;

public class InputRatesel3CSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3201;          // Prefab für richtige Antwort ("2")
    public GameObject prefab3202;          // Prefab für falsche Antwort
    public GameObject hinweis3;            // Prefab für Hinweis nach 3 Fehlern
    public GameObject prefab321;           // NEU: Prefab für die nächste Nachricht
    public GameObject prefab322;           // NEU: Prefab für die nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject inputRaetsel3DObjekt; // NEU: GameObject, das aktiviert werden soll

    private int falscheAntwortCounter = 0; // Zähler für prefab3202

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
            if (inputScript.playerInput == "2")
            {
                Instantiate(prefab3201, receiveBoxContent);
                StartCoroutine(RichtigeAntwortAbfolge());
            }
            else
            {
                Instantiate(prefab3202, receiveBoxContent);
                falscheAntwortCounter++;
                if (falscheAntwortCounter == 3 && hinweis3 != null)
                {
                    Instantiate(hinweis3, receiveBoxContent);
                }
            }
        }
    }

    IEnumerator RichtigeAntwortAbfolge()
    {
        yield return new WaitForSeconds(2f);
        if (prefab321 != null)
            Instantiate(prefab321, receiveBoxContent);

        yield return new WaitForSeconds(1f);
        if (prefab322 != null)
            Instantiate(prefab322, receiveBoxContent);

        // Dieses GameObject deaktivieren
        gameObject.SetActive(false);

        // Das andere GameObject aktivieren
        if (inputRaetsel3DObjekt != null)
            inputRaetsel3DObjekt.SetActive(true);
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
