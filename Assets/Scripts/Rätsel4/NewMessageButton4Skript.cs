using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewMessageButton4Skript : MonoBehaviour
{
    public List<GameObject> prefabs;           // Prefabs im Inspector zuweisen
    public Transform receiveBoxContent;        // Ziel-Container im Inspector zuweisen

    private int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Button-Komponente holen und Listener hinzufügen
        GetComponent<Button>().onClick.AddListener(OnClicked);

        // Button beim Start auswählen, damit Enter und Pfeiltasten funktionieren
        EventSystem.current.SetSelectedGameObject(gameObject);

        // Navigation auf None setzen, damit Pfeiltasten den Button nicht verlassen
        var nav = GetComponent<Button>().navigation;
        nav.mode = Navigation.Mode.None;
        GetComponent<Button>().navigation = nav;
    }

    // Update is called once per frame
    void Update()
    {
        // Prüfen, ob dieses GameObject ausgewählt ist und Enter oder Pfeil-nach-unten gedrückt wurde
        if (EventSystem.current.currentSelectedGameObject == gameObject &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            OnClicked();
        }
    }

    void OnClicked()
    {
        if (currentIndex < prefabs.Count)
        {
            Instantiate(prefabs[currentIndex], receiveBoxContent);
            currentIndex++;
        }
        else
        {
            Debug.Log("Alle Prefabs wurden bereits instanziert.");
        }
    }
}
