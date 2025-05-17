using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMessageButtonScript : MonoBehaviour
{
    [Header("Prefabs in Reihenfolge")]
    [SerializeField] private GameObject[] nachrichtenPrefabs; // Die Prefab-Liste
    [SerializeField] private Transform receiveContentEinführungsszene; // Ziel-Container

    private int currentIndex = 0;

    public void OnNewMessageButtonClicked()
    {
        Debug.Log("Button wurde geklickt!");

        if (nachrichtenPrefabs.Length == 0)
        {
            Debug.LogWarning("Die Prefab-Liste ist leer!");
            return;
        }

        if (receiveContentEinführungsszene == null)
        {
            Debug.LogWarning("Das Ziel-Transform (receiveContentEinführungsszene) ist nicht zugewiesen!");
            return;
        }

        // Lade das nächste Prefab in die Szene
        GameObject prefab = nachrichtenPrefabs[currentIndex];
        Debug.Log($"Instanziiere Prefab: {prefab.name} an Index {currentIndex}");
        Instantiate(prefab, receiveContentEinführungsszene);

        // Index erhöhen und ggf. zurücksetzen
        currentIndex = (currentIndex + 1) % nachrichtenPrefabs.Length;
    }
}
