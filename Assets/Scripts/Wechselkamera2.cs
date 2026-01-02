using UnityEngine;

public class Wechselkamera2 : StateMachineBehaviour
{
    [Header("Kameras (im Animator-State zuweisen)")]
    public Camera camera1; // bisher aktive Kamera (optional)
    public Camera camera2; // Zielkamera (Pflicht)

    // Wird aufgerufen, wenn der State betreten wird
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (camera2 != null)
        {
            // Kamera 2 aktivieren
            camera2.gameObject.SetActive(true);
            camera2.enabled = true;
        }

        if (camera1 != null)
        {
            // Kamera 1 deaktivieren
            camera1.enabled = false;
            camera1.gameObject.SetActive(false);
        }

        // Falls nur eine Zielkamera gesetzt ist, sicherstellen, dass diese rendert
        // Hinweis: Wenn mehrere Kameras aktiv sind, kann auch die Depth/TargetDisplay-Konfiguration relevant sein.
    }
}
