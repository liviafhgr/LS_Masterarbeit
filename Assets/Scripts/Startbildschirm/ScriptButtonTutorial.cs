using UnityEngine;

public class OpenYouTubeLink : MonoBehaviour
{
    [SerializeField] private string url = "https://youtu.be/Hy9jnzyjn7w";

    // Diese Methode im Button-OnClick zuweisen
    public void OpenInNewTab()
    {
        Application.OpenURL(url);
    }
}
