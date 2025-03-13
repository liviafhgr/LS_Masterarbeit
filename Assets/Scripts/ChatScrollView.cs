using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatScrollView : MonoBehaviour
{
    public GameObject sprachnachrichtPrefab;
    public GameObject textnachrichtPrefab;
    public GameObject bildnachrichtPrefab;
    public Transform contentTransform;
    public ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddMessages());
    }

    IEnumerator AddMessages()
    {
        GameObject sprachnachricht = Instantiate(sprachnachrichtPrefab, contentTransform);
        sprachnachricht.SetActive(true);
        yield return new WaitForSeconds(5);
        ScrollToBottom();

        GameObject textnachricht = Instantiate(textnachrichtPrefab, contentTransform);
        textnachricht.SetActive(true);
        yield return new WaitForSeconds(5);
        ScrollToBottom();

        GameObject bildnachricht = Instantiate(bildnachrichtPrefab, contentTransform);
        bildnachricht.SetActive(true);
        ScrollToBottom();
    }

    void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}