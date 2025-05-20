using System.Collections;
using UnityEngine;

public class InputRatesel3DSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3241;          // Prefab für richtige Antwort ("4")
    public GameObject prefab3242;          // Prefab für falsche Antwort
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

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
            if (inputScript.playerInput == "4")
            {
                Instantiate(prefab3241, receiveBoxContent);
            }
            else
            {
                Instantiate(prefab3242, receiveBoxContent);
            }
        }
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
