using System.Collections;
using UnityEngine;

public class InputRaetsel3Skript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3121;          // Prefab für richtige Antwort
    public GameObject prefab3122;          // Prefab für falsche Antwort
    public GameObject hinweis1;            // Prefab für Hinweis nach 3 Fehlern
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

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
}
