using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRatesel4Skript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab4111;          // Prefab für richtige Antwort
    public GameObject prefab4112;          // Prefab für falsche Antwort
    public GameObject prefab412;           // Prefab für nächste Nachricht
    public GameObject prefab413;           // Prefab für nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject inputRaetsel4BObjekt; // GameObject mit InputRatesel4BSkript

    // Liste aller erlaubten Schreibweisen
    private readonly HashSet<string> matildaVarianten = new HashSet<string>()
    {
        "matilda",
        "mathilda",
        "mathilde",
        "matilde",
        "mattilda",
        "mattilde",
        "matillda",
        "matildaa",
        "matidla",
        "matlida",
        "matiltda",
        "matildva",
        "matylda",
        "matyllda",
        "metilda",
        "madilda",
        "maitilda",
        "madtilda",
        "mazilda",
        "matilda1",
        "matilda!",
        "mat!lda",
        "m@tilda",
        "m4tilda",
        "mat_il_da"
    };

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
        yield return null; // Einen Frame warten, damit playerInput gesetzt ist
        var inputScript = sendingBoxContent.GetComponent<ChatInputHandler>();
        if (inputScript != null && !string.IsNullOrWhiteSpace(inputScript.playerInput))
        {
            string eingabe = inputScript.playerInput.Trim().ToLower();
            if (matildaVarianten.Contains(eingabe))
            {
                Instantiate(prefab4111, receiveBoxContent);
                StartCoroutine(RichtigeAntwortAbfolge());
            }
            else
            {
                Instantiate(prefab4112, receiveBoxContent);
            }
        }
    }

    IEnumerator RichtigeAntwortAbfolge()
    {
        yield return new WaitForSeconds(1f);
        if (prefab412 != null)
            Instantiate(prefab412, receiveBoxContent);

        yield return new WaitForSeconds(1f);
        if (prefab413 != null)
            Instantiate(prefab413, receiveBoxContent);

        // Nach prefab413: nächstes Rätsel aktivieren, dieses deaktivieren
        if (inputRaetsel4BObjekt != null)
            inputRaetsel4BObjekt.SetActive(true);

        gameObject.SetActive(false);
    }
}
