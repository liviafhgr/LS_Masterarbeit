using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRatesel4Skript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab4111;          // Prefab für richtige Antwort
    public GameObject prefab4112;          // Prefab für falsche Antwort
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    // Liste aller erlaubten Schreibweisen
    private readonly HashSet<string> matildaVarianten = new HashSet<string>()
    {
        "matilda", "mathilda", "mathilde", "matilde", "matillda", "matylda", "matyllda"
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
            }
            else
            {
                Instantiate(prefab4112, receiveBoxContent);
            }
        }
    }
}
