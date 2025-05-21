using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class NewMessageButton5 : MonoBehaviour
{
    public List<GameObject> prefabs;           // Prefabs im Inspector zuweisen
    public Transform receiveBoxContent;        // Ziel-Container im Inspector zuweisen

    private int currentIndex = 0;
    private Button button;
    private Image buttonImage;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
            originalColor = buttonImage.color;

        button.onClick.AddListener(OnClicked);

        // Button beim Start auswählen, damit Enter und Pfeiltasten funktionieren
        EventSystem.current.SetSelectedGameObject(gameObject);

        // Navigation auf None setzen, damit Pfeiltasten den Button nicht verlassen
        var nav = button.navigation;
        nav.mode = Navigation.Mode.None;
        button.navigation = nav;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            OnClicked();
        }
    }

    void OnClicked()
    {
        StartCoroutine(HighlightButtonCoroutine());

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

    IEnumerator HighlightButtonCoroutine()
    {
        if (buttonImage != null)
        {
            Color pressedColor = button.colors.pressedColor;
            buttonImage.color = pressedColor;
            yield return new WaitForSeconds(0.15f);
            buttonImage.color = originalColor;
        }
    }
}
