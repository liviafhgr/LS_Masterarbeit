using System.Collections;
using UnityEngine;

public class InputRaetsel3Skript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // Hier liegt das ChatInputHandler-Skript drauf
    public GameObject prefab3121;          // Prefab für richtige Antwort
    public GameObject prefab3122;          // Prefab für falsche Antwort
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

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
                Instantiate(prefab3121, receiveBoxContent);
            else
                Instantiate(prefab3122, receiveBoxContent);
        }
    }
}
