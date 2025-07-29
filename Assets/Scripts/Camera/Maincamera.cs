using UnityEngine;

public class CameraMoveOnStart : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(-4f, 4f, -7.5f);
    public Vector3 endPosition = new Vector3(-4f, 4f, -2.5f);
    public float duration = 3f; // Dauer der Kamerafahrt in Sekunden

    private float timer = 0f;
    private bool isMoving = true;
    private Quaternion fixedRotation;

    void Start()
    {
        transform.position = startPosition;
        fixedRotation = Quaternion.Euler(14.279f, 0f, 0f);
        transform.rotation = fixedRotation;
    }

    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            transform.rotation = fixedRotation; // Rotation bleibt konstant

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }
}