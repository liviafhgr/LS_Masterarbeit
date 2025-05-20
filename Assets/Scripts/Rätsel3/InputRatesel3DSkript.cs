using System.Collections;
using UnityEngine;

public class InputRatesel3DSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3241;          // Prefab für richtige Antwort ("4")
    public GameObject prefab3242;          // Prefab für falsche Antwort
    public GameObject hinweis4;            // Prefab für Hinweis nach 3 Fehlern
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    private int falscheAntwortCounter = 0; // Zähler für prefab3242

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
            if (inputScript.playerInput == "4")
            {
                Instantiate(prefab3241, receiveBoxContent);
            }
            else
            {
                Instantiate(prefab3242, receiveBoxContent);
                falscheAntwortCounter++;
                if (falscheAntwortCounter == 3 && hinweis4 != null)
                {
                    Instantiate(hinweis4, receiveBoxContent);
                }
            }
        }
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
