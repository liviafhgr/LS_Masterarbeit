using UnityEngine;

public class ActivateTargetAfterDelay : MonoBehaviour
{
    public GameObject targetObject;
    public float delay = 6f;

    void Start()
    {
        if (targetObject != null)
        {
            Invoke(nameof(ActivateTarget), delay);
        }
        else
        {
            Debug.LogWarning("Kein Zielobjekt zugewiesen!");
        }
    }

    void ActivateTarget()
    {
        targetObject.SetActive(true);
    }
}