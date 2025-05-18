using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMessageButton3Skript : MonoBehaviour
{
    public List<GameObject> prefabs; // Liste der Prefabs im Inspector zuweisen
    public Transform recieveBoxContent; // Das Ziel-GameObject im Inspector zuweisen
    private int currentIndex = 0;

    void Start()
    {
        // Button-Komponente holen und Listener hinzufügen
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        if (currentIndex < prefabs.Count)
        {
            Instantiate(prefabs[currentIndex], recieveBoxContent);
            currentIndex++;
        }
        else
        {
            Debug.Log("Alle Prefabs wurden bereits instanziiert.");
        }
    }
}
