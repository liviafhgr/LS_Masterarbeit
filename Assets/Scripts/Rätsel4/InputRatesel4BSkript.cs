using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRatesel4BSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab4151;          // Prefab für richtige Antwort
    public GameObject prefab4152;          // Prefab für falsche Antwort
    public GameObject prefab416;           // Prefab für nächste Nachricht
    public GameObject prefab417;           // Prefab für nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    // Liste aller erlaubten Schreibweisen für "davide"
    private readonly HashSet<string> davideVarianten = new HashSet<string>()
    {
        "davide", "davide!", "dav1de", "david", "davede", "daviede", "daviede", "davede", "davide1", "d@vide", "dav_ide"
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
            if (davideVarianten.Contains(eingabe))
            {
                Instantiate(prefab4151, receiveBoxContent);
                yield return new WaitForSeconds(1f);
                if (prefab416 != null)
                    Instantiate(prefab416, receiveBoxContent);
                yield return new WaitForSeconds(1f);
                if (prefab417 != null)
                    Instantiate(prefab417, receiveBoxContent);
            }
            else
            {
                Instantiate(prefab4152, receiveBoxContent);
            }
        }
    }
}
