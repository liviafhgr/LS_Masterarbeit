using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // <-- hinzufügen

public class NewMessageButton3Skript : MonoBehaviour
{
    public List<GameObject> prefabs; // Liste der Prefabs im Inspector zuweisen
    public Transform recieveBoxContent; // Das Ziel-GameObject im Inspector zuweisen
    public GameObject sendButton; // Im Inspector zuweisen
    public GameObject sendArea;   // Im Inspector zuweisen
    public GameObject raetsel3BildObjekt; // Das GameObject mit Rätsel3BildScript im Inspector zuweisen
    private int currentIndex = 0;

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

    void Update()
    {
        // Prüfen, ob dieses GameObject ausgewählt ist und die Pfeil-nach-unten-Taste gedrückt wurde
        if (EventSystem.current.currentSelectedGameObject == gameObject && Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Button auslösen
            OnClicked();
        }
    }

    void OnClicked()
    {
        if (currentIndex < prefabs.Count)
        {
            Instantiate(prefabs[currentIndex], recieveBoxContent);
            currentIndex++;

            if (currentIndex == 10) // Nach dem 10. Prefab (Index 9)
            {
                // Button deaktivieren
                GetComponent<Button>().interactable = false;

                // SendButton und SendArea aktivieren
                if (sendButton != null) sendButton.SetActive(true);
                if (sendArea != null) sendArea.SetActive(true);

                // Rätsel3-GameObject aktivieren
                if (raetsel3BildObjekt != null)
                {
                    raetsel3BildObjekt.SetActive(true);
                }

                // NewMessageButton3 deaktivieren
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Alle Prefabs wurden bereits instanziiert.");
        }
    }
}
