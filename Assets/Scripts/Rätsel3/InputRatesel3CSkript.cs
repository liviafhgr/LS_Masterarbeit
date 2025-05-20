using System.Collections;
using UnityEngine;

public class InputRatesel3CSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3201;          // Prefab für richtige Antwort ("2")
    public GameObject prefab3202;          // Prefab für falsche Antwort
    public GameObject hinweis3;            // Prefab für Hinweis nach 3 Fehlern
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

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

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
