using System.Collections;
using UnityEngine;

public class InputRatesel5Skript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab561;           // Prefab für richtige Antwort
    public GameObject prefab562;           // Prefab für falsche Antwort
    public GameObject prefab563;           // Prefab für Hinweis nach 3/4 Fehlern
    public GameObject prefab564;           // Prefab für Hinweis nach 5/6 Fehlern
    public GameObject prefab565;           // Prefab für Hinweis ab 7 Fehlern
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    private int wrongCount = 0;

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
        yield return null; // Einen Frame warten, damit playerInput gesetzt ist
        var inputScript = sendingBoxContent.GetComponent<ChatInputHandler>();
        if (inputScript != null && !string.IsNullOrWhiteSpace(inputScript.playerInput))
        {
            string eingabe = inputScript.playerInput.Trim();
            if (eingabe == "*6324**castagna**madasi*")
            {
                Instantiate(prefab561, receiveBoxContent);
            }
            else
            {
                Instantiate(prefab562, receiveBoxContent);
                wrongCount++;

                if (wrongCount == 3 || wrongCount == 4)
                {
                    if (prefab563 != null)
                        Instantiate(prefab563, receiveBoxContent);
                }
                else if (wrongCount == 5 || wrongCount == 6)
                {
                    if (prefab564 != null)
                        Instantiate(prefab564, receiveBoxContent);
                }
                else if (wrongCount >= 7)
                {
                    if (prefab565 != null)
                        Instantiate(prefab565, receiveBoxContent);
                }
            }
        }
    }
}
