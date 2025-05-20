using System.Collections;
using UnityEngine;

public class InputRatesel3DSkript : MonoBehaviour
{
    public GameObject sendingBoxContent;   // GameObject mit ChatInputHandler
    public GameObject prefab3241;          // Prefab für richtige Antwort ("4")
    public GameObject prefab3242;          // Prefab für falsche Antwort
    public GameObject hinweis4;            // Prefab für Hinweis nach 3 Fehlern
    public GameObject prefab325;           // Prefab für nächste Nachricht
    public GameObject prefab326;           // Prefab für nächste Nachricht
    public Transform receiveBoxContent;    // Ziel-Container für Instanziierung

    public GameObject inputRaetsel3EObjekt; // GameObject mit InputRatesel3ESkript

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
                StartCoroutine(RichtigeAntwortAbfolge());
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

    IEnumerator RichtigeAntwortAbfolge()
    {
        yield return new WaitForSeconds(1f);
        if (prefab325 != null)
            Instantiate(prefab325, receiveBoxContent);

        yield return new WaitForSeconds(1f);
        if (prefab326 != null)
            Instantiate(prefab326, receiveBoxContent);

        // Das nächste Rätsel-Objekt aktivieren
        if (inputRaetsel3EObjekt != null)
            inputRaetsel3EObjekt.SetActive(true);

        // Dieses GameObject deaktivieren
        gameObject.SetActive(false);
    }

    public void CheckInput()
    {
        StartCoroutine(CheckInputNextFrame());
    }
}
