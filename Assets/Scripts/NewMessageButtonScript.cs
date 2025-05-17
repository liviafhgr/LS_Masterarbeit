using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMessageButtonScript : MonoBehaviour
{
    [Header("Prefabs in Reihenfolge")]
    [SerializeField] private GameObject[] nachrichtenPrefabs; // Die Prefab-Liste
    [SerializeField] private Transform receiveContentEinführungsszene; // Ziel-Container
    [SerializeField] private MonoBehaviour inputRaetsel1; // Referenz auf das andere Skript
    [SerializeField] private GameObject sendButton; // Referenz auf den SendButton
    [SerializeField] private GameObject enterText;  // Referenz auf das EnterText-GameObject

    private int currentIndex = 0;
    private Button btn;
    private Image btnImage;
    private Color originalColor;
    [SerializeField] private Color pressedColor = new Color(0.7f, 0.7f, 1f, 1f); // z.B. leichtes Blau

    void Awake()
    {
        btn = GetComponent<Button>();
        btnImage = GetComponent<Image>();
        if (btnImage != null)
            originalColor = btnImage.color;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            if (btn == null || btn.interactable)
            {
                StartCoroutine(FlashButtonPressedState());
                OnNewMessageButtonClicked();
            }
        }
    }

    private IEnumerator FlashButtonPressedState()
    {
        if (btn != null)
        {
            var colors = btn.colors;
            Color original = btnImage != null ? btnImage.color : colors.normalColor;

            // Setze Button auf Pressed Color
            btnImage.color = colors.pressedColor;
            yield return new WaitForSeconds(0.15f);

            // Setze zurück auf Normal Color
            btnImage.color = colors.normalColor;
        }
    }

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

        currentIndex++;

        // Nach Listenplatz 18 (also nach dem 19. Element) InputRaetsel1-GameObject aktivieren
        if (currentIndex == 19)
        {
            Debug.Log("Listenplatz 18 erreicht, InputRaetsel1-GameObject wird aktiviert.");
            if (inputRaetsel1 != null)
                inputRaetsel1.gameObject.SetActive(true);
        }

        // Wechsel erst nach dem letzten Prefab
        if (currentIndex >= nachrichtenPrefabs.Length)
        {
            Debug.Log("Alle Nachrichten angezeigt, NewMessageButton wird deaktiviert, SendButton, EnterText und InputRaetsel1 werden aktiviert.");
            gameObject.SetActive(false); // NewMessageButton deaktivieren
            if (sendButton != null)
                sendButton.SetActive(true); // SendButton aktivieren
            if (enterText != null)
                enterText.SetActive(true);  // EnterText aktivieren
            if (inputRaetsel1 != null)
                inputRaetsel1.gameObject.SetActive(true); // Das andere GameObject aktivieren (optional, falls noch nicht aktiv)
            return;
        }
    }
}
