using System.Collections;
using UnityEngine;

public class InputRatesel3BSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3161;          // Prefab für richtige Antwort ("3")
    public GameObject prefab3162;          // Prefab für falsche Antwort
    public GameObject hinweis2;            // Prefab für Hinweis nach 3 Fehlern
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

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

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
